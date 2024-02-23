using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] List<GameObject> respawnPoints = new List<GameObject>();
    private int spawnIndex = 0;
    [SerializeField]
    private GameObject player;

    public void Respawn()
    {
        player.transform.position = respawnPoints[spawnIndex].transform.position;
        player.GetComponent<FPSControls>().SetHealth(player.GetComponent<FPSControls>().GetMaxHealth());
    }

    public void SetNextSpawnPoint()
    {
        spawnIndex += 1;
    }
}
