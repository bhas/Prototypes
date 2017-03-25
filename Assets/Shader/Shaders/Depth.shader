Shader "Custom/Depth"
{
	SubShader
	{
		Tags { "DaluxType" = "EM" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// ---------------------- Data structure ---------------------------

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float depth : TEXCOORD0;
			};

			// ---------------------- Helper structure ---------------------------

			// calculates the depth of a vertex
			float getDepth(appdata v)
			{
				float d;
				COMPUTE_EYEDEPTH(d);
				return (d - _ProjectionParams.y) / (_ProjectionParams.z - _ProjectionParams.y);
			}

			// ---------------------- Shader function ---------------------------

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.depth = getDepth(v);
				return o; 
			}

			float4 frag(v2f i) : SV_Target
			{
				return float4(1 - i.depth, 0, 0, 1);
			}

			ENDCG
		}
	}
}
