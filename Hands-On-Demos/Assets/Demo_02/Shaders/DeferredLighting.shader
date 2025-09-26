Shader "RenderGraph-Tutorial/DeferredLighting"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZTest Always ZWrite Off Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass    // 0 
        {
            Name "Deferred Lighting"

            HLSLPROGRAM
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
				
                #pragma vertex Vert 
                #pragma fragment Frag

				// step-2  フレームバッファ入力を宣言する
                
                half4 Frag(Varyings input) : SV_Target
                {
                    // step-3 タイルメモリからアルベドと法線をロードする

                    // step-4 ハーフランバート拡散反射を計算する
                }
            
            ENDHLSL
        }
    }
}
