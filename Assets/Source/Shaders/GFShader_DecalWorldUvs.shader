Shader "Games Foundry/Decal World Uvs"
{
	Properties {
		_Color("Main Color", Color) = (0.753, 0.753, 0.753, 1)
		_MainTex("Texture", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_TextureScale("TextureScale", float) = 1
		_TextureOffset("TextureOffset", float) = 0
	}

	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Geometry+2" "ForceNoShadowCasting" = "True" "IgnoreProjector" = "True" }
		Offset 0, -1

		CGPROGRAM
		#pragma surface surf Lambert decal:blend nolightmap

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		
		half _TextureScale;
		half _TextureOffset;
		sampler2D _MainTex;
		sampler2D _Mask;
		fixed4 _Color;
		

		void surf(Input IN, inout SurfaceOutput o) {
			float2 uvsMain = IN.worldPos.zx;

			uvsMain.x = ((uvsMain.x * 0.05)*_TextureScale) + _TextureOffset; //0.125 is just to align the texture better to the 5grid snap that floor tiles use
			uvsMain.y = ((uvsMain.y * 0.05)*_TextureScale) + _TextureOffset;

			float4 col = tex2D(_MainTex, uvsMain);
			float4 m = tex2D(_Mask, IN.uv_MainTex);

			o.Albedo = col.rgb * _Color.rgb;
			o.Alpha = m.a;
		}
		ENDCG
	}
	Fallback "Transparent/Cutout/Diffuse"
}
