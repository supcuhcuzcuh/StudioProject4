using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Entity
{
    [SerializeField]
    private GameObject turretTop;

    bool rotateDir = false;
    float turretAngle = 0;

    private List<Transform> scanPoints = new List<Transform>();
    [SerializeField]
    private Transform turretScan;

    private Transform currTarget;
    private float turretDamage = 2;
    private float nextTimeToShoot;
    private bool hasShot = false;

    [SerializeField]
    private ParticleEffectManager particleEffectManager;


    enum States
    { 
        SCAN,
        ATTACK,
        DEAD
    };

    [SerializeField]
    States currState = States.SCAN;


    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < turretScan.childCount; i++)
        {
            scanPoints.Add(turretScan.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {     
        
        if (currState == States.SCAN)
        {
            if (rotateDir == false)
            {
                turretTop.transform.Rotate(0, -20 * Time.deltaTime, 0);
                turretAngle += -20 * Time.deltaTime;
                if (turretAngle < -90)
                {
                    rotateDir = true;
                }
            }
            else
            {
                turretTop.transform.Rotate(0, 20 * Time.deltaTime, 0);
                turretAngle += 20 * Time.deltaTime;
                if (turretAngle > 90)
                {
                    rotateDir = false;
                }
            }
            DetectTarget();
        }
        else if(currState == States.ATTACK)
        {
            turretTop.transform.LookAt(currTarget.position);
            if(hasShot == false)
            {
                StartCoroutine("DoShootTarget");
                DetectTarget();
            }
            float distance = Vector3.Distance(transform.position, currTarget.position);
            if (distance < 25f)
            {
                currState = States.SCAN;
            }

        }
       
    }

    private void DetectTarget()
    {
        RaycastHit hit;
      
        if (Physics.Raycast(turretScan.position, turretTop.transform.forward, out hit, 10))
        {
            //enter attackstate
            if (hit.collider.gameObject.tag == "Player")
            {
                currTarget = hit.collider.transform;
                currState = States.ATTACK;
            }
        }

    }

    private void ShootTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(turretScan.position, turretTop.transform.forward, out hit, 20))
        {
            //enter attackstate
            if (hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Entity>().OnDamaged(turretDamage);
            }
            else
            {
                currState = States.SCAN;
            }
        }
    }

    private IEnumerator DoShootTarget()
    {
        hasShot = true;
        yield return new WaitForSeconds(0.2f);
        ShootTarget();
        hasShot = false;
    }

    public override void OnDamaged(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            currState = States.DEAD;
            particleEffectManager.PlayParticleEffect("DeadSparks", turretTop.transform.position);
            turretTop.transform.Rotate(20, 0, 0);
        }
    }

}
