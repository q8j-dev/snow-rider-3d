Shader "Custom/LegacyMobileVertexLit"
{
	Properties
	{
		[MainTexture] _MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" "Queue" = "Geometry" }
		LOD 80

		Pass
		{
			Name "Forward"
			Tags { "LightMode" = "UniversalForward" }
			Cull Back
			ZWrite On
			ZTest LEqual

			HLSLPROGRAM
			#pragma target 2.0
			#pragma vertex Vert
			#pragma fragment Frag
			#pragma multi_compile_fog
			#pragma multi_compile_instancing

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);

			CBUFFER_START(UnityPerMaterial)
			float4 _MainTex_ST;
			CBUFFER_END

			struct Attributes
			{
				float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD0;
				half3 lighting : TEXCOORD1;
				half fogFactor : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings Vert(Attributes input)
			{
				Varyings output = (Varyings)0;
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
				half3 normalWS = TransformObjectToWorldNormal(input.normalOS);
				Light mainLight = GetMainLight();
				half diffuse = saturate(dot(normalize(normalWS), mainLight.direction));
				output.positionCS = TransformWorldToHClip(positionWS);
				output.uv = input.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				output.lighting = SampleSH(normalWS) + mainLight.color * diffuse * mainLight.distanceAttenuation;
				output.fogFactor = ComputeFogFactor(output.positionCS.z);
				return output;
			}

			half4 Frag(Varyings input) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
				half3 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv).rgb * input.lighting;
				return half4(MixFog(color, input.fogFactor), 1.0h);
			}
			ENDHLSL
		}
	}
}
