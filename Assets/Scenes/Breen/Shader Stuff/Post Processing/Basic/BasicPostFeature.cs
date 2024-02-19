using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class BasicPostFeature : ScriptableRendererFeature
{
    [HideInInspector] public BasicPass basicPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(basicPass);
    }

    public override void Create()
    {
        basicPass = new BasicPass();
    }

    public void Trigger()
    {
        basicPass.isActive = true;
    }

    public class BasicPass : ScriptableRenderPass
    {
        private Material mat;
        int tintId = Shader.PropertyToID("_Temp");
        private RenderTargetIdentifier src, tint;

        [HideInInspector] public bool isActive = false;

        private float sizeSet;
        private bool isInit = false;

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

            if (!isInit)
            {
                sizeSet = (float)basicPost.initialCircleSize;
                isInit = true;
            }

            if (!isActive)
            {
                sizeSet = (float)basicPost.initialCircleSize;

                mat.SetFloat("size", sizeSet);
            }

            if (isActive)
            {
                // Reset
                if (mat.GetFloat("size") <= 0)
                {
                    sizeSet = (float)basicPost.initialCircleSize;
                    isActive = false;
                }
                // Expand
                else
                {
                    sizeSet -= (Time.deltaTime * (float)basicPost.timeScale);
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