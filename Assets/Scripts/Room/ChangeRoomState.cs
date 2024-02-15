using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomState : MonoBehaviour, ITimeTravelResponse
{
     GameObject presentObjects;
     GameObject pastObjects;      
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
        presentObjects.SetActive(true);
        pastObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTimeTravel()
    {
        currRoomState = ROOMSTATE.PAST;
        presentObjects.SetActive(false);
        pastObjects.SetActive(true);
    }

    public void OffTimeTravel()
    {
        currRoomState = ROOMSTATE.PRESENT;
        presentObjects.SetActive(true);
        pastObjects.SetActive(false);
    }
}
