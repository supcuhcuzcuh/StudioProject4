using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoomState : MonoBehaviour, ITimeTravelResponse
{

    enum ROOMSTATE
    {
        PAST,
        PRESENT
    };

    ROOMSTATE currRoomState = ROOMSTATE.PRESENT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTimeTravel()
    {

    }

    public void OffTimeTravel()
    {

    }
}
