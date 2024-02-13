using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeTarget : DesctrutableObjects
{
    [SerializeField]
    private float targetRotationZ = 90;

    private Vector3 targetRotation;

    private bool isShotdown = false;

    public ShootingRangeTutorialScriptedEvent shootingRangeTutorialScriptedEvent;

    void Update()
    {
        if(isShotdown == true && Mathf.Abs(transform.rotation.z) <= Mathf.Abs((targetRotationZ - 5)))
        {
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, (transform.rotation.z + 10 * Time.deltaTime));
            targetRotation = Vector3.Slerp(targetRotation, new Vector3(0, 0, -90), 4 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(targetRotation);
        }
    }

    public override void OnDamaged(float damage)
    {
        if(health > 0)
        {
            health -= damage;
            if (health <= 0) OnObjectDestroy();
        }
        
    }

    public override void OnObjectDestroy()
    {
        isShotdown = true;
        shootingRangeTutorialScriptedEvent.CompletedShootingRange();
    }
}
