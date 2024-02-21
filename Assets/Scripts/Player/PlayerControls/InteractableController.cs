using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [HideInInspector] public float interactRange;

    private Transform camTransform;

    public void Init(float interactRange)
    {
        this.interactRange = interactRange;

        camTransform = Camera.main.transform;
    }

    public void HandleInteract()
    {
        RaycastHit[] hits = Physics.RaycastAll(camTransform.position, camTransform.forward, interactRange);
        if (Input.GetKeyDown(KeyCode.E)) Interact(hits);
    }

    private void Interact(RaycastHit[] hits)
    {
        Lever closest = null;
        float closestDist = float.MaxValue;

        // Checking through all the hits for any closer Activatables
        foreach (RaycastHit hit in hits)
        {
            Lever activatable = hit.collider.GetComponent<Lever>();
            float distance = Vector3.Distance(hit.collider.transform.position, camTransform.position);

            if (activatable && distance < closestDist)
            {
                Debug.Log(hit.collider.name);
                closest = activatable;
                closestDist = distance;
            }
        }

        // Finally activate it
        if (closest)
        {
            closest.OnActivate();
        }
    }
}   
