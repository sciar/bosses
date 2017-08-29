Shader "phase/decal"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Geometry+2" "ForceNoShadowCasting" = "True"}
	Offset 0, -1

  CGPROGRAM
  #pragma surface surf Lambert decal:blend nolightmap

  struct Input {
   float2 uv_MainTex;
  };

  sampler2D _MainTex;

  void surf(Input IN, inout SurfaceOutput o) {
   float4 col = tex2D(_MainTex, IN.uv_MainTex);

   o.Albedo = col.rgb;
   o.Alpha = col.a;
  }
  ENDCG
 }
Fallback "Transparent/Cutout/Diffuse"
}
