// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'

Shader "Custom/Shadow/ShadowReceive" {
	Properties {
		_Diffuse("Diffuse", Color) = (1.0,1.0,1.0,1.0)
		_Specular("Specular", Color) = (1.0,1.0,1.0,1.0)
		_Gloss("Gloss", Range(8,256)) = 20
	}
	SubShader {
		Pass {
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag 
			#pragma multi_compile_fwdbase

			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			fixed4 _Diffuse;
			fixed4 _Specular;
			float _Gloss;


			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldPos : TEXCOORD0;
				float3 worldNormal : TEXCOORD1; 
				SHADOW_COORDS(2)
			};


			v2f vert(a2v i) 
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				o.worldPos = mul(unity_ObjectToWorld, i.vertex);
				o.worldNormal = UnityObjectToWorldNormal(i.normal);
				TRANSFER_SHADOW(o);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target 
			{
				fixed3 worldPos = normalize(i.worldPos);
				fixed3 worldNormal = normalize(i.worldNormal);

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * max(0, dot(worldNormal, worldLightDir));

				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				fixed3 halfDir = normalize(viewDir  + worldLightDir);

				fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(worldNormal, halfDir)), _Gloss);

				fixed atten = SHADOW_ATTENUATION(i);

				return fixed4(ambient + (diffuse + specular) * atten, 1.0);
			}

			
			ENDCG
		}

		Pass {
			Tags { "LightMode" = "ForwardAdd" }

			Blend One One

			CGPROGRAM		

			#pragma vertex vert
			#pragma fragment frag 
			#pragma multi_compile_fwdadd

			#include "Lighting.cginc"

			fixed4 _Diffuse;
			fixed4 _Specular;
			float _Gloss;


			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldPos : TEXCOORD0;
				float3 worldNormal : TEXCOORD1; 
			};


			v2f vert(a2v i) 
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
				o.worldPos = mul(unity_ObjectToWorld, i.vertex);
				o.worldNormal = UnityObjectToWorldNormal(i.normal);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target 
			{
				fixed3 worldPos = normalize(i.worldPos); 
				fixed3 worldNormal = normalize(i.worldNormal);

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

			#ifdef USING_DIRECTIONAL_LIGHT
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
			#else
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos.xyz); 
			#endif 
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * max(0, dot(worldNormal, worldLightDir));

				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				fixed3 halfDir = normalize(viewDir  + worldLightDir);

				fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(worldNormal, halfDir)), _Gloss);

				fixed atten = 1.0;
			#ifdef USING_DIRECTIONAL_LIGHT
				atten = 1.0;
			#else
				float3 lightCoord = mul(_LightMatrix0, float4(i.worldPos, 1)).xyz;
				atten = tex2D(_LightTexture0, dot(lightCoord, lightCoord).rr).UNITY_ATTEN_CHANNEL;
			#endif
				return fixed4(ambient + (diffuse + specular) * atten, 1.0);
			}

			ENDCG
		}
	}

	Fallback "Specular"
}
