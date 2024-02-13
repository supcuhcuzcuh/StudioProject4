using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : RevisedProjectile
{
    void OnCollisionEnter (Collision _col)
    {
        Debug.Log(_col.gameObject);
        ContactPoint contactPoint = _col.GetContact(0);
        weaponSource.SpawnBulletHole(contactPoint.point, contactPoint.normal, _col.gameObject.transform);
        ProjectileCollide(_col.gameObject);

        if(_col.gameObject.GetComponent<Entity>())
        {
            _col.gameObject.GetComponent<Entity>().OnDamaged(weaponSource.weaponDamage);
        }

    }
}
