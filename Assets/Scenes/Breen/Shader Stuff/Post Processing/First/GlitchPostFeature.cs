using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlitchPostFeature : ScriptableRendererFeature
{
    private GlitchPass glitchPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(glitchPass);
    }

    public override void Create()
    {
        glitchPass = new GlitchPass();
    }

    class GlitchPass : ScriptableRenderPass
    {
        private Material mat;
        int tintId = Shader.PropertyToID("_Temp");
        private RenderTargetIdentifier src, tint;

        public GlitchPass()
        {
            if (!mat)
                mat = CoreUtils.CreateEngineMaterial("CustomPost/GlitchPost");

            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            src = renderingData.cameraData.renderer.cameraColorTarget;
            cmd.GetTemporaryRT(tintId, descriptor, FilterMode.Bilinear);
            tint = new RenderTargetIdentifier(tintId);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get("GlitchFeature");
            VolumeStack volume = VolumeManager.instance.stack;

            GlitchPost glitchPost = volume.GetComponent<GlitchPost>();

            if (glitchPost.IsActive())
            {
                mat.SetFloat("intensity", (float)glitchPost.intensity);
                mat.SetFloat("timeScale", (float)glitchPost.timeScale);

                Blit(commandBuffer, src, tint, mat, 0);
                Blit(commandBuffer, tint, src);
            }

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            base.OnCameraCleanup(cmd);
        }
    }
}
