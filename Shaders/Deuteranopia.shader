Shader "Hidden/Deuteranopia" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader {
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed commonGreen = col.g * 0.3;
				return fixed4(
					col.r * 0.625 + col.g * 0.375, 
					col.r * 0.7 + commonGreen, 
					commonGreen + col.b * 0.7, col.a);
			}

			ENDCG
		}
	}
}