using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : HitscanWeapon
{

    WaitForSeconds shotDuration = new WaitForSeconds(0.07f);

    private LineRenderer laserLine;
    [SerializeField]
    private GameObject sniperScopeOverlay;
    [SerializeField]
    private GameObject sniperModel;

    protected override void Init()
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
        transform.localRotation = Quaternion.Euler(10, -90, 0);
    }
    public override void OffSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    protected override void HitscanHit(RaycastHit _hit)
    {
        laserLine.SetPosition(1, _hit.point);
        if(_hit.collider.gameObject.GetComponent<Entity>())
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
        if (isAiming == false)
        {
            recoilMultiplier = 0.1f;
            Vector3 targetPos = new Vector3(0, -0.5f, 0.6f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 25.0f, 5 * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 4 * Time.deltaTime); 
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4 * Time.deltaTime); 
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 5, 5), 4 * Time.deltaTime);

            if (Mathf.Abs(transform.localPosition.x) >= (Mathf.Abs(targetPos.x) - 0.01f) && Mathf.Abs(transform.localPosition.y) >= (Mathf.Abs(targetPos.y) - 0.01f) && Mathf.Abs(transform.localPosition.z) >= (Mathf.Abs(targetPos.z) - 0.01f))
            {
                isAiming = true;
                Camera.main.fieldOfView = 10;
                sniperScopeOverlay.SetActive(true);
                sniperModel.SetActive(false);
            }
        }

        //Debug.Log("Current: " + transform.forward + "  Target: " + Camera.main.transform.forward);
    }

    public override void OnMouse2Up()
    {
        recoilMultiplier = 1.0f;
        isAiming = false;
        Camera.main.fieldOfView = 60;
        if (playerStat.currState != PlayerStats.PLAYERSTATES.SPRINT)
        {
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 6 * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, setWeaponPosition, 6 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 6 * Time.deltaTime);
        }
        sniperScopeOverlay.SetActive(false);
        sniperModel.SetActive(true);

    }

}

