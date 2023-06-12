using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Camera mainCam;
    public static GameManager gameManager;

    [Header("Unit Lists")]
    public List<GameObject> selectedUnits = new List<GameObject>();
    public List<Transform> allUnits = new List<Transform>();
    public List<GameObject> myUnits = new List<GameObject>();
    public List<Transform> visableTargets = new List<Transform>();

    [Header("UnitTracking")]
    public int visableUnits;

    private void Awake()
    {
        mainCam = Camera.main;

        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameManager != this && gameManager != null)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        visableUnits = visableTargets.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (visableUnits != visableTargets.Count) VisabilityCheck(); // Might need to change later, Oh well
    }

    void VisabilityCheck()
    {
        bool isInBoth = false;
        visableUnits = visableTargets.Count;

        for (int i = 0; i < allUnits.Count; i++)
        {
            for (int x = 0; x < visableTargets.Count; x++)
            {
                if (allUnits[i] == visableTargets[x])
                {
                    isInBoth = true;
                }
            }
            if (allUnits[i].GetComponentInParent<Infantry>())
            {
                if (isInBoth == true) allUnits[i].GetComponentInParent<Infantry>().MakeVisable(allUnits[i]);
                else allUnits[i].GetComponentInParent<Infantry>().MakeInvisable(allUnits[i]);             
            }
            isInBoth = false;
        }
    }
}
