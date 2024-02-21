using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activatable
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }

    override public void OnActivate()
    {
        anim.SetTrigger("Activate");
    }
}
