using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pistol : ProjectileWeapon
{
     

    public override void FireWeapon()
    {
        if(objectPool != null)
        {
            RevisedProjectile bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            bulletObject.weaponSource = this;

            // align to gun barrel/muzzle position
            bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            // move projectile forward
            bulletObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * muzzleVelocity, ForceMode.Acceleration);

            // turn off after a few seconds
            bulletObject.Deactivate();
        }     
    }

    public override void OnSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
    public override void OffSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    //public override void SetWeapon()
    //{
    //    transform.localPosition = new Vector3(0.2f, -0.1f, 0.4f);
    //    transform.forward = Camera.main.transform.forward;
    //    transform.localRotation = Quaternion.Euler(0, -15, 0);
    //    GetComponent<Rigidbody>().isKinematic = true;
    //    GetComponent<Collider>().enabled = false;
    //}

    public override void OnMouse2()
    {
        if (isAiming == false)
        {
            recoilMultiplier = 0.5f;
            Vector3 targetPos = new Vector3(0, -0.15f, 0.6f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30.0f, 4 * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 4 * Time.deltaTime); 
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4 * Time.deltaTime); 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), 4 * Time.deltaTime);
        }

        //Debug.Log("Current: " + transform.forward + "  Target: " + Camera.main.transform.forward);
    }
}
