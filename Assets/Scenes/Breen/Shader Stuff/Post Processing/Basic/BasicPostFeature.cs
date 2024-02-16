using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class BasicPostFeature : ScriptableRendererFeature
{
    private BasicPass basicPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(basicPass);
    }

    public override void Create()
    {
        basicPass = new BasicPass();
    }

    class BasicPass : ScriptableRenderPass
    {
        private Material mat;
        int tintId = Shader.PropertyToID("_Temp");
        private RenderTargetIdentifier src, tint;

        public BasicPass()
        {
            if (!mat)
                mat = CoreUtils.CreateEngineMaterial("CustomPost/BasicPost");

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
            CommandBuffer commandBuffer = CommandBufferPool.Get("BasicFeature");
            VolumeStack volume = VolumeManager.instance.stack;

            BasicPost basicPost = volume.GetComponent<BasicPost>();

            if (basicPost.IsActive())
            {
                mat.SetFloat("thickness", (float)basicPost.thickness);

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
