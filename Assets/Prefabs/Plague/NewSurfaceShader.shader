Shader "Tutorial/007_Sprite"{
	Properties{
		_Color ("Tint", Color) = (0, 0, 0, 1)
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Texture", 2D) = "white" {}
	}

	SubShader{
		Tags{ 
			"RenderType"="Transparent" 
			"Queue"="Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha

		ZWrite off
		Cull off

		Pass{

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;

			fixed4 _Color;

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 noise_uv : TEXCOORD1;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v){
				v2f o;
				o.position =  UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.noise_uv = TRANSFORM_TEX(v.uv, _NoiseTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= _Color;
				col *= i.color;
			
				float rotation = _Time.z;
				float2x2 rotationMatrix = { cos(rotation), -sin(rotation), sin(rotation), cos(rotation) };
				col.a *= tex2D(_NoiseTex, mul(rotationMatrix, i.noise_uv)).x;
				float distance = length(i.noise_uv - float2(0.5, 0.5)); // Calculate distance from center
				float alpha = distance > 0.5 ? 0.0 : 1.0; // Set alpha based on distance
				col.a *= alpha;
				return col;
			}

			ENDCG
		}
	}
}