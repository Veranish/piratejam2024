Shader "Custom/HashedBlendShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Space(50)]
        _MainTex ("Albedo (RGB), Smooth (A)", 2D) = "white" {}
        [NoScaleOffset]
        _MainNormalHeightTex ("Normal (RGB), Height (A)", 2D) = "grey" {}
        [NoScaleOffset]

        [Space(50)]
        _SecondaryTex ("Secondary Albedo (RGB), Smooth (A)", 2D) = "white" {}
        [NoScaleOffset]
        _SecondaryNormalHeightTex ("Secondary Normal (RGB), Height (A)", 2D) = "grey" {}
        _SecondaryTextureTiling ("Secondary Texture Tiling", float) = 1
        _SecondaryHorizontalEdgeFactor ("Edge Coord Range", float) = 0.5
        _SecondaryHorizonalOffset ("Edge Coord Offset", float) = 0.5

        [Space(50)]
        _NoiseScale ("Noise Scale", float) = 1
        _NoiseAmount ("Noise Amount", float) = 0.5
        _EdgePower ("Edge Power", float) = 2
        _HeightInfluence ("Secondary Height Influence", float) = 1
        _BlendRange ("Blending Range", Range(0, 10)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200


        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #include "Noise/noises.cginc"
        #include "Noise/Simplexnoise2D.hlsl"

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;//color + height
        sampler2D _MainNormalHeightTex;
        sampler2D _SecondaryTex;
        sampler2D _SecondaryNormalHeightTex;

        half _Metallic;
       
        half _SecondaryHorizontalEdgeFactor;
        float _SecondaryHorizonalOffset;
      
        half _HeightInfluence;
        half _BlendRange;
        fixed4 _Color;
        half _NoiseScale;
        half _NoiseAmount;
        half _EdgePower;
        float _SecondaryTextureTiling;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondaryTex;
            fixed3 Normal;  // tangent space normal
            float3 worldPos : TEXCOORD0; // World position
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // // Albedo comes from a texture tinted by color
            fixed4 ColorSmooth1 = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 NormalHeight1 = tex2D(_MainNormalHeightTex, IN.uv_MainTex);

            fixed4 ColorSmooth2 = tex2D (_SecondaryTex, IN.worldPos.xz*_SecondaryTextureTiling);
            fixed4 NormalHeight2 = tex2D(_SecondaryNormalHeightTex, IN.worldPos.xz*_SecondaryTextureTiling);

            float noiseValue = clamp(SimplexNoiseGrad(IN.worldPos.xz * _NoiseScale), -1, 1)*_NoiseAmount;

            float secondaryHeightInfluence = NormalHeight2.a * _HeightInfluence + noiseValue;

            float heightComparison = smoothstep(
                secondaryHeightInfluence-_BlendRange, 
                secondaryHeightInfluence+_BlendRange, 
                NormalHeight1.a
            );

            float blendValue;
            blendValue = pow(
                abs(IN.uv_MainTex.x - 0.5 )*2* 
                _SecondaryHorizontalEdgeFactor, 
                _EdgePower
            );
            blendValue = clamp(blendValue + heightComparison * noiseValue, 0, 1);

            o.Normal = lerp(NormalHeight1.rgb, NormalHeight2.rgb, blendValue);

            o.Metallic = _Metallic;
            o.Smoothness = (1 - lerp(ColorSmooth1.a, ColorSmooth2.a, blendValue));
            o.Albedo = _Color * lerp(ColorSmooth1.rgb, ColorSmooth2.rgb, blendValue);
            // o.Albedo = float3(blendValue, blendValue, blendValue);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
