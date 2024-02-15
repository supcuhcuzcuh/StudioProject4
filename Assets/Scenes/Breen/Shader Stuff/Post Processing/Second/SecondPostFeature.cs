using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SecondPostFeature : ScriptableRendererFeature
{
    private SecondPass secondPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(secondPass);
    }

    public override void Create()
    {
        secondPass = new SecondPass();
    }

    class SecondPass : ScriptableRenderPass
    {
        private Material mat;
        int tintId = Shader.PropertyToID("_Temp");
        private RenderTargetIdentifier src, tint;

        public SecondPass()
        {
            if (!mat)
                mat = CoreUtils.CreateEngineMaterial("CustomPost/SecondPost");

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
            CommandBuffer commandBuffer = CommandBufferPool.Get("SecondFeature");
            VolumeStack volume = VolumeManager.instance.stack;

            SecondPost secondPost = volume.GetComponent<SecondPost>();

            if (secondPost.IsActive())
            {
                mat.SetFloat("pixelSize", (float)secondPost.pixelSize);
                mat.SetFloat("screenWidth", Screen.width);
                mat.SetFloat("screenHeight", Screen.height);

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
