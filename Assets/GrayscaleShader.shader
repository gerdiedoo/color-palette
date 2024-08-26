Shader "Custom/GrayscaleShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Intensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                return half4(lerp(col.rgb, gray.xxx, _Intensity), col.a);
            }
            ENDCG
        }
    }
}
