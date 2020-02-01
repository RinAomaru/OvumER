Shader "Lit/Outline"
{

	Properties
	{

		_Color("Colour", Color) = (1, 1, 1, 1)
		_ShadowColour("Shadow Colour", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}
		_HalfToneMask("HalfTone Shape", 2D) = "white" {}
		_ToneOffset("HalfTone Offset", Range(-3, 3)) = 0.3
		_ToneIntensity("HalfTone Intensity", Range(0, 5)) = 0.3
		_OutlineReference("Outline Layer", Int) = 4
		_OutlineColour("Outline Colour", Color) = (0, 0, 0, 0)
		_LineIntensity("Line thickness", Range(0, 0.5)) = 0.02

	}

		SubShader
		{

			Pass
			{


				Stencil
				{

					Ref[_OutlineReference]
					Comp Always
					Pass Replace

					ZFail keep

				}


				Tags
				{

					"LightMode" = "ForwardBase"
					"Queue" = "Geometry"

				}

				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag

				#include "Lighting.cginc"

				ENDCG

			}
			Pass
			{

				Stencil
				{

					Ref[_OutlineReference]
					Comp Always
					Pass Replace

					ZFail keep

				}

				Tags
				{

					"LightMode" = "ForwardAdd"
					"Queue" = "Geometry"

				}

				BlendOP Max
				ZWrite Off

				CGPROGRAM

				#pragma target 3.0

				#pragma multi_compile_fwdadd

				#pragma vertex vert
				#pragma fragment frag



				#include "Lighting.cginc"

				ENDCG

			}

			Pass
		{

			Tags {"Queue" = "Geometry-1"}

			ZTest ON
			//ZWrite OFF
			Cull Off

			Stencil
			{

				Ref[_OutlineReference]
				Comp NotEqual
				Fail keep
				Pass replace

			}

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityStandardBRDF.cginc"

			float _LineIntensity;
			float4 _OutlineColour;

			struct VertData
			{

				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;

			};

			struct v2f
			{

				float4 position: SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;

			};

			v2f vert(VertData v)
			{

				v2f i;
				float4 outlinePos = v.vertex;

				float3 normal = normalize(v.normal);
				outlinePos += float4(normal, 0) * _LineIntensity;

				i.position = UnityObjectToClipPos(outlinePos);
				i.worldPos = mul(unity_ObjectToWorld, v.vertex);

				i.normal = UnityObjectToWorldNormal(v.normal);
				i.uv = v.uv;
				//i.position += float4(i.normal * _LineIntensity, 0);

				return i;

			}

			float4 frag(v2f i) : SV_Target
			{


				return _OutlineColour;


			}



			ENDCG

		}

		}

			Fallback "VertexLit"

}