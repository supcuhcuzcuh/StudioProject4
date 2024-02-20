using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerStats : ScriptableObject
{
    public float moveSpeed;
    public float moveSpeedMultiplier;
    public float jumpForce;
    public bool canJump;

    public int playerAmmo;
    public int playerGrenades;

    //public int startingAmmo;

    public enum PLAYERSTATES
    {
        IDLE,
        WALK,
        SPRINT,
        JUMP,
        CROUCH,
        PRONE
    };

    public PLAYERSTATES currState;

}
