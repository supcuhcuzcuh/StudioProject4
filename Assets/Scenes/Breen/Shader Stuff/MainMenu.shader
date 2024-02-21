Shader "CustomPost/MainMenu"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float intensity;
            float timeScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // Lines

                if (sin(15.0f + _Time.y * timeScale * 3) > 0.99f ||
                    sin(15.0f + _Time.y * timeScale * 3) < 0.80f &&
                    sin(15.0f + _Time.y * timeScale * 6) > 0.793f)
                {
                    o.uv.x = 1 - o.uv.x * (1 / intensity);
                    o.uv.y = 1 - o.uv.y * (1.2 / intensity);
                }

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 result = tex2D(_MainTex, i.uv);

                // Vertical Transparent
                if (sin(i.uv.y * 15 + _Time.y * timeScale) > 0.5f)
                    return result *= 0.25f;

                // Lines
                if (i.uv.y % 0.012f < 0.003f)
                    return result *= 0.25f;

               /*  //Flicker
                if (sin(15.0f + _Time.y * timeScale * 3) > 0.95f ||
                    sin(15.0f + _Time.y * timeScale * 3) < 0.82f &&
                    sin(15.0f + _Time.y * timeScale * 6) > 0.77f)
                    return result *= 0.1f;*/

                return result;
            }

            ENDHLSL
        }
    }
}
