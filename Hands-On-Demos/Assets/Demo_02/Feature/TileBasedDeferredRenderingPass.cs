#define USE_RENDER_PASS

using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Demo_02
{
    public class TileBasedDeferredRenderingPass : ScriptableRenderPass, IDisposable
    {
        private static readonly ShaderTagId RenderGBufferShaderTagId = new("RenderGBuffer");
        
        private Material _deferredLightingMaterial;
        
        private class PassData
        {
            public Material BlitMaterial;
        }
        private class RenderGBufferPassData
        {
            public RendererListHandle RendererList;
        }
        /// <summary>
        ///     フレームバッファフェッチを使ったバージョンのRenderGraphを記録する
        /// </summary>
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (_deferredLightingMaterial == null)
            {
                _deferredLightingMaterial = CoreUtils.CreateEngineMaterial("RenderGraph-Tutorial/DeferredLighting");
            }
            var renderingData = frameData.Get<UniversalRenderingData>();
            var cameraData = frameData.Get<UniversalCameraData>();
            var resourceData = frameData.Get<UniversalResourceData>();
            var lightData = frameData.Get<UniversalLightData>();
            var cameraColor = resourceData.cameraColor;
            
            // G-Bufferを作成する
            var albedoTextureDesc = renderGraph.GetTextureDesc(resourceData.cameraColor);
            // アルベドテクスチャを作成
            albedoTextureDesc.colorFormat = GraphicsFormat.B8G8R8A8_UNorm;
            albedoTextureDesc.memoryless = RenderTextureMemoryless.Color;
            var albedoTextureHandle = renderGraph.CreateTexture(albedoTextureDesc);
            // 法線テクスチャを作成
            var normalTextureDesc = renderGraph.GetTextureDesc(resourceData.cameraColor);
            normalTextureDesc.colorFormat = GraphicsFormat.R8G8B8A8_UNorm;
            normalTextureDesc.memoryless = RenderTextureMemoryless.Color;
            var normalTextureHandle = renderGraph.CreateTexture(normalTextureDesc);
            
            // 不透明オブジェクトを描画するためのrendererListを作成する
            var opaqueFilteringSettings = new FilteringSettings(RenderQueueRange.opaque);
            var sortFlags = cameraData.defaultOpaqueSortFlags;
            var drawSettings =
                RenderingUtils.CreateDrawingSettings(RenderGBufferShaderTagId, renderingData, cameraData, lightData, sortFlags);
            drawSettings.perObjectData = PerObjectData.None;
            
            var rendererListParam = new RendererListParams(renderingData.cullResults, drawSettings, opaqueFilteringSettings);
            var rendererList = renderGraph.CreateRendererList(in rendererListParam);
            
            // 不透明オブジェクトをG-Bufferに描画していく
            using (var builder = renderGraph.AddRasterRenderPass<RenderGBufferPassData>("RenderGBufferPass",
                       out var passData))
            {
                passData.RendererList = rendererList;
                builder.UseRendererList(passData.RendererList);
                builder.SetRenderAttachment(albedoTextureHandle,0);
                builder.SetRenderAttachment(normalTextureHandle,1);
                builder.SetRenderAttachmentDepth(resourceData.activeDepthTexture);
                builder.SetRenderFunc(static (RenderGBufferPassData data, RasterGraphContext context) =>
                {
                    var cmd = context.cmd;
                    cmd.DrawRendererList(data.RendererList);
                });
                
            }
            
            // step-1 ディファードライティングを行う
            
        }
        
        public void Dispose()
        {
            if (_deferredLightingMaterial == null) return;
            CoreUtils.Destroy(_deferredLightingMaterial);
            _deferredLightingMaterial = null;
        }
    }
}
