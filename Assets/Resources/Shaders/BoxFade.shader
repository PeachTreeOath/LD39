Shader "Custom/BoxFade" {
	//Fade out from the top edge of a box - Dave's second greatest work
	Properties{
		//_MainTex is the start texture (filled automatically for sprites)
		_MainTex("Start Texture", 2D) = "white" {}
		_EndTex("End Texture", 2D) = "black" {}
		_FadeDist("Fade Distance", Range(0,1)) = 0.25
	}

		CGINCLUDE
				 #include "UnityCG.cginc"

				const float4 zero = float4(0, 0, 0, 1);

				float _FadeDist;
				sampler2D _MainTex;
				sampler2D _EndTex;

			 struct v2f {
				 float4 pos : SV_POSITION;
				 float4 color : COLOR0;
				 float4 uv : TEXCOORD0;
			 };

			 v2f vert(appdata_full v) {
				 v2f o;
				 o.color = v.color;
				 o.pos = UnityObjectToClipPos(v.vertex);
				 o.uv = v.texcoord;
				 return o;
			 }

			 fixed4 frag(v2f i) : SV_Target{
				 float t = i.uv.y - (1 - _FadeDist); //Shift by inverse distance amt
				 t = clamp(t, 0, 1);
				 t = t * t;
				 float4 textColA = tex2D(_MainTex, i.uv);
				 float4 textColB = tex2D(_EndTex, i.uv);
				 textColB.a = 0;
				 float4 result = (1-t) * textColA + t * textColB;
				 return fixed4(result);
			 }

				 ENDCG

		SubShader{

			Tags { "Queue" = "Transparent" }

				Lighting On
			Pass{
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off
				Cull Back

				 CGPROGRAM
				 #pragma vertex vert
				 #pragma fragment frag


				 ENDCG
			}

		}
}