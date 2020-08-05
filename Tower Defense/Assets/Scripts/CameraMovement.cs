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

    private void FixedUpdate() {
        if (Input.GetMouseButton(2)) {
            transform.position -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * panSpeed * Time.deltaTime * thisCamera.orthographicSize;
        } else {
            transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime * speed * thisCamera.orthographicSize;
        }
    }
    void Update()
    {
        #region Scrolling/Zoom
        if (!EventSystem.current.IsPointerOverGameObject()){
            thisCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            if (Input.GetKeyDown(KeyCode.Equals)){
                thisCamera.orthographicSize -= 1;
            } else if (Input.GetKeyDown(KeyCode.Minus)){
                thisCamera.orthographicSize += 1;
            }
        }
        thisCamera.orthographicSize = Mathf.Clamp(thisCamera.orthographicSize, minZoom, maxZoom);
        #endregion
    }
}