using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FPSControls : Entity   //Main Controller for all player movements, also derives from entity
{
    private MovementControl movementControl;
    private SprintControl sprintControl;
    private JumpControl jumpControl;
    private CrouchControl crouchControl;
    private GunController gunController;
    private TimeTravelControl timetravelControl;
    private InteractablesActivator interactablesActivator;

    [Header("Serialize Stats")]
    [SerializeField]
    private float interactionRadius = 2;
    [SerializeField]
    private PerlinNoiseShake perlinNoiseShake;
    [SerializeField]
    private HeadbobShake headbobShake;
    [SerializeField]
    private GunRecoilShake gunRecoilShake;
    [SerializeField]
    private GunBobShake gunBobShake;
    [SerializeField]
    private PlayerStatsUIManager playerStatsUIManager;
    [SerializeField]
    private CameraRecoil cameraRecoil;

    // Start is called before the first frame update
    void Start()
    {
        movementControl = GetComponent<MovementControl>();
        jumpControl = GetComponent<JumpControl>();
        crouchControl = GetComponent<CrouchControl>();
        gunController = GetComponent<GunController>();
        sprintControl = GetComponent<SprintControl>();
        timetravelControl = GetComponent<TimeTravelControl>();
        interactablesActivator = gameObject.AddComponent<InteractablesActivator>();  // Create when run
        interactablesActivator.Init(transform, interactionRadius);  // Setting variable for InteractablesActivator 
        //playerStatsUIManager.UpdateHealthUI(health.ToString());

        //gunController.SubscribeShootResponse(playerStatsUIManager);
        gunController.SubscribeShootResponse(cameraRecoil);

        //gunController.SubscribeReloadResponse(playerStatsUIManager.UpdateAmmoUI);
        gunController.SubscribeReloadResponse(playerStatsUIManager.UpdateReloadTimerUI);

        //gunController.SubscribeSwapResponse(playerStatsUIManager.UpdateAmmoUI);
        gunController.SubscribeGrenadeResponse(playerStatsUIManager.UpdateGrenadeUI);

        sprintControl.Subscribe(gunController);
    }

    // Update is called once per frame
    void Update()
    {
        sprintControl.Sprint();
        jumpControl.Jump();
        crouchControl.ActivateCrouch();
        gunController.HandleShooting();
        timetravelControl.TimeTravel();
        interactablesActivator.Handle();
    }

    private void FixedUpdate()
    {
        movementControl.Movement();
    }

        
    public override void OnDamaged(float _damage)
    {
        health -= _damage;
        playerStatsUIManager.UpdateHealthUI(health.ToString());
    }

}
