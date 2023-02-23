// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Normal Map" {
	Properties{
		_Diffuse("Diffuse", Color) = (1, 1, 1, 1)
		_Specular("Specular", Color) = (1, 1, 1, 1)
		_Gloss("Gloss", Range(8, 256)) = 20
		_MainTex("Main Tex", 2D) = "white" {}

		_BumpMap("Normal Map", 2D) = "bump" {}	//法线图
		_BumpScale("Bump Scale", Float) = 1.0
	}
		SubShader{
			Pass {
				Tags { "LightMode" = "ForwardBase" }

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#include "Lighting.cginc"

				fixed4 _Diffuse;
				fixed4 _Specular;
				float _Gloss;
				sampler2D _MainTex;
				float4 _MainTex_ST; //必须用纹理名+_ST来表示纹理属性(ST = scale + translation)

				sampler2D _BumpMap;
				float4 _BumpMap_ST;
				float _BumpScale;

				struct a2v {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 tangent : TANGENT; //切线
					float4 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float3 viewDir : TEXCOORD0;//切线坐标空间下的方向
					float3 lightDir : TEXCOORD1;
					float4 uv : TEXCOORD2;
				};

				v2f vert(a2v v) {
					v2f o;
					// Transform the vertex from object space to projection space
					o.pos = UnityObjectToClipPos(v.vertex);

					// Transform the normal from object space to world space

					o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;//缩放&偏移, _MainTex_ST来自图片de属性
					o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;
					//副法线（切线空间的一个基)
					float3 binormal = cross(normalize(v.normal), normalize(v.tangent.xyz)) * v.tangent.w;
					//物体空间到切线空间的变换矩阵
					float3x3 rotation = float3x3(v.tangent.xyz, binormal, v.normal); //TANGENT_SPACE_ROTATION;

					o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex)).xyz;
					o.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex)).xyz;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target {
					fixed3 tangentLightDir = normalize(i.lightDir);
					fixed3 tangentViewDir = normalize(i.viewDir);

					// Get the texel in the normal map
					fixed4 packedNormal = tex2D(_BumpMap, i.uv.zw);
					fixed3 tangentNormal;
					tangentNormal = UnpackNormal(packedNormal);
					tangentNormal.xy *= _BumpScale;
					tangentNormal.z = sqrt(1.0 - saturate(dot(tangentNormal.xy, tangentNormal.xy)));


					fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Diffuse.rgb;

					// Get ambient term
					fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo / 2; // /2 is wojiade

					// Compute diffuse term
					fixed3 diffuse = _LightColor0.rgb * albedo * saturate(dot(tangentNormal, tangentLightDir));

					fixed3 halfDir = normalize(tangentLightDir + tangentViewDir);
					fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(tangentNormal, halfDir)), _Gloss);

					fixed3 color = ambient + diffuse + specular;

					return fixed4(color, 1.0);
			}

			ENDCG
		}
	}
		FallBack "Diffuse"
}
