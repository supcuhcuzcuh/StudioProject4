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

        [HideInInspector] public bool isActive = true;

        private float sizeSet;

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
            mat.SetFloat("slownessOfExpansion", (float)basicPost.slownessOfExpansion);
            mat.SetTexture("_CameraDepthTex", (Texture)basicPost.depthTex);

            if (!isActive)
            {

            }

            if (isActive)
            {
                // Reset
                if (mat.GetFloat("size") >= (float)basicPost.slownessOfExpansion)
                {
                    sizeSet = 0;
                    isActive = false;
                }
                // Expand
                else
                {
                    sizeSet += Time.deltaTime * (float)basicPost.timeScale;
                }

                mat.SetFloat("size", sizeSet);

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
