Shader "Custom/UnlitOffsetShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset ("Offset", Vector) = (0,0,0,0)
        _ScrollSpeed ("Scroll Speed", Float) = 0.1 
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 100
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // 투명도 지원
            ZWrite Off // Z 버퍼 비활성화 (투명한 객체용)
            Cull Back // 후면 컬링

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float2 _Offset;
            float _ScrollSpeed;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                _Offset.x = _Time.y * _ScrollSpeed;
                o.uv = v.uv + _Offset.xy; // 여기에서 Offset 적용
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}