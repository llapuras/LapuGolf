Shader "Toon/Lit Environment" {
	Properties{

		[Header(Main Texture)]
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_Pastel("Pastel", Range(0,1)) = 1
		_MainTex("Base (RGB)", 2D) = "black" {}
		_ToonRamp("Toon Ramp",Color) = (0.5,0.5,0.5,1)
		_Mask("Effects Mask ( r=Emis, g = spec",2D) = "black" {}
		 [Space]
		[Header(Rim Lighting)]
		_RimColor("Rim Color", Color) = (0.5,0.5,0.5,1)
		_RimPower("Rim Power", Range(0,50)) = 1
		 [Space]
		[Header(Extra Worldspace Texture)]
		_ExtraTex("Extra Texture (RGB)", 2D) = "black" {}
		_ExtraTex2("Extra Texture 2 (RGB)", 2D) = "black" {}
		_ExtraColor("Extra Color", Color) = (0.5,0.5,0.5,1)
		_Scale("Top Scale", Range(0,2)) = 1
		_Scale2("Top Scale 2", Range(0,2)) = 1
		_TextureOpacity("Texture Opacity", Range(-6,6)) = 1
		 [Space]
		[Header(Specular)]
		[Toggle(SPEC)] _SPEC("Specular", Float) = 0
		_SColor("Specular Color", Color) = (0.5,0.5,0.5,1)
		_SpecSize("Specular Size", Range(0,2)) = 1
		_SpecOffset("Specular Offset", Range(0,2)) = 1
		_RampS("Specular Ramp", 2D) = "gray" {}
		 [Space]
		[Header(TopTexture)]
		[Toggle(TOP)] _TOP("Top Texture", Float) = 0
		_TColor("Top Color", Color) = (0.5,0.5,0.5,1)
		_TopSpread("Top Spread", Range(0,2)) = 1
		_TopScale("Top Scale", Range(0,2)) = 1
		_NoiseScale("Noise Scale", Range(0,2)) = 1
		_Normal("Noise Texture", 2D) = "black" {}
		_TopTex("Top Texture (RGB)", 2D) = "gray" {}
		_EdgeColor("Edge Color", Color) = (0.5,0.5,0.5,1)
		_EdgeWidth("Edge Width", Range(0,2)) = 1


	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200
			//Cull Off

			CGPROGRAM
			#pragma surface surf ToonRamp vertex:vert fullforwardshadows
			#pragma shader_feature SPEC //
			#pragma shader_feature TOP //
			float4 _ToonRamp;

		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten) {
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif

			float d = dot(s.Normal, lightDir);
			float dChange = fwidth(d);
			float3 lightIntensity = smoothstep(0, dChange + 0.06, d) + (_ToonRamp);
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * lightIntensity * (atten * 2);
			c.a = 0;
			return c;
		}
		uniform float _DayNightEmis;

		sampler2D _MainTex, _ExtraTex, _RampS, _TopTex, _Normal, _ExtraTex2, _Mask;
		float4 _Color, _RimColor, _SColor, _TColor, _EdgeColor,_ExtraColor;
		float _Scale,_TextureOpacity,_RimPower;
		float _SpecSize, _SpecOffset;
		float _TopSpread,_TopScale, _NoiseScale, _EdgeWidth, _Pastel, _Scale2;

		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float3 worldNormal; // world normal built-in value
			float3 worldPos; // world position built-in value
			float3 viewDir;
			float3 lightDir;
		};

		 void vert(inout appdata_full v, out Input o)
	{
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.lightDir = WorldSpaceLightDir(v.vertex); // get the worldspace lighting direction
	}

		void surf(Input IN, inout SurfaceOutput o) {

			float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			float4 mask = tex2D(_Mask, IN.uv_MainTex);

			// triplanar
			float3 blendNormal = saturate(pow(IN.worldNormal * 1.4,4));
			// triplanar for top texture for x, y, z sides
			float3 xm = tex2D(_ExtraTex, IN.worldPos.zy * _Scale);
			float3 zm = tex2D(_ExtraTex, IN.worldPos.xy * _Scale);
			float3 ym = tex2D(_ExtraTex, IN.worldPos.zx * _Scale);

			// lerped together all sides for top texture
			float3 toptexture = zm;
			toptexture = lerp(toptexture, xm, blendNormal.x);
			toptexture = lerp(toptexture, ym, blendNormal.y);

			// extratex smaller
			float3 xmm = tex2D(_ExtraTex2, IN.worldPos.zy  * _Scale2);
			float3 zmm = tex2D(_ExtraTex2, IN.worldPos.xy * _Scale2);
			float3 ymm = tex2D(_ExtraTex2, IN.worldPos.zx * _Scale2);

			// lerped together all sides for top texture
			float3 toptexture2 = zmm;
			toptexture2 = lerp(toptexture2, xmm, blendNormal.x);
			toptexture2 = lerp(toptexture2, ymm, blendNormal.y);

			toptexture = (toptexture2 * toptexture);



			// triplanar for side and bottom texture, x,y,z sides
		float3 x = tex2D(_TopTex, IN.worldPos.zy * _TopScale);
		float3 y = tex2D(_TopTex, IN.worldPos.zx * _TopScale);
		float3 z = tex2D(_TopTex, IN.worldPos.xy * _TopScale);

		// lerped together all sides for side bottom texture
		float3 sidetexture = z;
		sidetexture = lerp(sidetexture, x, blendNormal.x);
		sidetexture = lerp(sidetexture, y, blendNormal.y);

		// normal noise triplanar for x, y, z sides
		float3 xn = tex2D(_Normal, IN.worldPos.zy * _NoiseScale);
		float3 yn = tex2D(_Normal, IN.worldPos.zx * _NoiseScale);
		float3 zn = tex2D(_Normal, IN.worldPos.xy * _NoiseScale);

		// lerped together all sides for noise texture
		float3 noisetexture = zn;
		noisetexture = lerp(noisetexture, xn, blendNormal.x);
		noisetexture = lerp(noisetexture, yn, blendNormal.y);
		noisetexture *= 2;


		float worldNormalDotNoise = dot(o.Normal * noisetexture, blendNormal.y);

		// if dot product is higher than the top spread slider, multiplied by triplanar mapped top texture
		// step is replacing an if statement to avoid branching :
		// if (worldNormalDotNoise > _TopSpread{ o.Albedo = toptexture}
		float3 topTextureResult = step(_TopSpread + _EdgeWidth, worldNormalDotNoise) * sidetexture;
		float3 topTextureEdgeResult = (step(_TopSpread , worldNormalDotNoise) * step(worldNormalDotNoise, _TopSpread + _EdgeWidth)) * _EdgeColor;




		// Specular
		half d = dot(o.Normal, IN.lightDir + (IN.viewDir * 2))*0.5 + _SpecOffset; // basing on normal and light direction
		half3 rampS = tex2D(_RampS, float2(d, d)).rgb; // specular ramp
		float3 specularresult = float3(0,0,0);
#if SPEC
			specularresult = (step(_SpecSize, rampS.r)) * rampS * d* _SColor; // specular
#endif

			float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
			float spec = dot(IN.viewDir, o.Normal);// specular based on view and light direction
			float cutOff = step(saturate(spec), 0.8); // cutoff for where base color is
			float3 specularAlbedo = c.rgb * (1 - cutOff) * mask.g * _RimColor * 4;// inverted base cutoff times specular color

			// highlight 
			float highlight = saturate(dot(normalize(lightDir + (IN.viewDir * 0.5)), o.Normal)); // highlight based on light direction
			float3 highlightAlbedo = (step(0.9,highlight) * c.rgb *_RimColor * 2) * mask.g; //glowing highlight



			o.Albedo = c.rgb + saturate(toptexture.r * _TextureOpacity* _ExtraColor) + specularresult + _Pastel;
#if TOP
			o.Albedo = (step(worldNormalDotNoise, _TopSpread) * ((lerp(c.rgb, _ExtraColor, (toptexture.r * _TextureOpacity))) + (toptexture.r * _TextureOpacity * _ExtraColor) + specularresult)) + (topTextureResult * _TColor) + topTextureEdgeResult + _Pastel;
#endif
			o.Albedo += specularAlbedo + highlightAlbedo;
			// rim light
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
			o.Emission = (mask.r * _RimColor);

			o.Alpha = c.a;
		}

		ENDCG
		}

			Fallback "Diffuse"
}
