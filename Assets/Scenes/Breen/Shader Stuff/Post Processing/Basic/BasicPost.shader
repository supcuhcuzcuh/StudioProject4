Shader "CustomPost/BasicPost"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        size ("Size", float) = 1
        slownessOfExpansion ("Slowness Of Expansion", float) = 10
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float size;
            float slownessOfExpansion;

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 screenSpace : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Depth
                o.screenSpace = ComputeScreenPos(o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                 float4 mainTex = tex2D(_MainTex, i.uv);
                
                // Depth
                float2 screenSpaceUV = i.screenSpace.xy / i.screenSpace.w;
                float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenSpaceUV));

                float dist = distance(i.uv, float2(0.5, 0.5));

                // Inside the circle if the distance is less than the radius
                if (dist < size * depth)
                {
                    return float4(mainTex);
                }
                else if (size != 0)
                {
                    return float4(0, 0, 0, 1);
                }

                return float4(mainTex);
            }

            ENDHLSL
        }
    }
}