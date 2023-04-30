Shader "gamejam/spotlight"{
	Properties{
		_Color ("Tint", Color) = (0, 0, 0, 1)
		_VisibleDistance("Visibility", Float) = 0.2
		_MainTex ("Texture", 2D) = "white" {}

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

			fixed4 _Color;
			float _VisibleDistance;

			struct appdata{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v){
				v2f o;
				o.position =  UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				fixed4 col = fixed4(0,0,0,1);
				col *= i.color;
				float distance = length(i.uv - float2(0.5, 0.5)); // Calculate distance from center
				distance += 0.005 * (_SinTime.w + 2 * _CosTime.w);
				float light = distance > _VisibleDistance ? 1 : (distance*(1/_VisibleDistance)); // Set alpha based on distance
				col.a = light;
				return col;
			}

			ENDCG
		}
	}
}