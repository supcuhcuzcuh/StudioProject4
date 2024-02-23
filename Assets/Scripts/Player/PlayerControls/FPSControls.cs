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
    private InteractableController interactableController;
    private Sliding sliding;
   

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
        interactableController = gameObject.AddComponent<InteractableController>();
        interactableController.Init(3);
        sliding = GetComponent<Sliding>();
        playerStatsUIManager.UpdateHealthUI(health.ToString());

        gunController.SubscribeShootResponse(playerStatsUIManager);
        gunController.SubscribeShootResponse(cameraRecoil);

        gunController.SubscribeReloadResponse(playerStatsUIManager.UpdateAmmoUI);
        gunController.SubscribeReloadResponse(playerStatsUIManager.UpdateReloadTimerUI);

        gunController.SubscribeSwapResponse(playerStatsUIManager.UpdateAmmoUI);
        gunController.SubscribeGrenadeResponse(playerStatsUIManager.UpdateGrenadeUI);

        sprintControl.Subscribe(gunController);
    }

    // Update is called once per frame
    void Update()
    {
        sprintControl.Sprint();
        interactableController.HandleInteract();
        jumpControl.Jump();
        crouchControl.ActivateCrouch();
        sliding.SlidingUpdate();
        gunController.HandleShooting();
        timetravelControl.TimeTravel();
    }

    private void FixedUpdate()
    {
        movementControl.Movement();
        sliding.SlidingFixedUpdate();
    }


    public override void OnDamaged(float _damage)
    {
        health -= _damage;
        playerStatsUIManager.UpdateHealthUI(health.ToString());
    }
}
