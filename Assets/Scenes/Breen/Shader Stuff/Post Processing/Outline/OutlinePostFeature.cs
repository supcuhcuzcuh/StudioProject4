using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class OutlinePostFeature : ScriptableRendererFeature
{
    private OutlinePass outlinePass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(outlinePass);
    }

    public override void Create()
    {
        outlinePass = new OutlinePass();
    }

    class OutlinePass : ScriptableRenderPass
    {
        private Material mat;
        int tintId = Shader.PropertyToID("_Temp");
        private RenderTargetIdentifier src, tint;

        public OutlinePass()
        {
            if (!mat)
                mat = CoreUtils.CreateEngineMaterial("CustomPost/OutlinePost");

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
            CommandBuffer commandBuffer = CommandBufferPool.Get("OutlineFeature");
            VolumeStack volume = VolumeManager.instance.stack;

            OutlinePost outlinePost = volume.GetComponent<OutlinePost>();

            mat.SetFloat("_Delta", (float)outlinePost.delta);
            mat.SetFloat("_Foggy", (float)outlinePost.foggy);

            Blit(commandBuffer, src, tint, mat, 0);
            Blit(commandBuffer, tint, src);

            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            base.OnCameraCleanup(cmd);
        }
    }
}