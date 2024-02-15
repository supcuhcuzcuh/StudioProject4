Shader "CustomPost/SecondPost"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}

		SubShader
		{
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				HLSLPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					half4 vertex : POSITION;
					half2 uv : TEXCOORD0;
				};

				struct v2f
				{
					half2 uv : TEXCOORD0;
					half4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.uv;

					return o;
				}

				sampler2D _MainTex;
				float pixelSize;
				float screenWidth;
				float screenHeight;

				fixed4 frag(v2f i) : SV_Target
				{
					float4 result = tex2D(_MainTex, i.uv);
					float x = pixelSize * (1 / screenWidth);
					float y = pixelSize * (1 / screenHeight);
					float2 coord = float2(x * floor(i.uv.x / x), y * floor(i.uv.y / y));
					result = tex2D(_MainTex, coord);

					return result;
				}

				ENDHLSL
			}
		}
}
