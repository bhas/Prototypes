Shader "Isometric/Block"
{
	Properties
	{

	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert

		// ========================= Data object ===========================

		struct Input {
			float4 color : COLOR;
		};

		// ========================= Helper Functions ===========================

		// ========================= Shader Functions ===========================

		void surf(Input IN, inout SurfaceOutput o)
		{
			o.Albedo = 1;
		}

		ENDCG
	}
}