using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public float interactRange;

    public void HandleInteract()
    {
        if (Input.GetKey(KeyCode.E)) Interact();
    }

    private void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionRange))
        {
            Activatable activatable = hit.collider.GetComponent<Activatable>();
            if (activatable != null)
            {
                activatable.OnActivate();
            }
        }
    }
}   
