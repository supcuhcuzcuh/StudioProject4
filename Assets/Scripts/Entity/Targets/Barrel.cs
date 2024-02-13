using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Entity
{

    public float radius = 5f;
    public float explosionDamage = 1f;

    [SerializeField]
    private ParticleEffectManager particleEffectManager;
    [SerializeField]
    private GameObject spawnPickup;

    [SerializeField]
    private GameObject flameSpawn;

    private bool onFire = false;

    public override void OnDamaged(float damage)
    {
        health -= damage;
        if(onFire == false)
        {
            particleEffectManager.PlayParticleEffect("BarrelFlame", flameSpawn.transform.position, transform);
            onFire = true;
        }
      
        if (health <= 0)
        {
            particleEffectManager.PlayParticleEffect("Explosion", transform.position);
            GameObject.Find("CameraHolder").GetComponent<PerlinNoiseShake>().QueueExplosion(2, 1);

            Instantiate(spawnPickup, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
