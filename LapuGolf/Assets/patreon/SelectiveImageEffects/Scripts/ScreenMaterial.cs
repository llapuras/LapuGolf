using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class ScreenMaterial : MonoBehaviour
{
    public DepthTextureMode depthMode = DepthTextureMode.None;

    //AfterForwardAlpha will work for most effects
    public CameraEvent RenderOrder = CameraEvent.AfterForwardAlpha;
    public Material PostprocessMaterial;
    
    private CameraEvent tempCE;
    private CommandBuffer commandBuffer;
    private Camera m_Camera;
    private Camera Cam
        
    {
        get
        {
            if (m_Camera == null)
            {
                m_Camera = GetComponent<Camera>();
            }
            return m_Camera;
        }
    }

    //Update settings when edited
    private void OnValidate()
    {
        Cam.depthTextureMode = depthMode;
       

        if ((RenderOrder != tempCE && commandBuffer != null))
        {
            Cam.RemoveCommandBuffer(tempCE, commandBuffer);
            Cam.AddCommandBuffer(RenderOrder, commandBuffer);
            tempCE = RenderOrder;
        }
        else if (RenderOrder != tempCE && PostprocessMaterial != null)
            OnEnable();
    }

    private void OnEnable()
    {

        
        if (commandBuffer == null && PostprocessMaterial != null)
        {
            commandBuffer = new CommandBuffer();
            commandBuffer.name = "commandBuffer";

            int StencRT = Shader.PropertyToID("_Temp2");
            commandBuffer.GetTemporaryRT(StencRT, -1, -1, 0);

            commandBuffer.SetRenderTarget(color: StencRT, depth: StencRT);
            commandBuffer.ClearRenderTarget(false, true, Color.clear);
            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, StencRT);
            commandBuffer.SetGlobalTexture("_MainTex", StencRT);
          

            commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, BuiltinRenderTextureType.CameraTarget, PostprocessMaterial);

            Cam.AddCommandBuffer(RenderOrder, commandBuffer);
        }
    }

    private void OnDisable()
    {
        if (commandBuffer != null)
        {
            Cam.RemoveCommandBuffer(RenderOrder, commandBuffer);
            commandBuffer = null;
        }
    }
}