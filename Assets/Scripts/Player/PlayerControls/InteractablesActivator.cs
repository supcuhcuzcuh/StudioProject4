using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesActivator : MonoBehaviour
{
    private Transform playerTransform;
    private float interactionRadius;

    public void Init(Transform playerTransform, float interactionRadius)
    {
        this.playerTransform = playerTransform;
        this.interactionRadius = interactionRadius;
    }

    public void Handle()
    {
        // Activate the nearest lever to the player
        if (Input.GetKey(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(playerTransform.position, interactionRadius);
            float minDistance = float.MaxValue;
            Lever nearestLever = null;

            foreach (Collider col in colliders)
            {
                Lever lever = col.GetComponent<Lever>();
                if (lever)
                {
                    float distance = Vector3.Distance(playerTransform.position, lever.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestLever = lever;
                    }
                }
            }

            if (nearestLever) nearestLever.OnActivate();
        }
    }
}
