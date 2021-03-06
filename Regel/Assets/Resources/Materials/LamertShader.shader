﻿Shader "Custom/LambertShader"
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{

			Tags { "LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//User defined
			uniform float4 _Color;
			//Unity defined
			uniform float4 _LightColor0;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;

			};

			struct fragInput
			{
				float4 pos : SV_POSITION;
				float4 col : COLOR;
			};

			fragInput vert( vertexInput v)
			{
				fragInput o;

				float3 normalDirection = normalize (mul(float4(v.normal,0.0), _World2Object).xyz);
				float3 lightDirection;
				float atten = 1.0;

				lightDirection = normalize (_WorldSpaceLightPos0.xyz);

				float3 diffuseReflection = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection));
				float3 lightFinal = diffuseReflection + UNITY_LIGHTMODEL_AMBIENT.xyz;

				o.col = float4(lightFinal * _Color.rgb,1.0);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);



				return o;
			}

			float4 frag(fragInput i) : COLOR
			{
				return i.col;
			}




			ENDCG
		}
	}
	//Fallback "Diffuse"
}