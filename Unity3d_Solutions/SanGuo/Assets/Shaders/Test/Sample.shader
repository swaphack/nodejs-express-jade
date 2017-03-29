Shader "Custom/Sample" {
	// 属性
	Properties {
		_Int("Int", Int) = 2
		_Float("Float", Float)=1.5
		_Range("Range", Range(0.0, 5.0)) = 3.0
		_Color ("Main Color", Color) = (1,1,1,1)
		_Vector("Vector", Vector) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cube("Cube", Cube) = "white" {}
		_MainModel("Base (Model)", 3D) = "black" {}
	}
	SubShader {
		UsePass "Self-Illumin/VertexLit/BASE"
		UsePass "Bumped Diffuse/PPL"

		// 状态
		Cull Front
		ZTest GEqual
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha

		// 标签
		Tags {
			"Queue" = "Transparent"
			"RenderType" = "Opaque"
			"DisableBatching" = "True"
			"ForceNoShadowCasting" = "True"
			"IgnoreProjector" = "True"
			"CanUseSpriteAtlas" = "False"
			"PreviewType" = "Plane"
		}

		Pass {
			Name "SamplePass"

			// 状态
			Cull Front
			ZTest GEqual
			ZWrite On
			Blend SrcAlpha OneMinusSrcAlpha

			// 标签
			Tags {
				"LightMode" = "ForwardBase"
				"RequireOptions" = "SoftVegetation"
			}
		}

		GrabPass {

		}
	} 

	subShader {

		// 表面着色器
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float4 color : COLOR;
		};

		void surf(Input input, inout SurfaceOutput output) {
			output.Albedo = 1;
		}

		ENDCG

		Pass {
			// 顶点/片元着色器
			CGPROGRAM

			// 有3种基本数值类型：float、half和fixed。
			// 这3种基本数值类型可以再组成vector和matrix，比如half3是由3个half组成、float4x4是由16个float组成。


			// float：32位高精度浮点数。
			// half：16位中精度浮点数。范围是[-6万, +6万]，能精确到十进制的小数点后3.3位。
			// fixed：11位低精度浮点数。范围是[-2, 2]，精度是1/256。(这个具体精度有待确认，目前没找到可靠的解释，还有说cg中fixed是12位定点数，被所有fragment profile支持)
			// 数据类型影响性能
			// 精度够用就好。
			// 颜色和单位向量，使用fixed
			// 其他情况，尽量使用half（即范围在[-6万, +6万]内、精确到小数点后3.3位）；否则才使用float。
			#pragma vertex vert
			#pragma fragment frag

			float4 vert(float4 v : POSITION) : SV_POSITION {
				return mul(UNITY_MATRIX_MVP, v);
			}

			fixed4 frag() : SV_Target {
				return fixed4(1.0, 0.0, 0.0, 1.0);
			}

			ENDCG
		}
	}

	// 如果上面都不执行，调用这个
	FallBack "Diffuse"
}

