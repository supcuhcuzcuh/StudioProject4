Shader "CustomPost/BasicPost"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _DistortTexture ("Distort Texture", 2D) = "white" {}
        _Color ("Color", color) = (1,1,1,1)
        _Intensity ("Intensity", float) = 1
        _Ramp ("Ramp", float) = 1
        _GlowThickness ("Glow Thickness", float) = 1
        _GlowIntensity ("Glow Intensity", float) = 1
        _DistortThickness ("Distort Thickness", float) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _DistortTexture;
            float4 _MainTex_ST;

            float4 _Color;
            float _Intensity, _Ramp, _DistortThickness, _GlowThickness, _GlowIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 mainTex = tex2D(_MainTex, i.uv);
                float luminance = Luminance(mainTex);
                float3 mainColor = luminance * _Color;
                mainColor = pow(mainColor, _Ramp) * _Intensity;

                float3 distortTex = tex2D(_DistortTexture, i.uv);
                float distortMask = abs(sin(distortTex.r * 10 + _Time.y));
                float distortStep = step(distortMask, _DistortThickness);

                float glow = 1 - smoothstep(distortMask - _GlowThickness, distortMask + _GlowThickness, _DistortThickness);
                glow *= _GlowIntensity;
                return fixed4( mainColor + glow , distortStep);
            }
            ENDCG
        }
    }
}