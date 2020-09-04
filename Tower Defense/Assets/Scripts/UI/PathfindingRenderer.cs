using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;
[RequireComponent(typeof(LineRenderer))]
public class PathfindingRenderer : MonoBehaviour
{
    [SerializeField]
    private float lineUpdateRate = 2f;
    private LineRenderer lineRenderer;
    private GameObject selectedObject;
    private List<Vector3> selectedPath;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InvokeRepeating("UpdateLine", 2f, lineUpdateRate);
        lineRenderer.enabled = false;
    }
    private void FixedUpdate() {
        if (!selectedObject){
            UpdateLine();
        }
    }

    void UpdateLine()
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            Vector2 mousePos2D = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 0f);
            if (hit.collider != null) {
                selectedObject = hit.collider.gameObject;
                if (selectedObject.tag == "Enemy"){
                    if (selectedObject.GetComponent<EnemyPathfinding>().path == null){
                        selectedObject = null;
                        return;
                    }
                    selectedPath = selectedObject.GetComponent<EnemyPathfinding>().path.vectorPath;
                    if (selectedPath.Count > 0){
                        lineRenderer.enabled = true;
                        for (int i = 0; i < lineRenderer.positionCount; i++)
                        {
                            if (i < selectedPath.Count){
                                lineRenderer.SetPosition(i, selectedPath[i]);
                            } else {
                                lineRenderer.SetPosition(i, selectedPath[selectedPath.Count-1]);
                            }
                        }
                    }
                } else {
                    selectedObject = null;
                }
            } else {
                selectedObject = null;
            }
        }

        if (!selectedObject){
            lineRenderer.enabled = false;
        }
    }
}
