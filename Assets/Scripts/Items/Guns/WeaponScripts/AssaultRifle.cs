using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : HitscanWeapon
{
    WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }

    public override void FireWeapon()
    {
        StartCoroutine(ShotEffect());

        laserLine.SetPosition(0, muzzlePosition.position);

        DoHitscan();
    }

    public override void OnSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(20, -30, 50);
    }
    public override void OffSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    protected override void HitscanHit(RaycastHit _hit)
    {
        laserLine.SetPosition(1, _hit.point);
        if (_hit.collider.gameObject.GetComponent<Entity>())
        {
            _hit.collider.gameObject.GetComponent<Entity>().OnDamaged(weaponDamage);
           
        }
        bulletHoleSpawner.CreateBulletHole(_hit.point, _hit.normal, _hit.collider.gameObject.transform);
    }

    protected override void HitscanMiss(Vector3 _rayOrigin)
    {
        laserLine.SetPosition(1, _rayOrigin + (Camera.main.transform.forward * weaponRange));
    }

    //public override void SetWeapon()
    //{
    //    transform.localPosition = new Vector3(0.35f, -0.2f, 0.4f);
    //    transform.localRotation = Quaternion.Euler(0, 90, 0);
    //    transform.forward = Camera.main.transform.forward;
    //    GetComponent<Rigidbody>().isKinematic = true;
    //    GetComponent<Collider>().enabled = false;
    //}

    public override void OnMouse2()
    {
        if(isAiming == false)
        {
            recoilMultiplier = 0.5f;
            Vector3 targetPos = new Vector3(0, -0.25f, transform.localPosition.z);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30.0f, 4 * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 4 * Time.deltaTime); 
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4 * Time.deltaTime); 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), 4 * Time.deltaTime);
        }


        //Debug.Log("Current: " + transform.forward + "  Target: " + Camera.main.transform.forward);
    }
}

