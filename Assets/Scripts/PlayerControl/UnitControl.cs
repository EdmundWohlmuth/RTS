using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                    //Debug.Log("Multi-Select");
                    if (hit.collider.gameObject.transform.parent != null) GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject.transform.parent.gameObject);
                    else GameManager.gameManager.selectedUnits.Add(hit.collider.gameObject);
                }
                else if (Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.LeftControl))
                {
                    //Debug.Log("Single Select");
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
            else if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider.gameObject.layer == 0)
                {
                    GameManager.gameManager.selectedUnits.Clear();
                }          
            }
           
        }
    }

    bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void UnitOrders()
    {
        int unitCount = 0;
        float unitPosOffset = 4.5f; // OFFSET NEEDS TO INCREASE BY .5 WHENEVER IT DOES INCREASE

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == 0 && GameManager.gameManager.selectedUnits.Count > 0)           
            {
                foreach (GameObject unit in GameManager.gameManager.selectedUnits)
                {
                    if (!unit.GetComponent<Infantry>()) // OTHER MOVEMENT
                    {
                        float unitPosOffsetZ = 4.5f; // NOT DONE YET, HAVE IT PLACE BEHIND LINE
                        Debug.Log(unit.name + " is not infantry");
                    }
                    else if (!unit.GetComponent<Infantry>().isInLine) // COLUNM MOVEMENT
                    {
                        Debug.Log(unit.name + " move to " + new Vector3(hit.point.x, hit.point.y, hit.point.z - unitPosOffset));

                        unitCount++;
                        unitPosOffset += 5f;
                    }
                    else // LINE MOVEMENT
                    {
                        if (unitCount % 2 == 0)
                        {
                            //left // TEMP, DOESN'T ACCOUNT FOR ROTATION
                            Debug.Log(unit.name + " move to " + new Vector3(hit.point.x + unitPosOffset, hit.point.y, hit.point.z));
                            unit.GetComponent<Infantry>().MoveMe(new Vector3(hit.point.x + unitPosOffset, hit.point.y, hit.point.z));
                        }
                        else
                        {
                            //right // TEMP, DOESN'T ACCOUNT FOR ROTATION     
                            Debug.Log(unit.name + " move to " + new Vector3(hit.point.x + unitPosOffset * -1, hit.point.y, hit.point.z));
                            unit.GetComponent<Infantry>().MoveMe(new Vector3(hit.point.x + unitPosOffset * -1, hit.point.y, hit.point.z));
                        }

                        unitCount++;
                        if (unitCount % 2 == 0) unitPosOffset += 9f;
                    }

                    //Debug.Log(item.name + " move to " + hit.point);
                }
                
            }

        }

    }

    //-UI-INTERACTIONS-
    public void ToggleFormation() // MIGHT GET MOVED TO A METHOD IN INFANTRY.CS
    {
        int unitNum = 0;
        float unitPosOffset = 4.5f;

        Debug.Log("Toggle Formation");
        if (GameManager.gameManager.selectedUnits.Count > 0)
        {
            foreach (GameObject unit in GameManager.gameManager.selectedUnits)
            {
                foreach (Transform halfBattalion in unit.transform)
                {
                    if (unit.GetComponent<Infantry>())
                    {
                        if (unit.GetComponent<Infantry>().isInLine)
                        {
                            halfBattalion.transform.localScale = new Vector3(unit.GetComponent<Infantry>().marchingScaleX, 0.5f,
                                                                            unit.GetComponent<Infantry>().marchingScaleZ);
                            if (unitNum == 0)
                            {
                                // for position, I think?
                            }
                        }
                        else
                        {
                            halfBattalion.transform.localScale = new Vector3(unit.GetComponent<Infantry>().lineScaleX, 0.5f,
                                                                            unit.GetComponent<Infantry>().lineScaleZ);
                        }
                        
                    }
                }
                // SET STATE
                if (unit.GetComponent<Infantry>().isInLine) unit.GetComponent<Infantry>().isInLine = false;
                else unit.GetComponent<Infantry>().isInLine = true;

            }
        }
    }

    public void SetFiringRanges()
    {
        Debug.Log("Set Range");
    }
    public void Square()
    {
        Debug.Log("Square Up");
    }
    public void MergeUnits()
    {
        Debug.Log("Merge Units");
    }
}
