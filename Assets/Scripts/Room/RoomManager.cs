using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    private TimeTravelControl timetravelControl;
   

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            timetravelControl.SubscribeOnTimeTravel(transform.GetChild(i).gameObject.GetComponent<ChangeRoomState>());
            timetravelControl.SubscribeOnTimeTravel(transform.GetChild(i).gameObject.GetComponent<ChangeRoomState>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
