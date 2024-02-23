using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : State
{

    public Transform bossHead;
    public GameObject thebarrel;
    public GameObject explosionPrefab;
    public Collider col;

    private float elapsedtim = 0f;
    public float beforeshoot = 2.0f;

    private bool launch;

    private bool spawnexplode;

    [SerializeField] private AudioClip explode1,explode2;

    [SerializeField] private AudioSource Placetoplay;
    private void Start()
    {
        
    }

    public override State PlayCurrentState()
    {
        if (!spawnexplode)
        {
            // Your initialization code here// Spawn explosions at four different locations
            for (int i = 0; i < 4; i++)
            {
                col.enabled = false;
                Vector3 explosionPosition = bossHead.position + Random.onUnitSphere * 6f;
                // Optionally: Instantiate explosion particle effects or play explosion sound
                GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
                // Destroy the explosion particle effects after a delay
                Destroy(explosion, 20.0f); // Adjust the time according to your needs

             
                Placetoplay.PlayOneShot(explode2);
            }
            spawnexplode = true;

          
        }
        if (elapsedtim <= beforeshoot)
        {
            
            elapsedtim += Time.deltaTime;
        }

        if (elapsedtim >= beforeshoot)
        {
            // Activate gravity on thebarrel's rigidbody
            Rigidbody thebarrelRigidbody = thebarrel.GetComponent<Rigidbody>();

            BoxCollider barrelcollide = thebarrel.GetComponent<BoxCollider>();
            if (thebarrelRigidbody != null && launch == false)
            {
          
                Placetoplay.PlayOneShot(explode1);
                thebarrelRigidbody.useGravity = true;

                barrelcollide.enabled = true;
                // Add an impulse force upwards
                // Set explosion parameters
                float explosionForce = 3000f; // Adjust the force as needed
                float explosionRadius = 9f; // Adjust the radius as needed

                // Apply explosive force
                thebarrelRigidbody.AddExplosionForce(explosionForce, thebarrelRigidbody.position, explosionRadius);


                launch = true;
            }

        }

        return this;
    }

}
