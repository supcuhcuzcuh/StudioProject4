using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardEnemy : BaseEnemy
{
    [SerializeField] private TMPro.TMP_Text healthText;
    private void Update()
    {
        if (health <= 0)
        {
            SetHealth(0);

        }
        if (healthText != null)
        {
            healthText.text = "Guard Health: " + health;
        }
    }

    void TakeHit()
    {
        hitsToDie -= 1;
        if (hitsToDie <= 0)
        {
            Debug.Log("ENEMY IS DEAD");
        }
    }
}
