using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    [SerializeField] private BasicPostFeature basicPostFeature;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            basicPostFeature.basicPass.Trigger();
        }
    }
}
