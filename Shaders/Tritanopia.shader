Shader "Hidden/Tritanopia" {
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
				return fixed4(
					col.r * 0.95 + col.g * 0.05, 
					col.g * 0.433 + col.b * 0.567, 
					col.g * 0.475 + col.b * 0.525,
					col.a);
			}

			ENDCG
		}
	}
}