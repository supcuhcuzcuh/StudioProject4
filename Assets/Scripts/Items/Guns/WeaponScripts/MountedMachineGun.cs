using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedMachineGun : HitscanWeapon
{

    WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;

    private bool mounting = false;
    private GameObject player;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (mounting == true)
        {
            //player.transform.forward = Vector3.Lerp(player.transform.forward, transform.forward, 4 * Time.deltaTime);
            player.transform.position = Vector3.Lerp(player.transform.position, transform.position - player.transform.forward, 4 * Time.deltaTime);          
        }
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

    protected override void HitscanHit(RaycastHit _hit)
    {
        laserLine.SetPosition(1, _hit.point);
        if (_hit.collider.gameObject.GetComponent<DesctrutableObjects>())
        {
            _hit.collider.gameObject.GetComponent<DesctrutableObjects>().OnDamaged(weaponDamage);

        }
        //bulletHoleSpawner.CreateBulletHole(_hit.point, _hit.normal, _hit.collider.gameObject.transform);
    }

    protected override void HitscanMiss(Vector3 _rayOrigin)
    {
        laserLine.SetPosition(1, _rayOrigin + (Camera.main.transform.forward * weaponRange));
    }

    public override void SetWeapon()
    {
        mounting = true;
        player = GameObject.FindWithTag("Player");
        player.transform.position = transform.position - player.transform.forward;
    }

    public override void OnMouse2()
    {
        if (isAiming == false)
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