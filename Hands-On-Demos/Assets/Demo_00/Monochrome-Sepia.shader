Shader "RenderGraph-Tutorial/Monochrome-Sepia"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZTest Always ZWrite Off Cull Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass    // 0 モノクロ化
        {
            Name "Monochrome"

            HLSLPROGRAM
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
                #pragma vertex Vert 
                #pragma fragment Frag
                
                half4 Frag(Varyings input) : SV_Target
                {
                    // step-6 モノクロ化のフラグメントシェーダーの実装
                }
            
            ENDHLSL
        }
        Pass    // 1 セピア化
        {
            Name "Monochrome"

            HLSLPROGRAM
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
                #pragma vertex Vert 
                #pragma fragment Frag
                
                half4 Frag(Varyings input) : SV_Target
                {
                    // step-7 セピア化のフラグメントシェーダーの実装
                }
            
            ENDHLSL
        }
        Pass    // 2 最終合成
        {
            Name "Combine"

            HLSLPROGRAM
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
                #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
                #pragma vertex Vert 
                #pragma fragment Frag

                // step-8 利用するフレームバッファメモリの定義（SetInputAttachmentと対応する）

                // step-9 深度テクスチャの定義
                
                half4 Frag(Varyings input) : SV_Target
                {
                    // step-10 合成の処理を実装する。
                }

            ENDHLSL
        }
    }
}
