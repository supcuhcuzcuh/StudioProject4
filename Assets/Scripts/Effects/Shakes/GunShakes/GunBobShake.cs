using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBobShake : MonoBehaviour, IShake//, IShootResponse
{

    [SerializeField]
    private float shakeDuration = 1;
    [SerializeField]
    private float frequency = 0.5f;
    [SerializeField]
    private GameObject objectShake;

    private bool shaking = false;

    Vector3 startPosition;

    public void StartShake()
    {
        if (shaking == false)
        {
            startPosition = objectShake.transform.localPosition;
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
            objectShake.transform.localPosition = new Vector3(objectShake.transform.localPosition.x, ((Mathf.PerlinNoise(0, Time.time) * 2 - 2 - objectShake.transform.position.y) * frequency), objectShake.transform.localPosition.z);    //need to add the offset if the object you are shaking does not start at local pos 0
            yield return null;
            objectShake.transform.localPosition = startPosition;
            shaking = false;
        }
    }

    public void EndShake()
    {
        StopCoroutine("PerformShake");
        if (shaking == true)
        {
            shaking = false;
            objectShake.transform.localPosition = startPosition;
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
