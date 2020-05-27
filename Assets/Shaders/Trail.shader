Shader "Lyric/Trail"
{
    Properties
    {
        [HDR]_Color ("Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent" "IgnoreProjector" = "True" }
        Cull Back ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
        
		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

			fixed4 _Color;

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
                fixed4 col = _Color;
				col.a = i.uv.x;
                return col;
            }
            ENDCG
        }
    }
}
