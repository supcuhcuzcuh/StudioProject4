using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintTutorialScriptedEvent : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> dynamiteEffects = new List<ParticleSystem>();

    [SerializeField]
    private GameObject effectsGroup;

    [SerializeField]
    private GameObject exitDoor;

    private void Start()
    {
        for(int i = 0; i < effectsGroup.transform.childCount; i++)
        {
            dynamiteEffects.Add(effectsGroup.transform.GetChild(i).GetComponent<ParticleSystem>());
        }
    }

    private void OnTriggerEnter()
    {
        foreach(ParticleSystem _dynamiteEffect in dynamiteEffects)
        {
            _dynamiteEffect.Play();
        }

        exitDoor.SetActive(false);
    }
}
