using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<Activatable> Activatables = new List<Activatable>();

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    virtual public void OnActivate()
    {
        anim.SetTrigger("Activate");
        GetComponent<AudioManager>().PlaySoundEffect("OnActivate");

        // Activate all activatable things attached to this lever
        foreach (Activatable activatable in Activatables) activatable.OnActivate();
    }
}
