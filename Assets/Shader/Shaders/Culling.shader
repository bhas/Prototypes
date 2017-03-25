Shader "Custom/Culling"
{
	Properties
	{
		[NoScaleOffset]
		_DepthTexture("Depth texture", 2D) = "black" {}
		_Ambient("Ambient", Range(0, 1)) = 0.3
		_Diffuse("Diffuse", Range(0, 1)) = 1
		_DiffuseDir("Light direction", Vector) = (0, 1, 0, 0)
	}

	SubShader
	{
		//Tags{ "RenderType" = "Transparent" }
		//Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			// ---------------------- Data structure ---------------------------
			float _Diffuse, _Ambient;
			float3 _DiffuseDir;
			sampler2D _DepthTexture;

			struct appdata
			{
				float4 vertex : POSITION;			// position in object space
				float3 normal : NORMAL;				// normal in object space 
				fixed4 color : COLOR;				// vertex color
			};
			
			struct v2f
			{
				float4 vertex: SV_POSITION;			// position in clip space 
				fixed4 color : SV_Target;			// pixel color
				float depth : TEXCOORD0;			// depth of pixel
			};

			// ---------------------- Helper structure ---------------------------

			// calculates the depth of a vertex
			float getDepth(appdata v)
			{
				float d;
				COMPUTE_EYEDEPTH(d);
				return (d - _ProjectionParams.y) / (_ProjectionParams.z - _ProjectionParams.y);
			}

			// calculates the color based on the normal
			/*fixed4 getColor(float3 normal) 
			{
				float3 wn = UnityObjectToWorldNormal(n);
				float diffuse = _Diffuse * max(dot(normalize(_DiffuseDir), wn), 0);
				o.color = c * min(1, diffuse + _Ambient);
			}*/

			// ---------------------- Shader function ---------------------------
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.depth = getDepth(v);
				float3 wn = UnityObjectToWorldNormal(v.normal);
				float diffuse = _Diffuse * max(dot(normalize(_DiffuseDir), wn), 0);
				o.color = v.color * min(1, diffuse + _Ambient);
				return o;
			}

			// fragment shader
			fixed4 frag(v2f i) : SV_Target 
			{
				// actual depth (0 = near, 1 = far)
				float depth = i.depth;

				// get 2D pixel position in range (0,0) top-left to (1,1) bottom-right
				float2 pixelPos = i.position.xy / _ScreenParams.xy;
				pixelPos.y = 1 - pixelPos.y;

				// depth of environment mesh (from depth texture)
				float envDepth = tex2D(_DepthTexture, pixelPos).r;

				// ignore pixel if pixel is behind the environment mesh
				//clip(depth - envDepth - 0.001);

				if (envDepth < depth)
					return fixed4(envDepth, 0, 0, 1);
				else
					return i.color;
			}
			ENDCG
		}
	}
}
