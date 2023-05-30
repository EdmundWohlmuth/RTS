using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Refs")]
    public GameObject anchor;

    [Header("Variables")]
    [SerializeField] float speed;
    [SerializeField] float fastSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float maxHeight = 75f;
    [SerializeField] float minHeight = 5f;

    //rotation values
    Quaternion newRotation;
    Vector3 rotateStartPos;
    Vector3 rotateCurrentPos;

    // Start is called before the first frame update
    void Start()
    {
        newRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControls();
        MouseControls();

    }

    void KeyboardControls()
    {
        // set speeds
        speed = gameObject.transform.position.y / 150; // faster when farther out, slower while closer
        if (Input.GetKey(KeyCode.LeftShift)) fastSpeed = 1.5f;
        else fastSpeed = 1f;

        // Movement
        float horizontal = speed * fastSpeed * Input.GetAxis("Horizontal");
        float vertical = speed * fastSpeed * Input.GetAxis("Vertical");
        float scroll = -zoomSpeed * fastSpeed * Input.mouseScrollDelta.y;

        Vector3 horizontalMove = horizontal * transform.right;
        Vector3 forwardMove = transform.forward;
        Vector3 verticalMove = new Vector3(0, scroll, 0);

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vertical;

        Vector3 move = verticalMove + horizontalMove + forwardMove;
        anchor.transform.position += move;

        // Camera Scroll
        if (Input.mouseScrollDelta.y != 0)
        {
            // SCROLL
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                    gameObject.transform.position.y + Input.mouseScrollDelta.y,
                                    gameObject.transform.position.z + Input.mouseScrollDelta.y);
            // CLAMPS
            if (gameObject.transform.position.y > maxHeight)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                maxHeight,
                                                gameObject.transform.position.z - Input.mouseScrollDelta.y);
            }
            else if (gameObject.transform.position.y < minHeight)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                            minHeight,
                                                            gameObject.transform.position.z - Input.mouseScrollDelta.y);
            }
        }
    }
    void MouseControls() // BROKEN
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotateStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonDown(0))
        {
            rotateCurrentPos = Input.mousePosition;
            Vector3 differnce = rotateStartPos - rotateCurrentPos;
            rotateStartPos = rotateCurrentPos;

            newRotation *= Quaternion.Euler(Vector3.up * (-differnce.x / 5f));

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * 1f);
        }
    }
}
