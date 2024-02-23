using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoorEvent : ScriptedEvent
{
    [SerializeField]
    private PlayerRespawn playerRespawn;

    [SerializeField] private Activatable bossDoorPast;
    [SerializeField] private Activatable bossDoorPresent;
    [SerializeField] private GameObject boss;

    protected override void TriggerScriptedEvent(Collider col)
    {
        playerRespawn.SetNextSpawnPoint();
        bossDoorPast.OnActivate();
        bossDoorPresent.OnActivate();
        boss.SetActive(true);
    }
}
