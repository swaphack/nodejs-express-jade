// 半兰伯特漫反射
Shader "Custom/Light/DiffuseHalfLambert" {
	Properties {
		_Diffuse("Diffuse", Color) = (1.0, 1.0, 1.0, 1.0)
		_Alpha("Alpha", Range(0.0,1.0)) = 0.5
		_Beta("Beta", Range(0.0,1.0)) = 0.5
	}
	SubShader {
		Pass {
			Name "HalfLambert"
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#include "Lighting.cginc"

			#pragma vertex vert
			#pragma fragment frag

			fixed4 _Diffuse;
			fixed _Alpha;
			fixed _Beta;

			struct a2v {
				float4 pos : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
			};

			v2f vert(a2v v) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.pos);
				//o.worldNormal = mul(v.normal, (fixed3x3)unity_WorldToObject);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				fixed halfLambert = _Alpha * dot(worldNormal, worldLightDir) + _Beta;
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * halfLambert;

				fixed3 color = ambient + diffuse;

				return fixed4(color, 1.0);
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}

