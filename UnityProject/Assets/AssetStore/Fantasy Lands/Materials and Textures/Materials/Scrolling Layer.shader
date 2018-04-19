﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Transparent/Scrolling Layer" {
Properties {
_MainTex ("Base layer (RGB)", 2D) = "white" {}
//_DetailTex ("2nd layer (RGB)", 2D) = "white" {}
_ScrollX ("Base layer Scroll speed X", Float) = 1.0
_ScrollY ("Base layer Scroll speed Y", Float) = 0.0
//_Scroll2X ("2nd layer Scroll speed X", Float) = 1.0
//_Scroll2Y ("2nd layer Scroll speed Y", Float) = 0.0
_Intensity ("Intensity", Float) = 1.0
_Alpha ("Alpha", Range(0.0, 1.0)) = 1.0
}
SubShader {
Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
Lighting Off
Fog { Mode Off }
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha
LOD 100
CGINCLUDE
#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
#include "UnityCG.cginc"
sampler2D _MainTex;
//sampler2D _DetailTex;
float4 _MainTex_ST;
//float4 _DetailTex_ST;
float _ScrollX;
float _ScrollY;
//float _Scroll2X;
//float _Scroll2Y;
float _Intensity;
float _Alpha;
struct v2f {
float4 pos : SV_POSITION;
float2 uv : TEXCOORD0;
//float2 uv2 : TEXCOORD1;
fixed4 color : TEXCOORD1;
};
v2f vert (appdata_full v)
{
v2f o;
o.pos = UnityObjectToClipPos(v.vertex);
o.uv = TRANSFORM_TEX(v.texcoord.xy,_MainTex) + frac(float2(_ScrollX, _ScrollY) * _Time);
//o.uv2 = TRANSFORM_TEX(v.texcoord.xy,_DetailTex) + frac(float2(_Scroll2X, _Scroll2Y) * _Time);
o.color = fixed4(_Intensity, _Intensity, _Intensity, _Alpha);
return o;
}
ENDCG
Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
fixed4 frag (v2f i) : COLOR
{
fixed4 o;
fixed4 tex = tex2D (_MainTex, i.uv);
//fixed4 tex2 = tex2D (_DetailTex, i.uv2);
//o = (tex * tex2) * i.color;
o = tex * i.color;
return o;
}
ENDCG
}
}
}