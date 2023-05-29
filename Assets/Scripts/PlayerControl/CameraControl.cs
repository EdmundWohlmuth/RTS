using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float horizontal = speed * Input.GetAxis("Horizontal");
        float vertical = speed * Input.GetAxis("Vertical");
        float scroll = -zoomSpeed * Input.mouseScrollDelta.y;

        Vector3 horizontalMove = horizontal * transform.right;
        Vector3 forwardMove = transform.forward;
        Vector3 verticalMove = new Vector3(0, scroll, 0);

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vertical;

        Vector3 move = verticalMove + horizontalMove + forwardMove;
        transform.position += move;
    }
}
