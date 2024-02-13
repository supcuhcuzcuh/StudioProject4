using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileTutorialScriptedEvent : MonoBehaviour
{
    [SerializeField]
    private Helicopter helicopter;

    [SerializeField]
    private GameObject exitDoor;

    void Update()
    {
        if(helicopter.currState == Helicopter.States.DEAD)
        {
            exitDoor.SetActive(false);
        }
    }
}
