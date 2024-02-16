using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISprintResponse
{
    void OnSprint();
    void OffSprint();
}

public class SprintControl : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField] Animator animator;

    private List<ISprintResponse> sprintResponses = new List<ISprintResponse>();

    public void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerStats.currState == PlayerStats.PLAYERSTATES.WALK)
        {
            playerStats.currState = PlayerStats.PLAYERSTATES.SPRINT;
            animator.SetInteger("MoveCounter", 2);
            NotifyOnSprintResponse();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && playerStats.currState == PlayerStats.PLAYERSTATES.SPRINT)
        {
            playerStats.currState = PlayerStats.PLAYERSTATES.WALK;
            NotifyOffSprintResponse();
        }
    }

    private void NotifyOnSprintResponse()
    {
        foreach (var sprintResponse in sprintResponses)
        {
            sprintResponse.OnSprint();
        }
    }

    private void NotifyOffSprintResponse()
    {
        foreach (var sprintResponse in sprintResponses)
        {
            sprintResponse.OffSprint();
        }
    }

    public void Subscribe(ISprintResponse sprintResponse)
    {
        sprintResponses.Add(sprintResponse);
    }
}
