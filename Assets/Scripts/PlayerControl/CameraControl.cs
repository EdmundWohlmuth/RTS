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
        newRotation = anchor.transform.localRotation;
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
        anchor.transform.localPosition += move;

        // Camera Scroll
        if (Input.mouseScrollDelta.y != 0)
        {
            // SCROLL
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
                                    gameObject.transform.localPosition.y + Input.mouseScrollDelta.y,
                                    gameObject.transform.localPosition.z + Input.mouseScrollDelta.y);
            // CLAMPS
            if (gameObject.transform.localPosition.y > maxHeight)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
                                                maxHeight,
                                                gameObject.transform.localPosition.z - Input.mouseScrollDelta.y);
            }
            else if (gameObject.transform.localPosition.y < minHeight)
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
                                                            minHeight,
                                                            gameObject.transform.localPosition.z - Input.mouseScrollDelta.y);
            }
        }
    }
    void MouseControls() // Imperfect - Look into later
    {
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2)) // else if avoids executing both conditions at the same time
        {
            Vector3 rotateCurrentPos = Input.mousePosition;
            Vector3 difference = rotateStartPos - rotateCurrentPos;
            rotateStartPos = rotateCurrentPos;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));

            anchor.transform.localRotation = newRotation;
        }
    }
}
