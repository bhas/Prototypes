Shader "Custom/Depth"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM 
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float2 depth : TEXCOORD0;
			};
			
			v2f vert (float4 vertex : POSITION)
			{ 
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				COMPUTE_EYEDEPTH(o.depth);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//DECODE_EYEDEPTH(i) / LinearEyeDepth(i.depth) :
				return fixed4(1, 0, 0, 1);
			}
			ENDCG
		}
	}
}
