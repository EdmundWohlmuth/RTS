using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
    public Camera cam;
    int controllable = 6;  // 6 is Layer "Controllable"

    // Start is called before the first frame update
    void Start()
    {
        cam = GameManager.mainCam;
    }

    // Update is called once per frame
    void Update()
    {
        UnitSelection();
        UnitOrders();
    }

    void UnitSelection()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1.5f;
        cam.ScreenToViewportPoint(mousePos);
        mousePos.z = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == controllable)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Multi-Select");
                    if (hit.collider.gameObject.transform.parent != null) GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject.transform.parent.gameObject);
                    else GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject);
                }
                else if (Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Debug.Log("Single Select");
                    GameManager.gameManager.selectedUnits.Clear();
                    if (hit.collider.gameObject.transform.parent != null) GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject.transform.parent.gameObject);
                    else GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject);

                }

                if (hit.collider.gameObject.transform.parent != null)
                {
                    Debug.Log(hit.collider.gameObject.transform.parent.name);
                }
                else Debug.Log(hit.collider.name);
            }
            else GameManager.gameManager.selectedUnits.Clear();
        }
    }

    void UnitOrders()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == 0 && GameManager.gameManager.selectedUnits.Count > 0)           
            {
                foreach (var item in GameManager.gameManager.selectedUnits)
                {
                    Debug.Log(item.name + " move to " + hit.point);
                }
                
            }

        }

    }
}
