using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;
    public float zoomSpeed;
    public float zoomTime;

    public Vector3 newPosition;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;

    private Camera camera;
    private float targetZoom;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;

        camera = Camera.main;
        targetZoom = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInputs();
        HandleKeyboardInputs();
    }

    void HandleKeyboardInputs()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.up * movementSpeed * camera.orthographicSize;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.up * -movementSpeed * camera.orthographicSize;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed * camera.orthographicSize;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed * camera.orthographicSize;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }

    void HandleMouseInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        // Zoom
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomSpeed * camera.orthographicSize;
        targetZoom = Mathf.Clamp(targetZoom, 4f, 20f);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * movementTime);
    }
}
