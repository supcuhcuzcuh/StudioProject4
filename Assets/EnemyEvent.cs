using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private bool triggered = false;

    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(triggered == false)
        {
            foreach(GameObject spawnPoint in spawnPoints)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }
}
