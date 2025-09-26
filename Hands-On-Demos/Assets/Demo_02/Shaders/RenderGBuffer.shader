Shader "RenderGraph-Tutorial/RenderGBuffer"
{
    Properties
    {
        // Specular vs Metallic workflow
        [MainColor] _BaseColor("Color", Color) = (1,1,1,1)
        
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
        }
        LOD 300


        Pass
        {
            Name "GBuffer"
            Tags
            {
                "LightMode" = "RenderGBuffer"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 4.5
            
            #pragma exclude_renderers gles3 glcore

            // -------------------------------------
            // Shader Stages
            #pragma vertex CustomGBufferPassVertex
            #pragma fragment CustomGBufferPassFragment

            // -------------------------------------
            // Includes
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            CBUFFER_START(UnityPerMaterial)
            half4 _BaseColor;
            CBUFFER_END

            // 頂点バッファからの出力 and フラグメントシェーダーへの入力
            struct CustomVaryings
            {
                float4 positionCS : SV_POSITION;
                half3 normalWS : TEXCOORD0;
            };
            // フラグメントシェーダーの出力
            struct CustomFragmentOutput
            {
                half4 GBuffer0 : SV_Target0;
                half4 GBuffer1 : SV_Target1;
            };

            // 頂点シェーダー
            CustomVaryings CustomGBufferPassVertex(
                float4 positionOS : POSITION,
                float3 normalOS : NORMAL)
            {
                CustomVaryings output;
                
                output.positionCS = TransformObjectToHClip(positionOS);
                float3 normalWS = TransformObjectToWorldNormal(normalOS);
                output.normalWS = half3(normalize(normalWS));
                return output;
            }

            // フラグメントシェーダー
            CustomFragmentOutput CustomGBufferPassFragment(CustomVaryings input)
            {
                CustomFragmentOutput output;
                output.GBuffer0 = _BaseColor;
                output.GBuffer1 = half4(input.normalWS, 1);
                return output;
            }
            
            ENDHLSL
        }

    }

    FallBack "Hidden/Universal Render Pipeline/FallbackError"
    CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.LitShader"
}
