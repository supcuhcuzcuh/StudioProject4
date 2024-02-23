using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDoor : Activatable
{
    private Animator anim;
    private Collider col;
    [SerializeField] private float timeLimit = 2.0f;
    [SerializeField] private float activateDelay = 0.3f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    override public void OnActivate()
    {
        Debug.Log("Activate");
        StartCoroutine("Delay");
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeLimit + activateDelay);
        anim.Play("Closing");
        col.enabled = true;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(activateDelay);
        anim.SetTrigger("Activate");
        col.enabled = false;
    }
}
