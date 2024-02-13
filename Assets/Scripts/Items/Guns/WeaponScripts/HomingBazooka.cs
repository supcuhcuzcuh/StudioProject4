using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HomingBazooka : ProjectileWeapon
{
    [SerializeField]
    private GameObject homingBazookaScopeOverlay;
    private List<GameObject> homingBazookaLockOnOverlay = new List<GameObject>();

    [SerializeField]
    private GameObject homingBazookaModel;

    public GameObject targetObject = null;
    private bool tracking = false;

    [SerializeField]
    private LayerMask layermask;

    void Start()
    {
        for (int i = 0; i < homingBazookaScopeOverlay.transform.childCount; i++)
        {
            homingBazookaLockOnOverlay.Add(homingBazookaScopeOverlay.transform.GetChild(i).gameObject);          
        }
    }

    public override void FireWeapon()
    {
        if (objectPool != null && targetObject != null)
        {
            RevisedProjectile bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            bulletObject.weaponSource = this;

            // align to gun barrel/muzzle position
            bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            // move projectile forward
            bulletObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * muzzleVelocity, ForceMode.Force);

            // turn off after a few seconds
            bulletObject.Deactivate();
        }
    }

    public override void OnSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(22, -22, 0);
    }
    public override void OffSprintAnimation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    //public override void SetWeapon()
    //{
    //    transform.localPosition = new Vector3(0.6f, 0, 0.4f);
    //    transform.forward = Camera.main.transform.forward;
    //    GetComponent<Rigidbody>().isKinematic = true;
    //    GetComponent<Collider>().enabled = false;
    //}

    public override void OnMouse2()
    {
        if (isAiming == false)
        {
            Vector3 targetPos = new Vector3(0.9f, -0.4f, 0.75f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30.0f, 4 * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 4 * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 4 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3, 3, 3), 4 * Time.deltaTime);

            if (Mathf.Abs(transform.localPosition.x) >= (Mathf.Abs(targetPos.x) - 0.01f) && Mathf.Abs(transform.localPosition.y) >= (Mathf.Abs(targetPos.y) - 0.01f) && Mathf.Abs(transform.localPosition.z) >= (Mathf.Abs(targetPos.z) - 0.01f))
            {

               
                isAiming = true;
                Camera.main.fieldOfView = 30;
                homingBazookaScopeOverlay.SetActive(true);
                homingBazookaModel.SetActive(false);
            }
        } 
        else
        {
            if (targetObject != null || tracking == true)
            {

            }
            else
            {
                audioManager.PlaySoundEffect("LockOn");
                StartCoroutine("TrackTarget");
            }
        }
        //Debug.Log("Current: " + transform.forward + "  Target: " + Camera.main.transform.forward);
    }

    IEnumerator TrackTarget()
    {
        tracking = true;       
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;
        GameObject tempgameObject = null;
        int index = 0;

        yield return new WaitForSeconds(0.5f);
        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, 3000, layermask))
        {
            if (hit.collider.gameObject.GetComponent<Entity>())
            {
                tempgameObject = hit.collider.gameObject;
                homingBazookaLockOnOverlay[index].SetActive(true);
                index += 1;
            }

        }  
        if(tempgameObject != null)
        {
            yield return new WaitForSeconds(0.5f);
            homingBazookaLockOnOverlay[index].SetActive(true);
            index += 1;
            yield return new WaitForSeconds(0.5f);
            homingBazookaLockOnOverlay[index].SetActive(true);         
        }
        targetObject = tempgameObject;
        index = 0;
        tracking = false;
    }

    public override void OnMouse2Up()
    {
        recoilMultiplier = 1.0f;
        isAiming = false;
        Camera.main.fieldOfView = 60;
        targetObject = null;
        if (playerStat.currState != PlayerStats.PLAYERSTATES.SPRINT)
        {
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 6 * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, setWeaponPosition, 6 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 6 * Time.deltaTime);
        }
        for (int i = 0; i < homingBazookaScopeOverlay.transform.childCount; i++)
        {
            homingBazookaLockOnOverlay[i].SetActive(false);
        }
        homingBazookaScopeOverlay.SetActive(false);
        homingBazookaModel.SetActive(true);

    }

    public override Transform GetTarget()
    {
        return targetObject.transform;
    }
}
