Shader "Custom/Depth Visualizer" {
	Properties{
		[NoScaleOffset]
		_MyTex("Render Input", 2D) = "black" {}
	}
	SubShader 
	{
		Cull Off 
		ZWrite Off 
		ZTest Always
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MyTex;

			float4 frag(v2f_img IN) : COLOR
			{
				float d = tex2D(_MyTex, IN.uv).r;
				return float4(d, d, d, 1);
			}
			ENDCG
		}
	}
}