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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactRange))
        {
            Activatable activatable = hit.collider.GetComponent<Activatable>();
            if (activatable != null)
            {
                activatable.OnActivate();
            }
        }
    }
}   
