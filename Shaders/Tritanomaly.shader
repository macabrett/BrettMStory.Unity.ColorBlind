Shader "Hidden/Tritanomaly" {
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
					col.r * 0.967 + col.g * 0.033, 
					col.g * 0.733 + col.b * 0.267, 
					col.g * 0.183 + col.b * 0.817, 
					col.a);
			}

			ENDCG
		}
	}
}