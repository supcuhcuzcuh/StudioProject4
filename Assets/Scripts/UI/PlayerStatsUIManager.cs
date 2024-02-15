using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsUIManager : MonoBehaviour, IShootResponse
{

    [SerializeField]
    private TMP_Text weaponAmmoCount;
    [SerializeField]
    private TMP_Text grenadeCount;
    [SerializeField]
    private TMP_Text playerAmmoCount;

    [SerializeField]
    private GunController gunController;
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private TMP_Text playerHealthCount;

    [SerializeField]
    private GameObject reloadTimerUI;
    private Slider reloadTimerUISlider;

    private bool startReloadTimer = false;
    private float reloadTimerValue;

    // Start is called before the first frame update
    void Start()
    {      
        reloadTimerUISlider = reloadTimerUI.GetComponent<Slider>();
        reloadTimerUI.SetActive(false);

        playerAmmoCount.text = playerStats.playerAmmo.ToString();

        UpdateAmmoUI();
        UpdateGrenadeUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startReloadTimer == true)
        {
            reloadTimerUISlider.value += reloadTimerValue;
            if(reloadTimerUISlider.value >= reloadTimerUISlider.maxValue)
            {
                reloadTimerUI.SetActive(false);
                startReloadTimer = false;              
                UpdateAmmoUI();
            }
        }
    }

    public void OnMouse1()
    {
        UpdateAmmoUI();
    }

    public void OnMouse2()
    {

    }

    public void UpdateAmmoUI()
    {
        //Debug.Log(gunController.currWeapon.clipSizeCurr);
        if(gunController.currWeapon == null)
        {
            weaponAmmoCount.text = "0";       
        }
        else
        {
            weaponAmmoCount.text = gunController.currWeapon.clipSizeCurr.ToString();
            playerAmmoCount.text = playerStats.playerAmmo.ToString();
        }
        
    }

    public void UpdateGrenadeUI()
    {
        grenadeCount.text = playerStats.playerGrenades.ToString();      
    }

    public void UpdateReloadTimerUI()
    {
        reloadTimerUI.SetActive(true);
        reloadTimerUISlider.value = 0;
        startReloadTimer = true;
        reloadTimerValue = (Time.fixedDeltaTime / gunController.currWeapon.reloadSpeed) * 0.9f;  //reason we are multiplying by 0.9 is to make sure the internal ammo count is updated before sending it to the UI
    }

    public void UpdateHealthUI(string _health)
    {
        playerHealthCount.text = _health;
      
    }
}
