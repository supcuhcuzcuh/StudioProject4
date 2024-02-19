using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IShootResponse
{
    [Tooltip("End point of gun where shots appear")]
    [SerializeField] protected Transform muzzlePosition;
    [Tooltip("Time between shots / smaller = higher rate of fire")]
    public float cooldownWindow = 0.1f;
    [Tooltip("Damage weapon does")]
    public float weaponDamage = 10.0f;
    [Tooltip("Total Ammo weapon has")]
    public int clipSizeMax = 10;
    [Tooltip("Curremt Ammo weapon has")]
    public int clipSizeCurr = 10;
    [Tooltip("Time in seconds it takes to finish reloading")]
    public float reloadSpeed = 1.0f;
    public bool isReloading = false;

    [SerializeField] protected PlayerStats playerStat;
    protected bool isAiming;

    //Hipfire Recoil
    [SerializeField] private float recoilStrength;
    [SerializeField] protected float recoilMultiplier = 1;

    [SerializeField]
    protected Vector3 setWeaponPosition;

    [SerializeField]
    protected BulletHoleSpawner bulletHoleSpawner;

    [SerializeField]
    protected AudioManager audioManager;

    [SerializeField]
    private ParticleEffectManager particleEffectManager;

    public void OnMouse1()
    {
        if (clipSizeCurr > 0)
        {
            clipSizeCurr -= 1;
            FireWeapon();
            audioManager.PlaySoundEffect("Shot");
            particleEffectManager.PlayParticleEffect("MuzzleFlash", muzzlePosition.position, muzzlePosition);
        }
        else
        {
            audioManager.PlaySoundEffect("NoAmmo");
        }
    }

    protected virtual void Init()
    {

    }

    public Vector3 GetMuzzlePosition()
    {
        return muzzlePosition.position;
    }

    public float GetRecoilStrength()
    {
        return recoilStrength * recoilMultiplier;
    }

    public virtual void OnMouse2()
    {        

    }

    public virtual void OnMouse2Up()
    {
        recoilMultiplier = 1;
        isAiming = false;
        Camera.main.fieldOfView = 60;
        if (playerStat.currState != PlayerStats.PLAYERSTATES.SPRINT)
        {
            transform.forward = Vector3.Lerp(transform.forward, Camera.main.transform.forward, 6 * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, setWeaponPosition, 6 * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), 6 * Time.deltaTime);
        }

    }

    public virtual void OnSprintAnimation()
    {

    }
    public virtual void OffSprintAnimation()
    {

    }

    public virtual void FireWeapon()
    {

    }

    public virtual void OnReload()
    {
        StartCoroutine("ReloadTimer");
    }


    public virtual void SetWeapon()
    {
        transform.parent = Camera.main.transform;
        transform.localPosition = setWeaponPosition;
        transform.forward = Camera.main.transform.forward;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
    }

    public void UnsetWeapon()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(2, 4, 0), ForceMode.Impulse);
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().enabled = true;
    }


    IEnumerator ReloadTimer()
    {
        audioManager.PlaySoundEffect("Reload");
        isReloading = true;     
        yield return new WaitForSeconds(reloadSpeed);
        if (playerStat.playerAmmo == 0)     //Player has No ammo
        {
            Debug.Log("Here");
        }
        else if (playerStat.playerAmmo < clipSizeMax)   //Player has less ammo than clip size   
        {
            clipSizeCurr = playerStat.playerAmmo;
            playerStat.playerAmmo = 0;
        }
        else     //Regular Reload
        {
            int diff = clipSizeMax - clipSizeCurr;
            playerStat.playerAmmo -= diff;
            clipSizeCurr = clipSizeMax;
        }
        isReloading = false;
    }
}
