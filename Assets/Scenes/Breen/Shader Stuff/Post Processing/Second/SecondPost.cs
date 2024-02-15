using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/SecondPost", typeof(UniversalRenderPipeline))]
public class SecondPost : VolumeComponent, IPostProcessComponent
{
    public FloatParameter pixelSize = new FloatParameter(10);

    public bool IsActive() { return true; }
    public bool IsTileCompatible() { return true; ;  }
}
