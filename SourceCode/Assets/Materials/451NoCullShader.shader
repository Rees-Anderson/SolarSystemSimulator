// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451NoCullShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4x4 MyXformMat;  // our own transform matrix!!

			sampler2D _MainTex;
			float4 _MainTex_ST;  // must define to support TRANSFORM_TEX

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!

				o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);   // WHAT IS THIS DOING?

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				return col;
			}

			ENDCG
		}
	}
}