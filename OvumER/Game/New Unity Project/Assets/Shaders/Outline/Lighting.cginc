#if !defined(HALFTONE_LIGHTING_INCLUDED)

#define HALFTONE_LIGHTING_INCLUDED

#include "AutoLight.cginc"
#include "UnityStandardBRDF.cginc"

float4 _Color;
float4 _AmbientLight;
float4 _ShadowColour;

sampler2D _MainTex;
float4 _MainTex_ST;

sampler2D _HalfToneMask;
float4 _HalfToneMask_ST;

float _ToneOffset;
float _ToneIntensity;

struct VertData
{

	float4 vertex : POSITION;
	float2 uv: TEXCOORD0;
	float3 normal : NORMAL;

};

struct V2F
{

	float4 position : SV_POSITION;
	float2 uv : TEXCOORD0;
	float3 normal : TEXCOORD1;
	float3 worldPos : TEXCOORD2;
	float2 maskUV : TEXCOORD3;
	float4 screenPos : TEXCOORD4;

};

V2F vert(VertData v)
{

	V2F i;
	i.position = UnityObjectToClipPos(v.vertex);
	i.worldPos = mul(unity_ObjectToWorld, v.vertex);
	i.uv = TRANSFORM_TEX(v.uv, _MainTex);
	i.maskUV = TRANSFORM_TEX(v.uv, _HalfToneMask);
	i.normal = UnityObjectToWorldNormal(v.normal);
	i.screenPos = ComputeScreenPos(i.position);
	return i;

}

float3 Lighting(V2F i)
{

	float3 lightDir;
	float attenuationStrength;

	#if defined(POINT) || defined(POINT_COOKIE) || defined(SPOT)

	lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
	attenuationStrength = 10;

	#else

	lightDir = _WorldSpaceLightPos0.xyz;
	attenuationStrength = 1;

	#endif

	float2 maskPos = i.screenPos / i.screenPos.w;
	float aspect = _ScreenParams.x / _ScreenParams.y;
	maskPos.x *= aspect;
	maskPos.y += floor(maskPos.x * _HalfToneMask_ST.xy);

	fixed3 halfMask = tex2D(_HalfToneMask, maskPos * _HalfToneMask_ST.xy);

	float NdotL = dot(i.normal, lightDir);
	float diff = NdotL * 0.5 + 0.5;

	UNITY_LIGHT_ATTENUATION(attenuation, 0, i.worldPos);

	
	

	return NdotL;

}

float4 frag(V2F i) : SV_TARGET
{

	i.normal = normalize(i.normal);

	float3 albedo = tex2D(_MainTex, i.uv).rgb;
	float3 lighting = Lighting(i);
	float3 diffuse = lighting * albedo * _Color.rgb;
	return float4(diffuse, 1);

}

#endif