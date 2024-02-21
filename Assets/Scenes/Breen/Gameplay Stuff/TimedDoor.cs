using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDoor : Activatable
{
    private Animator anim;
    [SerializeField] private float timeLimit = 2.0f;
    [SerializeField] private float activateDelay = 0.3f;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }

    override public void OnActivate()
    {
        StartCoroutine("Delay");
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeLimit + activateDelay);
        anim.Play("Closing");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(activateDelay);
        anim.SetTrigger("Activate");
    }
}
