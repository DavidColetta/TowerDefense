using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacement : MonoBehaviour
{
    public Tower tower;
    private BoxCollider2D bc;
    private SpriteRenderer SpriteR;
    private LineRenderer lineR;
    private GameObject parent;
    public float range = 4;
    public LayerMask CollisionMask;
    public Color CollisionColor;
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    [SerializeField] private float mapX = 29;
    [SerializeField] private float mapY = 20;
    [SerializeField] private float mapOffsetX = 0.5f;
    [SerializeField] private float mapOffsetY = 0f;
    private Vector2 Offset;
    private bool CanPlace;
    static GameObject instance;
    void Start()
    {
        if (instance){
            Destroy(instance);
        }
        instance = gameObject;

        bc = GetComponent<BoxCollider2D>();
        SpriteR = GetComponent<SpriteRenderer>();
        lineR = GetComponent<LineRenderer>();

        SpriteR.sprite = tower.sprite;
        bc.size = tower.towerObj.GetComponent<BoxCollider2D>().size;
        parent = transform.parent.gameObject;

        if (Mathf.Round(bc.size.x)%2 == 0){
            Offset += new Vector2(0.5f, 0f);
        }
        if (Mathf.Round(bc.size.y)%2 == 0){
            Offset += new Vector2(0f, -0.5f);
        }

        Transform range = transform.Find("Range");
        range.localScale = Vector2.one*tower.range*2;
        if (tower.name == "Drill"){
            transform.Rotate(0f, 0f, 180f);
            range.gameObject.AddComponent<DrillRange>();
        }

        float vertExtent = bc.size.x/2;    
        float horzExtent = bc.size.y/2;

        minX = horzExtent - mapX / 2f + mapOffsetX;
        maxX = mapX / 2f - horzExtent + mapOffsetX;
        minY = vertExtent - mapY / 2f + mapOffsetY;
        maxY = mapY / 2f - vertExtent + mapOffsetY;
    }
    private void FixedUpdate() {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mouseWorldPosition.x), Mathf.Round(mouseWorldPosition.y));
        if (Mathf.RoundToInt(transform.rotation.eulerAngles.z)%180 == 0){
            transform.position += (Vector3)Offset;
        } else {
            transform.position += new Vector3(Offset.y, Offset.x, 0);
        }

        float distance = DistanceToClosestTower();

        if (distance <= range){
            Color normal = new Color(1f, 1f, 1f, CollisionColor.a);
            lineR.startColor = normal;
            lineR.endColor = normal;
            if (bc.IsTouchingLayers(CollisionMask)){
                CanPlace = false;
            } else {
                if (transform.position.x < maxX && transform.position.x > minX && transform.position.y < maxY && transform.position.y > minY){
                    CanPlace = true;
                } else {
                    CanPlace = false;
                }
            }
        } else {
            CanPlace = false;
            lineR.startColor = CollisionColor;
            lineR.endColor = CollisionColor;
        }
    }
    void Update()
    {
        if (!PauseManager.paused){
            if (Input.GetMouseButtonDown(0)){
                if (!EventSystem.current.IsPointerOverGameObject()){
                    if (CanPlace && MoneyManager.money >= tower.price && !bc.IsTouchingLayers(CollisionMask)){
                        GameObject _placedTower = Instantiate(tower.towerObj, transform.position, transform.rotation, parent.transform);
                        MoneyManager.GainMoney(tower.price * -1);
                        Selector.SelectTower(_placedTower);
                    } else {
                        Destroy(gameObject);
                    }
                } else {
                    Destroy(gameObject);
                }
            }

            if (Input.GetKeyDown(KeyCode.R)){
                transform.Rotate(0f, 0f, 90f);
            }

            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetMouseButtonDown(1)){
                Destroy(gameObject);
            }

            if (CanPlace){
                SpriteR.color = new Color(1f, 1f, 1f, CollisionColor.a);
            } else {
                SpriteR.color = CollisionColor;
            }
        }
    }
    float DistanceToClosestTower(){
        GameObject[] towers;
        towers = GameObject.FindGameObjectsWithTag("Tower");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject closest = null;
        foreach (GameObject potentialTarget in towers)
        {
            Vector2 diff = potentialTarget.transform.position - position;
            float _distance = diff.magnitude;
            if (_distance < distance){
                closest = potentialTarget;
                distance = _distance;
            }
        }
        if (closest){
            lineR.SetPosition(0,position);
            lineR.SetPosition(1,closest.transform.position);
            return distance;
        } else {
            return Mathf.Infinity;
        }
    }
}