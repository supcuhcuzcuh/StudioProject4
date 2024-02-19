using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomState : MonoBehaviour, ITimeTravelResponse
{
     GameObject presentObjects;
     GameObject pastObjects;

    private List<Collider> pastObjectCols = new List<Collider>();
    private List<Collider> presentObjectCols = new List<Collider>();

    enum ROOMSTATE
    {
        PAST,
        PRESENT
    };

    ROOMSTATE currRoomState = ROOMSTATE.PRESENT;

    // Start is called before the first frame update
    void Start()
    {
        presentObjects = transform.GetChild(0).gameObject;
        pastObjects = transform.GetChild(1).gameObject;

        for(int i = 0; i < pastObjects.transform.childCount; i++)
        {
            pastObjectCols.Add(pastObjects.transform.GetChild(i).GetComponent<Collider>());
        }

        for (int i = 0; i < presentObjects.transform.childCount; i++)
        {
            presentObjectCols.Add(presentObjects.transform.GetChild(i).GetComponent<Collider>());
        }
    }

    public void OnTimeTravel()
    {
        currRoomState = ROOMSTATE.PAST;
        foreach(Collider col in pastObjectCols)
        {
            col.isTrigger = false;
        }
        foreach (Collider col in presentObjectCols)
        {
            col.isTrigger = true;
        }
    }

    public void OffTimeTravel()
    {
        currRoomState = ROOMSTATE.PRESENT;
        foreach (Collider col in pastObjectCols)
        {
            col.isTrigger = true;
        }
        foreach (Collider col in presentObjectCols)
        {
            col.isTrigger = false;
        }
    }
}
