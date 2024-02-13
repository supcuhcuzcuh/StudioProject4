using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseShake : MonoBehaviour, IShake//, IShootResponse
{
    [SerializeField]
    private float shakeDuration = 1f;
    [SerializeField]
    private float frequency = 0.1f;

    private float frequencyMultiplier = 1;

    Vector3 startPosition;

    [SerializeField]
    private PlayerStats playerStats;

    private float explosionQueue = 0;

    private void Start()
    {
        startPosition = Camera.main.transform.localPosition;
    }

    public void OnMouse1()
    {
        StartShake();
    }

    public void OnMouse2()
    {
       
    }

    public void StartShake()
    {
        EndShake();
        frequencyMultiplier = explosionQueue;
        startPosition = Camera.main.transform.localPosition;
        StartCoroutine("PerformShake");
    }

    private void LateUpdate()
    {
        if(explosionQueue != 0)
        {
            StartShake();
            explosionQueue = 0;
            shakeDuration = 1f;
        }
    }

    public IEnumerator PerformShake()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            var strength = (frequency * frequencyMultiplier) * Mathf.Lerp(1, 0, elapsedTime / shakeDuration);
            Camera.main.transform.localPosition = new Vector3(
                startPosition.x + Random.Range(-1, 1) * strength,
                startPosition.y + Random.Range(-1, 1) * strength,
                startPosition.z + Random.Range(-1, 1) * strength
            );
            yield return null;
            Camera.main.transform.localPosition = startPosition;
        }
    }

    public void EndShake()
    {
        StopCoroutine("PerformShake");
        Camera.main.transform.localPosition = startPosition;
    }

    public void QueueExplosion(int _explosion, int _time)
    {
        if(_explosion > explosionQueue)
        {
            explosionQueue = _explosion;
            shakeDuration = _time;
        }
        
    }
}
