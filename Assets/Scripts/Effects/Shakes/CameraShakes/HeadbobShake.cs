using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbobShake : MonoBehaviour, IShake//, IShootResponse
{
    [SerializeField]
    private float shakeDuration = 1;
    [SerializeField]
    private float frequency = 0.5f;

    private float frequencyMultiplier = 1;

    private bool shaking = false;

    Vector3 startPosition;

    [SerializeField]
    private PlayerStats playerStats;

    public void StartShake()
    {      
        if (shaking == false)
        {
            startPosition = Camera.main.transform.localPosition;
            frequencyMultiplier = playerStats.moveSpeedMultiplier;
            shaking = true;
            StartCoroutine("PerformShake");         
        }
    }

    public IEnumerator PerformShake()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            Camera.main.transform.localPosition = new Vector3((Mathf.PerlinNoise(0, Time.time) * 2 - 1) * (frequency * frequencyMultiplier), Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
            yield return null;
            Camera.main.transform.localPosition = startPosition;
            shaking = false;
        }
    }

    public void EndShake()
    {
        StopCoroutine("PerformShake");
        if (shaking == true)
        {
            shaking = false;
            Camera.main.transform.localPosition = startPosition;
        }
    }

    public void OnMouse1()
    {
        EndShake();
    }

    public void OnMouse2()
    {

    }

}
