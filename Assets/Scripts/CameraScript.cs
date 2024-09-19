using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField][Range(0, 360)] private int maxRotationInOneSwipe = 180;
    float MouseZoomSpeed = 15.0f;
    float TouchZoomSpeed = 0.1f;
    float distanceToTarget = 10;

    // Use this for initialization


    private Vector3 previousPosition;

    void Update()
    {
        if (Input.touchSupported)//pinch code
        {
            // Pinch to zoom
            if (Input.touchCount == 2)
            {

                // get current touch positions
                Touch tZero = Input.GetTouch(0);
                Touch tOne = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
                Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

                float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
                float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

                // get offset value
                float deltaDistance = oldTouchDistance - currentTouchDistance;
                Zoom(deltaDistance, TouchZoomSpeed);
            }
        }
        else
        {

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);




            //end pinch code
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse button down");
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * maxRotationInOneSwipe; // camera moves horizontally
            float rotationAroundXAxis = direction.y * maxRotationInOneSwipe; // camera moves vertically

            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

            cam.transform.Translate(new Vector3(0, 0, -distanceToTarget));

            previousPosition = newPosition;
        }
    }
    void Zoom(float deltaMagnitudeDiff, float speed)//test
    {

        distanceToTarget += deltaMagnitudeDiff * speed;
        // set min and max value of Clamp function upon your requirement
        distanceToTarget = Mathf.Clamp(distanceToTarget, 1, 100);
        transform.position = target.position;
        transform.Translate(new Vector3(0, 0, -distanceToTarget));
    }
}