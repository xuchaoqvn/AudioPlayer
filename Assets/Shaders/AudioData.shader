Shader "AudioPlayer/AudioData"
{
    Properties
    {
		_MainColor ("MainColor",Color) = (1,1,1,1)
		_ViceColor ("ViceColor",Color) = (1,1,1,1)
		_Brightness ("Brightness",Range(2.0,10.0)) = 4
    }
    SubShader
    {
		Tags{"RenderType" = "Opaque"}
		//Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent" "IgnoreProjector" = "True" }
        //Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			fixed4 _MainColor;
			fixed4 _ViceColor;
			float _Brightness;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(_MainColor,_ViceColor,i.uv.y) * _Brightness;
                return col;
            }
            ENDCG
        }
    }
}
