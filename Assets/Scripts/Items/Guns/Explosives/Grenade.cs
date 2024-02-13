using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float grenadeDamage = 75f;

    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        GameObject.Find("CameraHolder").GetComponent<PerlinNoiseShake>().QueueExplosion(5, 1);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            if(nearbyObject.gameObject.GetComponent<Entity>())
            {
                nearbyObject.gameObject.GetComponent<Entity>().OnDamaged(grenadeDamage);
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

        }
        Destroy(gameObject);
    }

    public void StartGrenadeTimer()
    {
        StartCoroutine("GrenadeCookout");
    }

    IEnumerator GrenadeCookout()
    {
        yield return new WaitForSeconds(delay);

        Explode();

    }
}
