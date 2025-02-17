﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "CustomMaterialContrast" {
   Properties {
      _ColorMain ("Diffuse Material Color", Color) = (1,1,1,1) 
      _MidColor ("Semishadow Material Color", Color) = (1,1,1,1) 
      _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 1
      _AOFactor ("Ambient Occlusion Factor", Float) = 1
      _GlobalLightIntensity("Global Light Intensity", Float) = 1
      _Contrast ("Contrast", Range(0, 1)) = 0.5
   }
   SubShader {
    Tags {"RenderType" = "Opaque"} 
    Pass {   

         Tags { "LightMode" = "ForwardBase" } 

         CGPROGRAM

         #pragma vertex vert approxview
         #pragma fragment frag 
         #pragma multi_compile_fog
         #pragma multi_compile_fwdbase
         #pragma fragmentoption ARB_precision_hint_fastest
         #pragma fragmentoption ARB_fog_linear


         #include "AutoLight.cginc"
         #include "UnityCG.cginc"
         uniform half4 _LightColor0; 
         uniform half3 _ColorMain; 

         uniform half3 _SpecColor; 
         uniform half3 _MidColor; 
         uniform half _Shininess;
         uniform half _AOFactor;
         uniform half4 unity_FogStart;
         uniform half4 unity_FogEnd;
         uniform half _GlobalLightIntensity;
         uniform half _Contrast;

         struct vertexOutput 
         {
            
            half2 fogColorFactor:FLOAT;
            float4 pos : SV_POSITION;
            float3 posWorld : COLOR;
            half2 uv : TEXCOORD0;
            LIGHTING_COORDS(1,2)
         };

         struct appdata_t
         {
            float4 vertex   : POSITION;
            float3 normal   : NORMAL;
            float4 color    : COLOR;
            float2 texcoord : TEXCOORD0;
         };

         vertexOutput vert(appdata_t v) 
         {
            vertexOutput output;
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject;
            float4 outpos = UnityObjectToClipPos(v.vertex);

            half3 normalDirection = normalize(
               mul(half4(v.normal, 0.0), modelMatrixInverse).xyz);
            half3 viewDirection = normalize(_WorldSpaceCameraPos 
               - mul(modelMatrix, v.vertex).xyz);
            half3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
 


            half factor = max(0.0,dot(normalDirection, lightDirection) * 0.5 + 0.5 * pow(max(0.0, dot(reflect(-lightDirection, normalDirection), viewDirection)), _Shininess));


            //FOG


            output.pos = outpos;
            output.posWorld = _WorldSpaceCameraPos 
               - mul(modelMatrix, v.vertex).xyz;
            output.fogColorFactor.y = lerp(factor, factor * (v.color.r - 0.5f) *2.0f, _AOFactor);

            TRANSFER_VERTEX_TO_FRAGMENT(output);


            return output;
         }
 
         half4 frag(vertexOutput input) : COLOR
         {
            half factor = input.fogColorFactor.y * LIGHT_ATTENUATION(input);

            half factorl = floor(factor  + 0.5);
            half3 mainColor = lerp(_MidColor, _ColorMain, _Contrast * 2.0);
            half3 specColor = lerp(_MidColor, _SpecColor, _Contrast * 2.0);
            half3 tempcolshadowmid = lerp(mainColor,_MidColor,factor * 2.0);
            half3 tempcolmidlight = lerp(_MidColor,specColor,(factor - 0.5 ) * 2.0);
            half3 tempcol = tempcolshadowmid * (1 - factorl) + tempcolmidlight * factorl;

            half pos = length(input.posWorld);
            half diff = unity_FogEnd.x - unity_FogStart.x;
            half invDiff = 1.0f / diff;
            half3 finalColor = lerp(unity_FogColor.rgb, tempcol.rgb, clamp ((unity_FogEnd.x - pos) * invDiff, 0.0, 1.0));
            half4 col = half4(finalColor, 1.0f) * _GlobalLightIntensity;
            return col;
         }
 
         ENDCG
      }
      Pass {
            Tags {"LightMode" = "ForwardAdd"}                       // Again, this pass tag is important otherwise Unity may not give the correct light information.
            Blend One One                                           // Additively blend this pass with the previous one(s). This pass gets run once per pixel light.
            CGPROGRAM
                #pragma vertex vert approxview
                #pragma fragment frag
                #pragma multi_compile_fwdadd                        // This line tells Unity to compile this pass for forward add, giving attenuation information for the light.
                #pragma fragmentoption ARB_precision_hint_fastest

                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
                uniform fixed4 _SpecColor; 
                 
                struct v2f
                {
                    float4  pos         : SV_POSITION;
                    float3  lightDir    : TEXCOORD2;
                    float3 normal       : TEXCOORD1;
                    LIGHTING_COORDS(3,4)                            // Macro to send shadow & attenuation to the vertex shader.
                };
 
                v2f vert (appdata_tan v)
                {
                    v2f o;
                    
                    o.pos = UnityObjectToClipPos( v.vertex);
                    
                    o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
                    
                    o.normal =  v.normal;
                    TRANSFER_VERTEX_TO_FRAGMENT(o);                 // Macro to send shadow & attenuation to the fragment shader.
                    return o;
                }
 
                fixed4 _LightColor0; // Colour of the light used in this pass.
 
                half4 frag(v2f i) : COLOR
                {
                    half atten = LIGHT_ATTENUATION(i); // Macro to get you the combined shadow & attenuation value.
               
                    half diff = saturate(dot(i.normal, i.lightDir));

                    half4 c;
                    c.rgb = _SpecColor * ( _LightColor0.rgb * diff) * (atten* 2); // Diffuse and specular.
                    return c;
                }
            ENDCG
        }
 
    }
 
   Fallback "VertexLit"
}