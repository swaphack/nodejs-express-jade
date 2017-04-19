Shader "Custom/Texture/NormalMapWorldSpace" {
	Properties {
		_MainTex("Main Tex", 2D) = "white" {}
		_BumpTex("Bump Tex", 2D) = "bump" {}
		_Color("Color Tint", Color) = (1.0,1.0,1.0,1.0)
		_BumpScale("Bump Scale", Range(0,1.0)) = 0.8
		_Specular("Specular", Color) = (1.0,1.0,1.0,1.0)
		_Gloss("Gloss", Range(8,256)) = 20
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase"}

			CGPROGRAM
			#include "Lighting.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _BumpTex;
			float4 _BumpTex_ST;
			fixed4 _Color;
			float _BumpScale;
			fixed4 _Specular;
			float _Gloss;


			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				float4 T2W0 : TEXCOORD1;
				float4 T2W1 : TEXCOORD2;
				float4 T2W2 : TEXCOORD3;
			};

			v2f vert(a2v v)	{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord, _BumpTex);

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 worldNormal = UnityObjectToWorldNormal(v.normal).xyz;
				float3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz).xyz;
				float3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;

				o.T2W0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
				o.T2W1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
				o.T2W2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				float3 worldPos = float3(i.T2W0.w, i.T2W1.w, i.T2W2.w);

				fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				
				fixed3 bump = UnpackNormal(tex2D(_BumpTex, i.uv.zw));
				bump.xy *= _BumpScale;
				bump.z = sqrt(1.0 - saturate(dot(bump.xy, bump.xy)));
				bump = normalize(half3(dot(i.T2W0.xyz, bump), dot(i.T2W1.xyz, bump), dot(i.T2W2.xyz, bump)));


				fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
				fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(bump, lightDir));
				fixed3 halfDir = normalize(lightDir + viewDir);
				fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(bump, halfDir)), _Gloss);

				return fixed4(ambient + diffuse + specular, 1.0);
			}


			ENDCG
		}
	} 
	FallBack "Specular"
}

