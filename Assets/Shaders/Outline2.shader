Shader "Custom/2DOutline"
 {
     Properties
     {
     	 _Color("Outline Color", Color) = (1,1,1,1)
         _MainTex ("Base (RGB)", 2D) = "white" {}
         _Width ("Thickness", Range(0, 1)) = 0.5
     }
 
     SubShader
     {
         Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
         ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off
         Lighting Off
         LOD 110
 
         CGPROGRAM
         #pragma surface surf Lambert alpha
 
         struct Input
         {
             float2 uv_MainTex;
             fixed4 color : COLOR;
         };
 
         fixed4 _Color;
         sampler2D _MainTex;
         float _Width;
 
         void surf(Input IN, inout SurfaceOutput o)
         {
                 _Width /= 100;
 
                 fixed4 TempColor = tex2D(_MainTex, IN.uv_MainTex + float2(_Width, _Width))
                                  + tex2D(_MainTex, IN.uv_MainTex - float2(_Width, _Width));

                 fixed4 OutlineColor = 0;
                 OutlineColor.r = _Color.r;
                 OutlineColor.g = _Color.g;
                 OutlineColor.b = _Color.b;
                 OutlineColor.a = 1;
 
                 fixed4 AlphaColor = (0, 0, 0, TempColor.a);
                 fixed4 MainColor = AlphaColor * OutlineColor.rgba;
                 fixed4 TexColor = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
 
                 if(TexColor.a > 0.95)
                     MainColor = TexColor;
 
                 o.Albedo = MainColor.rgb;
                 o.Alpha = MainColor.a;
         }
         ENDCG
     }
 
     Fallback "Diffuse"
 }