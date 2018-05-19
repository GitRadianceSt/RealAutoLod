using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float moveSpeed = 1.0f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;
    Vector2 oldMousePosition;

    // Use this for initialization
    void Awake()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        oldMousePosition = Input.mousePosition;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    int lastTouchCount = 0;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Application.isMobilePlatform)
            {
                oldMousePosition = Input.GetTouch(0).position;
            }
            else
            {
                oldMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            return;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Vector2 mousePosition = oldMousePosition;

            if (Application.isMobilePlatform)
            {
                if (lastTouchCount != Input.touchCount)
                {
                    oldMousePosition = Input.GetTouch(0).position;
                    lastTouchCount = Input.touchCount;
                }

                mousePosition = Input.GetTouch(0).position - oldMousePosition;
                oldMousePosition = Input.GetTouch(0).position;
            }
            else
            {
                mousePosition = oldMousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                oldMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //rotate
            x += -mousePosition.x * xSpeed * 0.0008f;
            y -= -mousePosition.y * ySpeed * 0.0008f;
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            transform.rotation = rotation;
        }
        
        //move
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow)) moveVector = transform.right;
        if (Input.GetKey(KeyCode.LeftArrow)) moveVector = -transform.right;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetMouseButton(0) && Input.GetMouseButton(1)) moveVector = transform.forward;
        if (Input.GetKey(KeyCode.DownArrow)) moveVector = -transform.forward;
                
        moveVector = moveVector * Time.deltaTime * moveSpeed;
        transform.position = transform.position + moveVector;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}