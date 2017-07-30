Shader "Custom/TextureLerp" {
	//Correctly transition between two textures - Dave's greatest work
	Properties{
		//Put Time.time into _StartTime for when to begin the transition
		//E.g.: rend.material.SetFloat("_StartTime", Time.time);
		//_MainTex is the start texture (filled automatically for sprites)
		_StartTime("Start Time", Float) = 0
		_Duration("Duration", Range(0,10)) = 1
		_MainTex("Start Texture", 2D) = "white" {}
		_EndTex("End Texture", 2D) = "black" {}
	}

		CGINCLUDE
				 #include "UnityCG.cginc"

				const float4 zero = float4(0, 0, 0, 1);

				float _StartTime;
				float _Duration;
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
				 float t = _Time[1] - _StartTime;
				 t = clamp(t / _Duration, 0, 1);
				 float4 textColA = tex2D(_MainTex, i.uv);
				 float4 textColB = tex2D(_EndTex, i.uv);
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