Shader "Hidden/Protanomaly" {
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
					col.r * 0.817 + col.g * 0.183, 
					col.r * 0.333 + col.g * 0.667, 
					col.g * 0.125 + col.b * 0.875, 
					col.a);
			}

			ENDCG
		}
	}
}