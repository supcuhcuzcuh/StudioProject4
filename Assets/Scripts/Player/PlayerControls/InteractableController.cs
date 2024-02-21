using System.Collections;
using System.Collections.Generic;
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
        Activatable closest = null;
        float closestDist = float.MaxValue;
        float distance;

        // Checking through all the hits for any closer Activatables
        foreach (RaycastHit hit in hits)
        {
            Activatable activatable = hit.collider.GetComponent<Activatable>();
            distance = Vector3.Distance(hit.collider.transform.position, camTransform.position);

            if (activatable && distance < closestDist)
            {
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
