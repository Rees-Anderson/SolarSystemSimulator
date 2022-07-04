Shader "Unlit/AndersonCombinedShader"
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
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 vertexWC : TEXCOORD3;
			};


			float4 LightPosition;

			float4x4 MyXformMat;  // our own transform matrix!!

			sampler2D _MainTex;
			float4 _MainTex_ST;  // must define to support TRANSFORM_TEX

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!

				o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);   // WHAT IS THIS DOING?

				o.vertexWC = mul(MyXformMat, v.vertex);
				//o.vertexWC = mul(UNITY_MATRIX_M, v.vertex); // this is in WC space!

				// deals with normal
				float3 p = v.vertex + v.normal;
				p = mul(MyXformMat, float4(p, 1));  // now in WC space
				o.normal = normalize(p - o.vertexWC); // NOTE: this is in the world space

				//o.normal = mul(transpose(unity_WorldToObject), v.normal);

				return o;
			}

			// our own function
			fixed4 ComputeDiffuse(v2f i) {
				float3 l = normalize(LightPosition - i.vertexWC);
				return clamp(dot(i.normal, l), 0, 1);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				float diff = ComputeDiffuse(i);
				return col * diff;
			}


			ENDCG
		}
	}
}
