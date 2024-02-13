using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : Entity
{
    [SerializeField]
    private List<GameObject> waypoints = new List<GameObject>();
    [SerializeField]
    private GameObject helicopter;

    [SerializeField]
    private float enemySpeed = 10;

    private int targetIndex;
    private Vector3 destination;
    private Vector3 dir;

    [SerializeField]
    private ParticleSystem explosionEffect;
    [SerializeField]
    private ParticleSystem smokeEffect;


    [SerializeField]
    private ParticleEffectManager particleEffectManager;

    public enum States
    {
        MOVE,
        DEAD
    };


    public States currState = States.MOVE;

    private void Start()
    {
        targetIndex = 0;
        destination = waypoints[targetIndex].transform.position;
        dir = (destination - transform.position).normalized;
    }

    private void Update()
    {
        if (currState == States.MOVE)
        {
            helicopter.transform.position += dir * enemySpeed * Time.deltaTime;
            helicopter.transform.forward = dir;

            if (Vector3.Distance(destination, helicopter.transform.position) <= 1.0f)
            {
                targetIndex++;
                targetIndex %= waypoints.Count;
                destination = waypoints[targetIndex].transform.position;
                dir = (destination - helicopter.transform.position).normalized;
            }
        }
        else if(currState == States.DEAD)
        {
            helicopter.transform.position += (dir + new Vector3(0, -2, 0)) * enemySpeed * Time.deltaTime;
            helicopter.transform.Rotate(0, 120 * Time.deltaTime, 0);
        }
    }

 
    public override void OnDamaged(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Dead");
            particleEffectManager.PlayParticleEffect("Smoke", transform.position, transform);
            currState = States.DEAD;
            enemySpeed = 4f;
            StartCoroutine("CrashingDown");
        }
    }
    
    IEnumerator CrashingDown()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        particleEffectManager.PlayParticleEffect("Explosion", transform.position);
        GameObject.Find("CameraHolder").GetComponent<PerlinNoiseShake>().QueueExplosion(10, 2);   
    }



}
