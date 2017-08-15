// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Refl"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_HeightK("Height K", float) = 0.5
		_ZeroLevel("Zero Level", float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZTest Greater
		ZWrite Off
		Offset 0,-1

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal: NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{				
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _HeightK;
			float _ZeroLevel;

			v2f vert (appdata v)
			{
				v2f o;
				
				o.vertex = mul(unity_ObjectToWorld, v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.x =  0;
				o.uv.y = (-(o.vertex.y-_ZeroLevel))*_HeightK;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color.x = 1;
				o.color.y = 1;
				o.color.z = 1;
				o.color.w = 1;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col * i.color;
			}
			ENDCG
		}
	}
}
