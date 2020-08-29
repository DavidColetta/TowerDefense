using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float panSpeed;
    public float zoomSpeed;
    public Camera thisCamera;
    public float minZoom;
    public float maxZoom;
    private Vector2 MoveVector;
    private Vector3 panOrigin;
    private Vector2 oldCameraPosition;
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    [SerializeField] private float mapX = 15;
    [SerializeField] private float mapY = 10;
    [SerializeField] private float mapOffsetX = 0.5f;
    [SerializeField] private float mapOffsetY = 0.5f;
    private void FixedUpdate() {
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1)) {
            transform.position -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * panSpeed * Time.deltaTime * thisCamera.orthographicSize;
        } else {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed * thisCamera.orthographicSize;
        }

    }
    void Update()
    {
        float zoom = thisCamera.orthographicSize;
        if (!EventSystem.current.IsPointerOverGameObject()){
            zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            if (Input.GetKeyDown(KeyCode.Equals)){
                zoom -= 1;
            } else if (Input.GetKeyDown(KeyCode.Minus)){
                zoom += 1;
            }
        }
        UpdateCameraBounds();

        thisCamera.orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);

        Vector3 v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }
    void UpdateCameraBounds(){
        float vertExtent = thisCamera.orthographicSize;    
        float horzExtent = vertExtent * (Screen.width) / Screen.height;

        minX = horzExtent - mapX / 2f + mapOffsetX;
        maxX = mapX / 2f - horzExtent + mapOffsetX;
        minY = vertExtent - mapY / 2f + mapOffsetY;
        maxY = mapY / 2f - vertExtent + mapOffsetY;

        float maxZoomX = (mapX * Screen.height / (Screen.width))/2;
        float maxZoomY = mapY/2;
        maxZoom = Mathf.Min(maxZoomX,maxZoomY);
    }
}