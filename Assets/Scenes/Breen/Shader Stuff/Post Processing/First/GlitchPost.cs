using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/GlitchPost", typeof(UniversalRenderPipeline))]
public class GlitchPost : VolumeComponent, IPostProcessComponent
{
    public FloatParameter intensity = new FloatParameter(1);
    public FloatParameter timeScale = new FloatParameter(1);

    public bool IsActive() { return true; }
    public bool IsTileCompatible() { return true; ;  }
}
