Shader "Custom/Texture/GrayTexture" {
	Properties {
		_MainTex("Main Tex", 2D) = "white" {}
		_Color("Color Tint", Color) = (1.0,1.0,1.0,1.0)
	}
	SubShader {
		Pass {
			Name "Gray"

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;

			struct a2v {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;
			}


			fixed4 frag(v2f i) : SV_Target {
				fixed3 color = tex2D(_MainTex, i.uv) * _Color;
				float c = 0.299 * color.r + 0.587 * color.g + 0.184 * color.b;
				color.r = color.g = color.b = c;
				return fixed4(color, 1.0);
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}

