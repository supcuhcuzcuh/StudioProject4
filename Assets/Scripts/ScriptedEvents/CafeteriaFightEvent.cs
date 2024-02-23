using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeteriaFightEvent : Activatable
{
    [SerializeField]
    private PlayerRespawn playerRespawn;

    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    public override void OnActivate()
    {
        playerRespawn.SetNextSpawnPoint();
        //Spawn Enemies
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
    }
}
