Shader "Hidden/Sketchy"
{
	Properties
	{
		[HideInInspector]_MainTex("Texture", 2D) = "white" {}
		_Sketch("SketchTexture", 2D) = "white" {}
		_ThresholdLine("Outline Threshold", float) = 0.01
		_EdgeColor("Outline Color", Color) = (0,0,0,1)
		_Smooth("Outline Smoothness", float) = 0.01
			
		_Offset("Blue Cutoff 1", Range(0,1)) = 0.5
		_Offset2("Blue Cutoff 2", Range(0,1)) = 0.5
		_Offset3("Red Cutoff Extra", Range(0,1)) = 0.5
		_Intensity("Red Intensity Extra", Range(1,4)) = 0.5
		[Toggle(COL)] _COL("Original Colors?", Float) = 0
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			// 0
			Pass
			{
				ZTest Always Cull Off ZWrite Off
			   Stencil{
				Ref 1
				ReadMask 1
				Comp Equal
				 Pass Keep
				}


				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma shader_feature COL 
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 screenPos :TEXCOORD1;
				};

				sampler2D _CameraDepthNormalsTexture;
				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.screenPos = ComputeScreenPos(o.vertex);
					return o;
				}

				sampler2D _MainTex;
				float4 _MainTex_TexelSize;
				float _ThresholdLine;
				fixed4 _EdgeColor;
				float _Smooth, _Intensity;

				sampler2D _Sketch;
				float _Offset, _Offset2, _Offset3;

				float4 GetPixelValue(in float2 uv) {
					half3 normal;
					float depth;
					DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, uv), depth, normal);
					return fixed4(normal, depth);
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);
	
					float2 screenUV = i.screenPos.xy / i.screenPos.w;
					screenUV *= float2(8, 6);
					fixed4 sketchFX = tex2D(_Sketch, screenUV * 14);

					fixed4 orValue = GetPixelValue(i.uv);
					float2 offsets[8] = {
						float2(-1, -1),
						float2(-1, 0),
						float2(-1, 1),
						float2(0, -1),
						float2(0, 1),
						float2(1, -1),
						float2(1, 0),
						float2(1, 1)
					};
					fixed4 sampledValue = fixed4(0,0,0,0);
					for (int j = 0; j < 8; j++) {
						sampledValue += GetPixelValue(i.uv + offsets[j] * _MainTex_TexelSize.xy);
					}
					sampledValue /= 8;

					float4 dither = (smoothstep(_Offset, _Offset + 0.05, col.b) + smoothstep(_Offset2, _Offset2+0.05, col.b))* sketchFX.r;
					dither += (smoothstep(_Offset3, _Offset3 + 0.05, col.b) * _Intensity);
#if COL
					dither *= col;
#endif			
					return lerp(dither, _EdgeColor, smoothstep(_ThresholdLine, _ThresholdLine + _Smooth, length(orValue - sampledValue)));
				}
				ENDCG
			}
		}
}