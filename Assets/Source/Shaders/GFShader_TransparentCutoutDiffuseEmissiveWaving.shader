// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Games Foundry/Animated/Transparent/Transparent Cutout Diffuse Emissive Waving"
{
	Properties
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0, 1)) = 0.5
		_Emission("Emission", Float) = 0.05
		_Wind("Wind", Float) = 1
		_WindFrequency("Wind Frequency", Float) = 1
		_WindScale("Wind Scale", Float) = 1
	}

	SubShader
	{
		Tags { "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 300

		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff vertex:vert

		sampler2D _MainTex;
		fixed4 _Color;
		float _Emission;
		float _Wind;
		float _WindFrequency;
		float _WindScale;

		struct Input
		{
			float2 uv_MainTex;
		};

		void vert(inout appdata_full v) {
			float3 wp = mul(unity_ObjectToWorld, v.vertex)*0.02*_WindScale;
			float3 wind = float3(sin(_Time.y*0.4*_WindFrequency + wp.x) * sin(_Time.x*_WindFrequency+0.523 + wp.x), 0, sin(_Time.y*_WindFrequency*0.4152+0.4512 + wp.z)  * sin(_Time.x*_WindFrequency+2.821 + wp.z));

			wind.y = -abs(wind)*abs(wind)*0.2f;
			float s = (1-(cos(v.texcoord.y*3)));
			float4 vColor = v.color;
			v.vertex.xyz += wind * 0.5 * s*s * _Wind * vColor.r;
		}

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = c * _Emission;
		}
		ENDCG
	}

	Fallback "Transparent/Cutout/Diffuse"
}
