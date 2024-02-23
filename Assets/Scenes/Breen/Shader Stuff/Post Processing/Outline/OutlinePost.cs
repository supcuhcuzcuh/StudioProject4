using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenuForRenderPipeline("Custom/OutlinePost", typeof(UniversalRenderPipeline))]
public class OutlinePost : VolumeComponent
{
    [SerializeField] public FloatParameter delta = new FloatParameter(0.002f);
    [SerializeField] public FloatParameter foggy = new FloatParameter(100);

    public bool IsActive() { return true; }
    public bool IsTileCompatible() { return true; ; }
}
