Shader "Blender/BillboardGrass"
{
	Properties
	{
		_BaseColor("BaseColor", Color) = (1,1,1,1)
		_TipColor("Tip Color", Color) = (0,0,0,1)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		//LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// ========================= Data object ===========================

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				fixed4 color : SV_Target;
			};

			fixed4 _BaseColor;
			fixed4 _TipColor;

			// ======================== Helper functions =========================

			float getAnimFactor(float4 pos)
			{
				// make each animation have a slight semi-random offset
				float offset = pos.x * 5.93 + pos.z * 85.7;
				float t = sin(_Time.y + offset); // Time (t/20, t, t*2, t*3)
				return lerp(-0.01, 0.01, t);
			}

			// ======================== Shader functions =========================

			v2f vert(appdata v)
			{
				v2f o;

				if (v.uv.x > 0) {
					// grass tip
					float4 viewPos = mul(UNITY_MATRIX_MV, v.vertex);
					viewPos += float4(getAnimFactor(v.vertex), 0, 0, 0); // animate tip
					o.position = mul(UNITY_MATRIX_P, viewPos);
					o.color = _TipColor;
				}
				else {
					// grass base
					o.position = UnityObjectToClipPos(v.vertex);
					o.color = _BaseColor;
				}
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//return fixed4(1,0,0,1);
				return i.color;
			}
		ENDCG
	}
	}
}
