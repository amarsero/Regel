// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpecularShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Shininess ("Specular Power / Shininess", float) = 10

	}
	SubShader {
		Tags {"LightMode" = "ForwardBase"}
		Pass{
			CGPROGRAM


			#pragma vertex vert
			#pragma fragment frag

			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float _Shininess; 

			uniform float4 _LightColor0;

			

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;				
				float2 uv : TEXCOORD0;
			};

			struct vertexOutput
			{
						
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOOR1;
				float3 normalDir : TEXCOOR2;

			};

			vertexOutput vert (vertexInput v)
			{
				vertexOutput o;

				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				o.normalDir = normalize(mul(float4(v.normal,0.0), unity_WorldToObject).xyz);
				o.uv = v.uv;


				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 frag (vertexOutput i) : COLOR
			{
				float3 normalDirection = i.normalDir;
				float3 viewDirection =  normalize ( _WorldSpaceCameraPos.xyz - i.posWorld.xyz) ;
				float3 lightDirection;
				float atten = 1.0;



				lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float3 diffuseReflection = atten * _LightColor0.xyz * max (0.0 , dot (normalDirection, lightDirection)) ;
				float3 specularReflection = atten * _SpecColor.rgb * max (0.0 , dot (normalDirection, lightDirection)) * pow(max (0.0 ,dot ( reflect( -lightDirection, normalDirection) , viewDirection)) ,_Shininess);
				float3 lightFinal = diffuseReflection + specularReflection + UNITY_LIGHTMODEL_AMBIENT;

				if (i.uv.x < 0.01 | i.uv.y < 0.01 | i.uv.x > 0.99 | i.uv.y > 0.99  ){
					return float4(lightFinal*0.9,1.0);
					}
				

				return float4(lightFinal * _Color.rgb,1.0);
			}

			ENDCG
		}



	}
	//FallBack "Diffuse"
}
