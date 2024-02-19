using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homingmissile : MonoBehaviour
{
    public string targetTag = "Player";
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private Transform target;
    private Rigidbody rb;

    public float elapsedtim = 0f;
    public float beforeshoot = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Disable gravity for the missile
        rb.useGravity = false;
        FindTarget();
    }

    void FixedUpdate()
    {
        if (elapsedtim <= beforeshoot)
        {
          

            // Calculate the rotation to face the movement direction
            Quaternion toRotation = Quaternion.LookRotation(rb.velocity.normalized);

            // Apply rotation gradually
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 40* Time.deltaTime);
            elapsedtim += Time.deltaTime;
        }
        if (elapsedtim >= beforeshoot)
        {
            if (target == null)
            {
                FindTarget();
            }
            else
            {
                // Move towards the target using Rigidbody.AddForce
                Vector3 direction = target.position - transform.position;
                direction.Normalize();
                rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Acceleration);

                // Rotate towards the target
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
   }

        void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            // Find the closest target
            float closestDistance = Mathf.Infinity;
            foreach (GameObject potentialTarget in targets)
            {
                float distanceToTarget = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    target = potentialTarget.transform;
                }
            }
        }
    }
}
