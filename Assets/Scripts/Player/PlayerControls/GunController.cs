using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Animations.Rigging;

public interface IShootResponse
{
    void OnMouse1();
    void OnMouse2();
}

public class GunController : MonoBehaviour , ISprintResponse
{
        private List<IShootResponse> shootResponses = new List<IShootResponse>();     

        private float nextTimeToShoot;

        private System.Action _onReloadEvent = null;
        private System.Action _onSwapEvent = null;
        private System.Action _onGrenadeEvent = null;

        [SerializeField] private TwoBoneIKConstraint rightArmMover;
        [SerializeField] private Rig rightFingers;
        [SerializeField] private Transform rightArmTarget;

        public Weapon currWeapon;

        [SerializeField]
        private GameObject grenade;

        public PlayerStats playerStats;

        public void HandleShooting()
        {
            if(currWeapon != null)
            {
                if (currWeapon.isReloading == false && Input.GetButton("Fire1") && Time.time > nextTimeToShoot)     //Main action for shooting
                {
                    currWeapon.OnMouse1();
                    NotifyShootResponse();

                    // set cooldown delay
                    nextTimeToShoot = Time.time + currWeapon.cooldownWindow;
                }
                else if (Input.GetButton("Fire2"))  //Aim down sights
                {
                    currWeapon.OnMouse2();
                }

                if (!Input.GetButton("Fire2"))  //Back to hip fire
                {
                    currWeapon.OnMouse2Up();
                }

                if (Input.GetKeyDown(KeyCode.R))    //Reload
                {
                    if(currWeapon == null || currWeapon.clipSizeCurr == currWeapon.clipSizeMax)
                    {

                    }
                    else
                    {
                        currWeapon.OnReload();
                        _onReloadEvent.Invoke();
                    }
                    
                }

                //if (Input.GetKeyDown(KeyCode.Q))    //Qnequip Weapon
                //{
                //    Camera.main.transform.DetachChildren();
                //    currWeapon.UnsetWeapon();
                //    currWeapon = null;

                //}
            }
           

            if (Input.GetKeyDown(KeyCode.E))    //Equip Weapon
            {
                Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, 1000))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.tag == "Weapon")
                    {
                        Camera.main.transform.DetachChildren();
                        if (currWeapon != null)
                        {
                            currWeapon.UnsetWeapon();
                            currWeapon = null;
                        }                        
                        currWeapon = hitObject.GetComponent<Weapon>();

                        currWeapon.SetWeapon();
                        rightArmMover.weight = 1;
                        rightFingers.weight = 1;
                        rightArmTarget.position = currWeapon.handPosition.position;
                        rightArmTarget.SetParent(currWeapon.gameObject.transform);

                        var children = GetComponentsInChildren<Transform>();
                        foreach (var child in children)
                        {
                            child.gameObject.layer = 0;
                        }
                        currWeapon.gameObject.layer = 0;
                        
                        _onSwapEvent.Invoke();
                    }
                else if (hitObject.tag == "ENEMY_WEAPON")
                {
                    Camera.main.transform.DetachChildren();
                    if (currWeapon != null)
                    {
                        currWeapon.UnsetWeapon();
                        currWeapon = null;
                    }
                    currWeapon = hitObject.GetComponent<Weapon>();

                    currWeapon.SetWeapon();
                    _onSwapEvent.Invoke();
                }
            }
            }
            if (Input.GetKeyDown(KeyCode.G) && playerStats.playerGrenades != 0) //Throw grenade
            {               
                playerStats.playerGrenades -= 1;
                _onGrenadeEvent.Invoke();
                GameObject newGrenade = Instantiate(grenade, transform.position, transform.rotation);
                newGrenade.GetComponent<Rigidbody>().AddForce(( (Camera.main.transform.forward * 10f) + (Camera.main.transform.up * 6f) ), ForceMode.Impulse);
                newGrenade.GetComponent<Grenade>().StartGrenadeTimer();
            }
            
        }

        public void OnSprint()
        {
            if (currWeapon != null)
            {
                currWeapon.OnSprintAnimation();
            }
            
        }
        public void OffSprint()
        {
            if (currWeapon != null)
            {
                currWeapon.OffSprintAnimation();
            }   
        }

        IEnumerator WaitforReloadFinish()
        {
            yield return new WaitForSeconds(currWeapon.reloadSpeed);
            _onReloadEvent.Invoke();
        }

        private void NotifyShootResponse()
        {
            foreach (var shootResponse in shootResponses)
            {
                shootResponse.OnMouse1();
            }
        }

        public void SubscribeShootResponse(IShootResponse shootResponse)
        {
            shootResponses.Add(shootResponse);
        }

        public void SubscribeReloadResponse(System.Action _reloadEvent)
        {
            _onReloadEvent += _reloadEvent;
        }

        public void SubscribeSwapResponse(System.Action _swapEvent)
        {
            _onSwapEvent += _swapEvent;
        }

        public void SubscribeGrenadeResponse(System.Action grenadeEvent)
        {
            _onGrenadeEvent += grenadeEvent;
        }
}

