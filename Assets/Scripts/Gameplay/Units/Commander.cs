using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : Unit
{
    int controllableInt = 6;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManager.allUnits.Add(gameObject.transform);
        if (gameObject.layer == controllableInt)
        {
            GameManager.gameManager.myUnits.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Visability
    public void MakeVisable()
    {
        Debug.Log(gameObject.name + " Is Visable");
        //isVisable = true;
        foreach (GameObject unit in gameObject.transform)
        {
            unit.gameObject.GetComponent<MeshRenderer>().enabled = true;
            unit.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

    }
    public void MakeInvisable()
    {
        Debug.Log(gameObject.name + " Is Invisable");
        //isVisable = false;
        foreach (GameObject unit in gameObject.transform)
        {
            unit.gameObject.GetComponent<MeshRenderer>().enabled = true;
            unit.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
