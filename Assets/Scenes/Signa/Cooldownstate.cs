using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldownstate : State
{
    public float cooldownDuration = 4f; // Duration of cooldown in seconds
    public Material cooldownMaterial; // Reference to the cooldown material
    public BossShootState returnBossshoot; // Reference to the next state

    private bool doneCooldown;
    private float startTime;
    private Vector3 initialPosition;
    private Renderer bossHeadRenderer;
    private Quaternion initialRotation; // Added to store initial rotation
    private float lerpingDuration = 3f; // Adjust the duration for lerping to +20 degrees

    public Transform bossHead;
    [SerializeField] private Entity BossHp;

    public GameObject thebarrel;

    private bool playonce = false;
    [SerializeField] private AudioClip Shutdown;
    [SerializeField] private BossDeathState Deathstate;

    [SerializeField] private AudioSource Placetoplay;
    private void Start()
    {
        initialPosition = bossHead.position;
        bossHeadRenderer = thebarrel.GetComponent<Renderer>();
    }

    public override State PlayCurrentState()
    {
        if (!playonce )
        {
            playonce = true;
            Placetoplay.PlayOneShot(Shutdown);
           
        }
        // Lerping the x rotation axis to +20 degrees
        float lerpProgress = Mathf.Clamp01((Time.time - startTime) / lerpingDuration);
        Quaternion initialRotation = bossHead.rotation; // Store the initial rotation
        Quaternion targetRotation = Quaternion.Euler(20, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z);
        bossHead.rotation = Quaternion.Lerp(initialRotation, targetRotation, lerpProgress);
     
        // Set the material color to red during the lerping phase
        bossHeadRenderer.material.color = Color.red;

        // Check if lerping is done
        if (lerpProgress >= 1f)
        {
            // If lerping is done, start the cooldown timer
            if (!doneCooldown)
            {
                startTime = Time.time;
                doneCooldown = true;
            }

            // Set the material color to red during the cooldown phase
            bossHeadRenderer.material.color = Color.red;

            // Check if cooldown duration is reached
            if (Time.time - startTime >= cooldownDuration)
            {
                // If cooldown is done, reset the rotation and transition to the next state
                bossHead.rotation = initialRotation;
                // Set the material color to red during the cooldown phase
                bossHeadRenderer.material.color = Color.black;
                playonce = false;
                return returnBossshoot;
            }
        }
        float Bosshealth = BossHp.GetHealth();
        if (Bosshealth <= 0)
        {
            return Deathstate;

        }
        return this;
    }


}
