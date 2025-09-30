#define USE_RENDER_PASS

using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Demo_00
{
    public class MonoChromeSepiaPass : ScriptableRenderPass, IDisposable
    {
        private Material _material;
        private class PassData
        {
            public Material BlitMaterial;
            public TextureHandle SourceTexture;
        }
        
        /// <summary>
        ///     フレームバッファフェッチを使ったバージョンのRenderGraphを記録する
        /// </summary>
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (_material == null)
            {
                _material = CoreUtils.CreateEngineMaterial("RenderGraph-Tutorial/Monochrome-Sepia");
            }
            var universalResourceData = frameData.Get<UniversalResourceData>();
            var cameraColor = universalResourceData.cameraColor;
            
            // step-1 モノクロ画像の描きこみ先のテクスチャを作成する
            
            // step-2 セピア画像の描きこみ先のテクスチャを作成する
            
            // step-3 モノクロ化のパスを作成する
            
            // step-4 セピア化のパスを作成する
            
            // step-5 最終合成のパスを作成する
            
        }
        
        public void Dispose()
        {
            if (_material == null) return;
            CoreUtils.Destroy(_material);
            _material = null;
        }
    }
}
