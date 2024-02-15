using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class BasicPostFeature : ScriptableRendererFeature
{
    BasicPostPass basicPostPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(basicPostPass);
    }

    public override void Create()
    {
        basicPostPass = new BasicPostPass();
    }
}

[System.Serializable]
public class BasicPostPass : ScriptableRenderPass
{
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
    }
}

