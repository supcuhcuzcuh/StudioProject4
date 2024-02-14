using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeTutorialScriptedEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject shootingRangeTargets;

    [SerializeField]
    private List<GameObject> shootingRangeTargetsList = new List<GameObject>();

    private bool startUpShootingRange = false;
    //Vector3 targetRotation;
    private int count;

    [SerializeField]
    private GameObject exitDoor;
    [SerializeField]
    private GameObject entryDoor;

    void Start()
    {
        for(int i = 0; i < shootingRangeTargets.transform.childCount; i++)
        {
            shootingRangeTargetsList.Add(shootingRangeTargets.transform.GetChild(i).gameObject);
            shootingRangeTargetsList[i].GetComponent<ShootingRangeTarget>().shootingRangeTutorialScriptedEvent = this;
        }
    }

    void FixedUpdate()
    {
        if(startUpShootingRange == true)
        {
            StartCoroutine("StopRotating");
            foreach (GameObject shootingtarget in shootingRangeTargetsList)
            {
                shootingtarget.transform.Rotate(0, 0, 0.9f);
            }
        }       
    }

    IEnumerator StopRotating()
    {
        yield return new WaitForSeconds(2f);
        startUpShootingRange = false;
    }

    public void ResetRange()
    {
       
    }

    public void PrepareTargets()
    {
        startUpShootingRange = true;
        foreach (GameObject target in shootingRangeTargetsList)
        {
            target.GetComponent<ShootingRangeTarget>().SetHealth(10);
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        PrepareTargets();
        entryDoor.SetActive(true);
        GetComponent<Collider>().enabled = false;
    }

    public void CompletedShootingRange()
    {
        count += 1;
        if(count == shootingRangeTargets.transform.childCount)
        {
            exitDoor.SetActive(false);
        }
    }

}
