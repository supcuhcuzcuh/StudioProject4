using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealhui : MonoBehaviour
{
    public Image healthBarImage;
    public Entity entity;

    private void Start()
    {
        if (entity == null)
        {
            Debug.LogError("HealthBar is missing a reference to the Entity!");
            return;
        }

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();

        // Check for spacebar input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            entity.OnDamaged(10f);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null && entity != null)
        {
            float fillAmount = entity.GetHealth() / 100;
            healthBarImage.fillAmount = fillAmount;
        }
    }
}
