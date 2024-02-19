using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenuForRenderPipeline("Custom/BasicPost", typeof(UniversalRenderPipeline))]
public class BasicPost : VolumeComponent
{
    [SerializeField] public FloatParameter timeScale = new FloatParameter(1);
    [SerializeField] public FloatParameter initialCircleSize = new FloatParameter(200);

    public bool IsActive() { return true; }
    public bool IsTileCompatible() { return true; ; }
}
