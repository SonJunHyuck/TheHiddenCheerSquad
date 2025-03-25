Shader "Custom/FlashSpriteShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _FlashAmount ("Flash Amount", Range(0,1)) = 0
        _FlashColor ("Flash Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            half4 _Color;
            half4 _FlashColor;
            half _FlashAmount;
            // half4 unity_SpriteColor; // 여기서 받아온다!

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                half4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                o.color = v.color * _Color;// * unity_SpriteColor; // 핵심!
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 texColor = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                texColor.rgb = lerp(texColor.rgb, _FlashColor.rgb, _FlashAmount);
                return texColor;
            }
            ENDHLSL
        }
    }
}