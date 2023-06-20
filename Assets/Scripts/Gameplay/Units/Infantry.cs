using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit
{
    int controllableInt = 6;

    public bool isSelected;
    public bool isVisable;

    public bool isInLine;

    public float marchingScaleX = 2;
    public float marchingScaleZ = 4;
    public float lineScaleX = 4;
    public float lineScaleZ = 2;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        isInLine = true;

        foreach (Transform i in gameObject.transform)
        {
            if (i.gameObject.layer != 20) // 20 is an Ignore Layer
            {
                GameManager.gameManager.allUnits.Add(i.gameObject.transform);
            }
        }

        if (gameObject.layer == controllableInt)
        {
            GameManager.gameManager.myUnits.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //-SET-VISABILITY-------
    public void MakeVisable(Transform unitToShow)
    {
        //Debug.Log(gameObject.name + " Is Visable");
        isVisable = true;

        foreach (Transform i in gameObject.transform)
        {
            if (i == unitToShow)
            {
                i.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            //i.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
        }


    }
    public void MakeInvisable(Transform unitToHide)
    {
        //Debug.Log(gameObject.name + " Is Invisable");
        isVisable = false;

        foreach (Transform i in gameObject.transform)
        {
            if (i == unitToHide)
            {
                i.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            //i.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        }
    }

    public void MoveMe(Vector3 PosToMoveTo)
    {
        agent.SetDestination(PosToMoveTo);
    }

}
