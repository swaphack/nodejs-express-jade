Shader "Custom/Simple" {
	Properties {
		// 属性
		_BorderWidth("BorderWidth", Range(0.000,1.000)) = 0.000
		_Color ("Color", Color) = (1,1,1,1)
		_Texture("Texure", 2D) = "" {}
		_Cube("Cube", Cube) = "white" {}
		_Model("3D", 3D) = "black" {}
	}

	SubShader {
		// 显卡A使用的子着色器

		// 可选
		Tags {}

		Pass {
			// 顶点/片元着色器
		}
	}

	SubShader {
		// 显卡B使用的子着色器
		Tags {"RenderType" = "Opaque"}

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float4 color : color;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = 1;
		}

		ENDCG

		Pass {
			Name "TestSubPass1"

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			struct a2v {
				// 语义： POSITION, TANGENT, NORMAL, TEXCOORD0, TEXCOORD1, TEXCOORD2, TEXCOORD3,COLOR
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				fixed3 color : COLOR0;
			};

			v2f vert(a2v v):SV_POSITION {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				fixed3 c = i.color;
				c *= _Color.rgb;
				return fixed4(c, 1.0);
			}

			ENDCG

		}
	}


	Fallback "VertexLit"
}