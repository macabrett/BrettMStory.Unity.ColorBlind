Shader "Hidden/BlueCone" {
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
				fixed commonRed = col.r * 0.163;
				fixed commonGreen = col.g * 0.320;
				fixed commonBlue = col.b * 0.062;
				return fixed4(
					col.r * 0.618 + commonGreen + commonBlue,
					commonRed + col.g * 0.775 + commonBlue,
					commonRed + commonGreen + col.b * 0.616, 
					col.a);
			}

			ENDCG
		}
	}
}
