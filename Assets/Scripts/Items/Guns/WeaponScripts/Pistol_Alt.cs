using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pistol_Alt : HitscanWeapon
{

    WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;

    private Animator animator;

    protected override void Init()
    {
        laserLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
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
        animator.Play("FIRE");
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

    public override void OnSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
    public override void OffSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

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
