using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointsTracker : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField]
    public List<GameObject> waypoints = new List<GameObject>();
    private int _target = 0;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {
            Debug.Log("Successfully Attained Agent");
        }
        else
        {
            Debug.Log("Failed Attained Agent");
        }
    }

    public void ActivateWaypoints()
    {
        if (waypoints.Count > 1 && !(waypoints[0] == null))
        {
            _agent.SetDestination(waypoints[_target].transform.position);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WAYPOINT"))
        {
            _target++;  
            if (_target == waypoints.Count)
            {
                waypoints.Reverse(); // Loop to start of List
                _target = 1;
            }

            if (waypoints[_target] != null)
            {
                _agent.SetDestination(waypoints[_target].transform.position);
            }
            else
            {
                Debug.Log("Target Destination is null");
            }
        }
    }
}