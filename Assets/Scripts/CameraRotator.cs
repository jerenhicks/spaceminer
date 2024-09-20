using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraRotator : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    [Range(0.1f, 5f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 0.8f;
    [Range(0.01f, 100)]
    [Tooltip("How sensitive the touch drag to camera rotation")]
    public float touchRotateSpeed = 17.5f;
    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpValue = 0.25f;
    public enum RotateMethod { Mouse, Touch };
    [Tooltip("How do you like to rotate the camera")]
    public RotateMethod rotateMethod = RotateMethod.Mouse;

    [Tooltip("How sensitive the zoom is")]
    public float zoomSpeed = 2.0f;
    [Tooltip("Minimum distance to the target")]
    public float minZoomDistance = 2.0f;
    [Tooltip("Maximum distance to the target")]
    public float maxZoomDistance = 20.0f;

    [Tooltip("Initial rotation angle around the X axis")]
    public float initialXAngle = 30.0f;
    [Tooltip("Initial rotation angle around the Y axis")]
    public float initialYAngle = 0.0f;

    private Vector2 swipeDirection; //swipe delta vector2
    private Quaternion cameraRot; // store the quaternion after the slerp operation
    private Touch touch;
    private float distanceBetweenCameraAndTarget;

    private float minXRotAngle = -80; //min angle around x axis
    private float maxXRotAngle = 80; // max angle around x axis

    //Mouse rotation related
    private float rotX; // around x
    private float rotY; // around y

    //GameObject gamestateObj;

    private GameState gameState;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);

        // Set initial rotation angles
        rotX = initialXAngle;
        rotY = initialYAngle;

        // Apply initial rotation
        Quaternion initialRotation = Quaternion.Euler(rotX, rotY, 0);
        mainCamera.transform.position = target.position + initialRotation * new Vector3(0, 0, -distanceBetweenCameraAndTarget);
        mainCamera.transform.LookAt(target.position);

        gameState = GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.isPaused)
        {
            return;
        }
        if (rotateMethod == RotateMethod.Mouse)
        {
            if (Input.GetMouseButton(1))
            {
                rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; // around X
                rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
            }

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }

            // Handle zoom with mouse scroll wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distanceBetweenCameraAndTarget -= scroll * zoomSpeed;
            distanceBetweenCameraAndTarget = Mathf.Clamp(distanceBetweenCameraAndTarget, minZoomDistance, maxZoomDistance);
        }
        else if (rotateMethod == RotateMethod.Touch)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log("Touch Began");
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    swipeDirection += touch.deltaPosition * Time.deltaTime * touchRotateSpeed;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    //Debug.Log("Touch Ended");
                }

                // Handle zoom with pinch gesture
                if (Input.touchCount == 2)
                {
                    Touch touch1 = Input.GetTouch(0);
                    Touch touch2 = Input.GetTouch(1);

                    Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
                    Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

                    float prevTouchDeltaMag = (touch1PrevPos - touch2PrevPos).magnitude;
                    float touchDeltaMag = (touch1.position - touch2.position).magnitude;

                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    distanceBetweenCameraAndTarget += deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
                    distanceBetweenCameraAndTarget = Mathf.Clamp(distanceBetweenCameraAndTarget, minZoomDistance, maxZoomDistance);
                }
            }

            if (swipeDirection.y < minXRotAngle)
            {
                swipeDirection.y = minXRotAngle;
            }
            else if (swipeDirection.y > maxXRotAngle)
            {
                swipeDirection.y = maxXRotAngle;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distanceBetweenCameraAndTarget); //assign value to the distance between the maincamera and the target

        Quaternion newQ; // value equal to the delta change of our mouse or touch position
        if (rotateMethod == RotateMethod.Mouse)
        {
            newQ = Quaternion.Euler(rotX, rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
        }
        else
        {
            newQ = Quaternion.Euler(swipeDirection.y, -swipeDirection.x, 0);
        }
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue);  //let cameraRot value gradually reach newQ which corresponds to our touch
        mainCamera.transform.position = target.position + cameraRot * dir;
        mainCamera.transform.LookAt(target.position);
    }

    public void SetCamPos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.transform.position = new Vector3(0, 0, -distanceBetweenCameraAndTarget);
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
