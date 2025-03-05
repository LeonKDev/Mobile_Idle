Shader "Unlit/Particles"
{
    Properties { _MainTex ("Texture", 2D) = "white" {} }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct Particle {
                float2 position;
                float2 velocity;
                float radius;
            };

            StructuredBuffer<Particle> particles;

            struct appdata {
                uint id : SV_VertexID;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                Particle p = particles[v.id];

                float2 screenPos = p.position * 2.0;
                o.vertex = UnityObjectToClipPos(float4(screenPos, 0, 1));
                o.uv = float2(0.5, 0.5); // Just for simplicity
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return fixed4(1, 1, 1, 1); // White circles
            }
            ENDCG
        }
    }
}
