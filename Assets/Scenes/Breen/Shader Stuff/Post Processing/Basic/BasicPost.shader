Shader "CustomPost/BasicPost"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        cameraDepthTex ("Camera Depth Texture", 2D) = "gray" {}
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
            sampler2D _CameraDepthTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float depth : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Getting depth
                float2 screenUVs = o.vertex.xy / o.vertex.w;
                float zRaw = tex2Dproj(_CameraDepthTex, UNITY_PROJ_COORD(screenUVs)).r;
                o.depth = zRaw;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                 float4 mainTex = tex2D(_MainTex, i.uv);

                float dist = distance(i.uv, float2(0.5, 0.5));
                
                // Example: darken the main texture based on depth
                float darknessFactor = 1 - i.depth; // Adjust as needed
                mainTex *= darknessFactor;

                // Inside the circle if the distance is less than the radius
                if (dist < size)
                {
                    return float4(mainTex * (size / slownessOfExpansion));
                }
                else if (size != 0)
                {
                    return float4(mainTex * 0.01 * (size / slownessOfExpansion));
                }

                return float4(mainTex);
            }

            ENDHLSL
        }
    }
}