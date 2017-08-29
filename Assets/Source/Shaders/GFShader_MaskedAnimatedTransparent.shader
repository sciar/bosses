Shader "Games Foundry/GFShader_MaskedAnimatedTransparent" {
	Properties {
		_Color ("Main Color", Color) = (0.753,0.753,0.753,1)
		_MainTex ("Base (RGBA)", 2D) = "white" {}
		_DetailTex ("Detail Texture (RGB) Detail Mask (A)", 2D) = "black" {}
		_Mask ("Composite Mask", 2D) = "black" {}
		_XMove ("X Movement Speed", Float) = 0.0
		_YMove ("Y Movement Speed", Float) = 0.0
		_Emission ("Emission Slider", Range (0, 10)) = 0.0
		_Alpha ("Alpha", Range (0, 1)) = 1.0
	}
	SubShader {
		//Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		
		//LOD 200
		//Blend SrcAlpha OneMinusSrcAlpha
		
		//CGPROGRAM
		//#pragma surface surf Lambert decal:blend nolightmap 
		
		Tags { "RenderType" = "Transparent" "Queue" = "Geometry" "ForceNoShadowCasting" = "True" 
		}

		
		CGPROGRAM
		#pragma surface surf Lambert decal:blend nolightmap 

		sampler2D _MainTex, _DetailTex, _Mask;
		float _XMove, _YMove;

		struct Input {
			float2 uv_MainTex;
			float2 uv2_Mask;
		};
		fixed4	_Color;
		fixed _Emission;
		fixed _Alpha;
		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv = float2(IN.uv_MainTex.x + _XMove * _Time.x , IN.uv_MainTex.y + _YMove * _Time.x);
			half4 d = tex2D (_DetailTex, uv);
			half dm = tex2D (_Mask, IN.uv2_Mask).a;//IN.uv_MainTex).a;
			half4 c = tex2D (_MainTex, uv);//IN.uv_MainTex);
			o.Albedo = c.rgb * _Color;
			//o.Emission = d.rgb*d.a*dm*_Emission;
			o.Emission = _Emission;
			o.Alpha = c.a * dm * _Alpha;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}