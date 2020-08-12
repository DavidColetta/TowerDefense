using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathfinding : MonoBehaviour
{
    [HideInInspector]
    public Enemy enemy;
    [HideInInspector]
    public GameObject target;
    public float nextWaypointDistance = 1f;
    public float pathUpdateRate = 1f;
    public Path path = null;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;
    public bool reachedTower = false;
    [HideInInspector]
    public List<GameObject> cannotTarget = new List<GameObject>();
    public Quaternion direction;
    Seeker seeker;
    Rigidbody2D rb;
    public float speed;

    void Start(){
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateRate);
    }
    #region Create Path
    void UpdatePath(){
        if (seeker.IsDone()){
            //Create New Path
            target = FindClosestTower();
            if (target){
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            }
        }
    }
    void OnPathComplete(Path p){
        if (!p.error && target){
            //Check if Path is Possible
            BoxCollider2D targetBc = target.GetComponent<BoxCollider2D>();
            int finalWaypoint = p.vectorPath.Count - 1;
            Vector2 closestPoint = targetBc.ClosestPoint(p.vectorPath[finalWaypoint]);
            if (Vector2.Distance(closestPoint,p.vectorPath[finalWaypoint]) <= 0.6 + enemy.range){
                path = p;
                currentWaypoint = 0;
                reachedTower = false;
            } else {
                //Debug.Log("Path not possible!!!");
                cannotTarget.Add(target);
                target = null;
                path = null;
                UpdatePath();
            }
        }
    }
    GameObject FindClosestTower(){
        GameObject[] towers;
        towers = GameObject.FindGameObjectsWithTag("Tower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject potentialTarget in towers)
        {
            if (!cannotTarget.Contains(potentialTarget)){
                Vector2 diff = potentialTarget.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = potentialTarget;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
    #endregion
    #region Move Along Path
    void FixedUpdate(){
        if (path == null || !target){
            reachedEndOfPath = false;
            return;
        }

        if (Vector2.Distance(path.vectorPath[path.vectorPath.Count-1], transform.position) < nextWaypointDistance + enemy.range){
            reachedEndOfPath = true;
        } else if (Vector2.Distance(path.vectorPath[path.vectorPath.Count-1], transform.position) > 0.6 + enemy.range){
            reachedEndOfPath = false;
        }

        if (currentWaypoint >= path.vectorPath.Count){
            return;
        }

        if (reachedTower || reachedEndOfPath){
            return;
        }

        Vector2 lookPos = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]) > nextWaypointDistance){
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            direction = Quaternion.Euler(0, 0, angle);
        }

        Vector2 moveVector = lookPos * speed * Time.deltaTime;
        rb.position += moveVector;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) 
            currentWaypoint ++;
    }
    #endregion
}