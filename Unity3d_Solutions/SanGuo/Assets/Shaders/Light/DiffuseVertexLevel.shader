// 逐顶点漫反射
Shader "Custom/Light/DiffuseVertexLevel" {
	Properties {
		_Diffuse("Diffuse", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Pass {
			Name "DiffuseVertex"
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#include "Lighting.cginc"

			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Diffuse;

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				fixed3 color : COLOR;
			};
			
			// 计算顶点
			v2f vert(a2v v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				fixed lambert = saturate(dot(worldNormal, worldLightDir));
				
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * lambert;
				
				o.color = ambient + diffuse;
				
				return o;
			}

			// 计算片元
			fixed4  frag(v2f i) : SV_Target {
				return fixed4(i.color, 1.0);
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}

