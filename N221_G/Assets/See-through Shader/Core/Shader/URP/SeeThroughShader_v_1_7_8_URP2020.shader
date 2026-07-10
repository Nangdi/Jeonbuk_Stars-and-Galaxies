Shader "Custom/SeeThroughShaderURP2020"
{
   Properties
   {
      [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
      [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
      [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
      
	        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _TextureVisibility("TextureVisibility", Range(0,1)) = 1
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0
        [Gamma] _Metallic("Metallic", Range(0,1)) = 0.0
        _MetallicGlossMap("Gloss Map", 2D) = "white" {}
        _BumpScale ("Bump Scale", Float) = 1.0
        [Normal] _BumpMap ("Normal Map", 2D) = "bump" {}
        _Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
        _ParallaxMap ("Height Map", 2D) = "black" {}
        _OcclusionStrength("Occlusion Strength", Range(0, 1)) = 1.0
        _OcclusionMap ("Occlusion", 2D) = "white" {}
        _EmissionMap ("Emission", 2D) = "black" {}
		_EmissionColor ("Color", Color) = (0, 0, 0)
        _DetailMask("Detail Mask", 2D) = "white" {}
        _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
        _DetailNormalMapScale ("Scale", Float) = 1.0
        [Normal] _DetailNormalMap ("Normal Map", 2D) = "bump" {}
        [Enum(UV0,0,UV1,1)] _UVSec("UV Set for secondary textures", Float) = 0
        [HideInInspector] _Mode ("__mode", Float) = 0.0
	    [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
        _DissolveColor("Dissolve Color", Color) = (1,1,1,1)
        _DissolveColorSaturation("Dissolve Color Saturation", Range(0,1)) = 1.0
        _DissolveEmission("Dissolve Emission", Range(0,1)) = 1.0
        [AbsoluteValue()] _DissolveEmissionBooster("Dissolve Emission Booster", float) = 1
        _DissolveTex("Dissolve Effect Texture", 2D) = "white" {}
        [Enum(STSInteractionMode)] _InteractionMode ("Interaction Mode", Float) = 0
        [Enum(ObstructionMode)] _Obstruction ("Obstruction Mode", Float) = 0
        _AngleStrength("Angle Obstruction Strength", Range(0,1)) = 1.0
        _ConeStrength ("Cone Obstruction Strength", Range(0,1)) = 1.0
        _ConeObstructionDestroyRadius ("Cone Obstruction Destroy Radius", float) = 10.0
        _CylinderStrength ("Cylinder Obstruction Strength", Range(0,1)) = 1.0
        _CylinderObstructionDestroyRadius ("Cylinder Obstruction Destroy Radius", float) = 10.0
        _CircleStrength ("Circle Obstruction Strength", Range(0,1)) = 1.0
        _CircleObstructionDestroyRadius ("Circle Obstruction Destroy Radius", float) = 10.0
        _CurveStrength ("Curve Obstruction Strength", Range(0,1)) = 1.0
        _CurveObstructionDestroyRadius ("Curve Obstruction Destroy Radius", float) = 10.0
        [HideInInspector] _ObstructionCurve("Obstruction Curve", 2D) = "white" {}
        _DissolveFallOff("Dissolve FallOff", Range(0,1)) = 0.0
        _DissolveMask("Dissolve Mask", 2D) = "white" {}
        _DissolveMaskEnabled("Use DissolveMask", float) = 0.0
        _IntrinsicDissolveStrength("Intrinsic Dissolve Strength", Range(0,1)) = 0.0
        [MaterialToggle] _PreviewMode("Preview Mode", float) = 0.0
        _PreviewIndicatorLineThickness("Indicator Line Thickness",  Range(0.01,0.5)) = 0.04
        [AbsoluteValue()] _UVs ("Dissolve Texture Scale", float) = 1.0
        [MaterialToggle] _hasClippedShadows("Has Clipped Shadows", Float) = 0
        [MaterialToggle] _Floor ("Floor", float) = 1.0
        [Enum(FloorMode)] _FloorMode ("Floor Mode", Float) = 0
        _FloorY ("FloorY",  float) = 1.0
        _PlayerPosYOffset ("PlayerPos Y Offset", float) = 1.0  
        [AbsoluteValue()] _FloorYTextureGradientLength ("FloorY Texture Gradient Length", float) = 0.1  
        [MaterialToggle] _AnimationEnabled("Animation Enabled", Float) = 0
        _AnimationSpeed("Animation Speed", Range(0,2)) = 1
        [MaterialToggle] _IsReplacementShader ("hidden: _IsReplacementShader", Float) = 0
        [HideInInspector] _RaycastMode ("hidden: _RaycastMode", Float) = 0
        [HideInInspector] _TriggerMode ("hidden: _TriggerMode", Float) = 0
        [HideInInspector] _IsExempt ("_IsExempt", Float) = 0
        [AbsoluteValue()] _TransitionDuration ("Transition Duration In Seconds", Float) = 2
        [AbsoluteValue()] _DefaultEffectRadius ("Default Effect Radius",Float) = 25
        [HideInInspector] _numOfPlayersInside ("hidden: _numOfPlayersInside", Float) = 0
        [HideInInspector] _tValue ("hidden: _tValue", Float) = 0
        [HideInInspector] _tDirection ("hidden: _tDirection", Float) = 0
        [HideInInspector] _id ("hidden: _id", Float) = 0
        [MaterialToggle] _TexturedEmissionEdge("Textured Emission Edge", float) = 1.0
        _TexturedEmissionEdgeStrength("Textured Emission Edge Strength", Range(0,1)) = 0.3
        [MaterialToggle] _IsometricExclusion("Isometric Exclusion", float) = 0.0
        _IsometricExclusionDistance("Isometric Exclusion Distance", float) = 0.0
        _IsometricExclusionGradientLength("Isometric Exclusion Gradient Length", float) = 0.1
        [MaterialToggle] _Ceiling ("Ceiling", float) = 0.0
        [Enum(CeilingMode)] _CeilingMode ("Ceiling Mode", Float) = 0
        [Enum(CeilingBlendMode)] _CeilingBlendMode ("Blending Mode", Float) = 1.0
        _CeilingY ("CeilingY",  float) = 1.0
        _CeilingPlayerYOffset ("PlayerPos Y Offset", float) = 1.0  
        _CeilingYGradientLength ("CeilingY Gradient Length", float) = 0.1
        [MaterialToggle] _Zoning("Zoning", float) = 0.0
        [Enum(ZoningMode)] _ZoningMode("Zoning Mode", Float) = 0.0
        _ZoningEdgeGradientLength ("Edge Gradient Length", float) = 0.1
        [MaterialToggle] _IsZoningRevealable ("Is Zoning Revealable", float) = 0.0
        [MaterialToggle] _SyncZonesWithFloorY ("Sync Zones With FloorY", float) = 0.0
        _SyncZonesFloorYOffset ("Sync Zones Floor YOffset", float) = 0.0
        [MaterialToggle] _isReferenceMaterial("Is Reference Material", float) = 0.0



   }
   SubShader
   {
      Tags { "RenderPipeline"="UniversalPipeline" "RenderType" = "Opaque" "UniversalMaterialType" = "Lit" "Queue" = "Geometry" }

      

      
        Pass
        {
            Name "Universal Forward"
            Tags 
            { 
                "LightMode" = "UniversalForward"
            }
            Cull Back
            Blend One Zero
            ZTest LEqual
            ZWrite On

            Blend One Zero, One Zero
Cull Back
ZTest LEqual
ZWrite On

            

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON
        
            // Keywords
            #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
            #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            // GraphKeywords: <None>

            #define SHADER_PASS SHADERPASS_FORWARD
            #define SHADERPASS_FORWARD
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define _PASSFORWARD 1
            

            
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _METALLICGLOSSMAP
        #pragma shader_feature_local _PARALLAXMAP
        #pragma shader_feature_local _DETAIL_MULX2
        #pragma shader_feature_local _EMISSION       
        #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma shader_feature_local_fragment _OBSTRUCTION_CURVE

        #pragma shader_feature_local_fragment _DISSOLVEMASK
	        #pragma shader_feature_local_fragment _ZONING
        #pragma shader_feature_local_fragment _REPLACEMENT
        #pragma shader_feature_local_fragment _PLAYERINDEPENDENT



   #define _URP 1
#define _USINGTEXCOORD1 1


            // this has to be here or specular color will be ignored. Not in SG code
            #if _SIMPLELIT
               #define _SPECULAR_COLOR
            #endif


            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"


        

               #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
         // float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
         // half4 vertexColor : COLOR;
         // #endif

         // #if %EXTRAV2F0REQUIREKEY%
         // float4 extraV2F0 : TEXCOORD12;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
         // float4 extraV2F1 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
         // float4 extraV2F2 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
         // float4 extraV2F3 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
         // float4 extraV2F4 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD19;
         // #endif
            
         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD9;
         #endif
            float4 fogFactorAndVertexLight : TEXCOORD10;
            float4 shadowCoord : TEXCOORD11;
         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


         
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
               // float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetObjectToWorldMatrix()
               #undef GetWorldToObjectMatrix()
               #undef GetWorldToViewMatrix()
               #undef UNITY_MATRIX_I_V
               #undef UNITY_MATRIX_P
               #undef GetWorldToHClipMatrix()
               #undef GetObjectToWorldMatrix()V
               #undef UNITY_MATRIX_T_MV
               #undef UNITY_MATRIX_IT_MV
               #undef GetObjectToWorldMatrix()VP

               #define GetObjectToWorldMatrix()     unity_ObjectToWorld
               #define GetWorldToObjectMatrix()   unity_WorldToObject
               #define GetWorldToViewMatrix()     unity_MatrixV
               #define UNITY_MATRIX_I_V   unity_MatrixInvV
               #define GetViewToHClipMatrix()     OptimizeProjectionMatrix(glstate_matrix_projection)
               #define GetWorldToHClipMatrix()    unity_MatrixVP
               #define GetObjectToWorldMatrix()V    mul(GetWorldToViewMatrix(), GetObjectToWorldMatrix())
               #define UNITY_MATRIX_T_MV  transpose(GetObjectToWorldMatrix()V)
               #define UNITY_MATRIX_IT_MV transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V))
               #define GetObjectToWorldMatrix()VP   mul(GetWorldToHClipMatrix(), GetObjectToWorldMatrix())


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
         CBUFFER_START(UnityPerMaterial)

            
        float4 _MainTex_ST;
        float4 _DetailAlbedoMap_ST;
	        half4 _Color;
        half _Cutoff; 
        half _Mode;
        float _Cull;
        half _BumpScale; 
        half _DetailNormalMapScale;
        half _Metallic;
        float _Glossiness;
        float _GlossMapScale;
        half _OcclusionStrength;
        half _Parallax;
        half _UVSec;
        half3 _EmissionColor;
        float _IsReplacementShader;
        float _TriggerMode;
        float _RaycastMode;
        float _IsExempt;
        float _isReferenceMaterial;
        float _InteractionMode;
        int _ArrayLength = 0;
        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
            float4 _PlayersPosVectorArray[20];
            float _PlayersDataFloatArray[150];     
        #else
            float4 _PlayersPosVectorArray[100];
            float _PlayersDataFloatArray[500];  
        #endif
        float _tDirection = 0;
        float _numOfPlayersInside = 0;
        float _tValue = 0;
        float _id = 0;
        #if _ZONING
            #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
                float _ZDFA[500];
            #else
                float _ZDFA[1000];
            #endif
            float _ZonesDataCount;
        #endif
        #if _REPLACEMENT        
            half4 _DissolveColorGlobal;
            float _DissolveColorSaturationGlobal;
            float _DissolveEmissionGlobal;
            float _DissolveEmissionBoosterGlobal;
            float _TextureVisibilityGlobal;
            float _ObstructionGlobal;
            float _AngleStrengthGlobal;
            float _ConeStrengthGlobal;
            float _ConeObstructionDestroyRadiusGlobal;
            float _CylinderStrengthGlobal;
            float _CylinderObstructionDestroyRadiusGlobal;
            float _CircleStrengthGlobal;
            float _CircleObstructionDestroyRadiusGlobal;
            float _CurveStrengthGlobal;
            float _CurveObstructionDestroyRadiusGlobal;
            float _DissolveFallOffGlobal;
            float _IntrinsicDissolveStrengthGlobal;
            float _PreviewModeGlobal;
            float _UVsGlobal;
            float _hasClippedShadowsGlobal;
            float _FloorGlobal;
            float _FloorModeGlobal;
            float _FloorYGlobal;
            float _PlayerPosYOffsetGlobal;
            float _FloorYTextureGradientLengthGlobal;
            float _AnimationEnabledGlobal;
            float _AnimationSpeedGlobal;
            float _DefaultEffectRadiusGlobal;
            float _TransitionDurationGlobal;        
            float _TexturedEmissionEdgeGlobal;
            float _TexturedEmissionEdgeStrengthGlobal;
            float _IsometricExclusionGlobal;
            float _IsometricExclusionDistanceGlobal;
            float _IsometricExclusionGradientLengthGlobal;
            float _CeilingGlobal;
            float _CeilingModeGlobal;
            float _CeilingBlendModeGlobal;
            float _CeilingYGlobal;
            float _CeilingPlayerYOffsetGlobal;
            float _CeilingYGradientLengthGlobal;
            float _ZoningGlobal;
            float _ZoningModeGlobal;
            float _ZoningEdgeGradientLengthGlobal;
            float _IsZoningRevealableGlobal;
            float _SyncZonesWithFloorYGlobal;
            float _SyncZonesFloorYOffsetGlobal;
            float4 _ObstructionCurveGlobal_TexelSize;
            float4 _DissolveMaskGlobal_TexelSize;
            float _DissolveMaskEnabledGlobal;
            float _PreviewIndicatorLineThicknessGlobal;
        #else
            half _TextureVisibility;
            half _AngleStrength;
            float _Obstruction;
            float _UVs;
            float4 _ObstructionCurve_TexelSize;      
            float _DissolveMaskEnabled;
            float4 _DissolveMask_TexelSize;
            half4 _DissolveColor;
            float _DissolveColorSaturation;
            float _DissolveEmission;
            float _DissolveEmissionBooster;
            float _hasClippedShadows;
            float _ConeStrength;
            float _ConeObstructionDestroyRadius;
            float _CylinderStrength;
            float _CylinderObstructionDestroyRadius;
            float _CircleStrength;
            float _CircleObstructionDestroyRadius;
            float _CurveStrength;
            float _CurveObstructionDestroyRadius;
            float _IntrinsicDissolveStrength;
            float _DissolveFallOff;
            float _PreviewMode;
            float _PreviewIndicatorLineThickness;
            float _AnimationEnabled;
            float _AnimationSpeed;
            float _DefaultEffectRadius;
            float _TransitionDuration;
            float _TexturedEmissionEdge;
            float _TexturedEmissionEdgeStrength;
            float _IsometricExclusion;
            float _IsometricExclusionDistance;
            float _IsometricExclusionGradientLength;
            float _Floor;
            float _FloorMode;
            float _FloorY;
            float _FloorYTextureGradientLength;
            float _PlayerPosYOffset;
            float _Ceiling;
            float _CeilingMode;
            float _CeilingBlendMode;
            float _CeilingY;
            float _CeilingPlayerYOffset;
            float _CeilingYGradientLength;
            float _Zoning;
            float _ZoningMode;
            float _ZoningEdgeGradientLength;
            float _IsZoningRevealable;
            float _SyncZonesWithFloorY;
            float _SyncZonesFloorYOffset;
        #endif




         CBUFFER_END

         

         

         #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()

	        sampler2D _MainTex;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailMask;
            sampler2D _BumpMap;
            sampler2D _DetailNormalMap;
            sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
            sampler2D _ParallaxMap;
            sampler2D _EmissionMap;
        #if _REPLACEMENT
            sampler2D _DissolveTexGlobal;
        #else
            sampler2D _DissolveTex;
        #endif
        #if _DISSOLVEMASK
            #if _REPLACEMENT
                sampler2D _DissolveMaskGlobal;
            #else
                sampler2D _DissolveMask;
            #endif
        #endif
        #if _REPLACEMENT
            sampler2D _ObstructionCurveGlobal;
        #else
            sampler2D _ObstructionCurve;
        #endif


	    #if _URP
        half2 ParallaxOffset1Step (half h, half height, half3 viewDir)
        {
            h = h * height - height/2.0;
            half3 v = normalize(viewDir);
            v.z += 0.42;
            return h * (v.xy / v.z);
        }
        #ifdef UNITY_COLORSPACE_GAMMA
            #define unity_ColorSpaceDouble half4(2.0, 2.0, 2.0, 2.0)
        #else 
            #define unity_ColorSpaceDouble half4(4.59479380, 4.59479380, 4.59479380, 2.0)
        #endif
        half3 BlendNormals(half3 n1, half3 n2)
        {
            return normalize(half3(n1.xy + n2.xy, n1.z*n2.z));
        }
    #endif


	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
                  bool l0;
            #if _REPLACEMENT
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDurationGlobal ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #else
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDuration ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #endif
            bool ll0 = !_TriggerMode && !_RaycastMode;
            float lll0 = 0;
            half4 llll0 = half4(0,0,0,0);
            if(!_IsExempt && (l0 || ll0) ) {
                half4 lllll0;
                float llllll0;
                float lllllll0;
                float llllllll0;
                half lllllllll0;
                float llllllllll0;
                half lllllllllll0;
                float llllllllllll0;
                float lllllllllllll0;
                float llllllllllllll0;
                float lllllllllllllll0;
                float llllllllllllllll0;
                float lllllllllllllllll0;                
                float llllllllllllllllll0;
                float lllllllllllllllllll0;
                float llllllllllllllllllll0;
                float lllllllllllllllllllll0;
                float llllllllllllllllllllll0;
                float lllllllllllllllllllllll0;
                float llllllllllllllllllllllll0;
                float lllllllllllllllllllllllll0;
                float llllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllllll0;
                float l1;
                float ll1;
                float lll1;
                float llll1;
                float lllll1;
                float4 llllll1;
                float lllllll1;
                float llllllll1;
                float lllllllll1;
                float llllllllll1;
                float lllllllllll1;
                float llllllllllll1;
                float lllllllllllll1;
                float llllllllllllll1;
                float lllllllllllllll1;
                float llllllllllllllll1;
                float lllllllllllllllll1;
                float llllllllllllllllll1;
                float lllllllllllllllllll1;
                float llllllllllllllllllll1;
                float lllllllllllllllllllll1;
                float llllllllllllllllllllll1;
                float4 lllllllllllllllllllllll1;
                #if _REPLACEMENT
                    lllll0 = _DissolveColorGlobal;
                    llllll0 = _DissolveColorSaturationGlobal;
                    lllllll0 = _DissolveEmissionGlobal;
                    llllllll0 = _DissolveEmissionBoosterGlobal;
                    lllllllll0 = _TextureVisibilityGlobal;
                    llllllllll0 = _ObstructionGlobal;
                    lllllllllll0 = _AngleStrengthGlobal;
                    llllllllllll0 = _ConeStrengthGlobal;
                    lllllllllllll0 = _ConeObstructionDestroyRadiusGlobal;
                    llllllllllllll0 = _CylinderStrengthGlobal;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadiusGlobal;
                    llllllllllllllll0 = _CircleStrengthGlobal;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadiusGlobal;
                    llllllllllllllllll0 = _CurveStrengthGlobal;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadiusGlobal;
                    llllllllllllllllllll0 = _DissolveFallOffGlobal;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrengthGlobal;
                    llllllllllllllllllllll0 = _PreviewModeGlobal;
                    lllllllllllllllllllllll0 = _UVsGlobal;
                    llllllllllllllllllllllll0 = _hasClippedShadowsGlobal;                    
                    lllllllllllllllllllllllll0 = _FloorGlobal;
                    llllllllllllllllllllllllll0 = _FloorModeGlobal;
                    lllllllllllllllllllllllllll0 = _FloorYGlobal;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffsetGlobal;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLengthGlobal; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadiusGlobal;
                    lllllllllllllllllllllllllllllll0 = _TransitionDurationGlobal;
                    l1 = _AnimationEnabledGlobal;
                    ll1 = _AnimationSpeedGlobal;
                    lll1 = _TexturedEmissionEdgeGlobal;           
                    llll1 = _TexturedEmissionEdgeStrengthGlobal;
                    lllll1 = _DissolveMaskEnabledGlobal;
                    llllll1 = _DissolveMaskGlobal_TexelSize;
                    lllllll1 = _IsometricExclusionGlobal;
                    llllllll1 = _IsometricExclusionDistanceGlobal;
                    lllllllll1 = _IsometricExclusionGradientLengthGlobal;
                    llllllllll1 = _CeilingGlobal;
                    lllllllllll1 = _CeilingModeGlobal;
                    llllllllllll1 = _CeilingBlendModeGlobal;
                    lllllllllllll1 = _CeilingYGlobal;
                    llllllllllllll1 = _CeilingPlayerYOffsetGlobal;
                    lllllllllllllll1 = _CeilingYGradientLengthGlobal;
                    llllllllllllllll1 = _ZoningGlobal;
                    lllllllllllllllll1 = _ZoningModeGlobal;
                    llllllllllllllllll1 = _ZoningEdgeGradientLengthGlobal;
                    lllllllllllllllllll1 = _IsZoningRevealableGlobal;
                    llllllllllllllllllll1 = _SyncZonesWithFloorYGlobal;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffsetGlobal;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThicknessGlobal;
                    lllllllllllllllllllllll1 = _ObstructionCurveGlobal_TexelSize;
                #else
                    lllll0 = _DissolveColor;
                    llllll0 = _DissolveColorSaturation;
                    lllllll0 = _DissolveEmission;
                    llllllll0 = _DissolveEmissionBooster;
                    lllllllll0 = _TextureVisibility;
                    llllllllll0 = _Obstruction;
                    lllllllllll0 = _AngleStrength;
                    llllllllllll0 = _ConeStrength;
                    lllllllllllll0 = _ConeObstructionDestroyRadius;
                    llllllllllllll0 = _CylinderStrength;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadius;
                    llllllllllllllll0 = _CircleStrength;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadius;
                    llllllllllllllllll0 = _CurveStrength;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadius;
                    llllllllllllllllllll0 = _DissolveFallOff;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrength;
                    llllllllllllllllllllll0 = _PreviewMode;
                    lllllllllllllllllllllll0 = _UVs;
                    llllllllllllllllllllllll0 = _hasClippedShadows;                    
                    lllllllllllllllllllllllll0 = _Floor;
                    llllllllllllllllllllllllll0 = _FloorMode;
                    lllllllllllllllllllllllllll0 = _FloorY;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffset;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLength; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadius;
                    lllllllllllllllllllllllllllllll0 = _TransitionDuration;
                    l1 = _AnimationEnabled;
                    ll1 = _AnimationSpeed;
                    lll1 = _TexturedEmissionEdge;           
                    llll1 = _TexturedEmissionEdgeStrength;
                    lllll1 = _DissolveMaskEnabled;
                    llllll1 = _DissolveMask_TexelSize;
                    lllllll1 = _IsometricExclusion;
                    llllllll1 = _IsometricExclusionDistance;
                    lllllllll1 = _IsometricExclusionGradientLength;
                    llllllllll1 = _Ceiling;
                    lllllllllll1 = _CeilingMode;
                    llllllllllll1 = _CeilingBlendMode;
                    lllllllllllll1 = _CeilingY;
                    llllllllllllll1 = _CeilingPlayerYOffset;
                    lllllllllllllll1 = _CeilingYGradientLength;
                    llllllllllllllll1 = _Zoning;
                    lllllllllllllllll1 = _ZoningMode;
                    llllllllllllllllll1 = _ZoningEdgeGradientLength;
                    lllllllllllllllllll1 = _IsZoningRevealable;
                    llllllllllllllllllll1 = _SyncZonesWithFloorY;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffset;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThickness;
                    lllllllllllllllllllllll1 = _ObstructionCurve_TexelSize;
                #endif
                if(lllllllllllllllllllll0 < 0) {
                    lllllllllllllllllllll0 = 0;
                }
                float3 llllllllllllllllllllllll1;
                d.worldSpaceNormal = mul(o.Normal, (float3x3)d.TBNMatrix);
                float3 lllllllllllllllllllllllll1 = d.worldSpacePosition / (-1.0 * abs(lllllllllllllllllllllll0) );
                if(l1) {
                    lllllllllllllllllllllllll1 = lllllllllllllllllllllllll1 + abs(((_Time.y) * ll1));
                }  
                float3 llllllllllllllllllllllllll1 = float3(0,0,0);
                float3 lllllllllllllllllllllllllll1 = float3(0,0,0);
                float3 llllllllllllllllllllllllllll1 = float3(0,0,0);
                #if _REPLACEMENT
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xy ).rgb;
                #else
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xy ).rgb;
                #endif
                float lllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.x);
                float llllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.z);
                float3 lllllllllllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllll1, llllllllllllllllllllllllll1, lllllllllllllllllllllllllllll1 ).rgb;
                llllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllllllll1, llllllllllllllllllllllllllll1, llllllllllllllllllllllllllllll1 ).rgb;
                half l2 = llllllllllllllllllllllll1.r;  
                float3 ll2 = GetWorldToViewMatrix()[2].xyz;
                #if _HDRP
                    ll2 =  mul(GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);
                #else
                    ll2 = - mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                #endif
                float lll2 = 0;
                float llll2 = 1;
                bool lllll2 = false;
                float llllll2 = 0;
                float lllllll2 = 0;
                float llllllll2 = 0;
                float lllllllll2 = 0;
                float llllllllll2 = 0;
                float lllllllllll2 = 0;
                #if _ZONING
                    if(llllllllllllllll1) {
                        float llllllllllll2 = 0;
                        for (int z = 0; z < _ZonesDataCount; z++){
                            bool lllllllllllll2 = false;
                            float llllllllllllll2 = llllllllllll2;
                            if (_ZDFA[llllllllllll2 + 1] == 0) {  
                                float lllllllllllllll2 = llllllllllll2 + 2; 
                                float3 llllllllllllllll2 = d.worldSpacePosition - float3(_ZDFA[lllllllllllllll2],_ZDFA[lllllllllllllll2+1], _ZDFA[lllllllllllllll2+2]);
                                float3 lllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 3],_ZDFA[lllllllllllllll2+ 4], _ZDFA[lllllllllllllll2+ 5]);
                                float3 llllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 6],_ZDFA[lllllllllllllll2+ 7], _ZDFA[lllllllllllllll2+ 8]);
                                float3 lllllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 9],_ZDFA[lllllllllllllll2+10], _ZDFA[lllllllllllllll2+11]);
                                float3 llllllllllllllllllll2 = float3(_ZDFA[lllllllllllllll2+12],_ZDFA[lllllllllllllll2+13], _ZDFA[lllllllllllllll2+14]);
                                lllllllllllll2 =    abs(dot(llllllllllllllll2, lllllllllllllllll2)) <= llllllllllllllllllll2.x &&
                                            abs(dot(llllllllllllllll2, llllllllllllllllll2)) <= llllllllllllllllllll2.y &&
                                            abs(dot(llllllllllllllll2, lllllllllllllllllll2)) <= llllllllllllllllllll2.z;
                                if(lllllllllllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                                    llllllll2 = _ZDFA[lllllllllllllll2+1] - _ZDFA[lllllllllllllll2+13];  
                                    if(llllllllllllllllllllllllll0 == 0) {                                    
                                        bool lllllllllllllllllllll2 = ((llllllll2 - lllllllllllllllllllll1)  <= lllllllllllllllllllllllllll0); 
                                        if(!lllllllllllllllllllll2) {
                                            lllllllllllll2 = false;
                                        }
                                    }
                                }
                                llllllllllll2 = llllllllllll2 + 17 + 3; 
                                if(lllllllllllll2) {
                                    float llllllllllllllllllllll2 = llllllllllllllllllll2.x - abs(dot(llllllllllllllll2, lllllllllllllllll2));
                                    float lllllllllllllllllllllll2 = llllllllllllllllllll2.y - abs(dot(llllllllllllllll2, llllllllllllllllll2));
                                    float llllllllllllllllllllllll2 = llllllllllllllllllll2.z - abs(dot(llllllllllllllll2, lllllllllllllllllll2));
                                    lllllll2 = min(lllllllllllllllllllllll2,llllllllllllllllllllll2);
                                    lllllll2 = min(lllllll2,llllllllllllllllllllllll2);
                                    if(lllllll2<0) {
                                        lllllll2 = 0;
                                    }
                                }
                            }
                            if(lllllllllllll2) {
                                llllll2 = _ZDFA[llllllllllllll2];
                                lllll2 = true;    
                                lllllllll2 = _ZDFA[llllllllllllll2 + 17];
                                lllllllllll2 = _ZDFA[llllllllllllll2 + 18];
                                llllllllll2 = _ZDFA[llllllllllllll2 + 19];
                                break;                        
                            }
                        }
                    }
                #endif
                float lllllllllllllllllllllllll2 = 0;
                float llllllllllllllllllllllllll2 = lllll2;
                #if _PLAYERINDEPENDENT == 0
                    #if _ZONING
                        if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                            float lllllllllllllllllllllllllll2 = 0;
                            bool llllllllllllllllllllllllllll2 = false;
                            for (int i = 0; i < _ArrayLength; i++){
                                float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;               
                                if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {       
                                    if(!ll0) {
                                        float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                                        float l3 = 4;
                                        for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                            float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                            if (ll3 != 0 && ll3 == _id) {
                                                float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                if ((llll3 == -1 && _Time.y - lll3 < lllllllllllllllllllllllllllllll0 )|| (llll3 == 1) ) {
                                                    float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                                    if(llllllllllllllllllllll0) {
                                                        if(i==0) {
                                                            lllllllllllllllllllllllll2 = lllll3;
                                                        } else {
                                                            lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                                        }
                                                    }
                                                    bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                                    if(!llllll3) {
                                                        llllllllllllllllllllllllllll2 = true;
                                                    } 
                                                }                        
                                            }
                                        }
                                    } else if (distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition.xyz) < llllllllllllllllllllllllllllll0) {
                                        float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                        if(llllllllllllllllllllll0) {
                                            if(i==0) {
                                                lllllllllllllllllllllllll2 = lllll3;
                                            } else {
                                                lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                            }
                                        }
                                        bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                        if(!llllll3) {
                                            llllllllllllllllllllllllllll2 = true;
                                        } 
                                    }
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                                }
                            }
                            if(!llllllllllllllllllllllllllll2) {
                                lllll2 = false;
                            }
                        }
                    #endif
                    float lllllllllllllllllllllllllll2 = 0;
                    for (int i = 0; i < _ArrayLength; i++){
                        float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                        float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;                 
                        float llllllllllll3 = 0;
                        float l3 = 4;
                        if(!ll0) {
                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                if (ll3 != 0 && ll3 == _id) {
                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                    llllllllllll3 = 1;
                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < lllllllllllllllllllllllllllllll0) {
                                        if(llll3 == 1) {
                                            llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-lll3))/lllllllllllllllllllllllllllllll0);
                                        } else {
                                            llllllllllll3 = ((_Time.y-lll3)/lllllllllllllllllllllllllllllll0);
                                        }
                                    } else if(llll3 ==-1) {
                                        llllllllllll3 = 1;
                                    } else if(llll3 == 1) {
                                        llllllllllll3 = 0;
                                    } else {
                                        llllllllllll3 = 1;
                                    }
                                    llllllllllll3 = 1 - llllllllllll3;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                        float llllllllllllllllll3 = 0;
                        float lllllllllllllllllll3 = 0;     
                        float llllllllllllllllllll3 = lllllllllllllllllll3;
                        bool lllllllllllllllllllll3 = distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                        if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                        
                            #if _ZONING
                                if(llllllllllllllll1) {
                                    if(lllll2) 
                                    {
                                        if(lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 1;
                                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2]; llllll6++){
                                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                                if (ll3 != 0 && ll3 == llllll2) {
                                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                    llllllllllllllllll3 = 1;
                                                    float llllllllllllllllllllllllll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 3];
                                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                                        if(llll3 == 1) {
                                                            llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                                        } else {
                                                            llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                                        }
                                                    } else if(llll3 ==-1) {
                                                        llllllllllllllllll3 = 1;
                                                    } else if(llll3 == 1) {
                                                        llllllllllllllllll3 = 0;
                                                    } else {
                                                        llllllllllllllllll3 = 1;
                                                    }
                                                    llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                                }
                                                if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                                    if (lllllll2 < llllllllllllllllll1)  {
                                                        float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                        llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                                    }
                                                }
                                            }
                                        } else { 
                                        }
                                    } else {
                                    }
                                }
                            #endif
                            if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {    
                                if (llllllllll0 == 2 || llllllllll0 == 3 || llllllllll0 == 4 || llllllllll0 == 5 || llllllllll0 == 6 || llllllllll0 == 7) {
                                    float4 lllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float4 llllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float lllllllllllllllllllllllllllllll3 = 0;
                                    if(lllll1 || llllllllll0 == 6) {
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        #if _HDRP
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz), 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4 , _ProjectionParams.x);
                                        #else
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4);
                                        #endif
                                        llllllllllllllllllllllllllllll3.xy /= llllllllllllllllllllllllllllll3.w;
                                        llllllllllllllllllllllllllllll3.x *= l4;
                                        #if _HDRP
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(d.worldSpacePosition.xyz), 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4 , _ProjectionParams.x);
                                        #else
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(d.worldSpacePosition.xyz, 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4);
                                        #endif
                                        lllllllllllllllllllllllllllll3.xy /= lllllllllllllllllllllllllllll3.w;
                                        lllllllllllllllllllllllllllll3.x *= l4;
                                        #if _DISSOLVEMASK
                                            if(lllll1) {
                                                #if _REPLACEMENT
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMaskGlobal_TexelSize.z,_DissolveMaskGlobal_TexelSize.w);
                                                #else
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMask_TexelSize.z,_DissolveMask_TexelSize.w);
                                                #endif
                                            }
                                        #endif
                                    }
                                    float3 llllll4 = _WorldSpaceCameraPos - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 lllllll4 = normalize(llllll4);
                                    float llllllll4 = dot(d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, lllllll4);                           
                                    float lllllllll4 = 0;
                                    float llllllllll4 = 0;
                                    float2 lllllllllll4 = float2(0,0);
                                    if (llllllllll0 == 2 || llllllllll0 == 3) { 
                                        lllllllll4 = llllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllll4 = length(llllll4);
                                        float llllllllllllll4 = lllllllllllll0;
                                        float lllllllllllllll4 = (llllllll4 / lllllllllllll4) * llllllllllllll4;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            float llllllllllllllllll4 = llllllllllll4 < lllllllllllllll4;
                                            if(llllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 4 || llllllllll0 == 5 ) { 
                                        lllllllll4 = llllllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllllllllllll4 = lllllllllllllll0;
                                        float llllllllllllllllllllll4 = (llllllllllll4 < lllllllllllllllllllll4) && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if(llllllllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 6) { 
                                        lllllllll4 = llllllllllllllll0;
                                        float llllllllllllllllllllllllll4 = length(llllll4);
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        float llllllllllllllllllllllllllll4 = min(1,l4);
                                        float lllllllllllllllllllllllllllll4 =  distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy) < lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                        float llllllllllllllllllllllllllllll4 = (lllllllllllllllllllllllllllll4)  && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float lllllllllllllllllllllllllllllll4 = lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                            float llllllllllllllll4 = (2*lllllllllllllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if (llllllllllllllllllllllllllllll4) {
                                                float lll5 = (distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy)/(lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4));
                                                llllllllll4 = lll5;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } 
                                    else if (llllllllll0 == 7) { 
                                        #if _OBSTRUCTION_CURVE
                                            lllllllll4 = llllllllllllllllll0;
                                            float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                            float llllllllllllllllllllllllll4 = length(llllll4);
                                            float4 llllll5 = float4(0,0,0,0);
                                            float lllllll5 = lllllllllllllllllllllll1.z;
                                            float llllllll5 = (llllllll4/llllllllllllllllllllllllll4) * lllllll5;
                                            float4 lllllllll5 = float4(0,0,0,0);
                                            lllllllll5 = lllllllllllllllllllllll1;
                                            float2 llllllllll5 = (llllllll5+0.5) * lllllllll5.xy;
                                            #if _REPLACEMENT
                                                llllll5 = tex2D(_ObstructionCurveGlobal, llllllllll5);
                                            #else
                                                llllll5 = tex2D(_ObstructionCurve, llllllllll5);
                                            #endif
                                            float lllllllllll5 = llllll5.r * lllllllllllllllllll0;
                                            float llllllllllll5 = (llllllllllll4 < lllllllllll5) && llllllll4 > 0 ;
                                            #if _DISSOLVEMASK
                                                float llllllllllllllll4 = (2*lllllllllll5) / lllllllllllllllllllllllllllllll3;
                                                float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                                lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                                lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                            #else
                                                if(llllllllllll5){
                                                    float lllllllllllllllllll4 = llllllllllll4/lllllllllll5;
                                                    llllllllll4 = lllllllllllllllllll4;
                                                } else {
                                                    llllllllll4 = -1;
                                                }
                                            #endif
                                        #endif
                                    } 
                                    #if _DISSOLVEMASK
                                        if(lllll1) {
                                            float4 llllllllllllllll5 = float4(0,0,0,0);
                                            llllllllllllllll5 = llllll1;
                                            float2 lllllllllllllllll5 = float2(llllllllllllllll5.z/2,llllllllllllllll5.w/2);
                                            float2 llllllllllllllllll5 = lllllllllllllllll5 + lllllllllll4;
                                            float2 lllllllllllllllllll5 = (llllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                            float4 llllllllllllllllllll5 = float4(0,0,0,0);
                                            #if _REPLACEMENT
                                                llllllllllllllllllll5 = tex2D(_DissolveMaskGlobal, lllllllllllllllllll5);
                                            #else
                                                llllllllllllllllllll5 = tex2D(_DissolveMask, lllllllllllllllllll5);
                                            #endif
                                            float lllllllllllllllllllll5 = -1;
                                            if(llllllllllllllllll5.x <= llllllllllllllll5.z && llllllllllllllllll5.x >= 0 && llllllllllllllllll5.y <= llllllllllllllll5.w && llllllllllllllllll5.y >= 0 && llllllllllllllllllll5.x <= 0 && llllllll4 > 0 ){
                                                float llllllllllllllllllllll5 = sqrt(pow(llllllllllllllll5.z,2)+pow(llllllllllllllll5.w,2))/2;
                                                float lllllllllllllllllllllll5 = 40;
                                                float llllllllllllllllllllllll5 = llllllllllllllllllllll5/lllllllllllllllllllllll5;
                                                float lllllllllllllllllllllllll5 = 0;
                                                lllllllllllllllllllll5 = 0;     
                                                #if _REPLACEMENT
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMaskGlobal, float4(lllllllllllllllllllllllllll5, 0.0, 0.0));                           
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        } 
                                                    }   
                                                #else
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMask, float4(lllllllllllllllllllllllllll5, 0.0, 0.0)); 
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        }                                            
                                                    }   
                                                #endif
                                                lllllllllllllllllllll5 = 1-lllllllllllllllllllllllll5;  
                                            }         
                                            llllllllll4 = lllllllllllllllllllll5;
                                        }
                                    #endif
                                    if(llllllllllllllllllll0 <= 1) {
                                        if(llllllllll4 != -1) {
                                            float lll6 = max(llllllllllllllllllll0,0.00001);
                                            float llll6 = 1-lllllllll4;
                                            float lllll6 = exp(lll6*6);
                                            float llllll6 = llllllllll4;
                                            float lllllll6 = llll6/(lll6/(lll6*llll6-0.15*(lll6-llll6)));
                                            float llllllll6 = ((llllll6-lllllll6)/(lllll6*(1-llllll6)+llllll6))+lllllll6;
                                            llllllll6 = 1-llllllll6;
                                            lllllllllllllllllll3 =  llllllll6 * sign(lllllllll4);
                                        }
                                    } else {
                                        lllllllllllllllllll3 = llllllllll4;
                                    }
                                } 
                                if (llllllllll0 == 1 || llllllllll0 == 3 || llllllllll0 == 5) { 
                                    float lllllllll6 = distance(_WorldSpaceCameraPos, _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz);
                                    float llllllllll6 = distance(_WorldSpaceCameraPos, d.worldSpacePosition.xyz);
                                    float3 lllllllllll6 =  d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 llllllllllll6 =  d.worldSpaceNormal;
                                    float lllllllllllll6 = acos(dot(lllllllllll6,llllllllllll6)/(length(lllllllllll6)*length(llllllllllll6)));
                                    if(lllllllllllll6 <= 1.5 && lllllllll6 > llllllllll6) {
                                        float llllllllllllll6 = (sqrt((lllllllll6-llllllllll6)) * 25 /lllllllllllll6)*lllllllllll0;                   
                                        lllllllllllllllllll3 += max(0,log(llllllllllllll6*0.2));
                                    }
                                }
                            }
                            lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                            if(lllll2) { 
                                if(lllllllllllllllll1 == 1) { 
                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                    }
                                } 
                                else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                    float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                    } else {
                                        lllllllllllllllllll3 = 0;
                                    }
                                }
                            }
                            float lllllllllllllllllll6 = lllllllllllllllllll3 / lllllllll1;
                            if(lllllll1) {
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;
                                float3 lllllllllllllllllllll6 = d.worldSpacePosition.xyz - _WorldSpaceCameraPos;
                                float llllllllllllllllllllll6 = dot(lllllllllllllllllllll6, normalize(llllllllllllllllllllllllllllll2));
                                if(llllllllllllllllllllll6 - llllllll1 >= length(llllllllllllllllllllllllllllll2)) {
                                    float lllllllllllllllllllllll6 = llllllllllllllllllllll6 - llllllll1 - length(llllllllllllllllllllllllllllll2);
                                    if(lllllllllllllllllllllll6 < 0) {
                                        lllllllllllllllllllllll6 = 0;
                                    }
                                    if(lllllllllllllllllllllll6 < lllllllll1) 
                                    {
                                        lllllllllllllllllll3 = (lllllllll1 - lllllllllllllllllllllll6) * lllllllllllllllllll6;
                                    }
                                    else 
                                    {
                                        lllllllllllllllllll3 = 0; 
                                    }
                                }
                            }
                            if(llllllllllllllll1 && !lllll2) {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                            if(llllllllll1 == 1) {
                                float llllllllllllllllllllllll6 = 0;
                                if(llllllllllll1 == 0) {
                                    llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                                } else if (llllllllllll1 == 1) {  
                                    float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                        lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                    }
                                    llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                                }
                                if(lllllllllll1 == 1) {
        	                        if(d.worldSpacePosition.y > (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1)) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1);
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y > lllllllllllll1) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                }
                            }
                            if(lllllllllllllllllllllllll0 == 1) {
                                float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                                if(llllllllllllllllllllllllll0 == 1) {
        	                        if(d.worldSpacePosition.y < (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0)) {
                                        float lllllllllllllllllllllll6 = (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0) - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                        float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                }
                            }
                            if(!_TriggerMode && !_RaycastMode) {
                                if(distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                        if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                            llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                        }
                        if(_TriggerMode || _RaycastMode) {
                            lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                        } else {
                            lllllllllllllllllll3 = lllllllllllllllllll3;
                            if (llllllllllllllll1) {
                                if(lllll2) {
                                    if(lllllllllllllllllll1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                    }
                                } else {
                                    if(lllllllllllllllll1 == 1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                    }
                                }
                            }
                        }
                        lll2 = max(lll2,lllllllllllllllllll3);
                    }   
                #else
                    float llllllllllll3 = 0;
                    if(!ll0) {
                        llllllllllll3 = 1;
                        if( _tDirection!= 0 && _tValue != 0 && _Time.y-_tValue < lllllllllllllllllllllllllllllll0) {
                            if(_tDirection == 1) {
                                llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-_tValue))/lllllllllllllllllllllllllllllll0);
                            } else {
                                llllllllllll3 = ((_Time.y-_tValue)/lllllllllllllllllllllllllllllll0);
                            }
                        } else if(_tDirection ==-1) {
                            llllllllllll3 = 1;
                        } else if(_tDirection == 1) {
                            llllllllllll3 = 0;
                        } else {
                            llllllllllll3 = 1;
                        }
                        llllllllllll3 = 1 - llllllllllll3;
                    }
                    float lllllllllllllllllll3 = 0;     
                    float llllllllllllllllll3 = 0;
                    bool lllllllllllllllllllll3 = distance(_WorldSpaceCameraPos, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                    lllllllllllllllllllll3 = false;
                    if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                    
                        #if _ZONING
                            if(llllllllllllllll1) {
                                if(lllll2) 
                                {
                                    if(lllllllllllllllllll1) {
                                        float lll3 = lllllllll2;
                                        float llll3 = lllllllllll2;
                                        llllllllllllllllll3 = 1;
                                        float llllllllllllllllllllllllll3 = llllllllll2;
                                        if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                            if(llll3 == 1) {
                                                llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                            } else {
                                                llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                            }
                                        } else if(llll3 ==-1) {
                                            llllllllllllllllll3 = 1;
                                        } else if(llll3 == 1) {
                                            llllllllllllllllll3 = 0;
                                        } else {
                                            llllllllllllllllll3 = 1;
                                        }
                                        llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                        if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                            if (lllllll2 < llllllllllllllllll1)  {
                                                float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                            }
                                        }
                                    } else { 
                                    }
                                } else {
                                }
                            }
                        #endif
                        lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                        if(lllll2) {
                            if(lllllllllllllllll1 == 1) { 
                                float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                }
                            } 
                            else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                } else {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        if(llllllllllllllll1 && !lllll2) {
                            if(lllllllllllllllll1 == 1) {
                                lllllllllllllllllll3 = 0;
                            }
                        }
                        if(llllllllll1 == 1 && lllllllllll1 == 0) {
                            float llllllllllllllllllllllll6 = 0;
                            if(llllllllllll1 == 0) { 
                                llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                            } else if (llllllllllll1 == 1) {  
                                float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                    lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                }
                                llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                            }
                            if(d.worldSpacePosition.y > lllllllllllll1) {
                                float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(llllllllllll1 == 0) { 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                        lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 0;  
                                    }
                                } else {                                 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) { 
                                        lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 1;  
                                    }
                                    llllllllllllllllll3 = 1;
                                }
                            }
                        }
                        if(lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 0) {
                            float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                            if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                } else {
                                    lllllllllllllllllll3 = 0;  
                                }
                            }                                
                        }
                    } 
                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                        llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                    }
                    if(_TriggerMode || _RaycastMode) {
                        lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                    } else {
                        lllllllllllllllllll3 = lllllllllllllllllll3;
                        if (llllllllllllllll1) {
                            if(lllll2) {
                                if(lllllllllllllllllll1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                }
                            } else {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                }
                            }
                        }
                    }
                    lll2 = max(lll2,lllllllllllllllllll3);    
                #endif
                float llllllllllllllllllll3 = lll2;
                if(!llllllllllllllllllllll0) {
                    if (llllllllllllllllllll3 == 1) {
                        llllllllllllllllllll3 = 10;
                    }
                    if (!llllllllllllllllllllllll0 || llllllllll0 == 6) {
                    #if defined(UNITY_PASS_SHADOWCASTER) 
                    #if defined(SHADOWS_DEPTH) 
                    if (!any(unity_LightShadowBias))
                    {
                            clip(l2- llllllllllllllllllll3);
                    }
                    else
                    {
                        if(llllllllllllllllllllllll0 && llllllllll0 != 6) {
                            clip(l2- llllllllllllllllllll3);                  
                        }
                    }
                    #endif
                    #else
                        clip(l2- llllllllllllllllllll3);
                    #endif
                    } else {                
                        clip(l2- llllllllllllllllllll3);  
                    }
                }       
                if(llllllllllllllllllllll0) {
                    lll0 = 1;
                    if((l2 - llllllllllllllllllll3) < 0) {
                        llll0 = half4(1,1,1,1);
                        o.Emission = 1;
                    } else {
                        llll0 = half4(0,0,0,1);
                    }
                    if(llllllllllllllllllllllllll2) {
                        if((l2 - llllllllllllllllllll3) < 0) {
                            llll0 = half4(0.5,1,0.5,1);
                            o.Emission = 0;
                        } else {
                            llll0 = half4(0,0.1,0,1);
                        }
                    }
                    if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                        float lllllllllllllllllll7 = 0;
                        if(llllllllllllllllllllllllll0 == 1) {
                            lllllllllllllllllllllllll2 = lllllllllllllllllllllllll2 + lllllllllllllllllllll1;    
                            lllllllllllllllllll7 = lllllllllllllllllllllllll2;
                        } else {
                            lllllllllllllllllll7 = lllllllllllllllllllllllllll0 + lllllllllllllllllllll1;
                        }                        
                        if(d.worldSpacePosition.y > (lllllllllllllllllll7 - llllllllllllllllllllll1) && d.worldSpacePosition.y < (lllllllllllllllllll7 + llllllllllllllllllllll1)){
                            llll0 = half4(1,0,0,1);
                        }
                    }
                } else {
                    half3 llllllllllllllllllll7 = lerp(1, lllll0, llllll0).rgb;
                    if(lll1) {
                        llll1 = 0.2 + (llll1*(0.8-0.2)); 
                        o.Emission =  min(clamp(llllllllllllllllllll7 *  clamp(((llllllllllllllllllll3/llll1) - l2),0,1), 0, 1)*sqrt(lllllll0*llllllll0), clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0));
                    } else {
                        o.Emission =  clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0);
                    }
                    #if _HDRP
                        o.Emission =  o.Emission * pow(llllllll0,2);
                    #endif
                }
            }
            float4 lllllllllllllllllllll7;
            lllllllllllllllllllll7.xy = d.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
            lllllllllllllllllllll7.zw = (_UVSec == 0) ? d.texcoord0.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw : d.texcoord1.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
            #ifdef _PARALLAXMAP
                half llllllllllllllllllllll7 = tex2D (_ParallaxMap, lllllllllllllllllllll7.xy).g;
                float2 lllllllllllllllllllllll7 = ParallaxOffset1Step (llllllllllllllllllllll7, _Parallax, d.tangentSpaceViewDir);
                lllllllllllllllllllll7 = float4(lllllllllllllllllllll7.xy + lllllllllllllllllllllll7, lllllllllllllllllllll7.zw + lllllllllllllllllllllll7);
                o.Normal = float3(0, 0, 1);
            #endif
            if(lll0) {
                o.Albedo = llll0.rgb;
            } else {
                half3 llllllllllllllllllllllll7 = tex2D(_MainTex, lllllllllllllllllllll7.xy).rgb * _Color.rgb; 
                #ifdef _DETAIL_MULX2
                     half3 lllllllllllllllllllllllll7 = tex2D (_DetailAlbedoMap, lllllllllllllllllllll7.zw).rgb;
                     llllllllllllllllllllllll7 *= LerpWhiteTo (lllllllllllllllllllllllll7 * unity_ColorSpaceDouble.rgb, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
                #endif
                o.Albedo = llllllllllllllllllllllll7;
            }
            #ifdef _NORMALMAP
                half3 llllllllllll6 = UnpackScaleNormal(tex2D(_BumpMap, lllllllllllllllllllll7.xy), _BumpScale);
                #ifdef _DETAIL_MULX2
                    half3 lllllllllllllllllllllllllll7 = UnpackScaleNormal(tex2D(_DetailNormalMap, lllllllllllllllllllll7.zw), _DetailNormalMapScale);
	                lllllllllllllllllllllllllll7 = lerp(float3(0, 0, 1), lllllllllllllllllllllllllll7, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
	                llllllllllll6 = BlendNormals(llllllllllll6, lllllllllllllllllllllllllll7);
                #endif
                o.Normal = llllllllllll6;
            #endif
            half2 llllllllllllllllllllllllllll7 = half2(0,0);
            #ifdef _METALLICGLOSSMAP
                half4 lllllllllllllllllllllllllllll7 = tex2D(_MetallicGlossMap, lllllllllllllllllllll7.xy);
                llllllllllllllllllllllllllll7.r = lllllllllllllllllllllllllllll7.r;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g = tex2D(_MainTex, lllllllllllllllllllll7.xy).a;
                #else
                    llllllllllllllllllllllllllll7.g = lllllllllllllllllllllllllllll7.a;
                #endif
                llllllllllllllllllllllllllll7.g *= _GlossMapScale;
            #else
                llllllllllllllllllllllllllll7.r = _Metallic;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g  = tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _GlossMapScale;
                #else
                    llllllllllllllllllllllllllll7.g = _Glossiness;
                #endif
            #endif
            o.Metallic = llllllllllllllllllllllllllll7.r;
            o.Smoothness = llllllllllllllllllllllllllll7.g;
            o.Occlusion = lerp(1, tex2D(_OcclusionMap, lllllllllllllllllllll7.xy).g, _OcclusionStrength);
            #ifdef _EMISSION
                o.Emission += tex2D(_EmissionMap, lllllllllllllllllllll7.xy).rgb * _EmissionColor.rgb;
            #endif
            half llllllllllllllllllllllllllllll7 = 0;
            #if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
                llllllllllllllllllllllllllllll7 = _Color.a;
            #else
                llllllllllllllllllllllllllllll7 =  tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _Color.a;
            #endif
            if (_Mode) 
            {
                clip (llllllllllllllllllllllllllllll7 - _Cutoff);
            }
            o.Alpha = llllllllllllllllllllllllllllll7;

	}



        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                 //  Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                 // v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                 // v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                 // v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                 // v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                 // v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
               // d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
               // v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



         

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
            // d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
            // d.screenPos = i.screenPos;
            // d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
            // d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
            // d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
            // d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
            // d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
            // d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

         
         #if _PASSSHADOW
            float3 _LightDirection;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
           // o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);


          #if _PASSSHADOW
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, _LightDirection));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif


          // #if %SCREENPOSREQUIREKEY%
          // o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              #if _BAKEDLIT
                 half3 vertexLight = 0;
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
              #endif
              half fogFactor = ComputeFogFactor(o.pos.z);
              o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


         

         // fragment shader
         half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
         ) : SV_Target
         {
           UNITY_SETUP_INSTANCE_ID(IN);
           UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

           ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
           Surface l = (Surface)0;

           #ifdef _DEPTHOFFSET_ON
              l.outputDepth = outputDepth;
           #endif

           l.Albedo = half3(0.5, 0.5, 0.5);
           l.Normal = float3(0,0,1);
           l.Occlusion = 1;
           l.Alpha = 1;

           ChainSurfaceFunction(l, d);

           #ifdef _DEPTHOFFSET_ON
              outputDepth = l.outputDepth;
           #endif

           #if _USESPECULAR || _SIMPLELIT
              float3 specular = l.Specular;
              float metallic = 1;
           #else   
              float3 specular = 0;
              float metallic = l.Metallic;
           #endif


            
           
            InputData inputData;

            inputData.positionWS = IN.worldPos;
            #if _WORLDSPACENORMAL
              inputData.normalWS = l.Normal;
            #else
              inputData.normalWS = normalize(TangentToWorldSpace(d, l.Normal));
            #endif

            inputData.viewDirectionWS = SafeNormalize(d.worldSpaceViewDir);


            #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                  inputData.shadowCoord = IN.shadowCoord;
            #elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
                  inputData.shadowCoord = TransformWorldToShadowCoord(IN.worldPos);
            #else
                  inputData.shadowCoord = float4(0, 0, 0, 0);
            #endif

            inputData.fogCoord = IN.fogFactorAndVertexLight.x;
            inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;
            #if defined(_OVERRIDE_BAKEDGI)
               inputData.bakedGI = l.DiffuseGI;
               l.Emission += l.SpecularGI;
            #else
               inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.sh, inputData.normalWS);
            #endif
            inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.pos);
            #if !_BAKEDLIT
               inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUV);

               #if defined(_OVERRIDE_SHADOWMASK)
                  float4 mulColor = saturate(dot(l.ShadowMask, _MainLightOcclusionProbes)); //unity_OcclusionMaskSelector));
                  inputData.shadowMask = mulColor;
               #endif
            #endif

            #if !_UNLIT
               #if _SIMPLELIT
                  half4 color = UniversalFragmentBlinnPhong(
                     inputData,
                     l.Albedo,
                     float4(specular * l.Smoothness, 0),
                     l.SpecularPower * 128,
                     l.Emission,
                     l.Alpha);
                  color.a = l.Alpha;
               #elif _BAKEDLIT
                  color = UniversalFragmentBakedLit(inputData, l.Albedo, l.Alpha, normalTS);
               #else

                  
                  SurfaceData surface         = (SurfaceData)0;
                  surface.albedo              = l.Albedo;
                  surface.metallic            = saturate(metallic);
                  surface.specular            = specular;
                  surface.smoothness          = saturate(l.Smoothness),
                  surface.occlusion           = l.Occlusion,
                  surface.emission            = l.Emission,
                  surface.alpha               = saturate(l.Alpha);
                  surface.clearCoatMask       = 0;
                  surface.clearCoatSmoothness = 1;

                  #ifdef _CLEARCOAT
                      surface.clearCoatMask       = saturate(l.CoatMask);
                      surface.clearCoatSmoothness = saturate(l.CoatSmoothness);
                  #endif

                  half4 color = UniversalFragmentPBR(inputData, surface);

               #endif
               color.rgb = MixFog(color.rgb, inputData.fogCoord);

            #else
               half4 color = half4(l.Albedo, l.Alpha);
               
            #endif
            ChainFinalColorForward(l, d, color);

            return color;

         }

         ENDHLSL

      }


      
      
        Pass
        {
            Name "ShadowCaster"
            Tags 
            { 
                "LightMode" = "ShadowCaster"
            }
           
            // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>

            

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_instancing
        
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define SHADERPASS_SHADOWCASTER
            #define _PASSSHADOW 1

            
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _METALLICGLOSSMAP
        #pragma shader_feature_local _PARALLAXMAP
        #pragma shader_feature_local _DETAIL_MULX2
        #pragma shader_feature_local _EMISSION       
        #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma shader_feature_local_fragment _OBSTRUCTION_CURVE

        #pragma shader_feature_local_fragment _DISSOLVEMASK
	        #pragma shader_feature_local_fragment _ZONING
        #pragma shader_feature_local_fragment _REPLACEMENT
        #pragma shader_feature_local_fragment _PLAYERINDEPENDENT



   #define _URP 1
#define _USINGTEXCOORD1 1

                 
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
         // float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
         // half4 vertexColor : COLOR;
         // #endif

         // #if %EXTRAV2F0REQUIREKEY%
         // float4 extraV2F0 : TEXCOORD12;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
         // float4 extraV2F1 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
         // float4 extraV2F2 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
         // float4 extraV2F3 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
         // float4 extraV2F4 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD19;
         // #endif
            
         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD9;
         #endif
            float4 fogFactorAndVertexLight : TEXCOORD10;
            float4 shadowCoord : TEXCOORD11;
         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };

         
            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
               // float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetObjectToWorldMatrix()
               #undef GetWorldToObjectMatrix()
               #undef GetWorldToViewMatrix()
               #undef UNITY_MATRIX_I_V
               #undef UNITY_MATRIX_P
               #undef GetWorldToHClipMatrix()
               #undef GetObjectToWorldMatrix()V
               #undef UNITY_MATRIX_T_MV
               #undef UNITY_MATRIX_IT_MV
               #undef GetObjectToWorldMatrix()VP

               #define GetObjectToWorldMatrix()     unity_ObjectToWorld
               #define GetWorldToObjectMatrix()   unity_WorldToObject
               #define GetWorldToViewMatrix()     unity_MatrixV
               #define UNITY_MATRIX_I_V   unity_MatrixInvV
               #define GetViewToHClipMatrix()     OptimizeProjectionMatrix(glstate_matrix_projection)
               #define GetWorldToHClipMatrix()    unity_MatrixVP
               #define GetObjectToWorldMatrix()V    mul(GetWorldToViewMatrix(), GetObjectToWorldMatrix())
               #define UNITY_MATRIX_T_MV  transpose(GetObjectToWorldMatrix()V)
               #define UNITY_MATRIX_IT_MV transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V))
               #define GetObjectToWorldMatrix()VP   mul(GetWorldToHClipMatrix(), GetObjectToWorldMatrix())


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               
        float4 _MainTex_ST;
        float4 _DetailAlbedoMap_ST;
	        half4 _Color;
        half _Cutoff; 
        half _Mode;
        float _Cull;
        half _BumpScale; 
        half _DetailNormalMapScale;
        half _Metallic;
        float _Glossiness;
        float _GlossMapScale;
        half _OcclusionStrength;
        half _Parallax;
        half _UVSec;
        half3 _EmissionColor;
        float _IsReplacementShader;
        float _TriggerMode;
        float _RaycastMode;
        float _IsExempt;
        float _isReferenceMaterial;
        float _InteractionMode;
        int _ArrayLength = 0;
        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
            float4 _PlayersPosVectorArray[20];
            float _PlayersDataFloatArray[150];     
        #else
            float4 _PlayersPosVectorArray[100];
            float _PlayersDataFloatArray[500];  
        #endif
        float _tDirection = 0;
        float _numOfPlayersInside = 0;
        float _tValue = 0;
        float _id = 0;
        #if _ZONING
            #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
                float _ZDFA[500];
            #else
                float _ZDFA[1000];
            #endif
            float _ZonesDataCount;
        #endif
        #if _REPLACEMENT        
            half4 _DissolveColorGlobal;
            float _DissolveColorSaturationGlobal;
            float _DissolveEmissionGlobal;
            float _DissolveEmissionBoosterGlobal;
            float _TextureVisibilityGlobal;
            float _ObstructionGlobal;
            float _AngleStrengthGlobal;
            float _ConeStrengthGlobal;
            float _ConeObstructionDestroyRadiusGlobal;
            float _CylinderStrengthGlobal;
            float _CylinderObstructionDestroyRadiusGlobal;
            float _CircleStrengthGlobal;
            float _CircleObstructionDestroyRadiusGlobal;
            float _CurveStrengthGlobal;
            float _CurveObstructionDestroyRadiusGlobal;
            float _DissolveFallOffGlobal;
            float _IntrinsicDissolveStrengthGlobal;
            float _PreviewModeGlobal;
            float _UVsGlobal;
            float _hasClippedShadowsGlobal;
            float _FloorGlobal;
            float _FloorModeGlobal;
            float _FloorYGlobal;
            float _PlayerPosYOffsetGlobal;
            float _FloorYTextureGradientLengthGlobal;
            float _AnimationEnabledGlobal;
            float _AnimationSpeedGlobal;
            float _DefaultEffectRadiusGlobal;
            float _TransitionDurationGlobal;        
            float _TexturedEmissionEdgeGlobal;
            float _TexturedEmissionEdgeStrengthGlobal;
            float _IsometricExclusionGlobal;
            float _IsometricExclusionDistanceGlobal;
            float _IsometricExclusionGradientLengthGlobal;
            float _CeilingGlobal;
            float _CeilingModeGlobal;
            float _CeilingBlendModeGlobal;
            float _CeilingYGlobal;
            float _CeilingPlayerYOffsetGlobal;
            float _CeilingYGradientLengthGlobal;
            float _ZoningGlobal;
            float _ZoningModeGlobal;
            float _ZoningEdgeGradientLengthGlobal;
            float _IsZoningRevealableGlobal;
            float _SyncZonesWithFloorYGlobal;
            float _SyncZonesFloorYOffsetGlobal;
            float4 _ObstructionCurveGlobal_TexelSize;
            float4 _DissolveMaskGlobal_TexelSize;
            float _DissolveMaskEnabledGlobal;
            float _PreviewIndicatorLineThicknessGlobal;
        #else
            half _TextureVisibility;
            half _AngleStrength;
            float _Obstruction;
            float _UVs;
            float4 _ObstructionCurve_TexelSize;      
            float _DissolveMaskEnabled;
            float4 _DissolveMask_TexelSize;
            half4 _DissolveColor;
            float _DissolveColorSaturation;
            float _DissolveEmission;
            float _DissolveEmissionBooster;
            float _hasClippedShadows;
            float _ConeStrength;
            float _ConeObstructionDestroyRadius;
            float _CylinderStrength;
            float _CylinderObstructionDestroyRadius;
            float _CircleStrength;
            float _CircleObstructionDestroyRadius;
            float _CurveStrength;
            float _CurveObstructionDestroyRadius;
            float _IntrinsicDissolveStrength;
            float _DissolveFallOff;
            float _PreviewMode;
            float _PreviewIndicatorLineThickness;
            float _AnimationEnabled;
            float _AnimationSpeed;
            float _DefaultEffectRadius;
            float _TransitionDuration;
            float _TexturedEmissionEdge;
            float _TexturedEmissionEdgeStrength;
            float _IsometricExclusion;
            float _IsometricExclusionDistance;
            float _IsometricExclusionGradientLength;
            float _Floor;
            float _FloorMode;
            float _FloorY;
            float _FloorYTextureGradientLength;
            float _PlayerPosYOffset;
            float _Ceiling;
            float _CeilingMode;
            float _CeilingBlendMode;
            float _CeilingY;
            float _CeilingPlayerYOffset;
            float _CeilingYGradientLength;
            float _Zoning;
            float _ZoningMode;
            float _ZoningEdgeGradientLength;
            float _IsZoningRevealable;
            float _SyncZonesWithFloorY;
            float _SyncZonesFloorYOffset;
        #endif




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()

	        sampler2D _MainTex;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailMask;
            sampler2D _BumpMap;
            sampler2D _DetailNormalMap;
            sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
            sampler2D _ParallaxMap;
            sampler2D _EmissionMap;
        #if _REPLACEMENT
            sampler2D _DissolveTexGlobal;
        #else
            sampler2D _DissolveTex;
        #endif
        #if _DISSOLVEMASK
            #if _REPLACEMENT
                sampler2D _DissolveMaskGlobal;
            #else
                sampler2D _DissolveMask;
            #endif
        #endif
        #if _REPLACEMENT
            sampler2D _ObstructionCurveGlobal;
        #else
            sampler2D _ObstructionCurve;
        #endif


	    #if _URP
        half2 ParallaxOffset1Step (half h, half height, half3 viewDir)
        {
            h = h * height - height/2.0;
            half3 v = normalize(viewDir);
            v.z += 0.42;
            return h * (v.xy / v.z);
        }
        #ifdef UNITY_COLORSPACE_GAMMA
            #define unity_ColorSpaceDouble half4(2.0, 2.0, 2.0, 2.0)
        #else 
            #define unity_ColorSpaceDouble half4(4.59479380, 4.59479380, 4.59479380, 2.0)
        #endif
        half3 BlendNormals(half3 n1, half3 n2)
        {
            return normalize(half3(n1.xy + n2.xy, n1.z*n2.z));
        }
    #endif



	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
                  bool l0;
            #if _REPLACEMENT
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDurationGlobal ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #else
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDuration ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #endif
            bool ll0 = !_TriggerMode && !_RaycastMode;
            float lll0 = 0;
            half4 llll0 = half4(0,0,0,0);
            if(!_IsExempt && (l0 || ll0) ) {
                half4 lllll0;
                float llllll0;
                float lllllll0;
                float llllllll0;
                half lllllllll0;
                float llllllllll0;
                half lllllllllll0;
                float llllllllllll0;
                float lllllllllllll0;
                float llllllllllllll0;
                float lllllllllllllll0;
                float llllllllllllllll0;
                float lllllllllllllllll0;                
                float llllllllllllllllll0;
                float lllllllllllllllllll0;
                float llllllllllllllllllll0;
                float lllllllllllllllllllll0;
                float llllllllllllllllllllll0;
                float lllllllllllllllllllllll0;
                float llllllllllllllllllllllll0;
                float lllllllllllllllllllllllll0;
                float llllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllllll0;
                float l1;
                float ll1;
                float lll1;
                float llll1;
                float lllll1;
                float4 llllll1;
                float lllllll1;
                float llllllll1;
                float lllllllll1;
                float llllllllll1;
                float lllllllllll1;
                float llllllllllll1;
                float lllllllllllll1;
                float llllllllllllll1;
                float lllllllllllllll1;
                float llllllllllllllll1;
                float lllllllllllllllll1;
                float llllllllllllllllll1;
                float lllllllllllllllllll1;
                float llllllllllllllllllll1;
                float lllllllllllllllllllll1;
                float llllllllllllllllllllll1;
                float4 lllllllllllllllllllllll1;
                #if _REPLACEMENT
                    lllll0 = _DissolveColorGlobal;
                    llllll0 = _DissolveColorSaturationGlobal;
                    lllllll0 = _DissolveEmissionGlobal;
                    llllllll0 = _DissolveEmissionBoosterGlobal;
                    lllllllll0 = _TextureVisibilityGlobal;
                    llllllllll0 = _ObstructionGlobal;
                    lllllllllll0 = _AngleStrengthGlobal;
                    llllllllllll0 = _ConeStrengthGlobal;
                    lllllllllllll0 = _ConeObstructionDestroyRadiusGlobal;
                    llllllllllllll0 = _CylinderStrengthGlobal;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadiusGlobal;
                    llllllllllllllll0 = _CircleStrengthGlobal;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadiusGlobal;
                    llllllllllllllllll0 = _CurveStrengthGlobal;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadiusGlobal;
                    llllllllllllllllllll0 = _DissolveFallOffGlobal;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrengthGlobal;
                    llllllllllllllllllllll0 = _PreviewModeGlobal;
                    lllllllllllllllllllllll0 = _UVsGlobal;
                    llllllllllllllllllllllll0 = _hasClippedShadowsGlobal;                    
                    lllllllllllllllllllllllll0 = _FloorGlobal;
                    llllllllllllllllllllllllll0 = _FloorModeGlobal;
                    lllllllllllllllllllllllllll0 = _FloorYGlobal;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffsetGlobal;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLengthGlobal; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadiusGlobal;
                    lllllllllllllllllllllllllllllll0 = _TransitionDurationGlobal;
                    l1 = _AnimationEnabledGlobal;
                    ll1 = _AnimationSpeedGlobal;
                    lll1 = _TexturedEmissionEdgeGlobal;           
                    llll1 = _TexturedEmissionEdgeStrengthGlobal;
                    lllll1 = _DissolveMaskEnabledGlobal;
                    llllll1 = _DissolveMaskGlobal_TexelSize;
                    lllllll1 = _IsometricExclusionGlobal;
                    llllllll1 = _IsometricExclusionDistanceGlobal;
                    lllllllll1 = _IsometricExclusionGradientLengthGlobal;
                    llllllllll1 = _CeilingGlobal;
                    lllllllllll1 = _CeilingModeGlobal;
                    llllllllllll1 = _CeilingBlendModeGlobal;
                    lllllllllllll1 = _CeilingYGlobal;
                    llllllllllllll1 = _CeilingPlayerYOffsetGlobal;
                    lllllllllllllll1 = _CeilingYGradientLengthGlobal;
                    llllllllllllllll1 = _ZoningGlobal;
                    lllllllllllllllll1 = _ZoningModeGlobal;
                    llllllllllllllllll1 = _ZoningEdgeGradientLengthGlobal;
                    lllllllllllllllllll1 = _IsZoningRevealableGlobal;
                    llllllllllllllllllll1 = _SyncZonesWithFloorYGlobal;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffsetGlobal;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThicknessGlobal;
                    lllllllllllllllllllllll1 = _ObstructionCurveGlobal_TexelSize;
                #else
                    lllll0 = _DissolveColor;
                    llllll0 = _DissolveColorSaturation;
                    lllllll0 = _DissolveEmission;
                    llllllll0 = _DissolveEmissionBooster;
                    lllllllll0 = _TextureVisibility;
                    llllllllll0 = _Obstruction;
                    lllllllllll0 = _AngleStrength;
                    llllllllllll0 = _ConeStrength;
                    lllllllllllll0 = _ConeObstructionDestroyRadius;
                    llllllllllllll0 = _CylinderStrength;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadius;
                    llllllllllllllll0 = _CircleStrength;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadius;
                    llllllllllllllllll0 = _CurveStrength;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadius;
                    llllllllllllllllllll0 = _DissolveFallOff;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrength;
                    llllllllllllllllllllll0 = _PreviewMode;
                    lllllllllllllllllllllll0 = _UVs;
                    llllllllllllllllllllllll0 = _hasClippedShadows;                    
                    lllllllllllllllllllllllll0 = _Floor;
                    llllllllllllllllllllllllll0 = _FloorMode;
                    lllllllllllllllllllllllllll0 = _FloorY;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffset;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLength; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadius;
                    lllllllllllllllllllllllllllllll0 = _TransitionDuration;
                    l1 = _AnimationEnabled;
                    ll1 = _AnimationSpeed;
                    lll1 = _TexturedEmissionEdge;           
                    llll1 = _TexturedEmissionEdgeStrength;
                    lllll1 = _DissolveMaskEnabled;
                    llllll1 = _DissolveMask_TexelSize;
                    lllllll1 = _IsometricExclusion;
                    llllllll1 = _IsometricExclusionDistance;
                    lllllllll1 = _IsometricExclusionGradientLength;
                    llllllllll1 = _Ceiling;
                    lllllllllll1 = _CeilingMode;
                    llllllllllll1 = _CeilingBlendMode;
                    lllllllllllll1 = _CeilingY;
                    llllllllllllll1 = _CeilingPlayerYOffset;
                    lllllllllllllll1 = _CeilingYGradientLength;
                    llllllllllllllll1 = _Zoning;
                    lllllllllllllllll1 = _ZoningMode;
                    llllllllllllllllll1 = _ZoningEdgeGradientLength;
                    lllllllllllllllllll1 = _IsZoningRevealable;
                    llllllllllllllllllll1 = _SyncZonesWithFloorY;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffset;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThickness;
                    lllllllllllllllllllllll1 = _ObstructionCurve_TexelSize;
                #endif
                if(lllllllllllllllllllll0 < 0) {
                    lllllllllllllllllllll0 = 0;
                }
                float3 llllllllllllllllllllllll1;
                d.worldSpaceNormal = mul(o.Normal, (float3x3)d.TBNMatrix);
                float3 lllllllllllllllllllllllll1 = d.worldSpacePosition / (-1.0 * abs(lllllllllllllllllllllll0) );
                if(l1) {
                    lllllllllllllllllllllllll1 = lllllllllllllllllllllllll1 + abs(((_Time.y) * ll1));
                }  
                float3 llllllllllllllllllllllllll1 = float3(0,0,0);
                float3 lllllllllllllllllllllllllll1 = float3(0,0,0);
                float3 llllllllllllllllllllllllllll1 = float3(0,0,0);
                #if _REPLACEMENT
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xy ).rgb;
                #else
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xy ).rgb;
                #endif
                float lllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.x);
                float llllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.z);
                float3 lllllllllllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllll1, llllllllllllllllllllllllll1, lllllllllllllllllllllllllllll1 ).rgb;
                llllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllllllll1, llllllllllllllllllllllllllll1, llllllllllllllllllllllllllllll1 ).rgb;
                half l2 = llllllllllllllllllllllll1.r;  
                float3 ll2 = GetWorldToViewMatrix()[2].xyz;
                #if _HDRP
                    ll2 =  mul(GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);
                #else
                    ll2 = - mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                #endif
                float lll2 = 0;
                float llll2 = 1;
                bool lllll2 = false;
                float llllll2 = 0;
                float lllllll2 = 0;
                float llllllll2 = 0;
                float lllllllll2 = 0;
                float llllllllll2 = 0;
                float lllllllllll2 = 0;
                #if _ZONING
                    if(llllllllllllllll1) {
                        float llllllllllll2 = 0;
                        for (int z = 0; z < _ZonesDataCount; z++){
                            bool lllllllllllll2 = false;
                            float llllllllllllll2 = llllllllllll2;
                            if (_ZDFA[llllllllllll2 + 1] == 0) {  
                                float lllllllllllllll2 = llllllllllll2 + 2; 
                                float3 llllllllllllllll2 = d.worldSpacePosition - float3(_ZDFA[lllllllllllllll2],_ZDFA[lllllllllllllll2+1], _ZDFA[lllllllllllllll2+2]);
                                float3 lllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 3],_ZDFA[lllllllllllllll2+ 4], _ZDFA[lllllllllllllll2+ 5]);
                                float3 llllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 6],_ZDFA[lllllllllllllll2+ 7], _ZDFA[lllllllllllllll2+ 8]);
                                float3 lllllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 9],_ZDFA[lllllllllllllll2+10], _ZDFA[lllllllllllllll2+11]);
                                float3 llllllllllllllllllll2 = float3(_ZDFA[lllllllllllllll2+12],_ZDFA[lllllllllllllll2+13], _ZDFA[lllllllllllllll2+14]);
                                lllllllllllll2 =    abs(dot(llllllllllllllll2, lllllllllllllllll2)) <= llllllllllllllllllll2.x &&
                                            abs(dot(llllllllllllllll2, llllllllllllllllll2)) <= llllllllllllllllllll2.y &&
                                            abs(dot(llllllllllllllll2, lllllllllllllllllll2)) <= llllllllllllllllllll2.z;
                                if(lllllllllllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                                    llllllll2 = _ZDFA[lllllllllllllll2+1] - _ZDFA[lllllllllllllll2+13];  
                                    if(llllllllllllllllllllllllll0 == 0) {                                    
                                        bool lllllllllllllllllllll2 = ((llllllll2 - lllllllllllllllllllll1)  <= lllllllllllllllllllllllllll0); 
                                        if(!lllllllllllllllllllll2) {
                                            lllllllllllll2 = false;
                                        }
                                    }
                                }
                                llllllllllll2 = llllllllllll2 + 17 + 3; 
                                if(lllllllllllll2) {
                                    float llllllllllllllllllllll2 = llllllllllllllllllll2.x - abs(dot(llllllllllllllll2, lllllllllllllllll2));
                                    float lllllllllllllllllllllll2 = llllllllllllllllllll2.y - abs(dot(llllllllllllllll2, llllllllllllllllll2));
                                    float llllllllllllllllllllllll2 = llllllllllllllllllll2.z - abs(dot(llllllllllllllll2, lllllllllllllllllll2));
                                    lllllll2 = min(lllllllllllllllllllllll2,llllllllllllllllllllll2);
                                    lllllll2 = min(lllllll2,llllllllllllllllllllllll2);
                                    if(lllllll2<0) {
                                        lllllll2 = 0;
                                    }
                                }
                            }
                            if(lllllllllllll2) {
                                llllll2 = _ZDFA[llllllllllllll2];
                                lllll2 = true;    
                                lllllllll2 = _ZDFA[llllllllllllll2 + 17];
                                lllllllllll2 = _ZDFA[llllllllllllll2 + 18];
                                llllllllll2 = _ZDFA[llllllllllllll2 + 19];
                                break;                        
                            }
                        }
                    }
                #endif
                float lllllllllllllllllllllllll2 = 0;
                float llllllllllllllllllllllllll2 = lllll2;
                #if _PLAYERINDEPENDENT == 0
                    #if _ZONING
                        if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                            float lllllllllllllllllllllllllll2 = 0;
                            bool llllllllllllllllllllllllllll2 = false;
                            for (int i = 0; i < _ArrayLength; i++){
                                float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;               
                                if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {       
                                    if(!ll0) {
                                        float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                                        float l3 = 4;
                                        for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                            float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                            if (ll3 != 0 && ll3 == _id) {
                                                float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                if ((llll3 == -1 && _Time.y - lll3 < lllllllllllllllllllllllllllllll0 )|| (llll3 == 1) ) {
                                                    float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                                    if(llllllllllllllllllllll0) {
                                                        if(i==0) {
                                                            lllllllllllllllllllllllll2 = lllll3;
                                                        } else {
                                                            lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                                        }
                                                    }
                                                    bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                                    if(!llllll3) {
                                                        llllllllllllllllllllllllllll2 = true;
                                                    } 
                                                }                        
                                            }
                                        }
                                    } else if (distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition.xyz) < llllllllllllllllllllllllllllll0) {
                                        float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                        if(llllllllllllllllllllll0) {
                                            if(i==0) {
                                                lllllllllllllllllllllllll2 = lllll3;
                                            } else {
                                                lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                            }
                                        }
                                        bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                        if(!llllll3) {
                                            llllllllllllllllllllllllllll2 = true;
                                        } 
                                    }
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                                }
                            }
                            if(!llllllllllllllllllllllllllll2) {
                                lllll2 = false;
                            }
                        }
                    #endif
                    float lllllllllllllllllllllllllll2 = 0;
                    for (int i = 0; i < _ArrayLength; i++){
                        float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                        float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;                 
                        float llllllllllll3 = 0;
                        float l3 = 4;
                        if(!ll0) {
                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                if (ll3 != 0 && ll3 == _id) {
                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                    llllllllllll3 = 1;
                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < lllllllllllllllllllllllllllllll0) {
                                        if(llll3 == 1) {
                                            llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-lll3))/lllllllllllllllllllllllllllllll0);
                                        } else {
                                            llllllllllll3 = ((_Time.y-lll3)/lllllllllllllllllllllllllllllll0);
                                        }
                                    } else if(llll3 ==-1) {
                                        llllllllllll3 = 1;
                                    } else if(llll3 == 1) {
                                        llllllllllll3 = 0;
                                    } else {
                                        llllllllllll3 = 1;
                                    }
                                    llllllllllll3 = 1 - llllllllllll3;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                        float llllllllllllllllll3 = 0;
                        float lllllllllllllllllll3 = 0;     
                        float llllllllllllllllllll3 = lllllllllllllllllll3;
                        bool lllllllllllllllllllll3 = distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                        if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                        
                            #if _ZONING
                                if(llllllllllllllll1) {
                                    if(lllll2) 
                                    {
                                        if(lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 1;
                                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2]; llllll6++){
                                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                                if (ll3 != 0 && ll3 == llllll2) {
                                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                    llllllllllllllllll3 = 1;
                                                    float llllllllllllllllllllllllll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 3];
                                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                                        if(llll3 == 1) {
                                                            llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                                        } else {
                                                            llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                                        }
                                                    } else if(llll3 ==-1) {
                                                        llllllllllllllllll3 = 1;
                                                    } else if(llll3 == 1) {
                                                        llllllllllllllllll3 = 0;
                                                    } else {
                                                        llllllllllllllllll3 = 1;
                                                    }
                                                    llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                                }
                                                if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                                    if (lllllll2 < llllllllllllllllll1)  {
                                                        float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                        llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                                    }
                                                }
                                            }
                                        } else { 
                                        }
                                    } else {
                                    }
                                }
                            #endif
                            if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {    
                                if (llllllllll0 == 2 || llllllllll0 == 3 || llllllllll0 == 4 || llllllllll0 == 5 || llllllllll0 == 6 || llllllllll0 == 7) {
                                    float4 lllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float4 llllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float lllllllllllllllllllllllllllllll3 = 0;
                                    if(lllll1 || llllllllll0 == 6) {
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        #if _HDRP
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz), 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4 , _ProjectionParams.x);
                                        #else
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4);
                                        #endif
                                        llllllllllllllllllllllllllllll3.xy /= llllllllllllllllllllllllllllll3.w;
                                        llllllllllllllllllllllllllllll3.x *= l4;
                                        #if _HDRP
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(d.worldSpacePosition.xyz), 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4 , _ProjectionParams.x);
                                        #else
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(d.worldSpacePosition.xyz, 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4);
                                        #endif
                                        lllllllllllllllllllllllllllll3.xy /= lllllllllllllllllllllllllllll3.w;
                                        lllllllllllllllllllllllllllll3.x *= l4;
                                        #if _DISSOLVEMASK
                                            if(lllll1) {
                                                #if _REPLACEMENT
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMaskGlobal_TexelSize.z,_DissolveMaskGlobal_TexelSize.w);
                                                #else
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMask_TexelSize.z,_DissolveMask_TexelSize.w);
                                                #endif
                                            }
                                        #endif
                                    }
                                    float3 llllll4 = _WorldSpaceCameraPos - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 lllllll4 = normalize(llllll4);
                                    float llllllll4 = dot(d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, lllllll4);                           
                                    float lllllllll4 = 0;
                                    float llllllllll4 = 0;
                                    float2 lllllllllll4 = float2(0,0);
                                    if (llllllllll0 == 2 || llllllllll0 == 3) { 
                                        lllllllll4 = llllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllll4 = length(llllll4);
                                        float llllllllllllll4 = lllllllllllll0;
                                        float lllllllllllllll4 = (llllllll4 / lllllllllllll4) * llllllllllllll4;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            float llllllllllllllllll4 = llllllllllll4 < lllllllllllllll4;
                                            if(llllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 4 || llllllllll0 == 5 ) { 
                                        lllllllll4 = llllllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllllllllllll4 = lllllllllllllll0;
                                        float llllllllllllllllllllll4 = (llllllllllll4 < lllllllllllllllllllll4) && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if(llllllllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 6) { 
                                        lllllllll4 = llllllllllllllll0;
                                        float llllllllllllllllllllllllll4 = length(llllll4);
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        float llllllllllllllllllllllllllll4 = min(1,l4);
                                        float lllllllllllllllllllllllllllll4 =  distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy) < lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                        float llllllllllllllllllllllllllllll4 = (lllllllllllllllllllllllllllll4)  && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float lllllllllllllllllllllllllllllll4 = lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                            float llllllllllllllll4 = (2*lllllllllllllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if (llllllllllllllllllllllllllllll4) {
                                                float lll5 = (distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy)/(lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4));
                                                llllllllll4 = lll5;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } 
                                    else if (llllllllll0 == 7) { 
                                        #if _OBSTRUCTION_CURVE
                                            lllllllll4 = llllllllllllllllll0;
                                            float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                            float llllllllllllllllllllllllll4 = length(llllll4);
                                            float4 llllll5 = float4(0,0,0,0);
                                            float lllllll5 = lllllllllllllllllllllll1.z;
                                            float llllllll5 = (llllllll4/llllllllllllllllllllllllll4) * lllllll5;
                                            float4 lllllllll5 = float4(0,0,0,0);
                                            lllllllll5 = lllllllllllllllllllllll1;
                                            float2 llllllllll5 = (llllllll5+0.5) * lllllllll5.xy;
                                            #if _REPLACEMENT
                                                llllll5 = tex2D(_ObstructionCurveGlobal, llllllllll5);
                                            #else
                                                llllll5 = tex2D(_ObstructionCurve, llllllllll5);
                                            #endif
                                            float lllllllllll5 = llllll5.r * lllllllllllllllllll0;
                                            float llllllllllll5 = (llllllllllll4 < lllllllllll5) && llllllll4 > 0 ;
                                            #if _DISSOLVEMASK
                                                float llllllllllllllll4 = (2*lllllllllll5) / lllllllllllllllllllllllllllllll3;
                                                float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                                lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                                lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                            #else
                                                if(llllllllllll5){
                                                    float lllllllllllllllllll4 = llllllllllll4/lllllllllll5;
                                                    llllllllll4 = lllllllllllllllllll4;
                                                } else {
                                                    llllllllll4 = -1;
                                                }
                                            #endif
                                        #endif
                                    } 
                                    #if _DISSOLVEMASK
                                        if(lllll1) {
                                            float4 llllllllllllllll5 = float4(0,0,0,0);
                                            llllllllllllllll5 = llllll1;
                                            float2 lllllllllllllllll5 = float2(llllllllllllllll5.z/2,llllllllllllllll5.w/2);
                                            float2 llllllllllllllllll5 = lllllllllllllllll5 + lllllllllll4;
                                            float2 lllllllllllllllllll5 = (llllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                            float4 llllllllllllllllllll5 = float4(0,0,0,0);
                                            #if _REPLACEMENT
                                                llllllllllllllllllll5 = tex2D(_DissolveMaskGlobal, lllllllllllllllllll5);
                                            #else
                                                llllllllllllllllllll5 = tex2D(_DissolveMask, lllllllllllllllllll5);
                                            #endif
                                            float lllllllllllllllllllll5 = -1;
                                            if(llllllllllllllllll5.x <= llllllllllllllll5.z && llllllllllllllllll5.x >= 0 && llllllllllllllllll5.y <= llllllllllllllll5.w && llllllllllllllllll5.y >= 0 && llllllllllllllllllll5.x <= 0 && llllllll4 > 0 ){
                                                float llllllllllllllllllllll5 = sqrt(pow(llllllllllllllll5.z,2)+pow(llllllllllllllll5.w,2))/2;
                                                float lllllllllllllllllllllll5 = 40;
                                                float llllllllllllllllllllllll5 = llllllllllllllllllllll5/lllllllllllllllllllllll5;
                                                float lllllllllllllllllllllllll5 = 0;
                                                lllllllllllllllllllll5 = 0;     
                                                #if _REPLACEMENT
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMaskGlobal, float4(lllllllllllllllllllllllllll5, 0.0, 0.0));                           
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        } 
                                                    }   
                                                #else
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMask, float4(lllllllllllllllllllllllllll5, 0.0, 0.0)); 
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        }                                            
                                                    }   
                                                #endif
                                                lllllllllllllllllllll5 = 1-lllllllllllllllllllllllll5;  
                                            }         
                                            llllllllll4 = lllllllllllllllllllll5;
                                        }
                                    #endif
                                    if(llllllllllllllllllll0 <= 1) {
                                        if(llllllllll4 != -1) {
                                            float lll6 = max(llllllllllllllllllll0,0.00001);
                                            float llll6 = 1-lllllllll4;
                                            float lllll6 = exp(lll6*6);
                                            float llllll6 = llllllllll4;
                                            float lllllll6 = llll6/(lll6/(lll6*llll6-0.15*(lll6-llll6)));
                                            float llllllll6 = ((llllll6-lllllll6)/(lllll6*(1-llllll6)+llllll6))+lllllll6;
                                            llllllll6 = 1-llllllll6;
                                            lllllllllllllllllll3 =  llllllll6 * sign(lllllllll4);
                                        }
                                    } else {
                                        lllllllllllllllllll3 = llllllllll4;
                                    }
                                } 
                                if (llllllllll0 == 1 || llllllllll0 == 3 || llllllllll0 == 5) { 
                                    float lllllllll6 = distance(_WorldSpaceCameraPos, _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz);
                                    float llllllllll6 = distance(_WorldSpaceCameraPos, d.worldSpacePosition.xyz);
                                    float3 lllllllllll6 =  d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 llllllllllll6 =  d.worldSpaceNormal;
                                    float lllllllllllll6 = acos(dot(lllllllllll6,llllllllllll6)/(length(lllllllllll6)*length(llllllllllll6)));
                                    if(lllllllllllll6 <= 1.5 && lllllllll6 > llllllllll6) {
                                        float llllllllllllll6 = (sqrt((lllllllll6-llllllllll6)) * 25 /lllllllllllll6)*lllllllllll0;                   
                                        lllllllllllllllllll3 += max(0,log(llllllllllllll6*0.2));
                                    }
                                }
                            }
                            lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                            if(lllll2) { 
                                if(lllllllllllllllll1 == 1) { 
                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                    }
                                } 
                                else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                    float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                    } else {
                                        lllllllllllllllllll3 = 0;
                                    }
                                }
                            }
                            float lllllllllllllllllll6 = lllllllllllllllllll3 / lllllllll1;
                            if(lllllll1) {
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;
                                float3 lllllllllllllllllllll6 = d.worldSpacePosition.xyz - _WorldSpaceCameraPos;
                                float llllllllllllllllllllll6 = dot(lllllllllllllllllllll6, normalize(llllllllllllllllllllllllllllll2));
                                if(llllllllllllllllllllll6 - llllllll1 >= length(llllllllllllllllllllllllllllll2)) {
                                    float lllllllllllllllllllllll6 = llllllllllllllllllllll6 - llllllll1 - length(llllllllllllllllllllllllllllll2);
                                    if(lllllllllllllllllllllll6 < 0) {
                                        lllllllllllllllllllllll6 = 0;
                                    }
                                    if(lllllllllllllllllllllll6 < lllllllll1) 
                                    {
                                        lllllllllllllllllll3 = (lllllllll1 - lllllllllllllllllllllll6) * lllllllllllllllllll6;
                                    }
                                    else 
                                    {
                                        lllllllllllllllllll3 = 0; 
                                    }
                                }
                            }
                            if(llllllllllllllll1 && !lllll2) {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                            if(llllllllll1 == 1) {
                                float llllllllllllllllllllllll6 = 0;
                                if(llllllllllll1 == 0) {
                                    llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                                } else if (llllllllllll1 == 1) {  
                                    float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                        lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                    }
                                    llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                                }
                                if(lllllllllll1 == 1) {
        	                        if(d.worldSpacePosition.y > (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1)) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1);
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y > lllllllllllll1) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                }
                            }
                            if(lllllllllllllllllllllllll0 == 1) {
                                float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                                if(llllllllllllllllllllllllll0 == 1) {
        	                        if(d.worldSpacePosition.y < (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0)) {
                                        float lllllllllllllllllllllll6 = (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0) - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                        float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                }
                            }
                            if(!_TriggerMode && !_RaycastMode) {
                                if(distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                        if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                            llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                        }
                        if(_TriggerMode || _RaycastMode) {
                            lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                        } else {
                            lllllllllllllllllll3 = lllllllllllllllllll3;
                            if (llllllllllllllll1) {
                                if(lllll2) {
                                    if(lllllllllllllllllll1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                    }
                                } else {
                                    if(lllllllllllllllll1 == 1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                    }
                                }
                            }
                        }
                        lll2 = max(lll2,lllllllllllllllllll3);
                    }   
                #else
                    float llllllllllll3 = 0;
                    if(!ll0) {
                        llllllllllll3 = 1;
                        if( _tDirection!= 0 && _tValue != 0 && _Time.y-_tValue < lllllllllllllllllllllllllllllll0) {
                            if(_tDirection == 1) {
                                llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-_tValue))/lllllllllllllllllllllllllllllll0);
                            } else {
                                llllllllllll3 = ((_Time.y-_tValue)/lllllllllllllllllllllllllllllll0);
                            }
                        } else if(_tDirection ==-1) {
                            llllllllllll3 = 1;
                        } else if(_tDirection == 1) {
                            llllllllllll3 = 0;
                        } else {
                            llllllllllll3 = 1;
                        }
                        llllllllllll3 = 1 - llllllllllll3;
                    }
                    float lllllllllllllllllll3 = 0;     
                    float llllllllllllllllll3 = 0;
                    bool lllllllllllllllllllll3 = distance(_WorldSpaceCameraPos, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                    lllllllllllllllllllll3 = false;
                    if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                    
                        #if _ZONING
                            if(llllllllllllllll1) {
                                if(lllll2) 
                                {
                                    if(lllllllllllllllllll1) {
                                        float lll3 = lllllllll2;
                                        float llll3 = lllllllllll2;
                                        llllllllllllllllll3 = 1;
                                        float llllllllllllllllllllllllll3 = llllllllll2;
                                        if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                            if(llll3 == 1) {
                                                llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                            } else {
                                                llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                            }
                                        } else if(llll3 ==-1) {
                                            llllllllllllllllll3 = 1;
                                        } else if(llll3 == 1) {
                                            llllllllllllllllll3 = 0;
                                        } else {
                                            llllllllllllllllll3 = 1;
                                        }
                                        llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                        if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                            if (lllllll2 < llllllllllllllllll1)  {
                                                float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                            }
                                        }
                                    } else { 
                                    }
                                } else {
                                }
                            }
                        #endif
                        lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                        if(lllll2) {
                            if(lllllllllllllllll1 == 1) { 
                                float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                }
                            } 
                            else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                } else {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        if(llllllllllllllll1 && !lllll2) {
                            if(lllllllllllllllll1 == 1) {
                                lllllllllllllllllll3 = 0;
                            }
                        }
                        if(llllllllll1 == 1 && lllllllllll1 == 0) {
                            float llllllllllllllllllllllll6 = 0;
                            if(llllllllllll1 == 0) { 
                                llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                            } else if (llllllllllll1 == 1) {  
                                float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                    lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                }
                                llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                            }
                            if(d.worldSpacePosition.y > lllllllllllll1) {
                                float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(llllllllllll1 == 0) { 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                        lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 0;  
                                    }
                                } else {                                 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) { 
                                        lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 1;  
                                    }
                                    llllllllllllllllll3 = 1;
                                }
                            }
                        }
                        if(lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 0) {
                            float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                            if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                } else {
                                    lllllllllllllllllll3 = 0;  
                                }
                            }                                
                        }
                    } 
                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                        llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                    }
                    if(_TriggerMode || _RaycastMode) {
                        lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                    } else {
                        lllllllllllllllllll3 = lllllllllllllllllll3;
                        if (llllllllllllllll1) {
                            if(lllll2) {
                                if(lllllllllllllllllll1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                }
                            } else {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                }
                            }
                        }
                    }
                    lll2 = max(lll2,lllllllllllllllllll3);    
                #endif
                float llllllllllllllllllll3 = lll2;
                if(!llllllllllllllllllllll0) {
                    if (llllllllllllllllllll3 == 1) {
                        llllllllllllllllllll3 = 10;
                    }
                    if (!llllllllllllllllllllllll0 || llllllllll0 == 6) {
                    #if defined(UNITY_PASS_SHADOWCASTER) 
                    #if defined(SHADOWS_DEPTH) 
                    if (!any(unity_LightShadowBias))
                    {
                            clip(l2- llllllllllllllllllll3);
                    }
                    else
                    {
                        if(llllllllllllllllllllllll0 && llllllllll0 != 6) {
                            clip(l2- llllllllllllllllllll3);                  
                        }
                    }
                    #endif
                    #else
                        clip(l2- llllllllllllllllllll3);
                    #endif
                    } else {                
                        clip(l2- llllllllllllllllllll3);  
                    }
                }       
                if(llllllllllllllllllllll0) {
                    lll0 = 1;
                    if((l2 - llllllllllllllllllll3) < 0) {
                        llll0 = half4(1,1,1,1);
                        o.Emission = 1;
                    } else {
                        llll0 = half4(0,0,0,1);
                    }
                    if(llllllllllllllllllllllllll2) {
                        if((l2 - llllllllllllllllllll3) < 0) {
                            llll0 = half4(0.5,1,0.5,1);
                            o.Emission = 0;
                        } else {
                            llll0 = half4(0,0.1,0,1);
                        }
                    }
                    if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                        float lllllllllllllllllll7 = 0;
                        if(llllllllllllllllllllllllll0 == 1) {
                            lllllllllllllllllllllllll2 = lllllllllllllllllllllllll2 + lllllllllllllllllllll1;    
                            lllllllllllllllllll7 = lllllllllllllllllllllllll2;
                        } else {
                            lllllllllllllllllll7 = lllllllllllllllllllllllllll0 + lllllllllllllllllllll1;
                        }                        
                        if(d.worldSpacePosition.y > (lllllllllllllllllll7 - llllllllllllllllllllll1) && d.worldSpacePosition.y < (lllllllllllllllllll7 + llllllllllllllllllllll1)){
                            llll0 = half4(1,0,0,1);
                        }
                    }
                } else {
                    half3 llllllllllllllllllll7 = lerp(1, lllll0, llllll0).rgb;
                    if(lll1) {
                        llll1 = 0.2 + (llll1*(0.8-0.2)); 
                        o.Emission =  min(clamp(llllllllllllllllllll7 *  clamp(((llllllllllllllllllll3/llll1) - l2),0,1), 0, 1)*sqrt(lllllll0*llllllll0), clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0));
                    } else {
                        o.Emission =  clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0);
                    }
                    #if _HDRP
                        o.Emission =  o.Emission * pow(llllllll0,2);
                    #endif
                }
            }
            float4 lllllllllllllllllllll7;
            lllllllllllllllllllll7.xy = d.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
            lllllllllllllllllllll7.zw = (_UVSec == 0) ? d.texcoord0.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw : d.texcoord1.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
            #ifdef _PARALLAXMAP
                half llllllllllllllllllllll7 = tex2D (_ParallaxMap, lllllllllllllllllllll7.xy).g;
                float2 lllllllllllllllllllllll7 = ParallaxOffset1Step (llllllllllllllllllllll7, _Parallax, d.tangentSpaceViewDir);
                lllllllllllllllllllll7 = float4(lllllllllllllllllllll7.xy + lllllllllllllllllllllll7, lllllllllllllllllllll7.zw + lllllllllllllllllllllll7);
                o.Normal = float3(0, 0, 1);
            #endif
            if(lll0) {
                o.Albedo = llll0.rgb;
            } else {
                half3 llllllllllllllllllllllll7 = tex2D(_MainTex, lllllllllllllllllllll7.xy).rgb * _Color.rgb; 
                #ifdef _DETAIL_MULX2
                     half3 lllllllllllllllllllllllll7 = tex2D (_DetailAlbedoMap, lllllllllllllllllllll7.zw).rgb;
                     llllllllllllllllllllllll7 *= LerpWhiteTo (lllllllllllllllllllllllll7 * unity_ColorSpaceDouble.rgb, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
                #endif
                o.Albedo = llllllllllllllllllllllll7;
            }
            #ifdef _NORMALMAP
                half3 llllllllllll6 = UnpackScaleNormal(tex2D(_BumpMap, lllllllllllllllllllll7.xy), _BumpScale);
                #ifdef _DETAIL_MULX2
                    half3 lllllllllllllllllllllllllll7 = UnpackScaleNormal(tex2D(_DetailNormalMap, lllllllllllllllllllll7.zw), _DetailNormalMapScale);
	                lllllllllllllllllllllllllll7 = lerp(float3(0, 0, 1), lllllllllllllllllllllllllll7, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
	                llllllllllll6 = BlendNormals(llllllllllll6, lllllllllllllllllllllllllll7);
                #endif
                o.Normal = llllllllllll6;
            #endif
            half2 llllllllllllllllllllllllllll7 = half2(0,0);
            #ifdef _METALLICGLOSSMAP
                half4 lllllllllllllllllllllllllllll7 = tex2D(_MetallicGlossMap, lllllllllllllllllllll7.xy);
                llllllllllllllllllllllllllll7.r = lllllllllllllllllllllllllllll7.r;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g = tex2D(_MainTex, lllllllllllllllllllll7.xy).a;
                #else
                    llllllllllllllllllllllllllll7.g = lllllllllllllllllllllllllllll7.a;
                #endif
                llllllllllllllllllllllllllll7.g *= _GlossMapScale;
            #else
                llllllllllllllllllllllllllll7.r = _Metallic;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g  = tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _GlossMapScale;
                #else
                    llllllllllllllllllllllllllll7.g = _Glossiness;
                #endif
            #endif
            o.Metallic = llllllllllllllllllllllllllll7.r;
            o.Smoothness = llllllllllllllllllllllllllll7.g;
            o.Occlusion = lerp(1, tex2D(_OcclusionMap, lllllllllllllllllllll7.xy).g, _OcclusionStrength);
            #ifdef _EMISSION
                o.Emission += tex2D(_EmissionMap, lllllllllllllllllllll7.xy).rgb * _EmissionColor.rgb;
            #endif
            half llllllllllllllllllllllllllllll7 = 0;
            #if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
                llllllllllllllllllllllllllllll7 = _Color.a;
            #else
                llllllllllllllllllllllllllllll7 =  tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _Color.a;
            #endif
            if (_Mode) 
            {
                clip (llllllllllllllllllllllllllllll7 - _Cutoff);
            }
            o.Alpha = llllllllllllllllllllllllllllll7;

	}



        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                 //  Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                 // v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                 // v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                 // v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                 // v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                 // v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
               // d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
               // v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
            // d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
            // d.screenPos = i.screenPos;
            // d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
            // d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
            // d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
            // d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
            // d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
            // d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if _PASSSHADOW
            float3 _LightDirection;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
           // o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);


          #if _PASSSHADOW
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, _LightDirection));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif


          // #if %SCREENPOSREQUIREKEY%
          // o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              #if _BAKEDLIT
                 half3 vertexLight = 0;
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
              #endif
              half fogFactor = ComputeFogFactor(o.pos.z);
              o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
               Surface l = (Surface)0;

               #ifdef _DEPTHOFFSET_ON
                  l.outputDepth = outputDepth;
               #endif

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               #ifdef _DEPTHOFFSET_ON
                  outputDepth = l.outputDepth;
               #endif

             return 0;

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "DepthOnly"
            Tags 
            { 
                "LightMode" = "DepthOnly"
            }
           
            // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On
            ColorMask 0
            
            

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag


            #define SHADERPASS_DEPTHONLY
            #define _PASSDEPTH 1

            #pragma target 3.0
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _METALLICGLOSSMAP
        #pragma shader_feature_local _PARALLAXMAP
        #pragma shader_feature_local _DETAIL_MULX2
        #pragma shader_feature_local _EMISSION       
        #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma shader_feature_local_fragment _OBSTRUCTION_CURVE

        #pragma shader_feature_local_fragment _DISSOLVEMASK
	        #pragma shader_feature_local_fragment _ZONING
        #pragma shader_feature_local_fragment _REPLACEMENT
        #pragma shader_feature_local_fragment _PLAYERINDEPENDENT



   #define _URP 1
#define _USINGTEXCOORD1 1

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"


                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
         // float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
         // half4 vertexColor : COLOR;
         // #endif

         // #if %EXTRAV2F0REQUIREKEY%
         // float4 extraV2F0 : TEXCOORD12;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
         // float4 extraV2F1 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
         // float4 extraV2F2 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
         // float4 extraV2F3 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
         // float4 extraV2F4 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD19;
         // #endif
            
         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD9;
         #endif
            float4 fogFactorAndVertexLight : TEXCOORD10;
            float4 shadowCoord : TEXCOORD11;
         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };

         
            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
               // float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetObjectToWorldMatrix()
               #undef GetWorldToObjectMatrix()
               #undef GetWorldToViewMatrix()
               #undef UNITY_MATRIX_I_V
               #undef UNITY_MATRIX_P
               #undef GetWorldToHClipMatrix()
               #undef GetObjectToWorldMatrix()V
               #undef UNITY_MATRIX_T_MV
               #undef UNITY_MATRIX_IT_MV
               #undef GetObjectToWorldMatrix()VP

               #define GetObjectToWorldMatrix()     unity_ObjectToWorld
               #define GetWorldToObjectMatrix()   unity_WorldToObject
               #define GetWorldToViewMatrix()     unity_MatrixV
               #define UNITY_MATRIX_I_V   unity_MatrixInvV
               #define GetViewToHClipMatrix()     OptimizeProjectionMatrix(glstate_matrix_projection)
               #define GetWorldToHClipMatrix()    unity_MatrixVP
               #define GetObjectToWorldMatrix()V    mul(GetWorldToViewMatrix(), GetObjectToWorldMatrix())
               #define UNITY_MATRIX_T_MV  transpose(GetObjectToWorldMatrix()V)
               #define UNITY_MATRIX_IT_MV transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V))
               #define GetObjectToWorldMatrix()VP   mul(GetWorldToHClipMatrix(), GetObjectToWorldMatrix())


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               
        float4 _MainTex_ST;
        float4 _DetailAlbedoMap_ST;
	        half4 _Color;
        half _Cutoff; 
        half _Mode;
        float _Cull;
        half _BumpScale; 
        half _DetailNormalMapScale;
        half _Metallic;
        float _Glossiness;
        float _GlossMapScale;
        half _OcclusionStrength;
        half _Parallax;
        half _UVSec;
        half3 _EmissionColor;
        float _IsReplacementShader;
        float _TriggerMode;
        float _RaycastMode;
        float _IsExempt;
        float _isReferenceMaterial;
        float _InteractionMode;
        int _ArrayLength = 0;
        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
            float4 _PlayersPosVectorArray[20];
            float _PlayersDataFloatArray[150];     
        #else
            float4 _PlayersPosVectorArray[100];
            float _PlayersDataFloatArray[500];  
        #endif
        float _tDirection = 0;
        float _numOfPlayersInside = 0;
        float _tValue = 0;
        float _id = 0;
        #if _ZONING
            #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
                float _ZDFA[500];
            #else
                float _ZDFA[1000];
            #endif
            float _ZonesDataCount;
        #endif
        #if _REPLACEMENT        
            half4 _DissolveColorGlobal;
            float _DissolveColorSaturationGlobal;
            float _DissolveEmissionGlobal;
            float _DissolveEmissionBoosterGlobal;
            float _TextureVisibilityGlobal;
            float _ObstructionGlobal;
            float _AngleStrengthGlobal;
            float _ConeStrengthGlobal;
            float _ConeObstructionDestroyRadiusGlobal;
            float _CylinderStrengthGlobal;
            float _CylinderObstructionDestroyRadiusGlobal;
            float _CircleStrengthGlobal;
            float _CircleObstructionDestroyRadiusGlobal;
            float _CurveStrengthGlobal;
            float _CurveObstructionDestroyRadiusGlobal;
            float _DissolveFallOffGlobal;
            float _IntrinsicDissolveStrengthGlobal;
            float _PreviewModeGlobal;
            float _UVsGlobal;
            float _hasClippedShadowsGlobal;
            float _FloorGlobal;
            float _FloorModeGlobal;
            float _FloorYGlobal;
            float _PlayerPosYOffsetGlobal;
            float _FloorYTextureGradientLengthGlobal;
            float _AnimationEnabledGlobal;
            float _AnimationSpeedGlobal;
            float _DefaultEffectRadiusGlobal;
            float _TransitionDurationGlobal;        
            float _TexturedEmissionEdgeGlobal;
            float _TexturedEmissionEdgeStrengthGlobal;
            float _IsometricExclusionGlobal;
            float _IsometricExclusionDistanceGlobal;
            float _IsometricExclusionGradientLengthGlobal;
            float _CeilingGlobal;
            float _CeilingModeGlobal;
            float _CeilingBlendModeGlobal;
            float _CeilingYGlobal;
            float _CeilingPlayerYOffsetGlobal;
            float _CeilingYGradientLengthGlobal;
            float _ZoningGlobal;
            float _ZoningModeGlobal;
            float _ZoningEdgeGradientLengthGlobal;
            float _IsZoningRevealableGlobal;
            float _SyncZonesWithFloorYGlobal;
            float _SyncZonesFloorYOffsetGlobal;
            float4 _ObstructionCurveGlobal_TexelSize;
            float4 _DissolveMaskGlobal_TexelSize;
            float _DissolveMaskEnabledGlobal;
            float _PreviewIndicatorLineThicknessGlobal;
        #else
            half _TextureVisibility;
            half _AngleStrength;
            float _Obstruction;
            float _UVs;
            float4 _ObstructionCurve_TexelSize;      
            float _DissolveMaskEnabled;
            float4 _DissolveMask_TexelSize;
            half4 _DissolveColor;
            float _DissolveColorSaturation;
            float _DissolveEmission;
            float _DissolveEmissionBooster;
            float _hasClippedShadows;
            float _ConeStrength;
            float _ConeObstructionDestroyRadius;
            float _CylinderStrength;
            float _CylinderObstructionDestroyRadius;
            float _CircleStrength;
            float _CircleObstructionDestroyRadius;
            float _CurveStrength;
            float _CurveObstructionDestroyRadius;
            float _IntrinsicDissolveStrength;
            float _DissolveFallOff;
            float _PreviewMode;
            float _PreviewIndicatorLineThickness;
            float _AnimationEnabled;
            float _AnimationSpeed;
            float _DefaultEffectRadius;
            float _TransitionDuration;
            float _TexturedEmissionEdge;
            float _TexturedEmissionEdgeStrength;
            float _IsometricExclusion;
            float _IsometricExclusionDistance;
            float _IsometricExclusionGradientLength;
            float _Floor;
            float _FloorMode;
            float _FloorY;
            float _FloorYTextureGradientLength;
            float _PlayerPosYOffset;
            float _Ceiling;
            float _CeilingMode;
            float _CeilingBlendMode;
            float _CeilingY;
            float _CeilingPlayerYOffset;
            float _CeilingYGradientLength;
            float _Zoning;
            float _ZoningMode;
            float _ZoningEdgeGradientLength;
            float _IsZoningRevealable;
            float _SyncZonesWithFloorY;
            float _SyncZonesFloorYOffset;
        #endif




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()

	        sampler2D _MainTex;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailMask;
            sampler2D _BumpMap;
            sampler2D _DetailNormalMap;
            sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
            sampler2D _ParallaxMap;
            sampler2D _EmissionMap;
        #if _REPLACEMENT
            sampler2D _DissolveTexGlobal;
        #else
            sampler2D _DissolveTex;
        #endif
        #if _DISSOLVEMASK
            #if _REPLACEMENT
                sampler2D _DissolveMaskGlobal;
            #else
                sampler2D _DissolveMask;
            #endif
        #endif
        #if _REPLACEMENT
            sampler2D _ObstructionCurveGlobal;
        #else
            sampler2D _ObstructionCurve;
        #endif


	    #if _URP
        half2 ParallaxOffset1Step (half h, half height, half3 viewDir)
        {
            h = h * height - height/2.0;
            half3 v = normalize(viewDir);
            v.z += 0.42;
            return h * (v.xy / v.z);
        }
        #ifdef UNITY_COLORSPACE_GAMMA
            #define unity_ColorSpaceDouble half4(2.0, 2.0, 2.0, 2.0)
        #else 
            #define unity_ColorSpaceDouble half4(4.59479380, 4.59479380, 4.59479380, 2.0)
        #endif
        half3 BlendNormals(half3 n1, half3 n2)
        {
            return normalize(half3(n1.xy + n2.xy, n1.z*n2.z));
        }
    #endif


	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
                  bool l0;
            #if _REPLACEMENT
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDurationGlobal ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #else
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDuration ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #endif
            bool ll0 = !_TriggerMode && !_RaycastMode;
            float lll0 = 0;
            half4 llll0 = half4(0,0,0,0);
            if(!_IsExempt && (l0 || ll0) ) {
                half4 lllll0;
                float llllll0;
                float lllllll0;
                float llllllll0;
                half lllllllll0;
                float llllllllll0;
                half lllllllllll0;
                float llllllllllll0;
                float lllllllllllll0;
                float llllllllllllll0;
                float lllllllllllllll0;
                float llllllllllllllll0;
                float lllllllllllllllll0;                
                float llllllllllllllllll0;
                float lllllllllllllllllll0;
                float llllllllllllllllllll0;
                float lllllllllllllllllllll0;
                float llllllllllllllllllllll0;
                float lllllllllllllllllllllll0;
                float llllllllllllllllllllllll0;
                float lllllllllllllllllllllllll0;
                float llllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllllll0;
                float l1;
                float ll1;
                float lll1;
                float llll1;
                float lllll1;
                float4 llllll1;
                float lllllll1;
                float llllllll1;
                float lllllllll1;
                float llllllllll1;
                float lllllllllll1;
                float llllllllllll1;
                float lllllllllllll1;
                float llllllllllllll1;
                float lllllllllllllll1;
                float llllllllllllllll1;
                float lllllllllllllllll1;
                float llllllllllllllllll1;
                float lllllllllllllllllll1;
                float llllllllllllllllllll1;
                float lllllllllllllllllllll1;
                float llllllllllllllllllllll1;
                float4 lllllllllllllllllllllll1;
                #if _REPLACEMENT
                    lllll0 = _DissolveColorGlobal;
                    llllll0 = _DissolveColorSaturationGlobal;
                    lllllll0 = _DissolveEmissionGlobal;
                    llllllll0 = _DissolveEmissionBoosterGlobal;
                    lllllllll0 = _TextureVisibilityGlobal;
                    llllllllll0 = _ObstructionGlobal;
                    lllllllllll0 = _AngleStrengthGlobal;
                    llllllllllll0 = _ConeStrengthGlobal;
                    lllllllllllll0 = _ConeObstructionDestroyRadiusGlobal;
                    llllllllllllll0 = _CylinderStrengthGlobal;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadiusGlobal;
                    llllllllllllllll0 = _CircleStrengthGlobal;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadiusGlobal;
                    llllllllllllllllll0 = _CurveStrengthGlobal;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadiusGlobal;
                    llllllllllllllllllll0 = _DissolveFallOffGlobal;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrengthGlobal;
                    llllllllllllllllllllll0 = _PreviewModeGlobal;
                    lllllllllllllllllllllll0 = _UVsGlobal;
                    llllllllllllllllllllllll0 = _hasClippedShadowsGlobal;                    
                    lllllllllllllllllllllllll0 = _FloorGlobal;
                    llllllllllllllllllllllllll0 = _FloorModeGlobal;
                    lllllllllllllllllllllllllll0 = _FloorYGlobal;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffsetGlobal;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLengthGlobal; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadiusGlobal;
                    lllllllllllllllllllllllllllllll0 = _TransitionDurationGlobal;
                    l1 = _AnimationEnabledGlobal;
                    ll1 = _AnimationSpeedGlobal;
                    lll1 = _TexturedEmissionEdgeGlobal;           
                    llll1 = _TexturedEmissionEdgeStrengthGlobal;
                    lllll1 = _DissolveMaskEnabledGlobal;
                    llllll1 = _DissolveMaskGlobal_TexelSize;
                    lllllll1 = _IsometricExclusionGlobal;
                    llllllll1 = _IsometricExclusionDistanceGlobal;
                    lllllllll1 = _IsometricExclusionGradientLengthGlobal;
                    llllllllll1 = _CeilingGlobal;
                    lllllllllll1 = _CeilingModeGlobal;
                    llllllllllll1 = _CeilingBlendModeGlobal;
                    lllllllllllll1 = _CeilingYGlobal;
                    llllllllllllll1 = _CeilingPlayerYOffsetGlobal;
                    lllllllllllllll1 = _CeilingYGradientLengthGlobal;
                    llllllllllllllll1 = _ZoningGlobal;
                    lllllllllllllllll1 = _ZoningModeGlobal;
                    llllllllllllllllll1 = _ZoningEdgeGradientLengthGlobal;
                    lllllllllllllllllll1 = _IsZoningRevealableGlobal;
                    llllllllllllllllllll1 = _SyncZonesWithFloorYGlobal;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffsetGlobal;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThicknessGlobal;
                    lllllllllllllllllllllll1 = _ObstructionCurveGlobal_TexelSize;
                #else
                    lllll0 = _DissolveColor;
                    llllll0 = _DissolveColorSaturation;
                    lllllll0 = _DissolveEmission;
                    llllllll0 = _DissolveEmissionBooster;
                    lllllllll0 = _TextureVisibility;
                    llllllllll0 = _Obstruction;
                    lllllllllll0 = _AngleStrength;
                    llllllllllll0 = _ConeStrength;
                    lllllllllllll0 = _ConeObstructionDestroyRadius;
                    llllllllllllll0 = _CylinderStrength;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadius;
                    llllllllllllllll0 = _CircleStrength;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadius;
                    llllllllllllllllll0 = _CurveStrength;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadius;
                    llllllllllllllllllll0 = _DissolveFallOff;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrength;
                    llllllllllllllllllllll0 = _PreviewMode;
                    lllllllllllllllllllllll0 = _UVs;
                    llllllllllllllllllllllll0 = _hasClippedShadows;                    
                    lllllllllllllllllllllllll0 = _Floor;
                    llllllllllllllllllllllllll0 = _FloorMode;
                    lllllllllllllllllllllllllll0 = _FloorY;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffset;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLength; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadius;
                    lllllllllllllllllllllllllllllll0 = _TransitionDuration;
                    l1 = _AnimationEnabled;
                    ll1 = _AnimationSpeed;
                    lll1 = _TexturedEmissionEdge;           
                    llll1 = _TexturedEmissionEdgeStrength;
                    lllll1 = _DissolveMaskEnabled;
                    llllll1 = _DissolveMask_TexelSize;
                    lllllll1 = _IsometricExclusion;
                    llllllll1 = _IsometricExclusionDistance;
                    lllllllll1 = _IsometricExclusionGradientLength;
                    llllllllll1 = _Ceiling;
                    lllllllllll1 = _CeilingMode;
                    llllllllllll1 = _CeilingBlendMode;
                    lllllllllllll1 = _CeilingY;
                    llllllllllllll1 = _CeilingPlayerYOffset;
                    lllllllllllllll1 = _CeilingYGradientLength;
                    llllllllllllllll1 = _Zoning;
                    lllllllllllllllll1 = _ZoningMode;
                    llllllllllllllllll1 = _ZoningEdgeGradientLength;
                    lllllllllllllllllll1 = _IsZoningRevealable;
                    llllllllllllllllllll1 = _SyncZonesWithFloorY;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffset;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThickness;
                    lllllllllllllllllllllll1 = _ObstructionCurve_TexelSize;
                #endif
                if(lllllllllllllllllllll0 < 0) {
                    lllllllllllllllllllll0 = 0;
                }
                float3 llllllllllllllllllllllll1;
                d.worldSpaceNormal = mul(o.Normal, (float3x3)d.TBNMatrix);
                float3 lllllllllllllllllllllllll1 = d.worldSpacePosition / (-1.0 * abs(lllllllllllllllllllllll0) );
                if(l1) {
                    lllllllllllllllllllllllll1 = lllllllllllllllllllllllll1 + abs(((_Time.y) * ll1));
                }  
                float3 llllllllllllllllllllllllll1 = float3(0,0,0);
                float3 lllllllllllllllllllllllllll1 = float3(0,0,0);
                float3 llllllllllllllllllllllllllll1 = float3(0,0,0);
                #if _REPLACEMENT
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xy ).rgb;
                #else
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xy ).rgb;
                #endif
                float lllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.x);
                float llllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.z);
                float3 lllllllllllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllll1, llllllllllllllllllllllllll1, lllllllllllllllllllllllllllll1 ).rgb;
                llllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllllllll1, llllllllllllllllllllllllllll1, llllllllllllllllllllllllllllll1 ).rgb;
                half l2 = llllllllllllllllllllllll1.r;  
                float3 ll2 = GetWorldToViewMatrix()[2].xyz;
                #if _HDRP
                    ll2 =  mul(GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);
                #else
                    ll2 = - mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                #endif
                float lll2 = 0;
                float llll2 = 1;
                bool lllll2 = false;
                float llllll2 = 0;
                float lllllll2 = 0;
                float llllllll2 = 0;
                float lllllllll2 = 0;
                float llllllllll2 = 0;
                float lllllllllll2 = 0;
                #if _ZONING
                    if(llllllllllllllll1) {
                        float llllllllllll2 = 0;
                        for (int z = 0; z < _ZonesDataCount; z++){
                            bool lllllllllllll2 = false;
                            float llllllllllllll2 = llllllllllll2;
                            if (_ZDFA[llllllllllll2 + 1] == 0) {  
                                float lllllllllllllll2 = llllllllllll2 + 2; 
                                float3 llllllllllllllll2 = d.worldSpacePosition - float3(_ZDFA[lllllllllllllll2],_ZDFA[lllllllllllllll2+1], _ZDFA[lllllllllllllll2+2]);
                                float3 lllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 3],_ZDFA[lllllllllllllll2+ 4], _ZDFA[lllllllllllllll2+ 5]);
                                float3 llllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 6],_ZDFA[lllllllllllllll2+ 7], _ZDFA[lllllllllllllll2+ 8]);
                                float3 lllllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 9],_ZDFA[lllllllllllllll2+10], _ZDFA[lllllllllllllll2+11]);
                                float3 llllllllllllllllllll2 = float3(_ZDFA[lllllllllllllll2+12],_ZDFA[lllllllllllllll2+13], _ZDFA[lllllllllllllll2+14]);
                                lllllllllllll2 =    abs(dot(llllllllllllllll2, lllllllllllllllll2)) <= llllllllllllllllllll2.x &&
                                            abs(dot(llllllllllllllll2, llllllllllllllllll2)) <= llllllllllllllllllll2.y &&
                                            abs(dot(llllllllllllllll2, lllllllllllllllllll2)) <= llllllllllllllllllll2.z;
                                if(lllllllllllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                                    llllllll2 = _ZDFA[lllllllllllllll2+1] - _ZDFA[lllllllllllllll2+13];  
                                    if(llllllllllllllllllllllllll0 == 0) {                                    
                                        bool lllllllllllllllllllll2 = ((llllllll2 - lllllllllllllllllllll1)  <= lllllllllllllllllllllllllll0); 
                                        if(!lllllllllllllllllllll2) {
                                            lllllllllllll2 = false;
                                        }
                                    }
                                }
                                llllllllllll2 = llllllllllll2 + 17 + 3; 
                                if(lllllllllllll2) {
                                    float llllllllllllllllllllll2 = llllllllllllllllllll2.x - abs(dot(llllllllllllllll2, lllllllllllllllll2));
                                    float lllllllllllllllllllllll2 = llllllllllllllllllll2.y - abs(dot(llllllllllllllll2, llllllllllllllllll2));
                                    float llllllllllllllllllllllll2 = llllllllllllllllllll2.z - abs(dot(llllllllllllllll2, lllllllllllllllllll2));
                                    lllllll2 = min(lllllllllllllllllllllll2,llllllllllllllllllllll2);
                                    lllllll2 = min(lllllll2,llllllllllllllllllllllll2);
                                    if(lllllll2<0) {
                                        lllllll2 = 0;
                                    }
                                }
                            }
                            if(lllllllllllll2) {
                                llllll2 = _ZDFA[llllllllllllll2];
                                lllll2 = true;    
                                lllllllll2 = _ZDFA[llllllllllllll2 + 17];
                                lllllllllll2 = _ZDFA[llllllllllllll2 + 18];
                                llllllllll2 = _ZDFA[llllllllllllll2 + 19];
                                break;                        
                            }
                        }
                    }
                #endif
                float lllllllllllllllllllllllll2 = 0;
                float llllllllllllllllllllllllll2 = lllll2;
                #if _PLAYERINDEPENDENT == 0
                    #if _ZONING
                        if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                            float lllllllllllllllllllllllllll2 = 0;
                            bool llllllllllllllllllllllllllll2 = false;
                            for (int i = 0; i < _ArrayLength; i++){
                                float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;               
                                if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {       
                                    if(!ll0) {
                                        float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                                        float l3 = 4;
                                        for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                            float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                            if (ll3 != 0 && ll3 == _id) {
                                                float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                if ((llll3 == -1 && _Time.y - lll3 < lllllllllllllllllllllllllllllll0 )|| (llll3 == 1) ) {
                                                    float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                                    if(llllllllllllllllllllll0) {
                                                        if(i==0) {
                                                            lllllllllllllllllllllllll2 = lllll3;
                                                        } else {
                                                            lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                                        }
                                                    }
                                                    bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                                    if(!llllll3) {
                                                        llllllllllllllllllllllllllll2 = true;
                                                    } 
                                                }                        
                                            }
                                        }
                                    } else if (distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition.xyz) < llllllllllllllllllllllllllllll0) {
                                        float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                        if(llllllllllllllllllllll0) {
                                            if(i==0) {
                                                lllllllllllllllllllllllll2 = lllll3;
                                            } else {
                                                lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                            }
                                        }
                                        bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                        if(!llllll3) {
                                            llllllllllllllllllllllllllll2 = true;
                                        } 
                                    }
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                                }
                            }
                            if(!llllllllllllllllllllllllllll2) {
                                lllll2 = false;
                            }
                        }
                    #endif
                    float lllllllllllllllllllllllllll2 = 0;
                    for (int i = 0; i < _ArrayLength; i++){
                        float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                        float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;                 
                        float llllllllllll3 = 0;
                        float l3 = 4;
                        if(!ll0) {
                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                if (ll3 != 0 && ll3 == _id) {
                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                    llllllllllll3 = 1;
                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < lllllllllllllllllllllllllllllll0) {
                                        if(llll3 == 1) {
                                            llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-lll3))/lllllllllllllllllllllllllllllll0);
                                        } else {
                                            llllllllllll3 = ((_Time.y-lll3)/lllllllllllllllllllllllllllllll0);
                                        }
                                    } else if(llll3 ==-1) {
                                        llllllllllll3 = 1;
                                    } else if(llll3 == 1) {
                                        llllllllllll3 = 0;
                                    } else {
                                        llllllllllll3 = 1;
                                    }
                                    llllllllllll3 = 1 - llllllllllll3;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                        float llllllllllllllllll3 = 0;
                        float lllllllllllllllllll3 = 0;     
                        float llllllllllllllllllll3 = lllllllllllllllllll3;
                        bool lllllllllllllllllllll3 = distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                        if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                        
                            #if _ZONING
                                if(llllllllllllllll1) {
                                    if(lllll2) 
                                    {
                                        if(lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 1;
                                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2]; llllll6++){
                                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                                if (ll3 != 0 && ll3 == llllll2) {
                                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                    llllllllllllllllll3 = 1;
                                                    float llllllllllllllllllllllllll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 3];
                                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                                        if(llll3 == 1) {
                                                            llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                                        } else {
                                                            llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                                        }
                                                    } else if(llll3 ==-1) {
                                                        llllllllllllllllll3 = 1;
                                                    } else if(llll3 == 1) {
                                                        llllllllllllllllll3 = 0;
                                                    } else {
                                                        llllllllllllllllll3 = 1;
                                                    }
                                                    llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                                }
                                                if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                                    if (lllllll2 < llllllllllllllllll1)  {
                                                        float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                        llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                                    }
                                                }
                                            }
                                        } else { 
                                        }
                                    } else {
                                    }
                                }
                            #endif
                            if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {    
                                if (llllllllll0 == 2 || llllllllll0 == 3 || llllllllll0 == 4 || llllllllll0 == 5 || llllllllll0 == 6 || llllllllll0 == 7) {
                                    float4 lllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float4 llllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float lllllllllllllllllllllllllllllll3 = 0;
                                    if(lllll1 || llllllllll0 == 6) {
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        #if _HDRP
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz), 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4 , _ProjectionParams.x);
                                        #else
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4);
                                        #endif
                                        llllllllllllllllllllllllllllll3.xy /= llllllllllllllllllllllllllllll3.w;
                                        llllllllllllllllllllllllllllll3.x *= l4;
                                        #if _HDRP
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(d.worldSpacePosition.xyz), 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4 , _ProjectionParams.x);
                                        #else
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(d.worldSpacePosition.xyz, 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4);
                                        #endif
                                        lllllllllllllllllllllllllllll3.xy /= lllllllllllllllllllllllllllll3.w;
                                        lllllllllllllllllllllllllllll3.x *= l4;
                                        #if _DISSOLVEMASK
                                            if(lllll1) {
                                                #if _REPLACEMENT
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMaskGlobal_TexelSize.z,_DissolveMaskGlobal_TexelSize.w);
                                                #else
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMask_TexelSize.z,_DissolveMask_TexelSize.w);
                                                #endif
                                            }
                                        #endif
                                    }
                                    float3 llllll4 = _WorldSpaceCameraPos - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 lllllll4 = normalize(llllll4);
                                    float llllllll4 = dot(d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, lllllll4);                           
                                    float lllllllll4 = 0;
                                    float llllllllll4 = 0;
                                    float2 lllllllllll4 = float2(0,0);
                                    if (llllllllll0 == 2 || llllllllll0 == 3) { 
                                        lllllllll4 = llllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllll4 = length(llllll4);
                                        float llllllllllllll4 = lllllllllllll0;
                                        float lllllllllllllll4 = (llllllll4 / lllllllllllll4) * llllllllllllll4;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            float llllllllllllllllll4 = llllllllllll4 < lllllllllllllll4;
                                            if(llllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 4 || llllllllll0 == 5 ) { 
                                        lllllllll4 = llllllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllllllllllll4 = lllllllllllllll0;
                                        float llllllllllllllllllllll4 = (llllllllllll4 < lllllllllllllllllllll4) && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if(llllllllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 6) { 
                                        lllllllll4 = llllllllllllllll0;
                                        float llllllllllllllllllllllllll4 = length(llllll4);
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        float llllllllllllllllllllllllllll4 = min(1,l4);
                                        float lllllllllllllllllllllllllllll4 =  distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy) < lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                        float llllllllllllllllllllllllllllll4 = (lllllllllllllllllllllllllllll4)  && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float lllllllllllllllllllllllllllllll4 = lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                            float llllllllllllllll4 = (2*lllllllllllllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if (llllllllllllllllllllllllllllll4) {
                                                float lll5 = (distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy)/(lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4));
                                                llllllllll4 = lll5;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } 
                                    else if (llllllllll0 == 7) { 
                                        #if _OBSTRUCTION_CURVE
                                            lllllllll4 = llllllllllllllllll0;
                                            float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                            float llllllllllllllllllllllllll4 = length(llllll4);
                                            float4 llllll5 = float4(0,0,0,0);
                                            float lllllll5 = lllllllllllllllllllllll1.z;
                                            float llllllll5 = (llllllll4/llllllllllllllllllllllllll4) * lllllll5;
                                            float4 lllllllll5 = float4(0,0,0,0);
                                            lllllllll5 = lllllllllllllllllllllll1;
                                            float2 llllllllll5 = (llllllll5+0.5) * lllllllll5.xy;
                                            #if _REPLACEMENT
                                                llllll5 = tex2D(_ObstructionCurveGlobal, llllllllll5);
                                            #else
                                                llllll5 = tex2D(_ObstructionCurve, llllllllll5);
                                            #endif
                                            float lllllllllll5 = llllll5.r * lllllllllllllllllll0;
                                            float llllllllllll5 = (llllllllllll4 < lllllllllll5) && llllllll4 > 0 ;
                                            #if _DISSOLVEMASK
                                                float llllllllllllllll4 = (2*lllllllllll5) / lllllllllllllllllllllllllllllll3;
                                                float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                                lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                                lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                            #else
                                                if(llllllllllll5){
                                                    float lllllllllllllllllll4 = llllllllllll4/lllllllllll5;
                                                    llllllllll4 = lllllllllllllllllll4;
                                                } else {
                                                    llllllllll4 = -1;
                                                }
                                            #endif
                                        #endif
                                    } 
                                    #if _DISSOLVEMASK
                                        if(lllll1) {
                                            float4 llllllllllllllll5 = float4(0,0,0,0);
                                            llllllllllllllll5 = llllll1;
                                            float2 lllllllllllllllll5 = float2(llllllllllllllll5.z/2,llllllllllllllll5.w/2);
                                            float2 llllllllllllllllll5 = lllllllllllllllll5 + lllllllllll4;
                                            float2 lllllllllllllllllll5 = (llllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                            float4 llllllllllllllllllll5 = float4(0,0,0,0);
                                            #if _REPLACEMENT
                                                llllllllllllllllllll5 = tex2D(_DissolveMaskGlobal, lllllllllllllllllll5);
                                            #else
                                                llllllllllllllllllll5 = tex2D(_DissolveMask, lllllllllllllllllll5);
                                            #endif
                                            float lllllllllllllllllllll5 = -1;
                                            if(llllllllllllllllll5.x <= llllllllllllllll5.z && llllllllllllllllll5.x >= 0 && llllllllllllllllll5.y <= llllllllllllllll5.w && llllllllllllllllll5.y >= 0 && llllllllllllllllllll5.x <= 0 && llllllll4 > 0 ){
                                                float llllllllllllllllllllll5 = sqrt(pow(llllllllllllllll5.z,2)+pow(llllllllllllllll5.w,2))/2;
                                                float lllllllllllllllllllllll5 = 40;
                                                float llllllllllllllllllllllll5 = llllllllllllllllllllll5/lllllllllllllllllllllll5;
                                                float lllllllllllllllllllllllll5 = 0;
                                                lllllllllllllllllllll5 = 0;     
                                                #if _REPLACEMENT
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMaskGlobal, float4(lllllllllllllllllllllllllll5, 0.0, 0.0));                           
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        } 
                                                    }   
                                                #else
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMask, float4(lllllllllllllllllllllllllll5, 0.0, 0.0)); 
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        }                                            
                                                    }   
                                                #endif
                                                lllllllllllllllllllll5 = 1-lllllllllllllllllllllllll5;  
                                            }         
                                            llllllllll4 = lllllllllllllllllllll5;
                                        }
                                    #endif
                                    if(llllllllllllllllllll0 <= 1) {
                                        if(llllllllll4 != -1) {
                                            float lll6 = max(llllllllllllllllllll0,0.00001);
                                            float llll6 = 1-lllllllll4;
                                            float lllll6 = exp(lll6*6);
                                            float llllll6 = llllllllll4;
                                            float lllllll6 = llll6/(lll6/(lll6*llll6-0.15*(lll6-llll6)));
                                            float llllllll6 = ((llllll6-lllllll6)/(lllll6*(1-llllll6)+llllll6))+lllllll6;
                                            llllllll6 = 1-llllllll6;
                                            lllllllllllllllllll3 =  llllllll6 * sign(lllllllll4);
                                        }
                                    } else {
                                        lllllllllllllllllll3 = llllllllll4;
                                    }
                                } 
                                if (llllllllll0 == 1 || llllllllll0 == 3 || llllllllll0 == 5) { 
                                    float lllllllll6 = distance(_WorldSpaceCameraPos, _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz);
                                    float llllllllll6 = distance(_WorldSpaceCameraPos, d.worldSpacePosition.xyz);
                                    float3 lllllllllll6 =  d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 llllllllllll6 =  d.worldSpaceNormal;
                                    float lllllllllllll6 = acos(dot(lllllllllll6,llllllllllll6)/(length(lllllllllll6)*length(llllllllllll6)));
                                    if(lllllllllllll6 <= 1.5 && lllllllll6 > llllllllll6) {
                                        float llllllllllllll6 = (sqrt((lllllllll6-llllllllll6)) * 25 /lllllllllllll6)*lllllllllll0;                   
                                        lllllllllllllllllll3 += max(0,log(llllllllllllll6*0.2));
                                    }
                                }
                            }
                            lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                            if(lllll2) { 
                                if(lllllllllllllllll1 == 1) { 
                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                    }
                                } 
                                else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                    float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                    } else {
                                        lllllllllllllllllll3 = 0;
                                    }
                                }
                            }
                            float lllllllllllllllllll6 = lllllllllllllllllll3 / lllllllll1;
                            if(lllllll1) {
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;
                                float3 lllllllllllllllllllll6 = d.worldSpacePosition.xyz - _WorldSpaceCameraPos;
                                float llllllllllllllllllllll6 = dot(lllllllllllllllllllll6, normalize(llllllllllllllllllllllllllllll2));
                                if(llllllllllllllllllllll6 - llllllll1 >= length(llllllllllllllllllllllllllllll2)) {
                                    float lllllllllllllllllllllll6 = llllllllllllllllllllll6 - llllllll1 - length(llllllllllllllllllllllllllllll2);
                                    if(lllllllllllllllllllllll6 < 0) {
                                        lllllllllllllllllllllll6 = 0;
                                    }
                                    if(lllllllllllllllllllllll6 < lllllllll1) 
                                    {
                                        lllllllllllllllllll3 = (lllllllll1 - lllllllllllllllllllllll6) * lllllllllllllllllll6;
                                    }
                                    else 
                                    {
                                        lllllllllllllllllll3 = 0; 
                                    }
                                }
                            }
                            if(llllllllllllllll1 && !lllll2) {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                            if(llllllllll1 == 1) {
                                float llllllllllllllllllllllll6 = 0;
                                if(llllllllllll1 == 0) {
                                    llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                                } else if (llllllllllll1 == 1) {  
                                    float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                        lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                    }
                                    llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                                }
                                if(lllllllllll1 == 1) {
        	                        if(d.worldSpacePosition.y > (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1)) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1);
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y > lllllllllllll1) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                }
                            }
                            if(lllllllllllllllllllllllll0 == 1) {
                                float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                                if(llllllllllllllllllllllllll0 == 1) {
        	                        if(d.worldSpacePosition.y < (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0)) {
                                        float lllllllllllllllllllllll6 = (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0) - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                        float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                }
                            }
                            if(!_TriggerMode && !_RaycastMode) {
                                if(distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                        if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                            llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                        }
                        if(_TriggerMode || _RaycastMode) {
                            lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                        } else {
                            lllllllllllllllllll3 = lllllllllllllllllll3;
                            if (llllllllllllllll1) {
                                if(lllll2) {
                                    if(lllllllllllllllllll1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                    }
                                } else {
                                    if(lllllllllllllllll1 == 1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                    }
                                }
                            }
                        }
                        lll2 = max(lll2,lllllllllllllllllll3);
                    }   
                #else
                    float llllllllllll3 = 0;
                    if(!ll0) {
                        llllllllllll3 = 1;
                        if( _tDirection!= 0 && _tValue != 0 && _Time.y-_tValue < lllllllllllllllllllllllllllllll0) {
                            if(_tDirection == 1) {
                                llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-_tValue))/lllllllllllllllllllllllllllllll0);
                            } else {
                                llllllllllll3 = ((_Time.y-_tValue)/lllllllllllllllllllllllllllllll0);
                            }
                        } else if(_tDirection ==-1) {
                            llllllllllll3 = 1;
                        } else if(_tDirection == 1) {
                            llllllllllll3 = 0;
                        } else {
                            llllllllllll3 = 1;
                        }
                        llllllllllll3 = 1 - llllllllllll3;
                    }
                    float lllllllllllllllllll3 = 0;     
                    float llllllllllllllllll3 = 0;
                    bool lllllllllllllllllllll3 = distance(_WorldSpaceCameraPos, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                    lllllllllllllllllllll3 = false;
                    if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                    
                        #if _ZONING
                            if(llllllllllllllll1) {
                                if(lllll2) 
                                {
                                    if(lllllllllllllllllll1) {
                                        float lll3 = lllllllll2;
                                        float llll3 = lllllllllll2;
                                        llllllllllllllllll3 = 1;
                                        float llllllllllllllllllllllllll3 = llllllllll2;
                                        if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                            if(llll3 == 1) {
                                                llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                            } else {
                                                llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                            }
                                        } else if(llll3 ==-1) {
                                            llllllllllllllllll3 = 1;
                                        } else if(llll3 == 1) {
                                            llllllllllllllllll3 = 0;
                                        } else {
                                            llllllllllllllllll3 = 1;
                                        }
                                        llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                        if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                            if (lllllll2 < llllllllllllllllll1)  {
                                                float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                            }
                                        }
                                    } else { 
                                    }
                                } else {
                                }
                            }
                        #endif
                        lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                        if(lllll2) {
                            if(lllllllllllllllll1 == 1) { 
                                float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                }
                            } 
                            else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                } else {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        if(llllllllllllllll1 && !lllll2) {
                            if(lllllllllllllllll1 == 1) {
                                lllllllllllllllllll3 = 0;
                            }
                        }
                        if(llllllllll1 == 1 && lllllllllll1 == 0) {
                            float llllllllllllllllllllllll6 = 0;
                            if(llllllllllll1 == 0) { 
                                llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                            } else if (llllllllllll1 == 1) {  
                                float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                    lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                }
                                llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                            }
                            if(d.worldSpacePosition.y > lllllllllllll1) {
                                float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(llllllllllll1 == 0) { 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                        lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 0;  
                                    }
                                } else {                                 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) { 
                                        lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 1;  
                                    }
                                    llllllllllllllllll3 = 1;
                                }
                            }
                        }
                        if(lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 0) {
                            float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                            if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                } else {
                                    lllllllllllllllllll3 = 0;  
                                }
                            }                                
                        }
                    } 
                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                        llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                    }
                    if(_TriggerMode || _RaycastMode) {
                        lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                    } else {
                        lllllllllllllllllll3 = lllllllllllllllllll3;
                        if (llllllllllllllll1) {
                            if(lllll2) {
                                if(lllllllllllllllllll1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                }
                            } else {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                }
                            }
                        }
                    }
                    lll2 = max(lll2,lllllllllllllllllll3);    
                #endif
                float llllllllllllllllllll3 = lll2;
                if(!llllllllllllllllllllll0) {
                    if (llllllllllllllllllll3 == 1) {
                        llllllllllllllllllll3 = 10;
                    }
                    if (!llllllllllllllllllllllll0 || llllllllll0 == 6) {
                    #if defined(UNITY_PASS_SHADOWCASTER) 
                    #if defined(SHADOWS_DEPTH) 
                    if (!any(unity_LightShadowBias))
                    {
                            clip(l2- llllllllllllllllllll3);
                    }
                    else
                    {
                        if(llllllllllllllllllllllll0 && llllllllll0 != 6) {
                            clip(l2- llllllllllllllllllll3);                  
                        }
                    }
                    #endif
                    #else
                        clip(l2- llllllllllllllllllll3);
                    #endif
                    } else {                
                        clip(l2- llllllllllllllllllll3);  
                    }
                }       
                if(llllllllllllllllllllll0) {
                    lll0 = 1;
                    if((l2 - llllllllllllllllllll3) < 0) {
                        llll0 = half4(1,1,1,1);
                        o.Emission = 1;
                    } else {
                        llll0 = half4(0,0,0,1);
                    }
                    if(llllllllllllllllllllllllll2) {
                        if((l2 - llllllllllllllllllll3) < 0) {
                            llll0 = half4(0.5,1,0.5,1);
                            o.Emission = 0;
                        } else {
                            llll0 = half4(0,0.1,0,1);
                        }
                    }
                    if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                        float lllllllllllllllllll7 = 0;
                        if(llllllllllllllllllllllllll0 == 1) {
                            lllllllllllllllllllllllll2 = lllllllllllllllllllllllll2 + lllllllllllllllllllll1;    
                            lllllllllllllllllll7 = lllllllllllllllllllllllll2;
                        } else {
                            lllllllllllllllllll7 = lllllllllllllllllllllllllll0 + lllllllllllllllllllll1;
                        }                        
                        if(d.worldSpacePosition.y > (lllllllllllllllllll7 - llllllllllllllllllllll1) && d.worldSpacePosition.y < (lllllllllllllllllll7 + llllllllllllllllllllll1)){
                            llll0 = half4(1,0,0,1);
                        }
                    }
                } else {
                    half3 llllllllllllllllllll7 = lerp(1, lllll0, llllll0).rgb;
                    if(lll1) {
                        llll1 = 0.2 + (llll1*(0.8-0.2)); 
                        o.Emission =  min(clamp(llllllllllllllllllll7 *  clamp(((llllllllllllllllllll3/llll1) - l2),0,1), 0, 1)*sqrt(lllllll0*llllllll0), clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0));
                    } else {
                        o.Emission =  clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0);
                    }
                    #if _HDRP
                        o.Emission =  o.Emission * pow(llllllll0,2);
                    #endif
                }
            }
            float4 lllllllllllllllllllll7;
            lllllllllllllllllllll7.xy = d.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
            lllllllllllllllllllll7.zw = (_UVSec == 0) ? d.texcoord0.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw : d.texcoord1.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
            #ifdef _PARALLAXMAP
                half llllllllllllllllllllll7 = tex2D (_ParallaxMap, lllllllllllllllllllll7.xy).g;
                float2 lllllllllllllllllllllll7 = ParallaxOffset1Step (llllllllllllllllllllll7, _Parallax, d.tangentSpaceViewDir);
                lllllllllllllllllllll7 = float4(lllllllllllllllllllll7.xy + lllllllllllllllllllllll7, lllllllllllllllllllll7.zw + lllllllllllllllllllllll7);
                o.Normal = float3(0, 0, 1);
            #endif
            if(lll0) {
                o.Albedo = llll0.rgb;
            } else {
                half3 llllllllllllllllllllllll7 = tex2D(_MainTex, lllllllllllllllllllll7.xy).rgb * _Color.rgb; 
                #ifdef _DETAIL_MULX2
                     half3 lllllllllllllllllllllllll7 = tex2D (_DetailAlbedoMap, lllllllllllllllllllll7.zw).rgb;
                     llllllllllllllllllllllll7 *= LerpWhiteTo (lllllllllllllllllllllllll7 * unity_ColorSpaceDouble.rgb, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
                #endif
                o.Albedo = llllllllllllllllllllllll7;
            }
            #ifdef _NORMALMAP
                half3 llllllllllll6 = UnpackScaleNormal(tex2D(_BumpMap, lllllllllllllllllllll7.xy), _BumpScale);
                #ifdef _DETAIL_MULX2
                    half3 lllllllllllllllllllllllllll7 = UnpackScaleNormal(tex2D(_DetailNormalMap, lllllllllllllllllllll7.zw), _DetailNormalMapScale);
	                lllllllllllllllllllllllllll7 = lerp(float3(0, 0, 1), lllllllllllllllllllllllllll7, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
	                llllllllllll6 = BlendNormals(llllllllllll6, lllllllllllllllllllllllllll7);
                #endif
                o.Normal = llllllllllll6;
            #endif
            half2 llllllllllllllllllllllllllll7 = half2(0,0);
            #ifdef _METALLICGLOSSMAP
                half4 lllllllllllllllllllllllllllll7 = tex2D(_MetallicGlossMap, lllllllllllllllllllll7.xy);
                llllllllllllllllllllllllllll7.r = lllllllllllllllllllllllllllll7.r;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g = tex2D(_MainTex, lllllllllllllllllllll7.xy).a;
                #else
                    llllllllllllllllllllllllllll7.g = lllllllllllllllllllllllllllll7.a;
                #endif
                llllllllllllllllllllllllllll7.g *= _GlossMapScale;
            #else
                llllllllllllllllllllllllllll7.r = _Metallic;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g  = tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _GlossMapScale;
                #else
                    llllllllllllllllllllllllllll7.g = _Glossiness;
                #endif
            #endif
            o.Metallic = llllllllllllllllllllllllllll7.r;
            o.Smoothness = llllllllllllllllllllllllllll7.g;
            o.Occlusion = lerp(1, tex2D(_OcclusionMap, lllllllllllllllllllll7.xy).g, _OcclusionStrength);
            #ifdef _EMISSION
                o.Emission += tex2D(_EmissionMap, lllllllllllllllllllll7.xy).rgb * _EmissionColor.rgb;
            #endif
            half llllllllllllllllllllllllllllll7 = 0;
            #if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
                llllllllllllllllllllllllllllll7 = _Color.a;
            #else
                llllllllllllllllllllllllllllll7 =  tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _Color.a;
            #endif
            if (_Mode) 
            {
                clip (llllllllllllllllllllllllllllll7 - _Cutoff);
            }
            o.Alpha = llllllllllllllllllllllllllllll7;

	}



        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                 //  Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                 // v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                 // v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                 // v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                 // v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                 // v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
               // d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
               // v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
            // d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
            // d.screenPos = i.screenPos;
            // d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
            // d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
            // d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
            // d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
            // d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
            // d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if _PASSSHADOW
            float3 _LightDirection;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
           // o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);


          #if _PASSSHADOW
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, _LightDirection));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif


          // #if %SCREENPOSREQUIREKEY%
          // o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              #if _BAKEDLIT
                 half3 vertexLight = 0;
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
              #endif
              half fogFactor = ComputeFogFactor(o.pos.z);
              o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);
               UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
               Surface l = (Surface)0;

               #ifdef _DEPTHOFFSET_ON
                  l.outputDepth = outputDepth;
               #endif

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               #ifdef _DEPTHOFFSET_ON
                  outputDepth = l.outputDepth;
               #endif

               return 0;

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "Meta"
            Tags 
            { 
                "LightMode" = "Meta"
            }

             // Render State
            Blend One Zero, One Zero
            Cull Back
            ZTest LEqual
            ZWrite On
            // ColorMask: <None>

            

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
        
            #define SHADERPASS_META
            #define _PASSMETA 1


            
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _METALLICGLOSSMAP
        #pragma shader_feature_local _PARALLAXMAP
        #pragma shader_feature_local _DETAIL_MULX2
        #pragma shader_feature_local _EMISSION       
        #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma shader_feature_local_fragment _OBSTRUCTION_CURVE

        #pragma shader_feature_local_fragment _DISSOLVEMASK
	        #pragma shader_feature_local_fragment _ZONING
        #pragma shader_feature_local_fragment _REPLACEMENT
        #pragma shader_feature_local_fragment _PLAYERINDEPENDENT



   #define _URP 1
#define _USINGTEXCOORD1 1



            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"
        

                  #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
         // float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
         // half4 vertexColor : COLOR;
         // #endif

         // #if %EXTRAV2F0REQUIREKEY%
         // float4 extraV2F0 : TEXCOORD12;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
         // float4 extraV2F1 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
         // float4 extraV2F2 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
         // float4 extraV2F3 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
         // float4 extraV2F4 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD19;
         // #endif
            
         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD9;
         #endif
            float4 fogFactorAndVertexLight : TEXCOORD10;
            float4 shadowCoord : TEXCOORD11;
         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


            
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
               // float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetObjectToWorldMatrix()
               #undef GetWorldToObjectMatrix()
               #undef GetWorldToViewMatrix()
               #undef UNITY_MATRIX_I_V
               #undef UNITY_MATRIX_P
               #undef GetWorldToHClipMatrix()
               #undef GetObjectToWorldMatrix()V
               #undef UNITY_MATRIX_T_MV
               #undef UNITY_MATRIX_IT_MV
               #undef GetObjectToWorldMatrix()VP

               #define GetObjectToWorldMatrix()     unity_ObjectToWorld
               #define GetWorldToObjectMatrix()   unity_WorldToObject
               #define GetWorldToViewMatrix()     unity_MatrixV
               #define UNITY_MATRIX_I_V   unity_MatrixInvV
               #define GetViewToHClipMatrix()     OptimizeProjectionMatrix(glstate_matrix_projection)
               #define GetWorldToHClipMatrix()    unity_MatrixVP
               #define GetObjectToWorldMatrix()V    mul(GetWorldToViewMatrix(), GetObjectToWorldMatrix())
               #define UNITY_MATRIX_T_MV  transpose(GetObjectToWorldMatrix()V)
               #define UNITY_MATRIX_IT_MV transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V))
               #define GetObjectToWorldMatrix()VP   mul(GetWorldToHClipMatrix(), GetObjectToWorldMatrix())


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
            CBUFFER_START(UnityPerMaterial)

               
        float4 _MainTex_ST;
        float4 _DetailAlbedoMap_ST;
	        half4 _Color;
        half _Cutoff; 
        half _Mode;
        float _Cull;
        half _BumpScale; 
        half _DetailNormalMapScale;
        half _Metallic;
        float _Glossiness;
        float _GlossMapScale;
        half _OcclusionStrength;
        half _Parallax;
        half _UVSec;
        half3 _EmissionColor;
        float _IsReplacementShader;
        float _TriggerMode;
        float _RaycastMode;
        float _IsExempt;
        float _isReferenceMaterial;
        float _InteractionMode;
        int _ArrayLength = 0;
        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
            float4 _PlayersPosVectorArray[20];
            float _PlayersDataFloatArray[150];     
        #else
            float4 _PlayersPosVectorArray[100];
            float _PlayersDataFloatArray[500];  
        #endif
        float _tDirection = 0;
        float _numOfPlayersInside = 0;
        float _tValue = 0;
        float _id = 0;
        #if _ZONING
            #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
                float _ZDFA[500];
            #else
                float _ZDFA[1000];
            #endif
            float _ZonesDataCount;
        #endif
        #if _REPLACEMENT        
            half4 _DissolveColorGlobal;
            float _DissolveColorSaturationGlobal;
            float _DissolveEmissionGlobal;
            float _DissolveEmissionBoosterGlobal;
            float _TextureVisibilityGlobal;
            float _ObstructionGlobal;
            float _AngleStrengthGlobal;
            float _ConeStrengthGlobal;
            float _ConeObstructionDestroyRadiusGlobal;
            float _CylinderStrengthGlobal;
            float _CylinderObstructionDestroyRadiusGlobal;
            float _CircleStrengthGlobal;
            float _CircleObstructionDestroyRadiusGlobal;
            float _CurveStrengthGlobal;
            float _CurveObstructionDestroyRadiusGlobal;
            float _DissolveFallOffGlobal;
            float _IntrinsicDissolveStrengthGlobal;
            float _PreviewModeGlobal;
            float _UVsGlobal;
            float _hasClippedShadowsGlobal;
            float _FloorGlobal;
            float _FloorModeGlobal;
            float _FloorYGlobal;
            float _PlayerPosYOffsetGlobal;
            float _FloorYTextureGradientLengthGlobal;
            float _AnimationEnabledGlobal;
            float _AnimationSpeedGlobal;
            float _DefaultEffectRadiusGlobal;
            float _TransitionDurationGlobal;        
            float _TexturedEmissionEdgeGlobal;
            float _TexturedEmissionEdgeStrengthGlobal;
            float _IsometricExclusionGlobal;
            float _IsometricExclusionDistanceGlobal;
            float _IsometricExclusionGradientLengthGlobal;
            float _CeilingGlobal;
            float _CeilingModeGlobal;
            float _CeilingBlendModeGlobal;
            float _CeilingYGlobal;
            float _CeilingPlayerYOffsetGlobal;
            float _CeilingYGradientLengthGlobal;
            float _ZoningGlobal;
            float _ZoningModeGlobal;
            float _ZoningEdgeGradientLengthGlobal;
            float _IsZoningRevealableGlobal;
            float _SyncZonesWithFloorYGlobal;
            float _SyncZonesFloorYOffsetGlobal;
            float4 _ObstructionCurveGlobal_TexelSize;
            float4 _DissolveMaskGlobal_TexelSize;
            float _DissolveMaskEnabledGlobal;
            float _PreviewIndicatorLineThicknessGlobal;
        #else
            half _TextureVisibility;
            half _AngleStrength;
            float _Obstruction;
            float _UVs;
            float4 _ObstructionCurve_TexelSize;      
            float _DissolveMaskEnabled;
            float4 _DissolveMask_TexelSize;
            half4 _DissolveColor;
            float _DissolveColorSaturation;
            float _DissolveEmission;
            float _DissolveEmissionBooster;
            float _hasClippedShadows;
            float _ConeStrength;
            float _ConeObstructionDestroyRadius;
            float _CylinderStrength;
            float _CylinderObstructionDestroyRadius;
            float _CircleStrength;
            float _CircleObstructionDestroyRadius;
            float _CurveStrength;
            float _CurveObstructionDestroyRadius;
            float _IntrinsicDissolveStrength;
            float _DissolveFallOff;
            float _PreviewMode;
            float _PreviewIndicatorLineThickness;
            float _AnimationEnabled;
            float _AnimationSpeed;
            float _DefaultEffectRadius;
            float _TransitionDuration;
            float _TexturedEmissionEdge;
            float _TexturedEmissionEdgeStrength;
            float _IsometricExclusion;
            float _IsometricExclusionDistance;
            float _IsometricExclusionGradientLength;
            float _Floor;
            float _FloorMode;
            float _FloorY;
            float _FloorYTextureGradientLength;
            float _PlayerPosYOffset;
            float _Ceiling;
            float _CeilingMode;
            float _CeilingBlendMode;
            float _CeilingY;
            float _CeilingPlayerYOffset;
            float _CeilingYGradientLength;
            float _Zoning;
            float _ZoningMode;
            float _ZoningEdgeGradientLength;
            float _IsZoningRevealable;
            float _SyncZonesWithFloorY;
            float _SyncZonesFloorYOffset;
        #endif




            CBUFFER_END

            

            

            #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()

	        sampler2D _MainTex;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailMask;
            sampler2D _BumpMap;
            sampler2D _DetailNormalMap;
            sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
            sampler2D _ParallaxMap;
            sampler2D _EmissionMap;
        #if _REPLACEMENT
            sampler2D _DissolveTexGlobal;
        #else
            sampler2D _DissolveTex;
        #endif
        #if _DISSOLVEMASK
            #if _REPLACEMENT
                sampler2D _DissolveMaskGlobal;
            #else
                sampler2D _DissolveMask;
            #endif
        #endif
        #if _REPLACEMENT
            sampler2D _ObstructionCurveGlobal;
        #else
            sampler2D _ObstructionCurve;
        #endif


	    #if _URP
        half2 ParallaxOffset1Step (half h, half height, half3 viewDir)
        {
            h = h * height - height/2.0;
            half3 v = normalize(viewDir);
            v.z += 0.42;
            return h * (v.xy / v.z);
        }
        #ifdef UNITY_COLORSPACE_GAMMA
            #define unity_ColorSpaceDouble half4(2.0, 2.0, 2.0, 2.0)
        #else 
            #define unity_ColorSpaceDouble half4(4.59479380, 4.59479380, 4.59479380, 2.0)
        #endif
        half3 BlendNormals(half3 n1, half3 n2)
        {
            return normalize(half3(n1.xy + n2.xy, n1.z*n2.z));
        }
    #endif


	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
                  bool l0;
            #if _REPLACEMENT
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDurationGlobal ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #else
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDuration ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #endif
            bool ll0 = !_TriggerMode && !_RaycastMode;
            float lll0 = 0;
            half4 llll0 = half4(0,0,0,0);
            if(!_IsExempt && (l0 || ll0) ) {
                half4 lllll0;
                float llllll0;
                float lllllll0;
                float llllllll0;
                half lllllllll0;
                float llllllllll0;
                half lllllllllll0;
                float llllllllllll0;
                float lllllllllllll0;
                float llllllllllllll0;
                float lllllllllllllll0;
                float llllllllllllllll0;
                float lllllllllllllllll0;                
                float llllllllllllllllll0;
                float lllllllllllllllllll0;
                float llllllllllllllllllll0;
                float lllllllllllllllllllll0;
                float llllllllllllllllllllll0;
                float lllllllllllllllllllllll0;
                float llllllllllllllllllllllll0;
                float lllllllllllllllllllllllll0;
                float llllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllllll0;
                float l1;
                float ll1;
                float lll1;
                float llll1;
                float lllll1;
                float4 llllll1;
                float lllllll1;
                float llllllll1;
                float lllllllll1;
                float llllllllll1;
                float lllllllllll1;
                float llllllllllll1;
                float lllllllllllll1;
                float llllllllllllll1;
                float lllllllllllllll1;
                float llllllllllllllll1;
                float lllllllllllllllll1;
                float llllllllllllllllll1;
                float lllllllllllllllllll1;
                float llllllllllllllllllll1;
                float lllllllllllllllllllll1;
                float llllllllllllllllllllll1;
                float4 lllllllllllllllllllllll1;
                #if _REPLACEMENT
                    lllll0 = _DissolveColorGlobal;
                    llllll0 = _DissolveColorSaturationGlobal;
                    lllllll0 = _DissolveEmissionGlobal;
                    llllllll0 = _DissolveEmissionBoosterGlobal;
                    lllllllll0 = _TextureVisibilityGlobal;
                    llllllllll0 = _ObstructionGlobal;
                    lllllllllll0 = _AngleStrengthGlobal;
                    llllllllllll0 = _ConeStrengthGlobal;
                    lllllllllllll0 = _ConeObstructionDestroyRadiusGlobal;
                    llllllllllllll0 = _CylinderStrengthGlobal;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadiusGlobal;
                    llllllllllllllll0 = _CircleStrengthGlobal;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadiusGlobal;
                    llllllllllllllllll0 = _CurveStrengthGlobal;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadiusGlobal;
                    llllllllllllllllllll0 = _DissolveFallOffGlobal;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrengthGlobal;
                    llllllllllllllllllllll0 = _PreviewModeGlobal;
                    lllllllllllllllllllllll0 = _UVsGlobal;
                    llllllllllllllllllllllll0 = _hasClippedShadowsGlobal;                    
                    lllllllllllllllllllllllll0 = _FloorGlobal;
                    llllllllllllllllllllllllll0 = _FloorModeGlobal;
                    lllllllllllllllllllllllllll0 = _FloorYGlobal;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffsetGlobal;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLengthGlobal; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadiusGlobal;
                    lllllllllllllllllllllllllllllll0 = _TransitionDurationGlobal;
                    l1 = _AnimationEnabledGlobal;
                    ll1 = _AnimationSpeedGlobal;
                    lll1 = _TexturedEmissionEdgeGlobal;           
                    llll1 = _TexturedEmissionEdgeStrengthGlobal;
                    lllll1 = _DissolveMaskEnabledGlobal;
                    llllll1 = _DissolveMaskGlobal_TexelSize;
                    lllllll1 = _IsometricExclusionGlobal;
                    llllllll1 = _IsometricExclusionDistanceGlobal;
                    lllllllll1 = _IsometricExclusionGradientLengthGlobal;
                    llllllllll1 = _CeilingGlobal;
                    lllllllllll1 = _CeilingModeGlobal;
                    llllllllllll1 = _CeilingBlendModeGlobal;
                    lllllllllllll1 = _CeilingYGlobal;
                    llllllllllllll1 = _CeilingPlayerYOffsetGlobal;
                    lllllllllllllll1 = _CeilingYGradientLengthGlobal;
                    llllllllllllllll1 = _ZoningGlobal;
                    lllllllllllllllll1 = _ZoningModeGlobal;
                    llllllllllllllllll1 = _ZoningEdgeGradientLengthGlobal;
                    lllllllllllllllllll1 = _IsZoningRevealableGlobal;
                    llllllllllllllllllll1 = _SyncZonesWithFloorYGlobal;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffsetGlobal;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThicknessGlobal;
                    lllllllllllllllllllllll1 = _ObstructionCurveGlobal_TexelSize;
                #else
                    lllll0 = _DissolveColor;
                    llllll0 = _DissolveColorSaturation;
                    lllllll0 = _DissolveEmission;
                    llllllll0 = _DissolveEmissionBooster;
                    lllllllll0 = _TextureVisibility;
                    llllllllll0 = _Obstruction;
                    lllllllllll0 = _AngleStrength;
                    llllllllllll0 = _ConeStrength;
                    lllllllllllll0 = _ConeObstructionDestroyRadius;
                    llllllllllllll0 = _CylinderStrength;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadius;
                    llllllllllllllll0 = _CircleStrength;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadius;
                    llllllllllllllllll0 = _CurveStrength;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadius;
                    llllllllllllllllllll0 = _DissolveFallOff;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrength;
                    llllllllllllllllllllll0 = _PreviewMode;
                    lllllllllllllllllllllll0 = _UVs;
                    llllllllllllllllllllllll0 = _hasClippedShadows;                    
                    lllllllllllllllllllllllll0 = _Floor;
                    llllllllllllllllllllllllll0 = _FloorMode;
                    lllllllllllllllllllllllllll0 = _FloorY;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffset;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLength; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadius;
                    lllllllllllllllllllllllllllllll0 = _TransitionDuration;
                    l1 = _AnimationEnabled;
                    ll1 = _AnimationSpeed;
                    lll1 = _TexturedEmissionEdge;           
                    llll1 = _TexturedEmissionEdgeStrength;
                    lllll1 = _DissolveMaskEnabled;
                    llllll1 = _DissolveMask_TexelSize;
                    lllllll1 = _IsometricExclusion;
                    llllllll1 = _IsometricExclusionDistance;
                    lllllllll1 = _IsometricExclusionGradientLength;
                    llllllllll1 = _Ceiling;
                    lllllllllll1 = _CeilingMode;
                    llllllllllll1 = _CeilingBlendMode;
                    lllllllllllll1 = _CeilingY;
                    llllllllllllll1 = _CeilingPlayerYOffset;
                    lllllllllllllll1 = _CeilingYGradientLength;
                    llllllllllllllll1 = _Zoning;
                    lllllllllllllllll1 = _ZoningMode;
                    llllllllllllllllll1 = _ZoningEdgeGradientLength;
                    lllllllllllllllllll1 = _IsZoningRevealable;
                    llllllllllllllllllll1 = _SyncZonesWithFloorY;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffset;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThickness;
                    lllllllllllllllllllllll1 = _ObstructionCurve_TexelSize;
                #endif
                if(lllllllllllllllllllll0 < 0) {
                    lllllllllllllllllllll0 = 0;
                }
                float3 llllllllllllllllllllllll1;
                d.worldSpaceNormal = mul(o.Normal, (float3x3)d.TBNMatrix);
                float3 lllllllllllllllllllllllll1 = d.worldSpacePosition / (-1.0 * abs(lllllllllllllllllllllll0) );
                if(l1) {
                    lllllllllllllllllllllllll1 = lllllllllllllllllllllllll1 + abs(((_Time.y) * ll1));
                }  
                float3 llllllllllllllllllllllllll1 = float3(0,0,0);
                float3 lllllllllllllllllllllllllll1 = float3(0,0,0);
                float3 llllllllllllllllllllllllllll1 = float3(0,0,0);
                #if _REPLACEMENT
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xy ).rgb;
                #else
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xy ).rgb;
                #endif
                float lllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.x);
                float llllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.z);
                float3 lllllllllllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllll1, llllllllllllllllllllllllll1, lllllllllllllllllllllllllllll1 ).rgb;
                llllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllllllll1, llllllllllllllllllllllllllll1, llllllllllllllllllllllllllllll1 ).rgb;
                half l2 = llllllllllllllllllllllll1.r;  
                float3 ll2 = GetWorldToViewMatrix()[2].xyz;
                #if _HDRP
                    ll2 =  mul(GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);
                #else
                    ll2 = - mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                #endif
                float lll2 = 0;
                float llll2 = 1;
                bool lllll2 = false;
                float llllll2 = 0;
                float lllllll2 = 0;
                float llllllll2 = 0;
                float lllllllll2 = 0;
                float llllllllll2 = 0;
                float lllllllllll2 = 0;
                #if _ZONING
                    if(llllllllllllllll1) {
                        float llllllllllll2 = 0;
                        for (int z = 0; z < _ZonesDataCount; z++){
                            bool lllllllllllll2 = false;
                            float llllllllllllll2 = llllllllllll2;
                            if (_ZDFA[llllllllllll2 + 1] == 0) {  
                                float lllllllllllllll2 = llllllllllll2 + 2; 
                                float3 llllllllllllllll2 = d.worldSpacePosition - float3(_ZDFA[lllllllllllllll2],_ZDFA[lllllllllllllll2+1], _ZDFA[lllllllllllllll2+2]);
                                float3 lllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 3],_ZDFA[lllllllllllllll2+ 4], _ZDFA[lllllllllllllll2+ 5]);
                                float3 llllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 6],_ZDFA[lllllllllllllll2+ 7], _ZDFA[lllllllllllllll2+ 8]);
                                float3 lllllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 9],_ZDFA[lllllllllllllll2+10], _ZDFA[lllllllllllllll2+11]);
                                float3 llllllllllllllllllll2 = float3(_ZDFA[lllllllllllllll2+12],_ZDFA[lllllllllllllll2+13], _ZDFA[lllllllllllllll2+14]);
                                lllllllllllll2 =    abs(dot(llllllllllllllll2, lllllllllllllllll2)) <= llllllllllllllllllll2.x &&
                                            abs(dot(llllllllllllllll2, llllllllllllllllll2)) <= llllllllllllllllllll2.y &&
                                            abs(dot(llllllllllllllll2, lllllllllllllllllll2)) <= llllllllllllllllllll2.z;
                                if(lllllllllllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                                    llllllll2 = _ZDFA[lllllllllllllll2+1] - _ZDFA[lllllllllllllll2+13];  
                                    if(llllllllllllllllllllllllll0 == 0) {                                    
                                        bool lllllllllllllllllllll2 = ((llllllll2 - lllllllllllllllllllll1)  <= lllllllllllllllllllllllllll0); 
                                        if(!lllllllllllllllllllll2) {
                                            lllllllllllll2 = false;
                                        }
                                    }
                                }
                                llllllllllll2 = llllllllllll2 + 17 + 3; 
                                if(lllllllllllll2) {
                                    float llllllllllllllllllllll2 = llllllllllllllllllll2.x - abs(dot(llllllllllllllll2, lllllllllllllllll2));
                                    float lllllllllllllllllllllll2 = llllllllllllllllllll2.y - abs(dot(llllllllllllllll2, llllllllllllllllll2));
                                    float llllllllllllllllllllllll2 = llllllllllllllllllll2.z - abs(dot(llllllllllllllll2, lllllllllllllllllll2));
                                    lllllll2 = min(lllllllllllllllllllllll2,llllllllllllllllllllll2);
                                    lllllll2 = min(lllllll2,llllllllllllllllllllllll2);
                                    if(lllllll2<0) {
                                        lllllll2 = 0;
                                    }
                                }
                            }
                            if(lllllllllllll2) {
                                llllll2 = _ZDFA[llllllllllllll2];
                                lllll2 = true;    
                                lllllllll2 = _ZDFA[llllllllllllll2 + 17];
                                lllllllllll2 = _ZDFA[llllllllllllll2 + 18];
                                llllllllll2 = _ZDFA[llllllllllllll2 + 19];
                                break;                        
                            }
                        }
                    }
                #endif
                float lllllllllllllllllllllllll2 = 0;
                float llllllllllllllllllllllllll2 = lllll2;
                #if _PLAYERINDEPENDENT == 0
                    #if _ZONING
                        if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                            float lllllllllllllllllllllllllll2 = 0;
                            bool llllllllllllllllllllllllllll2 = false;
                            for (int i = 0; i < _ArrayLength; i++){
                                float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;               
                                if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {       
                                    if(!ll0) {
                                        float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                                        float l3 = 4;
                                        for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                            float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                            if (ll3 != 0 && ll3 == _id) {
                                                float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                if ((llll3 == -1 && _Time.y - lll3 < lllllllllllllllllllllllllllllll0 )|| (llll3 == 1) ) {
                                                    float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                                    if(llllllllllllllllllllll0) {
                                                        if(i==0) {
                                                            lllllllllllllllllllllllll2 = lllll3;
                                                        } else {
                                                            lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                                        }
                                                    }
                                                    bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                                    if(!llllll3) {
                                                        llllllllllllllllllllllllllll2 = true;
                                                    } 
                                                }                        
                                            }
                                        }
                                    } else if (distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition.xyz) < llllllllllllllllllllllllllllll0) {
                                        float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                        if(llllllllllllllllllllll0) {
                                            if(i==0) {
                                                lllllllllllllllllllllllll2 = lllll3;
                                            } else {
                                                lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                            }
                                        }
                                        bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                        if(!llllll3) {
                                            llllllllllllllllllllllllllll2 = true;
                                        } 
                                    }
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                                }
                            }
                            if(!llllllllllllllllllllllllllll2) {
                                lllll2 = false;
                            }
                        }
                    #endif
                    float lllllllllllllllllllllllllll2 = 0;
                    for (int i = 0; i < _ArrayLength; i++){
                        float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                        float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;                 
                        float llllllllllll3 = 0;
                        float l3 = 4;
                        if(!ll0) {
                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                if (ll3 != 0 && ll3 == _id) {
                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                    llllllllllll3 = 1;
                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < lllllllllllllllllllllllllllllll0) {
                                        if(llll3 == 1) {
                                            llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-lll3))/lllllllllllllllllllllllllllllll0);
                                        } else {
                                            llllllllllll3 = ((_Time.y-lll3)/lllllllllllllllllllllllllllllll0);
                                        }
                                    } else if(llll3 ==-1) {
                                        llllllllllll3 = 1;
                                    } else if(llll3 == 1) {
                                        llllllllllll3 = 0;
                                    } else {
                                        llllllllllll3 = 1;
                                    }
                                    llllllllllll3 = 1 - llllllllllll3;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                        float llllllllllllllllll3 = 0;
                        float lllllllllllllllllll3 = 0;     
                        float llllllllllllllllllll3 = lllllllllllllllllll3;
                        bool lllllllllllllllllllll3 = distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                        if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                        
                            #if _ZONING
                                if(llllllllllllllll1) {
                                    if(lllll2) 
                                    {
                                        if(lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 1;
                                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2]; llllll6++){
                                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                                if (ll3 != 0 && ll3 == llllll2) {
                                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                    llllllllllllllllll3 = 1;
                                                    float llllllllllllllllllllllllll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 3];
                                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                                        if(llll3 == 1) {
                                                            llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                                        } else {
                                                            llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                                        }
                                                    } else if(llll3 ==-1) {
                                                        llllllllllllllllll3 = 1;
                                                    } else if(llll3 == 1) {
                                                        llllllllllllllllll3 = 0;
                                                    } else {
                                                        llllllllllllllllll3 = 1;
                                                    }
                                                    llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                                }
                                                if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                                    if (lllllll2 < llllllllllllllllll1)  {
                                                        float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                        llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                                    }
                                                }
                                            }
                                        } else { 
                                        }
                                    } else {
                                    }
                                }
                            #endif
                            if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {    
                                if (llllllllll0 == 2 || llllllllll0 == 3 || llllllllll0 == 4 || llllllllll0 == 5 || llllllllll0 == 6 || llllllllll0 == 7) {
                                    float4 lllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float4 llllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float lllllllllllllllllllllllllllllll3 = 0;
                                    if(lllll1 || llllllllll0 == 6) {
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        #if _HDRP
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz), 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4 , _ProjectionParams.x);
                                        #else
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4);
                                        #endif
                                        llllllllllllllllllllllllllllll3.xy /= llllllllllllllllllllllllllllll3.w;
                                        llllllllllllllllllllllllllllll3.x *= l4;
                                        #if _HDRP
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(d.worldSpacePosition.xyz), 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4 , _ProjectionParams.x);
                                        #else
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(d.worldSpacePosition.xyz, 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4);
                                        #endif
                                        lllllllllllllllllllllllllllll3.xy /= lllllllllllllllllllllllllllll3.w;
                                        lllllllllllllllllllllllllllll3.x *= l4;
                                        #if _DISSOLVEMASK
                                            if(lllll1) {
                                                #if _REPLACEMENT
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMaskGlobal_TexelSize.z,_DissolveMaskGlobal_TexelSize.w);
                                                #else
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMask_TexelSize.z,_DissolveMask_TexelSize.w);
                                                #endif
                                            }
                                        #endif
                                    }
                                    float3 llllll4 = _WorldSpaceCameraPos - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 lllllll4 = normalize(llllll4);
                                    float llllllll4 = dot(d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, lllllll4);                           
                                    float lllllllll4 = 0;
                                    float llllllllll4 = 0;
                                    float2 lllllllllll4 = float2(0,0);
                                    if (llllllllll0 == 2 || llllllllll0 == 3) { 
                                        lllllllll4 = llllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllll4 = length(llllll4);
                                        float llllllllllllll4 = lllllllllllll0;
                                        float lllllllllllllll4 = (llllllll4 / lllllllllllll4) * llllllllllllll4;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            float llllllllllllllllll4 = llllllllllll4 < lllllllllllllll4;
                                            if(llllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 4 || llllllllll0 == 5 ) { 
                                        lllllllll4 = llllllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllllllllllll4 = lllllllllllllll0;
                                        float llllllllllllllllllllll4 = (llllllllllll4 < lllllllllllllllllllll4) && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if(llllllllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 6) { 
                                        lllllllll4 = llllllllllllllll0;
                                        float llllllllllllllllllllllllll4 = length(llllll4);
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        float llllllllllllllllllllllllllll4 = min(1,l4);
                                        float lllllllllllllllllllllllllllll4 =  distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy) < lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                        float llllllllllllllllllllllllllllll4 = (lllllllllllllllllllllllllllll4)  && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float lllllllllllllllllllllllllllllll4 = lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                            float llllllllllllllll4 = (2*lllllllllllllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if (llllllllllllllllllllllllllllll4) {
                                                float lll5 = (distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy)/(lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4));
                                                llllllllll4 = lll5;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } 
                                    else if (llllllllll0 == 7) { 
                                        #if _OBSTRUCTION_CURVE
                                            lllllllll4 = llllllllllllllllll0;
                                            float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                            float llllllllllllllllllllllllll4 = length(llllll4);
                                            float4 llllll5 = float4(0,0,0,0);
                                            float lllllll5 = lllllllllllllllllllllll1.z;
                                            float llllllll5 = (llllllll4/llllllllllllllllllllllllll4) * lllllll5;
                                            float4 lllllllll5 = float4(0,0,0,0);
                                            lllllllll5 = lllllllllllllllllllllll1;
                                            float2 llllllllll5 = (llllllll5+0.5) * lllllllll5.xy;
                                            #if _REPLACEMENT
                                                llllll5 = tex2D(_ObstructionCurveGlobal, llllllllll5);
                                            #else
                                                llllll5 = tex2D(_ObstructionCurve, llllllllll5);
                                            #endif
                                            float lllllllllll5 = llllll5.r * lllllllllllllllllll0;
                                            float llllllllllll5 = (llllllllllll4 < lllllllllll5) && llllllll4 > 0 ;
                                            #if _DISSOLVEMASK
                                                float llllllllllllllll4 = (2*lllllllllll5) / lllllllllllllllllllllllllllllll3;
                                                float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                                lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                                lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                            #else
                                                if(llllllllllll5){
                                                    float lllllllllllllllllll4 = llllllllllll4/lllllllllll5;
                                                    llllllllll4 = lllllllllllllllllll4;
                                                } else {
                                                    llllllllll4 = -1;
                                                }
                                            #endif
                                        #endif
                                    } 
                                    #if _DISSOLVEMASK
                                        if(lllll1) {
                                            float4 llllllllllllllll5 = float4(0,0,0,0);
                                            llllllllllllllll5 = llllll1;
                                            float2 lllllllllllllllll5 = float2(llllllllllllllll5.z/2,llllllllllllllll5.w/2);
                                            float2 llllllllllllllllll5 = lllllllllllllllll5 + lllllllllll4;
                                            float2 lllllllllllllllllll5 = (llllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                            float4 llllllllllllllllllll5 = float4(0,0,0,0);
                                            #if _REPLACEMENT
                                                llllllllllllllllllll5 = tex2D(_DissolveMaskGlobal, lllllllllllllllllll5);
                                            #else
                                                llllllllllllllllllll5 = tex2D(_DissolveMask, lllllllllllllllllll5);
                                            #endif
                                            float lllllllllllllllllllll5 = -1;
                                            if(llllllllllllllllll5.x <= llllllllllllllll5.z && llllllllllllllllll5.x >= 0 && llllllllllllllllll5.y <= llllllllllllllll5.w && llllllllllllllllll5.y >= 0 && llllllllllllllllllll5.x <= 0 && llllllll4 > 0 ){
                                                float llllllllllllllllllllll5 = sqrt(pow(llllllllllllllll5.z,2)+pow(llllllllllllllll5.w,2))/2;
                                                float lllllllllllllllllllllll5 = 40;
                                                float llllllllllllllllllllllll5 = llllllllllllllllllllll5/lllllllllllllllllllllll5;
                                                float lllllllllllllllllllllllll5 = 0;
                                                lllllllllllllllllllll5 = 0;     
                                                #if _REPLACEMENT
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMaskGlobal, float4(lllllllllllllllllllllllllll5, 0.0, 0.0));                           
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        } 
                                                    }   
                                                #else
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMask, float4(lllllllllllllllllllllllllll5, 0.0, 0.0)); 
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        }                                            
                                                    }   
                                                #endif
                                                lllllllllllllllllllll5 = 1-lllllllllllllllllllllllll5;  
                                            }         
                                            llllllllll4 = lllllllllllllllllllll5;
                                        }
                                    #endif
                                    if(llllllllllllllllllll0 <= 1) {
                                        if(llllllllll4 != -1) {
                                            float lll6 = max(llllllllllllllllllll0,0.00001);
                                            float llll6 = 1-lllllllll4;
                                            float lllll6 = exp(lll6*6);
                                            float llllll6 = llllllllll4;
                                            float lllllll6 = llll6/(lll6/(lll6*llll6-0.15*(lll6-llll6)));
                                            float llllllll6 = ((llllll6-lllllll6)/(lllll6*(1-llllll6)+llllll6))+lllllll6;
                                            llllllll6 = 1-llllllll6;
                                            lllllllllllllllllll3 =  llllllll6 * sign(lllllllll4);
                                        }
                                    } else {
                                        lllllllllllllllllll3 = llllllllll4;
                                    }
                                } 
                                if (llllllllll0 == 1 || llllllllll0 == 3 || llllllllll0 == 5) { 
                                    float lllllllll6 = distance(_WorldSpaceCameraPos, _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz);
                                    float llllllllll6 = distance(_WorldSpaceCameraPos, d.worldSpacePosition.xyz);
                                    float3 lllllllllll6 =  d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 llllllllllll6 =  d.worldSpaceNormal;
                                    float lllllllllllll6 = acos(dot(lllllllllll6,llllllllllll6)/(length(lllllllllll6)*length(llllllllllll6)));
                                    if(lllllllllllll6 <= 1.5 && lllllllll6 > llllllllll6) {
                                        float llllllllllllll6 = (sqrt((lllllllll6-llllllllll6)) * 25 /lllllllllllll6)*lllllllllll0;                   
                                        lllllllllllllllllll3 += max(0,log(llllllllllllll6*0.2));
                                    }
                                }
                            }
                            lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                            if(lllll2) { 
                                if(lllllllllllllllll1 == 1) { 
                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                    }
                                } 
                                else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                    float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                    } else {
                                        lllllllllllllllllll3 = 0;
                                    }
                                }
                            }
                            float lllllllllllllllllll6 = lllllllllllllllllll3 / lllllllll1;
                            if(lllllll1) {
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;
                                float3 lllllllllllllllllllll6 = d.worldSpacePosition.xyz - _WorldSpaceCameraPos;
                                float llllllllllllllllllllll6 = dot(lllllllllllllllllllll6, normalize(llllllllllllllllllllllllllllll2));
                                if(llllllllllllllllllllll6 - llllllll1 >= length(llllllllllllllllllllllllllllll2)) {
                                    float lllllllllllllllllllllll6 = llllllllllllllllllllll6 - llllllll1 - length(llllllllllllllllllllllllllllll2);
                                    if(lllllllllllllllllllllll6 < 0) {
                                        lllllllllllllllllllllll6 = 0;
                                    }
                                    if(lllllllllllllllllllllll6 < lllllllll1) 
                                    {
                                        lllllllllllllllllll3 = (lllllllll1 - lllllllllllllllllllllll6) * lllllllllllllllllll6;
                                    }
                                    else 
                                    {
                                        lllllllllllllllllll3 = 0; 
                                    }
                                }
                            }
                            if(llllllllllllllll1 && !lllll2) {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                            if(llllllllll1 == 1) {
                                float llllllllllllllllllllllll6 = 0;
                                if(llllllllllll1 == 0) {
                                    llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                                } else if (llllllllllll1 == 1) {  
                                    float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                        lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                    }
                                    llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                                }
                                if(lllllllllll1 == 1) {
        	                        if(d.worldSpacePosition.y > (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1)) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1);
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y > lllllllllllll1) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                }
                            }
                            if(lllllllllllllllllllllllll0 == 1) {
                                float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                                if(llllllllllllllllllllllllll0 == 1) {
        	                        if(d.worldSpacePosition.y < (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0)) {
                                        float lllllllllllllllllllllll6 = (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0) - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                        float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                }
                            }
                            if(!_TriggerMode && !_RaycastMode) {
                                if(distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                        if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                            llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                        }
                        if(_TriggerMode || _RaycastMode) {
                            lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                        } else {
                            lllllllllllllllllll3 = lllllllllllllllllll3;
                            if (llllllllllllllll1) {
                                if(lllll2) {
                                    if(lllllllllllllllllll1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                    }
                                } else {
                                    if(lllllllllllllllll1 == 1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                    }
                                }
                            }
                        }
                        lll2 = max(lll2,lllllllllllllllllll3);
                    }   
                #else
                    float llllllllllll3 = 0;
                    if(!ll0) {
                        llllllllllll3 = 1;
                        if( _tDirection!= 0 && _tValue != 0 && _Time.y-_tValue < lllllllllllllllllllllllllllllll0) {
                            if(_tDirection == 1) {
                                llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-_tValue))/lllllllllllllllllllllllllllllll0);
                            } else {
                                llllllllllll3 = ((_Time.y-_tValue)/lllllllllllllllllllllllllllllll0);
                            }
                        } else if(_tDirection ==-1) {
                            llllllllllll3 = 1;
                        } else if(_tDirection == 1) {
                            llllllllllll3 = 0;
                        } else {
                            llllllllllll3 = 1;
                        }
                        llllllllllll3 = 1 - llllllllllll3;
                    }
                    float lllllllllllllllllll3 = 0;     
                    float llllllllllllllllll3 = 0;
                    bool lllllllllllllllllllll3 = distance(_WorldSpaceCameraPos, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                    lllllllllllllllllllll3 = false;
                    if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                    
                        #if _ZONING
                            if(llllllllllllllll1) {
                                if(lllll2) 
                                {
                                    if(lllllllllllllllllll1) {
                                        float lll3 = lllllllll2;
                                        float llll3 = lllllllllll2;
                                        llllllllllllllllll3 = 1;
                                        float llllllllllllllllllllllllll3 = llllllllll2;
                                        if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                            if(llll3 == 1) {
                                                llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                            } else {
                                                llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                            }
                                        } else if(llll3 ==-1) {
                                            llllllllllllllllll3 = 1;
                                        } else if(llll3 == 1) {
                                            llllllllllllllllll3 = 0;
                                        } else {
                                            llllllllllllllllll3 = 1;
                                        }
                                        llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                        if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                            if (lllllll2 < llllllllllllllllll1)  {
                                                float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                            }
                                        }
                                    } else { 
                                    }
                                } else {
                                }
                            }
                        #endif
                        lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                        if(lllll2) {
                            if(lllllllllllllllll1 == 1) { 
                                float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                }
                            } 
                            else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                } else {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        if(llllllllllllllll1 && !lllll2) {
                            if(lllllllllllllllll1 == 1) {
                                lllllllllllllllllll3 = 0;
                            }
                        }
                        if(llllllllll1 == 1 && lllllllllll1 == 0) {
                            float llllllllllllllllllllllll6 = 0;
                            if(llllllllllll1 == 0) { 
                                llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                            } else if (llllllllllll1 == 1) {  
                                float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                    lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                }
                                llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                            }
                            if(d.worldSpacePosition.y > lllllllllllll1) {
                                float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(llllllllllll1 == 0) { 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                        lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 0;  
                                    }
                                } else {                                 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) { 
                                        lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 1;  
                                    }
                                    llllllllllllllllll3 = 1;
                                }
                            }
                        }
                        if(lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 0) {
                            float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                            if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                } else {
                                    lllllllllllllllllll3 = 0;  
                                }
                            }                                
                        }
                    } 
                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                        llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                    }
                    if(_TriggerMode || _RaycastMode) {
                        lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                    } else {
                        lllllllllllllllllll3 = lllllllllllllllllll3;
                        if (llllllllllllllll1) {
                            if(lllll2) {
                                if(lllllllllllllllllll1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                }
                            } else {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                }
                            }
                        }
                    }
                    lll2 = max(lll2,lllllllllllllllllll3);    
                #endif
                float llllllllllllllllllll3 = lll2;
                if(!llllllllllllllllllllll0) {
                    if (llllllllllllllllllll3 == 1) {
                        llllllllllllllllllll3 = 10;
                    }
                    if (!llllllllllllllllllllllll0 || llllllllll0 == 6) {
                    #if defined(UNITY_PASS_SHADOWCASTER) 
                    #if defined(SHADOWS_DEPTH) 
                    if (!any(unity_LightShadowBias))
                    {
                            clip(l2- llllllllllllllllllll3);
                    }
                    else
                    {
                        if(llllllllllllllllllllllll0 && llllllllll0 != 6) {
                            clip(l2- llllllllllllllllllll3);                  
                        }
                    }
                    #endif
                    #else
                        clip(l2- llllllllllllllllllll3);
                    #endif
                    } else {                
                        clip(l2- llllllllllllllllllll3);  
                    }
                }       
                if(llllllllllllllllllllll0) {
                    lll0 = 1;
                    if((l2 - llllllllllllllllllll3) < 0) {
                        llll0 = half4(1,1,1,1);
                        o.Emission = 1;
                    } else {
                        llll0 = half4(0,0,0,1);
                    }
                    if(llllllllllllllllllllllllll2) {
                        if((l2 - llllllllllllllllllll3) < 0) {
                            llll0 = half4(0.5,1,0.5,1);
                            o.Emission = 0;
                        } else {
                            llll0 = half4(0,0.1,0,1);
                        }
                    }
                    if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                        float lllllllllllllllllll7 = 0;
                        if(llllllllllllllllllllllllll0 == 1) {
                            lllllllllllllllllllllllll2 = lllllllllllllllllllllllll2 + lllllllllllllllllllll1;    
                            lllllllllllllllllll7 = lllllllllllllllllllllllll2;
                        } else {
                            lllllllllllllllllll7 = lllllllllllllllllllllllllll0 + lllllllllllllllllllll1;
                        }                        
                        if(d.worldSpacePosition.y > (lllllllllllllllllll7 - llllllllllllllllllllll1) && d.worldSpacePosition.y < (lllllllllllllllllll7 + llllllllllllllllllllll1)){
                            llll0 = half4(1,0,0,1);
                        }
                    }
                } else {
                    half3 llllllllllllllllllll7 = lerp(1, lllll0, llllll0).rgb;
                    if(lll1) {
                        llll1 = 0.2 + (llll1*(0.8-0.2)); 
                        o.Emission =  min(clamp(llllllllllllllllllll7 *  clamp(((llllllllllllllllllll3/llll1) - l2),0,1), 0, 1)*sqrt(lllllll0*llllllll0), clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0));
                    } else {
                        o.Emission =  clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0);
                    }
                    #if _HDRP
                        o.Emission =  o.Emission * pow(llllllll0,2);
                    #endif
                }
            }
            float4 lllllllllllllllllllll7;
            lllllllllllllllllllll7.xy = d.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
            lllllllllllllllllllll7.zw = (_UVSec == 0) ? d.texcoord0.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw : d.texcoord1.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
            #ifdef _PARALLAXMAP
                half llllllllllllllllllllll7 = tex2D (_ParallaxMap, lllllllllllllllllllll7.xy).g;
                float2 lllllllllllllllllllllll7 = ParallaxOffset1Step (llllllllllllllllllllll7, _Parallax, d.tangentSpaceViewDir);
                lllllllllllllllllllll7 = float4(lllllllllllllllllllll7.xy + lllllllllllllllllllllll7, lllllllllllllllllllll7.zw + lllllllllllllllllllllll7);
                o.Normal = float3(0, 0, 1);
            #endif
            if(lll0) {
                o.Albedo = llll0.rgb;
            } else {
                half3 llllllllllllllllllllllll7 = tex2D(_MainTex, lllllllllllllllllllll7.xy).rgb * _Color.rgb; 
                #ifdef _DETAIL_MULX2
                     half3 lllllllllllllllllllllllll7 = tex2D (_DetailAlbedoMap, lllllllllllllllllllll7.zw).rgb;
                     llllllllllllllllllllllll7 *= LerpWhiteTo (lllllllllllllllllllllllll7 * unity_ColorSpaceDouble.rgb, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
                #endif
                o.Albedo = llllllllllllllllllllllll7;
            }
            #ifdef _NORMALMAP
                half3 llllllllllll6 = UnpackScaleNormal(tex2D(_BumpMap, lllllllllllllllllllll7.xy), _BumpScale);
                #ifdef _DETAIL_MULX2
                    half3 lllllllllllllllllllllllllll7 = UnpackScaleNormal(tex2D(_DetailNormalMap, lllllllllllllllllllll7.zw), _DetailNormalMapScale);
	                lllllllllllllllllllllllllll7 = lerp(float3(0, 0, 1), lllllllllllllllllllllllllll7, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
	                llllllllllll6 = BlendNormals(llllllllllll6, lllllllllllllllllllllllllll7);
                #endif
                o.Normal = llllllllllll6;
            #endif
            half2 llllllllllllllllllllllllllll7 = half2(0,0);
            #ifdef _METALLICGLOSSMAP
                half4 lllllllllllllllllllllllllllll7 = tex2D(_MetallicGlossMap, lllllllllllllllllllll7.xy);
                llllllllllllllllllllllllllll7.r = lllllllllllllllllllllllllllll7.r;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g = tex2D(_MainTex, lllllllllllllllllllll7.xy).a;
                #else
                    llllllllllllllllllllllllllll7.g = lllllllllllllllllllllllllllll7.a;
                #endif
                llllllllllllllllllllllllllll7.g *= _GlossMapScale;
            #else
                llllllllllllllllllllllllllll7.r = _Metallic;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g  = tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _GlossMapScale;
                #else
                    llllllllllllllllllllllllllll7.g = _Glossiness;
                #endif
            #endif
            o.Metallic = llllllllllllllllllllllllllll7.r;
            o.Smoothness = llllllllllllllllllllllllllll7.g;
            o.Occlusion = lerp(1, tex2D(_OcclusionMap, lllllllllllllllllllll7.xy).g, _OcclusionStrength);
            #ifdef _EMISSION
                o.Emission += tex2D(_EmissionMap, lllllllllllllllllllll7.xy).rgb * _EmissionColor.rgb;
            #endif
            half llllllllllllllllllllllllllllll7 = 0;
            #if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
                llllllllllllllllllllllllllllll7 = _Color.a;
            #else
                llllllllllllllllllllllllllllll7 =  tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _Color.a;
            #endif
            if (_Mode) 
            {
                clip (llllllllllllllllllllllllllllll7 - _Cutoff);
            }
            o.Alpha = llllllllllllllllllllllllllllll7;

	}



        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                 //  Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                 // v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                 // v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                 // v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                 // v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                 // v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
               // d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
               // v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



            

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
            // d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
            // d.screenPos = i.screenPos;
            // d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
            // d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
            // d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
            // d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
            // d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
            // d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

            
         #if _PASSSHADOW
            float3 _LightDirection;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
           // o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);


          #if _PASSSHADOW
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, _LightDirection));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif


          // #if %SCREENPOSREQUIREKEY%
          // o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              #if _BAKEDLIT
                 half3 vertexLight = 0;
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
              #endif
              half fogFactor = ComputeFogFactor(o.pos.z);
              o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


            

            // fragment shader
            half4 Frag (VertexToPixel IN
               #if NEED_FACING
                  , bool facing : SV_IsFrontFace
               #endif
            ) : SV_Target
            {
               UNITY_SETUP_INSTANCE_ID(IN);

               ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );

               Surface l = (Surface)0;

               l.Albedo = half3(0.5, 0.5, 0.5);
               l.Normal = float3(0,0,1);
               l.Occlusion = 1;
               l.Alpha = 1;

               ChainSurfaceFunction(l, d);

               MetaInput metaInput = (MetaInput)0;
               metaInput.Albedo = l.Albedo;
               metaInput.Emission = l.Emission;

               return MetaFragment(metaInput);

            }

         ENDHLSL

      }


      
        Pass
        {
            Name "DepthNormals"
            Tags
            {
               "LightMode" = "DepthNormals"
            }
    
            // Render State
            Cull Back
            Blend One Zero
            ZTest LEqual
            ZWrite On

            

            HLSLPROGRAM

               #pragma vertex Vert
   #pragma fragment Frag

            #pragma target 3.0

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON
        
            #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
            #define _PASSDEPTH 1
            #define _PASSDEPTHNORMALS 1


            
        #pragma shader_feature_local _NORMALMAP
        #pragma shader_feature_local _METALLICGLOSSMAP
        #pragma shader_feature_local _PARALLAXMAP
        #pragma shader_feature_local _DETAIL_MULX2
        #pragma shader_feature_local _EMISSION       
        #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

        #pragma shader_feature_local_fragment _OBSTRUCTION_CURVE

        #pragma shader_feature_local_fragment _DISSOLVEMASK
	        #pragma shader_feature_local_fragment _ZONING
        #pragma shader_feature_local_fragment _REPLACEMENT
        #pragma shader_feature_local_fragment _PLAYERINDEPENDENT



   #define _URP 1
#define _USINGTEXCOORD1 1


            // this has to be here or specular color will be ignored. Not in SG code
            #if _SIMPLELIT
               #define _SPECULAR_COLOR
            #endif


            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Version.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
            #include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"


        

               #undef WorldNormalVector
      #define WorldNormalVector(data, normal) mul(normal, data.TBNMatrix)
      
      #define UnityObjectToWorldNormal(normal) mul(GetObjectToWorldMatrix(), normal)

      #define _WorldSpaceLightPos0 _MainLightPosition
      
      #define UNITY_DECLARE_TEX2D(name) TEXTURE2D(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2D_NOSAMPLER(name) TEXTURE2D(name);
      #define UNITY_DECLARE_TEX2DARRAY(name) TEXTURE2D_ARRAY(name); SAMPLER(sampler##name);
      #define UNITY_DECLARE_TEX2DARRAY_NOSAMPLER(name) TEXTURE2D_ARRAY(name);

      #define UNITY_SAMPLE_TEX2DARRAY(tex,coord)            SAMPLE_TEXTURE2D_ARRAY(tex, sampler##tex, coord.xy, coord.z)
      #define UNITY_SAMPLE_TEX2DARRAY_LOD(tex,coord,lod)    SAMPLE_TEXTURE2D_ARRAY_LOD(tex, sampler##tex, coord.xy, coord.z, lod)
      #define UNITY_SAMPLE_TEX2D(tex, coord)                SAMPLE_TEXTURE2D(tex, sampler##tex, coord)
      #define UNITY_SAMPLE_TEX2D_SAMPLER(tex, samp, coord)  SAMPLE_TEXTURE2D(tex, sampler##samp, coord)

      #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod)   SAMPLE_TEXTURE2D_LOD(tex, sampler_##tex, coord, lod)
      #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) SAMPLE_TEXTURE2D_LOD (tex, sampler##samplertex,coord, lod)
     
      #if defined(UNITY_COMPILER_HLSL)
         #define UNITY_INITIALIZE_OUTPUT(type,name) name = (type)0;
      #else
         #define UNITY_INITIALIZE_OUTPUT(type,name)
      #endif

      #define sampler2D_float sampler2D
      #define sampler2D_half sampler2D

      

      // data across stages, stripped like the above.
      struct VertexToPixel
      {
         float4 pos : SV_POSITION;
         float3 worldPos : TEXCOORD0;
         float3 worldNormal : TEXCOORD1;
         float4 worldTangent : TEXCOORD2;
          float4 texcoord0 : TEXCOORD3;
          float4 texcoord1 : TEXCOORD4;
         // float4 texcoord2 : TEXCOORD5;

         // #if %TEXCOORD3REQUIREKEY%
         // float4 texcoord3 : TEXCOORD6;
         // #endif

         // #if %SCREENPOSREQUIREKEY%
         // float4 screenPos : TEXCOORD7;
         // #endif

         // #if %VERTEXCOLORREQUIREKEY%
         // half4 vertexColor : COLOR;
         // #endif

         // #if %EXTRAV2F0REQUIREKEY%
         // float4 extraV2F0 : TEXCOORD12;
         // #endif

         // #if %EXTRAV2F1REQUIREKEY%
         // float4 extraV2F1 : TEXCOORD13;
         // #endif

         // #if %EXTRAV2F2REQUIREKEY%
         // float4 extraV2F2 : TEXCOORD14;
         // #endif

         // #if %EXTRAV2F3REQUIREKEY%
         // float4 extraV2F3 : TEXCOORD15;
         // #endif

         // #if %EXTRAV2F4REQUIREKEY%
         // float4 extraV2F4 : TEXCOORD16;
         // #endif

         // #if %EXTRAV2F5REQUIREKEY%
         // float4 extraV2F5 : TEXCOORD17;
         // #endif

         // #if %EXTRAV2F6REQUIREKEY%
         // float4 extraV2F6 : TEXCOORD18;
         // #endif

         // #if %EXTRAV2F7REQUIREKEY%
         // float4 extraV2F7 : TEXCOORD19;
         // #endif
            
         #if defined(LIGHTMAP_ON)
            float2 lightmapUV : TEXCOORD8;
         #endif
         #if !defined(LIGHTMAP_ON)
            float3 sh : TEXCOORD9;
         #endif
            float4 fogFactorAndVertexLight : TEXCOORD10;
            float4 shadowCoord : TEXCOORD11;
         #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
         #endif
         #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
         #endif
         #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
         #endif
         #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
         #endif
      };


         
            
            // data describing the user output of a pixel
            struct Surface
            {
               half3 Albedo;
               half Height;
               half3 Normal;
               half Smoothness;
               half3 Emission;
               half Metallic;
               half3 Specular;
               half Occlusion;
               half SpecularPower; // for simple lighting
               half Alpha;
               float outputDepth; // if written, SV_Depth semantic is used. ShaderData.clipPos.z is unused value
               // HDRP Only
               half SpecularOcclusion;
               half SubsurfaceMask;
               half Thickness;
               half CoatMask;
               half CoatSmoothness;
               half Anisotropy;
               half IridescenceMask;
               half IridescenceThickness;
               int DiffusionProfileHash;
               float SpecularAAThreshold;
               float SpecularAAScreenSpaceVariance;
               // requires _OVERRIDE_BAKEDGI to be defined, but is mapped in all pipelines
               float3 DiffuseGI;
               float3 BackDiffuseGI;
               float3 SpecularGI;
               // requires _OVERRIDE_SHADOWMASK to be defines
               float4 ShadowMask;
            };

            // Data the user declares in blackboard blocks
            struct Blackboard
            {
                
                float blackboardDummyData;
            };

            // data the user might need, this will grow to be big. But easy to strip
            struct ShaderData
            {
               float4 clipPos; // SV_POSITION
               float3 localSpacePosition;
               float3 localSpaceNormal;
               float3 localSpaceTangent;
        
               float3 worldSpacePosition;
               float3 worldSpaceNormal;
               float3 worldSpaceTangent;
               float tangentSign;

               float3 worldSpaceViewDir;
               float3 tangentSpaceViewDir;

               float4 texcoord0;
               float4 texcoord1;
               float4 texcoord2;
               float4 texcoord3;

               float2 screenUV;
               float4 screenPos;

               float4 vertexColor;
               bool isFrontFace;

               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;

               float3x3 TBNMatrix;
               Blackboard blackboard;
            };

            struct VertexData
            {
               #if SHADER_TARGET > 30
               // uint vertexID : SV_VertexID;
               #endif
               float4 vertex : POSITION;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;

               // optimize out mesh coords when not in use by user or lighting system
               #if _URP && (_USINGTEXCOORD1 || _PASSMETA || _PASSFORWARD || _PASSGBUFFER)
                  float4 texcoord1 : TEXCOORD1;
               #endif

               #if _URP && (_USINGTEXCOORD2 || _PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && defined(DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif

               #if _STANDARD && (_USINGTEXCOORD1 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER || _PASSFORWARDADD) && LIGHTMAP_ON)))
                  float4 texcoord1 : TEXCOORD1;
               #endif
               #if _STANDARD && (_USINGTEXCOORD2 || (_PASSMETA || ((_PASSFORWARD || _PASSGBUFFER) && DYNAMICLIGHTMAP_ON)))
                  float4 texcoord2 : TEXCOORD2;
               #endif


               #if _HDRP
                  float4 texcoord1 : TEXCOORD1;
                  float4 texcoord2 : TEXCOORD2;
               #endif

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD4; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity    : TEXCOORD5; // Add Precomputed Velocity (Alembic computes velocities on runtime side).
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct TessVertex 
            {
               float4 vertex : INTERNALTESSPOS;
               float3 normal : NORMAL;
               float4 tangent : TANGENT;
               float4 texcoord0 : TEXCOORD0;
               float4 texcoord1 : TEXCOORD1;
               float4 texcoord2 : TEXCOORD2;

               // #if %TEXCOORD3REQUIREKEY%
               // float4 texcoord3 : TEXCOORD3;
               // #endif

               // #if %VERTEXCOLORREQUIREKEY%
               // float4 vertexColor : COLOR;
               // #endif

               // #if %EXTRAV2F0REQUIREKEY%
               // float4 extraV2F0 : TEXCOORD5;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // float4 extraV2F1 : TEXCOORD6;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // float4 extraV2F2 : TEXCOORD7;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // float4 extraV2F3 : TEXCOORD8;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // float4 extraV2F4 : TEXCOORD9;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // float4 extraV2F5 : TEXCOORD10;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // float4 extraV2F6 : TEXCOORD11;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // float4 extraV2F7 : TEXCOORD12;
               // #endif

               #if _HDRP && (_PASSMOTIONVECTOR || ((_PASSFORWARD || _PASSUNLIT) && defined(_WRITE_TRANSPARENT_MOTION_VECTOR)))
                  float3 previousPositionOS : TEXCOORD13; // Contain previous transform position (in case of skinning for example)
                  #if defined (_ADD_PRECOMPUTED_VELOCITY)
                     float3 precomputedVelocity : TEXCOORD14;
                  #endif
               #endif

               UNITY_VERTEX_INPUT_INSTANCE_ID
               UNITY_VERTEX_OUTPUT_STEREO
            };

            struct ExtraV2F
            {
               float4 extraV2F0;
               float4 extraV2F1;
               float4 extraV2F2;
               float4 extraV2F3;
               float4 extraV2F4;
               float4 extraV2F5;
               float4 extraV2F6;
               float4 extraV2F7;
               Blackboard blackboard;
               float4 time;
            };


            float3 WorldToTangentSpace(ShaderData d, float3 normal)
            {
               return mul(d.TBNMatrix, normal);
            }

            float3 TangentToWorldSpace(ShaderData d, float3 normal)
            {
               return mul(normal, d.TBNMatrix);
            }

            // in this case, make standard more like SRPs, because we can't fix
            // unity_WorldToObject in HDRP, since it already does macro-fu there

            #if _STANDARD
               float3 TransformWorldToObject(float3 p) { return mul(unity_WorldToObject, float4(p, 1)); };
               float3 TransformObjectToWorld(float3 p) { return mul(unity_ObjectToWorld, float4(p, 1)); };
               float4 TransformWorldToObject(float4 p) { return mul(unity_WorldToObject, p); };
               float4 TransformObjectToWorld(float4 p) { return mul(unity_ObjectToWorld, p); };
               float4x4 GetWorldToObjectMatrix() { return unity_WorldToObject; }
               float4x4 GetObjectToWorldMatrix() { return unity_ObjectToWorld; }
               #if (defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (SHADER_TARGET_SURFACE_ANALYSIS && !SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord, lod) tex.SampleLevel (sampler##tex,coord, lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord, lod) tex.SampleLevel (sampler##samplertex,coord, lod)
              #else
                 #define UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod) tex2D (tex,coord,0,lod)
                 #define UNITY_SAMPLE_TEX2D_SAMPLER_LOD(tex,samplertex,coord,lod) tex2D (tex,coord,0,lod)
              #endif

               #undef GetObjectToWorldMatrix()
               #undef GetWorldToObjectMatrix()
               #undef GetWorldToViewMatrix()
               #undef UNITY_MATRIX_I_V
               #undef UNITY_MATRIX_P
               #undef GetWorldToHClipMatrix()
               #undef GetObjectToWorldMatrix()V
               #undef UNITY_MATRIX_T_MV
               #undef UNITY_MATRIX_IT_MV
               #undef GetObjectToWorldMatrix()VP

               #define GetObjectToWorldMatrix()     unity_ObjectToWorld
               #define GetWorldToObjectMatrix()   unity_WorldToObject
               #define GetWorldToViewMatrix()     unity_MatrixV
               #define UNITY_MATRIX_I_V   unity_MatrixInvV
               #define GetViewToHClipMatrix()     OptimizeProjectionMatrix(glstate_matrix_projection)
               #define GetWorldToHClipMatrix()    unity_MatrixVP
               #define GetObjectToWorldMatrix()V    mul(GetWorldToViewMatrix(), GetObjectToWorldMatrix())
               #define UNITY_MATRIX_T_MV  transpose(GetObjectToWorldMatrix()V)
               #define UNITY_MATRIX_IT_MV transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V))
               #define GetObjectToWorldMatrix()VP   mul(GetWorldToHClipMatrix(), GetObjectToWorldMatrix())


            #endif

            float3 GetCameraWorldPosition()
            {
               #if _HDRP
                  return GetCameraRelativePositionWS(_WorldSpaceCameraPos);
               #else
                  return _WorldSpaceCameraPos;
               #endif
            }

            #if _GRABPASSUSED
               #if _STANDARD
                  TEXTURE2D(%GRABTEXTURE%);
                  SAMPLER(sampler_%GRABTEXTURE%);
               #endif

               half3 GetSceneColor(float2 uv)
               {
                  #if _STANDARD
                     return SAMPLE_TEXTURE2D(%GRABTEXTURE%, sampler_%GRABTEXTURE%, uv).rgb;
                  #else
                     return SHADERGRAPH_SAMPLE_SCENE_COLOR(uv);
                  #endif
               }
            #endif


      
            #if _STANDARD
               UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
               float GetSceneDepth(float2 uv) { return SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv)); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv)); } 
            #else
               float GetSceneDepth(float2 uv) { return SHADERGRAPH_SAMPLE_SCENE_DEPTH(uv); }
               float GetLinear01Depth(float2 uv) { return Linear01Depth(GetSceneDepth(uv), _ZBufferParams); }
               float GetLinearEyeDepth(float2 uv) { return LinearEyeDepth(GetSceneDepth(uv), _ZBufferParams); } 
            #endif

            float3 GetWorldPositionFromDepthBuffer(float2 uv, float3 worldSpaceViewDir)
            {
               float eye = GetLinearEyeDepth(uv);
               float3 camView = mul((float3x3)GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);

               float dt = dot(worldSpaceViewDir, camView);
               float3 div = worldSpaceViewDir/dt;
               float3 wpos = (eye * div) + GetCameraWorldPosition();
               return wpos;
            }

            #if _STANDARD
               UNITY_DECLARE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture);
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  float4 depthNorms = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CameraDepthNormalsTexture, uv);
                  float3 norms = DecodeViewNormalStereo(depthNorms);
                  norms = mul((float3x3)GetWorldToViewMatrix(), norms) * 0.5 + 0.5;
                  return norms;
               }
            #elif _HDRP
               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  NormalData nd;
                  DecodeFromNormalBuffer(_ScreenSize.xy * uv, nd);
                  return nd.normalWS;
               }
            #elif _URP
               #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                  #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
               #endif

               float3 GetSceneNormal(float2 uv, float3 worldSpaceViewDir)
               {
                  #if (SHADER_LIBRARY_VERSION_MAJOR >= 10)
                     return SampleSceneNormals(uv);
                  #else
                     float3 wpos = GetWorldPositionFromDepthBuffer(uv, worldSpaceViewDir);
                     return normalize(-cross(ddx(wpos), ddy(wpos))) * 0.5 + 0.5;
                  #endif

                }
             #endif

             #if _HDRP

               half3 UnpackNormalmapRGorAG(half4 packednormal)
               {
                     // This do the trick
                  packednormal.x *= packednormal.w;

                  half3 normal;
                  normal.xy = packednormal.xy * 2 - 1;
                  normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                  return normal;
               }
               half3 UnpackNormal(half4 packednormal)
               {
                  #if defined(UNITY_NO_DXT5nm)
                     return packednormal.xyz * 2 - 1;
                  #else
                     return UnpackNormalmapRGorAG(packednormal);
                  #endif
               }
               #endif
               #if _HDRP || _URP

               half3 UnpackScaleNormal(half4 packednormal, half scale)
               {
                 #ifndef UNITY_NO_DXT5nm
                   // Unpack normal as DXT5nm (1, y, 1, x) or BC5 (x, y, 0, 1)
                   // Note neutral texture like "bump" is (0, 0, 1, 1) to work with both plain RGB normal and DXT5nm/BC5
                   packednormal.x *= packednormal.w;
                 #endif
                   half3 normal;
                   normal.xy = (packednormal.xy * 2 - 1) * scale;
                   normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
                   return normal;
               }	

             #endif


            void GetSun(out float3 lightDir, out float3 color)
            {
               lightDir = float3(0.5, 0.5, 0);
               color = 1;
               #if _HDRP
                  if (_DirectionalLightCount > 0)
                  {
                     DirectionalLightData light = _DirectionalLightDatas[0];
                     lightDir = -light.forward.xyz;
                     color = light.color;
                  }
               #elif _STANDARD
			         lightDir = normalize(_WorldSpaceLightPos0.xyz);
                  color = _LightColor0.rgb;
               #elif _URP
	               Light light = GetMainLight();
	               lightDir = light.direction;
	               color = light.color;
               #endif
            }


            
         CBUFFER_START(UnityPerMaterial)

            
        float4 _MainTex_ST;
        float4 _DetailAlbedoMap_ST;
	        half4 _Color;
        half _Cutoff; 
        half _Mode;
        float _Cull;
        half _BumpScale; 
        half _DetailNormalMapScale;
        half _Metallic;
        float _Glossiness;
        float _GlossMapScale;
        half _OcclusionStrength;
        half _Parallax;
        half _UVSec;
        half3 _EmissionColor;
        float _IsReplacementShader;
        float _TriggerMode;
        float _RaycastMode;
        float _IsExempt;
        float _isReferenceMaterial;
        float _InteractionMode;
        int _ArrayLength = 0;
        #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
            float4 _PlayersPosVectorArray[20];
            float _PlayersDataFloatArray[150];     
        #else
            float4 _PlayersPosVectorArray[100];
            float _PlayersDataFloatArray[500];  
        #endif
        float _tDirection = 0;
        float _numOfPlayersInside = 0;
        float _tValue = 0;
        float _id = 0;
        #if _ZONING
            #if (defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)) 
                float _ZDFA[500];
            #else
                float _ZDFA[1000];
            #endif
            float _ZonesDataCount;
        #endif
        #if _REPLACEMENT        
            half4 _DissolveColorGlobal;
            float _DissolveColorSaturationGlobal;
            float _DissolveEmissionGlobal;
            float _DissolveEmissionBoosterGlobal;
            float _TextureVisibilityGlobal;
            float _ObstructionGlobal;
            float _AngleStrengthGlobal;
            float _ConeStrengthGlobal;
            float _ConeObstructionDestroyRadiusGlobal;
            float _CylinderStrengthGlobal;
            float _CylinderObstructionDestroyRadiusGlobal;
            float _CircleStrengthGlobal;
            float _CircleObstructionDestroyRadiusGlobal;
            float _CurveStrengthGlobal;
            float _CurveObstructionDestroyRadiusGlobal;
            float _DissolveFallOffGlobal;
            float _IntrinsicDissolveStrengthGlobal;
            float _PreviewModeGlobal;
            float _UVsGlobal;
            float _hasClippedShadowsGlobal;
            float _FloorGlobal;
            float _FloorModeGlobal;
            float _FloorYGlobal;
            float _PlayerPosYOffsetGlobal;
            float _FloorYTextureGradientLengthGlobal;
            float _AnimationEnabledGlobal;
            float _AnimationSpeedGlobal;
            float _DefaultEffectRadiusGlobal;
            float _TransitionDurationGlobal;        
            float _TexturedEmissionEdgeGlobal;
            float _TexturedEmissionEdgeStrengthGlobal;
            float _IsometricExclusionGlobal;
            float _IsometricExclusionDistanceGlobal;
            float _IsometricExclusionGradientLengthGlobal;
            float _CeilingGlobal;
            float _CeilingModeGlobal;
            float _CeilingBlendModeGlobal;
            float _CeilingYGlobal;
            float _CeilingPlayerYOffsetGlobal;
            float _CeilingYGradientLengthGlobal;
            float _ZoningGlobal;
            float _ZoningModeGlobal;
            float _ZoningEdgeGradientLengthGlobal;
            float _IsZoningRevealableGlobal;
            float _SyncZonesWithFloorYGlobal;
            float _SyncZonesFloorYOffsetGlobal;
            float4 _ObstructionCurveGlobal_TexelSize;
            float4 _DissolveMaskGlobal_TexelSize;
            float _DissolveMaskEnabledGlobal;
            float _PreviewIndicatorLineThicknessGlobal;
        #else
            half _TextureVisibility;
            half _AngleStrength;
            float _Obstruction;
            float _UVs;
            float4 _ObstructionCurve_TexelSize;      
            float _DissolveMaskEnabled;
            float4 _DissolveMask_TexelSize;
            half4 _DissolveColor;
            float _DissolveColorSaturation;
            float _DissolveEmission;
            float _DissolveEmissionBooster;
            float _hasClippedShadows;
            float _ConeStrength;
            float _ConeObstructionDestroyRadius;
            float _CylinderStrength;
            float _CylinderObstructionDestroyRadius;
            float _CircleStrength;
            float _CircleObstructionDestroyRadius;
            float _CurveStrength;
            float _CurveObstructionDestroyRadius;
            float _IntrinsicDissolveStrength;
            float _DissolveFallOff;
            float _PreviewMode;
            float _PreviewIndicatorLineThickness;
            float _AnimationEnabled;
            float _AnimationSpeed;
            float _DefaultEffectRadius;
            float _TransitionDuration;
            float _TexturedEmissionEdge;
            float _TexturedEmissionEdgeStrength;
            float _IsometricExclusion;
            float _IsometricExclusionDistance;
            float _IsometricExclusionGradientLength;
            float _Floor;
            float _FloorMode;
            float _FloorY;
            float _FloorYTextureGradientLength;
            float _PlayerPosYOffset;
            float _Ceiling;
            float _CeilingMode;
            float _CeilingBlendMode;
            float _CeilingY;
            float _CeilingPlayerYOffset;
            float _CeilingYGradientLength;
            float _Zoning;
            float _ZoningMode;
            float _ZoningEdgeGradientLength;
            float _IsZoningRevealable;
            float _SyncZonesWithFloorY;
            float _SyncZonesFloorYOffset;
        #endif




         CBUFFER_END

         

         

         #ifdef unity_WorldToObject
#undef unity_WorldToObject
#endif
#ifdef unity_ObjectToWorld
#undef unity_ObjectToWorld
#endif
#define unity_ObjectToWorld GetObjectToWorldMatrix()
#define unity_WorldToObject GetWorldToObjectMatrix()

	        sampler2D _MainTex;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailMask;
            sampler2D _BumpMap;
            sampler2D _DetailNormalMap;
            sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
            sampler2D _ParallaxMap;
            sampler2D _EmissionMap;
        #if _REPLACEMENT
            sampler2D _DissolveTexGlobal;
        #else
            sampler2D _DissolveTex;
        #endif
        #if _DISSOLVEMASK
            #if _REPLACEMENT
                sampler2D _DissolveMaskGlobal;
            #else
                sampler2D _DissolveMask;
            #endif
        #endif
        #if _REPLACEMENT
            sampler2D _ObstructionCurveGlobal;
        #else
            sampler2D _ObstructionCurve;
        #endif


	    #if _URP
        half2 ParallaxOffset1Step (half h, half height, half3 viewDir)
        {
            h = h * height - height/2.0;
            half3 v = normalize(viewDir);
            v.z += 0.42;
            return h * (v.xy / v.z);
        }
        #ifdef UNITY_COLORSPACE_GAMMA
            #define unity_ColorSpaceDouble half4(2.0, 2.0, 2.0, 2.0)
        #else 
            #define unity_ColorSpaceDouble half4(4.59479380, 4.59479380, 4.59479380, 2.0)
        #endif
        half3 BlendNormals(half3 n1, half3 n2)
        {
            return normalize(half3(n1.xy + n2.xy, n1.z*n2.z));
        }
    #endif



	void Ext_SurfaceFunction0 (inout Surface o, ShaderData d)
	{
                  bool l0;
            #if _REPLACEMENT
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDurationGlobal ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #else
                l0 =  (_numOfPlayersInside > 0 || _tDirection == -1 && _Time.y - _tValue < _TransitionDuration ) || (_numOfPlayersInside >= 0 && _tDirection == 1); 
            #endif
            bool ll0 = !_TriggerMode && !_RaycastMode;
            float lll0 = 0;
            half4 llll0 = half4(0,0,0,0);
            if(!_IsExempt && (l0 || ll0) ) {
                half4 lllll0;
                float llllll0;
                float lllllll0;
                float llllllll0;
                half lllllllll0;
                float llllllllll0;
                half lllllllllll0;
                float llllllllllll0;
                float lllllllllllll0;
                float llllllllllllll0;
                float lllllllllllllll0;
                float llllllllllllllll0;
                float lllllllllllllllll0;                
                float llllllllllllllllll0;
                float lllllllllllllllllll0;
                float llllllllllllllllllll0;
                float lllllllllllllllllllll0;
                float llllllllllllllllllllll0;
                float lllllllllllllllllllllll0;
                float llllllllllllllllllllllll0;
                float lllllllllllllllllllllllll0;
                float llllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllll0;
                float llllllllllllllllllllllllllllll0;
                float lllllllllllllllllllllllllllllll0;
                float l1;
                float ll1;
                float lll1;
                float llll1;
                float lllll1;
                float4 llllll1;
                float lllllll1;
                float llllllll1;
                float lllllllll1;
                float llllllllll1;
                float lllllllllll1;
                float llllllllllll1;
                float lllllllllllll1;
                float llllllllllllll1;
                float lllllllllllllll1;
                float llllllllllllllll1;
                float lllllllllllllllll1;
                float llllllllllllllllll1;
                float lllllllllllllllllll1;
                float llllllllllllllllllll1;
                float lllllllllllllllllllll1;
                float llllllllllllllllllllll1;
                float4 lllllllllllllllllllllll1;
                #if _REPLACEMENT
                    lllll0 = _DissolveColorGlobal;
                    llllll0 = _DissolveColorSaturationGlobal;
                    lllllll0 = _DissolveEmissionGlobal;
                    llllllll0 = _DissolveEmissionBoosterGlobal;
                    lllllllll0 = _TextureVisibilityGlobal;
                    llllllllll0 = _ObstructionGlobal;
                    lllllllllll0 = _AngleStrengthGlobal;
                    llllllllllll0 = _ConeStrengthGlobal;
                    lllllllllllll0 = _ConeObstructionDestroyRadiusGlobal;
                    llllllllllllll0 = _CylinderStrengthGlobal;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadiusGlobal;
                    llllllllllllllll0 = _CircleStrengthGlobal;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadiusGlobal;
                    llllllllllllllllll0 = _CurveStrengthGlobal;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadiusGlobal;
                    llllllllllllllllllll0 = _DissolveFallOffGlobal;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrengthGlobal;
                    llllllllllllllllllllll0 = _PreviewModeGlobal;
                    lllllllllllllllllllllll0 = _UVsGlobal;
                    llllllllllllllllllllllll0 = _hasClippedShadowsGlobal;                    
                    lllllllllllllllllllllllll0 = _FloorGlobal;
                    llllllllllllllllllllllllll0 = _FloorModeGlobal;
                    lllllllllllllllllllllllllll0 = _FloorYGlobal;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffsetGlobal;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLengthGlobal; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadiusGlobal;
                    lllllllllllllllllllllllllllllll0 = _TransitionDurationGlobal;
                    l1 = _AnimationEnabledGlobal;
                    ll1 = _AnimationSpeedGlobal;
                    lll1 = _TexturedEmissionEdgeGlobal;           
                    llll1 = _TexturedEmissionEdgeStrengthGlobal;
                    lllll1 = _DissolveMaskEnabledGlobal;
                    llllll1 = _DissolveMaskGlobal_TexelSize;
                    lllllll1 = _IsometricExclusionGlobal;
                    llllllll1 = _IsometricExclusionDistanceGlobal;
                    lllllllll1 = _IsometricExclusionGradientLengthGlobal;
                    llllllllll1 = _CeilingGlobal;
                    lllllllllll1 = _CeilingModeGlobal;
                    llllllllllll1 = _CeilingBlendModeGlobal;
                    lllllllllllll1 = _CeilingYGlobal;
                    llllllllllllll1 = _CeilingPlayerYOffsetGlobal;
                    lllllllllllllll1 = _CeilingYGradientLengthGlobal;
                    llllllllllllllll1 = _ZoningGlobal;
                    lllllllllllllllll1 = _ZoningModeGlobal;
                    llllllllllllllllll1 = _ZoningEdgeGradientLengthGlobal;
                    lllllllllllllllllll1 = _IsZoningRevealableGlobal;
                    llllllllllllllllllll1 = _SyncZonesWithFloorYGlobal;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffsetGlobal;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThicknessGlobal;
                    lllllllllllllllllllllll1 = _ObstructionCurveGlobal_TexelSize;
                #else
                    lllll0 = _DissolveColor;
                    llllll0 = _DissolveColorSaturation;
                    lllllll0 = _DissolveEmission;
                    llllllll0 = _DissolveEmissionBooster;
                    lllllllll0 = _TextureVisibility;
                    llllllllll0 = _Obstruction;
                    lllllllllll0 = _AngleStrength;
                    llllllllllll0 = _ConeStrength;
                    lllllllllllll0 = _ConeObstructionDestroyRadius;
                    llllllllllllll0 = _CylinderStrength;
                    lllllllllllllll0 = _CylinderObstructionDestroyRadius;
                    llllllllllllllll0 = _CircleStrength;
                    lllllllllllllllll0 = _CircleObstructionDestroyRadius;
                    llllllllllllllllll0 = _CurveStrength;
                    lllllllllllllllllll0 = _CurveObstructionDestroyRadius;
                    llllllllllllllllllll0 = _DissolveFallOff;
                    lllllllllllllllllllll0 = _IntrinsicDissolveStrength;
                    llllllllllllllllllllll0 = _PreviewMode;
                    lllllllllllllllllllllll0 = _UVs;
                    llllllllllllllllllllllll0 = _hasClippedShadows;                    
                    lllllllllllllllllllllllll0 = _Floor;
                    llllllllllllllllllllllllll0 = _FloorMode;
                    lllllllllllllllllllllllllll0 = _FloorY;
                    llllllllllllllllllllllllllll0 = _PlayerPosYOffset;
                    lllllllllllllllllllllllllllll0 = _FloorYTextureGradientLength; 
                    llllllllllllllllllllllllllllll0 = _DefaultEffectRadius;
                    lllllllllllllllllllllllllllllll0 = _TransitionDuration;
                    l1 = _AnimationEnabled;
                    ll1 = _AnimationSpeed;
                    lll1 = _TexturedEmissionEdge;           
                    llll1 = _TexturedEmissionEdgeStrength;
                    lllll1 = _DissolveMaskEnabled;
                    llllll1 = _DissolveMask_TexelSize;
                    lllllll1 = _IsometricExclusion;
                    llllllll1 = _IsometricExclusionDistance;
                    lllllllll1 = _IsometricExclusionGradientLength;
                    llllllllll1 = _Ceiling;
                    lllllllllll1 = _CeilingMode;
                    llllllllllll1 = _CeilingBlendMode;
                    lllllllllllll1 = _CeilingY;
                    llllllllllllll1 = _CeilingPlayerYOffset;
                    lllllllllllllll1 = _CeilingYGradientLength;
                    llllllllllllllll1 = _Zoning;
                    lllllllllllllllll1 = _ZoningMode;
                    llllllllllllllllll1 = _ZoningEdgeGradientLength;
                    lllllllllllllllllll1 = _IsZoningRevealable;
                    llllllllllllllllllll1 = _SyncZonesWithFloorY;
                    lllllllllllllllllllll1 = _SyncZonesFloorYOffset;
                    llllllllllllllllllllll1 = _PreviewIndicatorLineThickness;
                    lllllllllllllllllllllll1 = _ObstructionCurve_TexelSize;
                #endif
                if(lllllllllllllllllllll0 < 0) {
                    lllllllllllllllllllll0 = 0;
                }
                float3 llllllllllllllllllllllll1;
                d.worldSpaceNormal = mul(o.Normal, (float3x3)d.TBNMatrix);
                float3 lllllllllllllllllllllllll1 = d.worldSpacePosition / (-1.0 * abs(lllllllllllllllllllllll0) );
                if(l1) {
                    lllllllllllllllllllllllll1 = lllllllllllllllllllllllll1 + abs(((_Time.y) * ll1));
                }  
                float3 llllllllllllllllllllllllll1 = float3(0,0,0);
                float3 lllllllllllllllllllllllllll1 = float3(0,0,0);
                float3 llllllllllllllllllllllllllll1 = float3(0,0,0);
                #if _REPLACEMENT
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTexGlobal, lllllllllllllllllllllllll1.xy ).rgb;
                #else
                    llllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.yz ).rgb;
                    lllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xz ).rgb;
                    llllllllllllllllllllllllllll1 = tex2D ( _DissolveTex, lllllllllllllllllllllllll1.xy ).rgb;
                #endif
                float lllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.x);
                float llllllllllllllllllllllllllllll1 = abs (d.worldSpaceNormal.z);
                float3 lllllllllllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllll1, llllllllllllllllllllllllll1, lllllllllllllllllllllllllllll1 ).rgb;
                llllllllllllllllllllllll1 = lerp ( lllllllllllllllllllllllllllllll1, llllllllllllllllllllllllllll1, llllllllllllllllllllllllllllll1 ).rgb;
                half l2 = llllllllllllllllllllllll1.r;  
                float3 ll2 = GetWorldToViewMatrix()[2].xyz;
                #if _HDRP
                    ll2 =  mul(GetObjectToWorldMatrix(), transpose(mul(GetWorldToObjectMatrix(), UNITY_MATRIX_I_V)) [2].xyz);
                #else
                    ll2 = - mul((float3x3)unity_CameraToWorld, float3(0,0,1));
                #endif
                float lll2 = 0;
                float llll2 = 1;
                bool lllll2 = false;
                float llllll2 = 0;
                float lllllll2 = 0;
                float llllllll2 = 0;
                float lllllllll2 = 0;
                float llllllllll2 = 0;
                float lllllllllll2 = 0;
                #if _ZONING
                    if(llllllllllllllll1) {
                        float llllllllllll2 = 0;
                        for (int z = 0; z < _ZonesDataCount; z++){
                            bool lllllllllllll2 = false;
                            float llllllllllllll2 = llllllllllll2;
                            if (_ZDFA[llllllllllll2 + 1] == 0) {  
                                float lllllllllllllll2 = llllllllllll2 + 2; 
                                float3 llllllllllllllll2 = d.worldSpacePosition - float3(_ZDFA[lllllllllllllll2],_ZDFA[lllllllllllllll2+1], _ZDFA[lllllllllllllll2+2]);
                                float3 lllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 3],_ZDFA[lllllllllllllll2+ 4], _ZDFA[lllllllllllllll2+ 5]);
                                float3 llllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 6],_ZDFA[lllllllllllllll2+ 7], _ZDFA[lllllllllllllll2+ 8]);
                                float3 lllllllllllllllllll2 =     float3(_ZDFA[lllllllllllllll2+ 9],_ZDFA[lllllllllllllll2+10], _ZDFA[lllllllllllllll2+11]);
                                float3 llllllllllllllllllll2 = float3(_ZDFA[lllllllllllllll2+12],_ZDFA[lllllllllllllll2+13], _ZDFA[lllllllllllllll2+14]);
                                lllllllllllll2 =    abs(dot(llllllllllllllll2, lllllllllllllllll2)) <= llllllllllllllllllll2.x &&
                                            abs(dot(llllllllllllllll2, llllllllllllllllll2)) <= llllllllllllllllllll2.y &&
                                            abs(dot(llllllllllllllll2, lllllllllllllllllll2)) <= llllllllllllllllllll2.z;
                                if(lllllllllllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                                    llllllll2 = _ZDFA[lllllllllllllll2+1] - _ZDFA[lllllllllllllll2+13];  
                                    if(llllllllllllllllllllllllll0 == 0) {                                    
                                        bool lllllllllllllllllllll2 = ((llllllll2 - lllllllllllllllllllll1)  <= lllllllllllllllllllllllllll0); 
                                        if(!lllllllllllllllllllll2) {
                                            lllllllllllll2 = false;
                                        }
                                    }
                                }
                                llllllllllll2 = llllllllllll2 + 17 + 3; 
                                if(lllllllllllll2) {
                                    float llllllllllllllllllllll2 = llllllllllllllllllll2.x - abs(dot(llllllllllllllll2, lllllllllllllllll2));
                                    float lllllllllllllllllllllll2 = llllllllllllllllllll2.y - abs(dot(llllllllllllllll2, llllllllllllllllll2));
                                    float llllllllllllllllllllllll2 = llllllllllllllllllll2.z - abs(dot(llllllllllllllll2, lllllllllllllllllll2));
                                    lllllll2 = min(lllllllllllllllllllllll2,llllllllllllllllllllll2);
                                    lllllll2 = min(lllllll2,llllllllllllllllllllllll2);
                                    if(lllllll2<0) {
                                        lllllll2 = 0;
                                    }
                                }
                            }
                            if(lllllllllllll2) {
                                llllll2 = _ZDFA[llllllllllllll2];
                                lllll2 = true;    
                                lllllllll2 = _ZDFA[llllllllllllll2 + 17];
                                lllllllllll2 = _ZDFA[llllllllllllll2 + 18];
                                llllllllll2 = _ZDFA[llllllllllllll2 + 19];
                                break;                        
                            }
                        }
                    }
                #endif
                float lllllllllllllllllllllllll2 = 0;
                float llllllllllllllllllllllllll2 = lllll2;
                #if _PLAYERINDEPENDENT == 0
                    #if _ZONING
                        if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                            float lllllllllllllllllllllllllll2 = 0;
                            bool llllllllllllllllllllllllllll2 = false;
                            for (int i = 0; i < _ArrayLength; i++){
                                float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;               
                                if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {       
                                    if(!ll0) {
                                        float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                                        float l3 = 4;
                                        for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                            float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                            if (ll3 != 0 && ll3 == _id) {
                                                float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                if ((llll3 == -1 && _Time.y - lll3 < lllllllllllllllllllllllllllllll0 )|| (llll3 == 1) ) {
                                                    float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                                    if(llllllllllllllllllllll0) {
                                                        if(i==0) {
                                                            lllllllllllllllllllllllll2 = lllll3;
                                                        } else {
                                                            lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                                        }
                                                    }
                                                    bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                                    if(!llllll3) {
                                                        llllllllllllllllllllllllllll2 = true;
                                                    } 
                                                }                        
                                            }
                                        }
                                    } else if (distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition.xyz) < llllllllllllllllllllllllllllll0) {
                                        float lllll3 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+ llllllllllllllllllllllllllll0;
                                        if(llllllllllllllllllllll0) {
                                            if(i==0) {
                                                lllllllllllllllllllllllll2 = lllll3;
                                            } else {
                                                lllllllllllllllllllllllll2 = max(lllllllllllllllllllllllll2,lllll3);
                                            }
                                        }
                                        bool llllll3 = llllllll2 >= lllll3 + lllllllllllllllllllll1; 
                                        if(!llllll3) {
                                            llllllllllllllllllllllllllll2 = true;
                                        } 
                                    }
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                                    lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                                }
                            }
                            if(!llllllllllllllllllllllllllll2) {
                                lllll2 = false;
                            }
                        }
                    #endif
                    float lllllllllllllllllllllllllll2 = 0;
                    for (int i = 0; i < _ArrayLength; i++){
                        float lllllllllllllllllllllllllllll2 = _PlayersDataFloatArray[lllllllllllllllllllllllllll2+1]; 
                        float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;                 
                        float llllllllllll3 = 0;
                        float l3 = 4;
                        if(!ll0) {
                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 3;
                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]; llllll6++){
                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                if (ll3 != 0 && ll3 == _id) {
                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                    llllllllllll3 = 1;
                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < lllllllllllllllllllllllllllllll0) {
                                        if(llll3 == 1) {
                                            llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-lll3))/lllllllllllllllllllllllllllllll0);
                                        } else {
                                            llllllllllll3 = ((_Time.y-lll3)/lllllllllllllllllllllllllllllll0);
                                        }
                                    } else if(llll3 ==-1) {
                                        llllllllllll3 = 1;
                                    } else if(llll3 == 1) {
                                        llllllllllll3 = 0;
                                    } else {
                                        llllllllllll3 = 1;
                                    }
                                    llllllllllll3 = 1 - llllllllllll3;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2 + 2]*4 + 3; 
                        float llllllllllllllllll3 = 0;
                        float lllllllllllllllllll3 = 0;     
                        float llllllllllllllllllll3 = lllllllllllllllllll3;
                        bool lllllllllllllllllllll3 = distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                        if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                        
                            #if _ZONING
                                if(llllllllllllllll1) {
                                    if(lllll2) 
                                    {
                                        if(lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + 1;
                                            for (int llllll6 = 0; llllll6 < _PlayersDataFloatArray[lllllllllllllllllllllllllll2]; llllll6++){
                                                float ll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 2];
                                                if (ll3 != 0 && ll3 == llllll2) {
                                                    float lll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 ];
                                                    float llll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 1];
                                                    llllllllllllllllll3 = 1;
                                                    float llllllllllllllllllllllllll3 = _PlayersDataFloatArray[lllllllllllllllllllllllllllllll2 + llllll6 * l3 + 3];
                                                    if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                                        if(llll3 == 1) {
                                                            llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                                        } else {
                                                            llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                                        }
                                                    } else if(llll3 ==-1) {
                                                        llllllllllllllllll3 = 1;
                                                    } else if(llll3 == 1) {
                                                        llllllllllllllllll3 = 0;
                                                    } else {
                                                        llllllllllllllllll3 = 1;
                                                    }
                                                    llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                                }
                                                if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                                    if (lllllll2 < llllllllllllllllll1)  {
                                                        float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                        llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                                    }
                                                }
                                            }
                                        } else { 
                                        }
                                    } else {
                                    }
                                }
                            #endif
                            if(dot(ll2,llllllllllllllllllllllllllllll2) <= 0) {    
                                if (llllllllll0 == 2 || llllllllll0 == 3 || llllllllll0 == 4 || llllllllll0 == 5 || llllllllll0 == 6 || llllllllll0 == 7) {
                                    float4 lllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float4 llllllllllllllllllllllllllllll3 = float4(0,0,0,0);
                                    float lllllllllllllllllllllllllllllll3 = 0;
                                    if(lllll1 || llllllllll0 == 6) {
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        #if _HDRP
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz), 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4 , _ProjectionParams.x);
                                        #else
                                            float4 ll4 = mul(GetWorldToHClipMatrix(), float4(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, 1.0));
                                            llllllllllllllllllllllllllllll3 = ComputeScreenPos(ll4);
                                        #endif
                                        llllllllllllllllllllllllllllll3.xy /= llllllllllllllllllllllllllllll3.w;
                                        llllllllllllllllllllllllllllll3.x *= l4;
                                        #if _HDRP
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(GetCameraRelativePositionWS(d.worldSpacePosition.xyz), 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4 , _ProjectionParams.x);
                                        #else
                                            float4 llll4 = mul(GetWorldToHClipMatrix(), float4(d.worldSpacePosition.xyz, 1.0));
                                            lllllllllllllllllllllllllllll3 = ComputeScreenPos(llll4);
                                        #endif
                                        lllllllllllllllllllllllllllll3.xy /= lllllllllllllllllllllllllllll3.w;
                                        lllllllllllllllllllllllllllll3.x *= l4;
                                        #if _DISSOLVEMASK
                                            if(lllll1) {
                                                #if _REPLACEMENT
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMaskGlobal_TexelSize.z,_DissolveMaskGlobal_TexelSize.w);
                                                #else
                                                    lllllllllllllllllllllllllllllll3 = max(_DissolveMask_TexelSize.z,_DissolveMask_TexelSize.w);
                                                #endif
                                            }
                                        #endif
                                    }
                                    float3 llllll4 = _WorldSpaceCameraPos - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 lllllll4 = normalize(llllll4);
                                    float llllllll4 = dot(d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, lllllll4);                           
                                    float lllllllll4 = 0;
                                    float llllllllll4 = 0;
                                    float2 lllllllllll4 = float2(0,0);
                                    if (llllllllll0 == 2 || llllllllll0 == 3) { 
                                        lllllllll4 = llllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllll4 = length(llllll4);
                                        float llllllllllllll4 = lllllllllllll0;
                                        float lllllllllllllll4 = (llllllll4 / lllllllllllll4) * llllllllllllll4;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            float llllllllllllllllll4 = llllllllllll4 < lllllllllllllll4;
                                            if(llllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 4 || llllllllll0 == 5 ) { 
                                        lllllllll4 = llllllllllllll0;
                                        float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                        float lllllllllllllllllllll4 = lllllllllllllll0;
                                        float llllllllllllllllllllll4 = (llllllllllll4 < lllllllllllllllllllll4) && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float llllllllllllllll4 = (2*lllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if(llllllllllllllllllllll4){
                                                float lllllllllllllllllll4 = llllllllllll4/lllllllllllllllllllll4;
                                                llllllllll4 = lllllllllllllllllll4;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } else if (llllllllll0 == 6) { 
                                        lllllllll4 = llllllllllllllll0;
                                        float llllllllllllllllllllllllll4 = length(llllll4);
                                        float l4 = _ScreenParams.x / _ScreenParams.y;   
                                        float llllllllllllllllllllllllllll4 = min(1,l4);
                                        float lllllllllllllllllllllllllllll4 =  distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy) < lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                        float llllllllllllllllllllllllllllll4 = (lllllllllllllllllllllllllllll4)  && llllllll4 > 0 ;
                                        #if _DISSOLVEMASK
                                            float lllllllllllllllllllllllllllllll4 = lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4;
                                            float llllllllllllllll4 = (2*lllllllllllllllllllllllllllllll4) / lllllllllllllllllllllllllllllll3;
                                            float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                            lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                        #else
                                            if (llllllllllllllllllllllllllllll4) {
                                                float lll5 = (distance(lllllllllllllllllllllllllllll3.xy,llllllllllllllllllllllllllllll3.xy)/(lllllllllllllllll0/llllllllllllllllllllllllll4*llllllllllllllllllllllllllll4));
                                                llllllllll4 = lll5;
                                            } else {
                                                llllllllll4 = -1;
                                            }
                                        #endif
                                    } 
                                    else if (llllllllll0 == 7) { 
                                        #if _OBSTRUCTION_CURVE
                                            lllllllll4 = llllllllllllllllll0;
                                            float llllllllllll4 = length((d.worldSpacePosition.xyz  - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz) - llllllll4 * lllllll4);
                                            float llllllllllllllllllllllllll4 = length(llllll4);
                                            float4 llllll5 = float4(0,0,0,0);
                                            float lllllll5 = lllllllllllllllllllllll1.z;
                                            float llllllll5 = (llllllll4/llllllllllllllllllllllllll4) * lllllll5;
                                            float4 lllllllll5 = float4(0,0,0,0);
                                            lllllllll5 = lllllllllllllllllllllll1;
                                            float2 llllllllll5 = (llllllll5+0.5) * lllllllll5.xy;
                                            #if _REPLACEMENT
                                                llllll5 = tex2D(_ObstructionCurveGlobal, llllllllll5);
                                            #else
                                                llllll5 = tex2D(_ObstructionCurve, llllllllll5);
                                            #endif
                                            float lllllllllll5 = llllll5.r * lllllllllllllllllll0;
                                            float llllllllllll5 = (llllllllllll4 < lllllllllll5) && llllllll4 > 0 ;
                                            #if _DISSOLVEMASK
                                                float llllllllllllllll4 = (2*lllllllllll5) / lllllllllllllllllllllllllllllll3;
                                                float2 lllllllllllllllll4 = lllllllllllllllllllllllllllll3.xy - llllllllllllllllllllllllllllll3.xy;
                                                lllllllllllllllll4 =  normalize(lllllllllllllllll4)*llllllllllll4;
                                                lllllllllll4 = lllllllllllllllll4 /llllllllllllllll4;
                                            #else
                                                if(llllllllllll5){
                                                    float lllllllllllllllllll4 = llllllllllll4/lllllllllll5;
                                                    llllllllll4 = lllllllllllllllllll4;
                                                } else {
                                                    llllllllll4 = -1;
                                                }
                                            #endif
                                        #endif
                                    } 
                                    #if _DISSOLVEMASK
                                        if(lllll1) {
                                            float4 llllllllllllllll5 = float4(0,0,0,0);
                                            llllllllllllllll5 = llllll1;
                                            float2 lllllllllllllllll5 = float2(llllllllllllllll5.z/2,llllllllllllllll5.w/2);
                                            float2 llllllllllllllllll5 = lllllllllllllllll5 + lllllllllll4;
                                            float2 lllllllllllllllllll5 = (llllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                            float4 llllllllllllllllllll5 = float4(0,0,0,0);
                                            #if _REPLACEMENT
                                                llllllllllllllllllll5 = tex2D(_DissolveMaskGlobal, lllllllllllllllllll5);
                                            #else
                                                llllllllllllllllllll5 = tex2D(_DissolveMask, lllllllllllllllllll5);
                                            #endif
                                            float lllllllllllllllllllll5 = -1;
                                            if(llllllllllllllllll5.x <= llllllllllllllll5.z && llllllllllllllllll5.x >= 0 && llllllllllllllllll5.y <= llllllllllllllll5.w && llllllllllllllllll5.y >= 0 && llllllllllllllllllll5.x <= 0 && llllllll4 > 0 ){
                                                float llllllllllllllllllllll5 = sqrt(pow(llllllllllllllll5.z,2)+pow(llllllllllllllll5.w,2))/2;
                                                float lllllllllllllllllllllll5 = 40;
                                                float llllllllllllllllllllllll5 = llllllllllllllllllllll5/lllllllllllllllllllllll5;
                                                float lllllllllllllllllllllllll5 = 0;
                                                lllllllllllllllllllll5 = 0;     
                                                #if _REPLACEMENT
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMaskGlobal, float4(lllllllllllllllllllllllllll5, 0.0, 0.0));                           
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        } 
                                                    }   
                                                #else
                                                    for (int i = 0; i < lllllllllllllllllllllll5; i++){
                                                        float2 llllllllllllllllllllllllll5 = lllllllllllllllll5 + (lllllllllll4 + ( normalize(lllllllllll4)*llllllllllllllllllllllll5*i));
                                                        float2 lllllllllllllllllllllllllll5 = (llllllllllllllllllllllllll5+0.5) * llllllllllllllll5.xy;
                                                        float4 llllllllllllllllllllllllllll5 = tex2Dlod(_DissolveMask, float4(lllllllllllllllllllllllllll5, 0.0, 0.0)); 
                                                        float2 lllllllllllllllllllllllllllll5 = step(float2(0,0), llllllllllllllllllllllllll5) - step(float2(llllllllllllllll5.z,llllllllllllllll5.w), llllllllllllllllllllllllll5);
                                                        if(llllllllllllllllllllllllllll5.x <= 0) {
                                                            lllllllllllllllllllllllll5 +=  (1/lllllllllllllllllllllll5) * (lllllllllllllllllllllllllllll5.x * lllllllllllllllllllllllllllll5.y);
                                                        }                                            
                                                    }   
                                                #endif
                                                lllllllllllllllllllll5 = 1-lllllllllllllllllllllllll5;  
                                            }         
                                            llllllllll4 = lllllllllllllllllllll5;
                                        }
                                    #endif
                                    if(llllllllllllllllllll0 <= 1) {
                                        if(llllllllll4 != -1) {
                                            float lll6 = max(llllllllllllllllllll0,0.00001);
                                            float llll6 = 1-lllllllll4;
                                            float lllll6 = exp(lll6*6);
                                            float llllll6 = llllllllll4;
                                            float lllllll6 = llll6/(lll6/(lll6*llll6-0.15*(lll6-llll6)));
                                            float llllllll6 = ((llllll6-lllllll6)/(lllll6*(1-llllll6)+llllll6))+lllllll6;
                                            llllllll6 = 1-llllllll6;
                                            lllllllllllllllllll3 =  llllllll6 * sign(lllllllll4);
                                        }
                                    } else {
                                        lllllllllllllllllll3 = llllllllll4;
                                    }
                                } 
                                if (llllllllll0 == 1 || llllllllll0 == 3 || llllllllll0 == 5) { 
                                    float lllllllll6 = distance(_WorldSpaceCameraPos, _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz);
                                    float llllllllll6 = distance(_WorldSpaceCameraPos, d.worldSpacePosition.xyz);
                                    float3 lllllllllll6 =  d.worldSpacePosition.xyz - _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz;
                                    float3 llllllllllll6 =  d.worldSpaceNormal;
                                    float lllllllllllll6 = acos(dot(lllllllllll6,llllllllllll6)/(length(lllllllllll6)*length(llllllllllll6)));
                                    if(lllllllllllll6 <= 1.5 && lllllllll6 > llllllllll6) {
                                        float llllllllllllll6 = (sqrt((lllllllll6-llllllllll6)) * 25 /lllllllllllll6)*lllllllllll0;                   
                                        lllllllllllllllllll3 += max(0,log(llllllllllllll6*0.2));
                                    }
                                }
                            }
                            lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                            if(lllll2) { 
                                if(lllllllllllllllll1 == 1) { 
                                    float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                    }
                                } 
                                else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                    float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                    if (lllllll2 < llllllllllllllllll1)  {
                                        float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                        lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                    } else {
                                        lllllllllllllllllll3 = 0;
                                    }
                                }
                            }
                            float lllllllllllllllllll6 = lllllllllllllllllll3 / lllllllll1;
                            if(lllllll1) {
                                float3 llllllllllllllllllllllllllllll2 = _PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz - _WorldSpaceCameraPos;
                                float3 lllllllllllllllllllll6 = d.worldSpacePosition.xyz - _WorldSpaceCameraPos;
                                float llllllllllllllllllllll6 = dot(lllllllllllllllllllll6, normalize(llllllllllllllllllllllllllllll2));
                                if(llllllllllllllllllllll6 - llllllll1 >= length(llllllllllllllllllllllllllllll2)) {
                                    float lllllllllllllllllllllll6 = llllllllllllllllllllll6 - llllllll1 - length(llllllllllllllllllllllllllllll2);
                                    if(lllllllllllllllllllllll6 < 0) {
                                        lllllllllllllllllllllll6 = 0;
                                    }
                                    if(lllllllllllllllllllllll6 < lllllllll1) 
                                    {
                                        lllllllllllllllllll3 = (lllllllll1 - lllllllllllllllllllllll6) * lllllllllllllllllll6;
                                    }
                                    else 
                                    {
                                        lllllllllllllllllll3 = 0; 
                                    }
                                }
                            }
                            if(llllllllllllllll1 && !lllll2) {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                            if(llllllllll1 == 1) {
                                float llllllllllllllllllllllll6 = 0;
                                if(llllllllllll1 == 0) {
                                    llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                                } else if (llllllllllll1 == 1) {  
                                    float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                        lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                    }
                                    llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                                }
                                if(lllllllllll1 == 1) {
        	                        if(d.worldSpacePosition.y > (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1)) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllll1);
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y > lllllllllllll1) {
                                        float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(llllllllllll1 == 0) {
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 0;  
                                            }
                                        } else {                                 
                                            if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                                lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                            } 
                                            else {
                                                lllllllllllllllllll3 = 1;  
                                            }
                                            llllllllllllllllll3 = 1;
                                        }
                                    }
                                }
                            }
                            if(lllllllllllllllllllllllll0 == 1) {
                                float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                                if(llllllllllllllllllllllllll0 == 1) {
        	                        if(d.worldSpacePosition.y < (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0)) {
                                        float lllllllllllllllllllllll6 = (_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].y+llllllllllllllllllllllllllll0) - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                } else {
                                    if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                        float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                        if(lllllllllllllllllllllll6 < 0) {
                                            lllllllllllllllllllllll6 = 0;
                                        }
                                        if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                            lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                        } else {
                                            lllllllllllllllllll3 = 0;  
                                        }
                                    }
                                }
                            }
                            if(!_TriggerMode && !_RaycastMode) {
                                if(distance(_PlayersPosVectorArray[lllllllllllllllllllllllllllll2].xyz, d.worldSpacePosition) > llllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        lllllllllllllllllllllllllll2 = lllllllllllllllllllllllllll2 + _PlayersDataFloatArray[lllllllllllllllllllllllllll2]*4 + 1; 
                        if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                            llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                        }
                        if(_TriggerMode || _RaycastMode) {
                            lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                        } else {
                            lllllllllllllllllll3 = lllllllllllllllllll3;
                            if (llllllllllllllll1) {
                                if(lllll2) {
                                    if(lllllllllllllllllll1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                    }
                                } else {
                                    if(lllllllllllllllll1 == 1) {
                                        lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                    }
                                }
                            }
                        }
                        lll2 = max(lll2,lllllllllllllllllll3);
                    }   
                #else
                    float llllllllllll3 = 0;
                    if(!ll0) {
                        llllllllllll3 = 1;
                        if( _tDirection!= 0 && _tValue != 0 && _Time.y-_tValue < lllllllllllllllllllllllllllllll0) {
                            if(_tDirection == 1) {
                                llllllllllll3 = ((lllllllllllllllllllllllllllllll0-(_Time.y-_tValue))/lllllllllllllllllllllllllllllll0);
                            } else {
                                llllllllllll3 = ((_Time.y-_tValue)/lllllllllllllllllllllllllllllll0);
                            }
                        } else if(_tDirection ==-1) {
                            llllllllllll3 = 1;
                        } else if(_tDirection == 1) {
                            llllllllllll3 = 0;
                        } else {
                            llllllllllll3 = 1;
                        }
                        llllllllllll3 = 1 - llllllllllll3;
                    }
                    float lllllllllllllllllll3 = 0;     
                    float llllllllllllllllll3 = 0;
                    bool lllllllllllllllllllll3 = distance(_WorldSpaceCameraPos, d.worldSpacePosition) > llllllllllllllllllllllllllllll0;
                    lllllllllllllllllllll3 = false;
                    if ((llllllllllll3 != 0) || ( (!_TriggerMode && !_RaycastMode) && !lllllllllllllllllllll3) ) {                                                    
                        #if _ZONING
                            if(llllllllllllllll1) {
                                if(lllll2) 
                                {
                                    if(lllllllllllllllllll1) {
                                        float lll3 = lllllllll2;
                                        float llll3 = lllllllllll2;
                                        llllllllllllllllll3 = 1;
                                        float llllllllllllllllllllllllll3 = llllllllll2;
                                        if( llll3!= 0 && lll3 != 0 && _Time.y-lll3 < llllllllllllllllllllllllll3) {
                                            if(llll3 == 1) {
                                                llllllllllllllllll3 = ((llllllllllllllllllllllllll3-(_Time.y-lll3))/llllllllllllllllllllllllll3);
                                            } else {
                                                llllllllllllllllll3 = ((_Time.y-lll3)/llllllllllllllllllllllllll3);
                                            }
                                        } else if(llll3 ==-1) {
                                            llllllllllllllllll3 = 1;
                                        } else if(llll3 == 1) {
                                            llllllllllllllllll3 = 0;
                                        } else {
                                            llllllllllllllllll3 = 1;
                                        }
                                        llllllllllllllllll3 = 1 - llllllllllllllllll3;
                                        if(lllllllllllllllll1 == 0 && lllllllllllllllllll1) {
                                            float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                            if (lllllll2 < llllllllllllllllll1)  {
                                                float llllllllllllllllllllllllllll3 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                                llllllllllllllllll3 =  max(llllllllllllllllll3,llllllllllllllllllllllllllll3);
                                            }
                                        }
                                    } else { 
                                    }
                                } else {
                                }
                            }
                        #endif
                        lllllllllllllllllll3 = min(lllllllllllllllllll3 + (1 * lllllllllllllllllllll0),1);
                        if(lllll2) {
                            if(lllllllllllllllll1 == 1) { 
                                float lllllllllllllllllllllllllll3 = 1 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = 1 - ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  min(lllllllllllllllllll3,llllllllllllllll6);
                                }
                            } 
                            else  if(lllllllllllllllll1 == 0 && !lllllllllllllllllll1)  { 
                                float lllllllllllllllllllllllllll3 = lllllllllllllllllll3 / llllllllllllllllll1;
                                if (lllllll2 < llllllllllllllllll1)  {
                                    float llllllllllllllll6 = ((llllllllllllllllll1-lllllll2) * lllllllllllllllllllllllllll3);
                                    lllllllllllllllllll3 =  max(0,llllllllllllllll6);
                                } else {
                                    lllllllllllllllllll3 = 0;
                                }
                            }
                        }
                        if(llllllllllllllll1 && !lllll2) {
                            if(lllllllllllllllll1 == 1) {
                                lllllllllllllllllll3 = 0;
                            }
                        }
                        if(llllllllll1 == 1 && lllllllllll1 == 0) {
                            float llllllllllllllllllllllll6 = 0;
                            if(llllllllllll1 == 0) { 
                                llllllllllllllllllllllll6 = (lllllllllllllllllll3)/ lllllllllllllll1;
                            } else if (llllllllllll1 == 1) {  
                                float lllllllllllllllllllllllll6 = 1-lllllllllllllllllll3;
                                if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                                    lllllllllllllllllllllllll6 = max(1-lllllllllllllllllll3, 1-(lllllllllllllllllll3*llllllllllllllllll3));
                                }
                                llllllllllllllllllllllll6 = lllllllllllllllllllllllll6 / lllllllllllllll1;
                            }
                            if(d.worldSpacePosition.y > lllllllllllll1) {
                                float lllllllllllllllllllllll6 = d.worldSpacePosition.y - lllllllllllll1;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(llllllllllll1 == 0) { 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) {
                                        lllllllllllllllllll3 = ((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 0;  
                                    }
                                } else {                                 
                                    if(lllllllllllllllllllllll6 < lllllllllllllll1) { 
                                        lllllllllllllllllll3 = 1 -((lllllllllllllll1 - lllllllllllllllllllllll6) * llllllllllllllllllllllll6);
                                    } 
                                    else {
                                        lllllllllllllllllll3 = 1;  
                                    }
                                    llllllllllllllllll3 = 1;
                                }
                            }
                        }
                        if(lllllllllllllllllllllllll0 == 1 && llllllllllllllllllllllllll0 == 0) {
                            float llllllllllllllllllllllllllll6 = lllllllllllllllllll3 / lllllllllllllllllllllllllllll0;
                            if(d.worldSpacePosition.y < lllllllllllllllllllllllllll0) {
                                float lllllllllllllllllllllll6 = lllllllllllllllllllllllllll0 - d.worldSpacePosition.y;
                                if(lllllllllllllllllllllll6 < 0) {
                                    lllllllllllllllllllllll6 = 0;
                                }
                                if(lllllllllllllllllllllll6 < lllllllllllllllllllllllllllll0) {
                                    lllllllllllllllllll3 = (lllllllllllllllllllllllllllll0 - lllllllllllllllllllllll6) * llllllllllllllllllllllllllll6;
                                } else {
                                    lllllllllllllllllll3 = 0;  
                                }
                            }                                
                        }
                    } 
                    if(llllllllllllllll1 && lllll2 && lllllllllllllllllll1) {
                        llllllllllll3 = llllllllllll3 * llllllllllllllllll3; 
                    }
                    if(_TriggerMode || _RaycastMode) {
                        lllllllllllllllllll3 =  llllllllllll3 * lllllllllllllllllll3;  
                    } else {
                        lllllllllllllllllll3 = lllllllllllllllllll3;
                        if (llllllllllllllll1) {
                            if(lllll2) {
                                if(lllllllllllllllllll1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3; 
                                }
                            } else {
                                if(lllllllllllllllll1 == 1) {
                                    lllllllllllllllllll3 =  llllllllllllllllll3 * lllllllllllllllllll3;
                                }
                            }
                        }
                    }
                    lll2 = max(lll2,lllllllllllllllllll3);    
                #endif
                float llllllllllllllllllll3 = lll2;
                if(!llllllllllllllllllllll0) {
                    if (llllllllllllllllllll3 == 1) {
                        llllllllllllllllllll3 = 10;
                    }
                    if (!llllllllllllllllllllllll0 || llllllllll0 == 6) {
                    #if defined(UNITY_PASS_SHADOWCASTER) 
                    #if defined(SHADOWS_DEPTH) 
                    if (!any(unity_LightShadowBias))
                    {
                            clip(l2- llllllllllllllllllll3);
                    }
                    else
                    {
                        if(llllllllllllllllllllllll0 && llllllllll0 != 6) {
                            clip(l2- llllllllllllllllllll3);                  
                        }
                    }
                    #endif
                    #else
                        clip(l2- llllllllllllllllllll3);
                    #endif
                    } else {                
                        clip(l2- llllllllllllllllllll3);  
                    }
                }       
                if(llllllllllllllllllllll0) {
                    lll0 = 1;
                    if((l2 - llllllllllllllllllll3) < 0) {
                        llll0 = half4(1,1,1,1);
                        o.Emission = 1;
                    } else {
                        llll0 = half4(0,0,0,1);
                    }
                    if(llllllllllllllllllllllllll2) {
                        if((l2 - llllllllllllllllllll3) < 0) {
                            llll0 = half4(0.5,1,0.5,1);
                            o.Emission = 0;
                        } else {
                            llll0 = half4(0,0.1,0,1);
                        }
                    }
                    if(lllll2 && lllllllllllllllllllllllll0 == 1 && llllllllllllllllllll1) {
                        float lllllllllllllllllll7 = 0;
                        if(llllllllllllllllllllllllll0 == 1) {
                            lllllllllllllllllllllllll2 = lllllllllllllllllllllllll2 + lllllllllllllllllllll1;    
                            lllllllllllllllllll7 = lllllllllllllllllllllllll2;
                        } else {
                            lllllllllllllllllll7 = lllllllllllllllllllllllllll0 + lllllllllllllllllllll1;
                        }                        
                        if(d.worldSpacePosition.y > (lllllllllllllllllll7 - llllllllllllllllllllll1) && d.worldSpacePosition.y < (lllllllllllllllllll7 + llllllllllllllllllllll1)){
                            llll0 = half4(1,0,0,1);
                        }
                    }
                } else {
                    half3 llllllllllllllllllll7 = lerp(1, lllll0, llllll0).rgb;
                    if(lll1) {
                        llll1 = 0.2 + (llll1*(0.8-0.2)); 
                        o.Emission =  min(clamp(llllllllllllllllllll7 *  clamp(((llllllllllllllllllll3/llll1) - l2),0,1), 0, 1)*sqrt(lllllll0*llllllll0), clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0));
                    } else {
                        o.Emission =  clamp(llllllllllllllllllll7*llllllllllllllllllll3, 0, 1)*sqrt(lllllll0*llllllll0);
                    }
                    #if _HDRP
                        o.Emission =  o.Emission * pow(llllllll0,2);
                    #endif
                }
            }
            float4 lllllllllllllllllllll7;
            lllllllllllllllllllll7.xy = d.texcoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw; 
            lllllllllllllllllllll7.zw = (_UVSec == 0) ? d.texcoord0.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw : d.texcoord1.xy * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
            #ifdef _PARALLAXMAP
                half llllllllllllllllllllll7 = tex2D (_ParallaxMap, lllllllllllllllllllll7.xy).g;
                float2 lllllllllllllllllllllll7 = ParallaxOffset1Step (llllllllllllllllllllll7, _Parallax, d.tangentSpaceViewDir);
                lllllllllllllllllllll7 = float4(lllllllllllllllllllll7.xy + lllllllllllllllllllllll7, lllllllllllllllllllll7.zw + lllllllllllllllllllllll7);
                o.Normal = float3(0, 0, 1);
            #endif
            if(lll0) {
                o.Albedo = llll0.rgb;
            } else {
                half3 llllllllllllllllllllllll7 = tex2D(_MainTex, lllllllllllllllllllll7.xy).rgb * _Color.rgb; 
                #ifdef _DETAIL_MULX2
                     half3 lllllllllllllllllllllllll7 = tex2D (_DetailAlbedoMap, lllllllllllllllllllll7.zw).rgb;
                     llllllllllllllllllllllll7 *= LerpWhiteTo (lllllllllllllllllllllllll7 * unity_ColorSpaceDouble.rgb, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
                #endif
                o.Albedo = llllllllllllllllllllllll7;
            }
            #ifdef _NORMALMAP
                half3 llllllllllll6 = UnpackScaleNormal(tex2D(_BumpMap, lllllllllllllllllllll7.xy), _BumpScale);
                #ifdef _DETAIL_MULX2
                    half3 lllllllllllllllllllllllllll7 = UnpackScaleNormal(tex2D(_DetailNormalMap, lllllllllllllllllllll7.zw), _DetailNormalMapScale);
	                lllllllllllllllllllllllllll7 = lerp(float3(0, 0, 1), lllllllllllllllllllllllllll7, tex2D(_DetailMask, lllllllllllllllllllll7.xy).a);
	                llllllllllll6 = BlendNormals(llllllllllll6, lllllllllllllllllllllllllll7);
                #endif
                o.Normal = llllllllllll6;
            #endif
            half2 llllllllllllllllllllllllllll7 = half2(0,0);
            #ifdef _METALLICGLOSSMAP
                half4 lllllllllllllllllllllllllllll7 = tex2D(_MetallicGlossMap, lllllllllllllllllllll7.xy);
                llllllllllllllllllllllllllll7.r = lllllllllllllllllllllllllllll7.r;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g = tex2D(_MainTex, lllllllllllllllllllll7.xy).a;
                #else
                    llllllllllllllllllllllllllll7.g = lllllllllllllllllllllllllllll7.a;
                #endif
                llllllllllllllllllllllllllll7.g *= _GlossMapScale;
            #else
                llllllllllllllllllllllllllll7.r = _Metallic;
                #ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
                    llllllllllllllllllllllllllll7.g  = tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _GlossMapScale;
                #else
                    llllllllllllllllllllllllllll7.g = _Glossiness;
                #endif
            #endif
            o.Metallic = llllllllllllllllllllllllllll7.r;
            o.Smoothness = llllllllllllllllllllllllllll7.g;
            o.Occlusion = lerp(1, tex2D(_OcclusionMap, lllllllllllllllllllll7.xy).g, _OcclusionStrength);
            #ifdef _EMISSION
                o.Emission += tex2D(_EmissionMap, lllllllllllllllllllll7.xy).rgb * _EmissionColor.rgb;
            #endif
            half llllllllllllllllllllllllllllll7 = 0;
            #if defined(_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A)
                llllllllllllllllllllllllllllll7 = _Color.a;
            #else
                llllllllllllllllllllllllllllll7 =  tex2D(_MainTex, lllllllllllllllllllll7.xy).a * _Color.a;
            #endif
            if (_Mode) 
            {
                clip (llllllllllllllllllllllllllllll7 - _Cutoff);
            }
            o.Alpha = llllllllllllllllllllllllllllll7;

	}



        
            void ChainSurfaceFunction(inout Surface l, inout ShaderData d)
            {
                  Ext_SurfaceFunction0(l, d);
                 // Ext_SurfaceFunction1(l, d);
                 // Ext_SurfaceFunction2(l, d);
                 // Ext_SurfaceFunction3(l, d);
                 // Ext_SurfaceFunction4(l, d);
                 // Ext_SurfaceFunction5(l, d);
                 // Ext_SurfaceFunction6(l, d);
                 // Ext_SurfaceFunction7(l, d);
                 // Ext_SurfaceFunction8(l, d);
                 // Ext_SurfaceFunction9(l, d);
		           // Ext_SurfaceFunction10(l, d);
                 // Ext_SurfaceFunction11(l, d);
                 // Ext_SurfaceFunction12(l, d);
                 // Ext_SurfaceFunction13(l, d);
                 // Ext_SurfaceFunction14(l, d);
                 // Ext_SurfaceFunction15(l, d);
                 // Ext_SurfaceFunction16(l, d);
                 // Ext_SurfaceFunction17(l, d);
                 // Ext_SurfaceFunction18(l, d);
		           // Ext_SurfaceFunction19(l, d);
                 // Ext_SurfaceFunction20(l, d);
                 // Ext_SurfaceFunction21(l, d);
                 // Ext_SurfaceFunction22(l, d);
                 // Ext_SurfaceFunction23(l, d);
                 // Ext_SurfaceFunction24(l, d);
                 // Ext_SurfaceFunction25(l, d);
                 // Ext_SurfaceFunction26(l, d);
                 // Ext_SurfaceFunction27(l, d);
                 // Ext_SurfaceFunction28(l, d);
		           // Ext_SurfaceFunction29(l, d);
            }

            void ChainModifyVertex(inout VertexData v, inout VertexToPixel v2p, float4 time)
            {
                 ExtraV2F d;
                 
                 ZERO_INITIALIZE(ExtraV2F, d);
                 ZERO_INITIALIZE(Blackboard, d.blackboard);
                 // due to motion vectors in HDRP, we need to use the last
                 // time in certain spots. So if you are going to use _Time to adjust vertices,
                 // you need to use this time or motion vectors will break. 
                 d.time = time;

                 //  Ext_ModifyVertex0(v, d);
                 // Ext_ModifyVertex1(v, d);
                 // Ext_ModifyVertex2(v, d);
                 // Ext_ModifyVertex3(v, d);
                 // Ext_ModifyVertex4(v, d);
                 // Ext_ModifyVertex5(v, d);
                 // Ext_ModifyVertex6(v, d);
                 // Ext_ModifyVertex7(v, d);
                 // Ext_ModifyVertex8(v, d);
                 // Ext_ModifyVertex9(v, d);
                 // Ext_ModifyVertex10(v, d);
                 // Ext_ModifyVertex11(v, d);
                 // Ext_ModifyVertex12(v, d);
                 // Ext_ModifyVertex13(v, d);
                 // Ext_ModifyVertex14(v, d);
                 // Ext_ModifyVertex15(v, d);
                 // Ext_ModifyVertex16(v, d);
                 // Ext_ModifyVertex17(v, d);
                 // Ext_ModifyVertex18(v, d);
                 // Ext_ModifyVertex19(v, d);
                 // Ext_ModifyVertex20(v, d);
                 // Ext_ModifyVertex21(v, d);
                 // Ext_ModifyVertex22(v, d);
                 // Ext_ModifyVertex23(v, d);
                 // Ext_ModifyVertex24(v, d);
                 // Ext_ModifyVertex25(v, d);
                 // Ext_ModifyVertex26(v, d);
                 // Ext_ModifyVertex27(v, d);
                 // Ext_ModifyVertex28(v, d);
                 // Ext_ModifyVertex29(v, d);


                 // #if %EXTRAV2F0REQUIREKEY%
                 // v2p.extraV2F0 = d.extraV2F0;
                 // #endif

                 // #if %EXTRAV2F1REQUIREKEY%
                 // v2p.extraV2F1 = d.extraV2F1;
                 // #endif

                 // #if %EXTRAV2F2REQUIREKEY%
                 // v2p.extraV2F2 = d.extraV2F2;
                 // #endif

                 // #if %EXTRAV2F3REQUIREKEY%
                 // v2p.extraV2F3 = d.extraV2F3;
                 // #endif

                 // #if %EXTRAV2F4REQUIREKEY%
                 // v2p.extraV2F4 = d.extraV2F4;
                 // #endif

                 // #if %EXTRAV2F5REQUIREKEY%
                 // v2p.extraV2F5 = d.extraV2F5;
                 // #endif

                 // #if %EXTRAV2F6REQUIREKEY%
                 // v2p.extraV2F6 = d.extraV2F6;
                 // #endif

                 // #if %EXTRAV2F7REQUIREKEY%
                 // v2p.extraV2F7 = d.extraV2F7;
                 // #endif
            }

            void ChainModifyTessellatedVertex(inout VertexData v, inout VertexToPixel v2p)
            {
               ExtraV2F d;
               ZERO_INITIALIZE(ExtraV2F, d);
               ZERO_INITIALIZE(Blackboard, d.blackboard);

               // #if %EXTRAV2F0REQUIREKEY%
               // d.extraV2F0 = v2p.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // d.extraV2F1 = v2p.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // d.extraV2F2 = v2p.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // d.extraV2F3 = v2p.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // d.extraV2F4 = v2p.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // d.extraV2F5 = v2p.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // d.extraV2F6 = v2p.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // d.extraV2F7 = v2p.extraV2F7;
               // #endif


               // Ext_ModifyTessellatedVertex0(v, d);
               // Ext_ModifyTessellatedVertex1(v, d);
               // Ext_ModifyTessellatedVertex2(v, d);
               // Ext_ModifyTessellatedVertex3(v, d);
               // Ext_ModifyTessellatedVertex4(v, d);
               // Ext_ModifyTessellatedVertex5(v, d);
               // Ext_ModifyTessellatedVertex6(v, d);
               // Ext_ModifyTessellatedVertex7(v, d);
               // Ext_ModifyTessellatedVertex8(v, d);
               // Ext_ModifyTessellatedVertex9(v, d);
               // Ext_ModifyTessellatedVertex10(v, d);
               // Ext_ModifyTessellatedVertex11(v, d);
               // Ext_ModifyTessellatedVertex12(v, d);
               // Ext_ModifyTessellatedVertex13(v, d);
               // Ext_ModifyTessellatedVertex14(v, d);
               // Ext_ModifyTessellatedVertex15(v, d);
               // Ext_ModifyTessellatedVertex16(v, d);
               // Ext_ModifyTessellatedVertex17(v, d);
               // Ext_ModifyTessellatedVertex18(v, d);
               // Ext_ModifyTessellatedVertex19(v, d);
               // Ext_ModifyTessellatedVertex20(v, d);
               // Ext_ModifyTessellatedVertex21(v, d);
               // Ext_ModifyTessellatedVertex22(v, d);
               // Ext_ModifyTessellatedVertex23(v, d);
               // Ext_ModifyTessellatedVertex24(v, d);
               // Ext_ModifyTessellatedVertex25(v, d);
               // Ext_ModifyTessellatedVertex26(v, d);
               // Ext_ModifyTessellatedVertex27(v, d);
               // Ext_ModifyTessellatedVertex28(v, d);
               // Ext_ModifyTessellatedVertex29(v, d);

               // #if %EXTRAV2F0REQUIREKEY%
               // v2p.extraV2F0 = d.extraV2F0;
               // #endif

               // #if %EXTRAV2F1REQUIREKEY%
               // v2p.extraV2F1 = d.extraV2F1;
               // #endif

               // #if %EXTRAV2F2REQUIREKEY%
               // v2p.extraV2F2 = d.extraV2F2;
               // #endif

               // #if %EXTRAV2F3REQUIREKEY%
               // v2p.extraV2F3 = d.extraV2F3;
               // #endif

               // #if %EXTRAV2F4REQUIREKEY%
               // v2p.extraV2F4 = d.extraV2F4;
               // #endif

               // #if %EXTRAV2F5REQUIREKEY%
               // v2p.extraV2F5 = d.extraV2F5;
               // #endif

               // #if %EXTRAV2F6REQUIREKEY%
               // v2p.extraV2F6 = d.extraV2F6;
               // #endif

               // #if %EXTRAV2F7REQUIREKEY%
               // v2p.extraV2F7 = d.extraV2F7;
               // #endif
            }

            void ChainFinalColorForward(inout Surface l, inout ShaderData d, inout half4 color)
            {
               //   Ext_FinalColorForward0(l, d, color);
               //   Ext_FinalColorForward1(l, d, color);
               //   Ext_FinalColorForward2(l, d, color);
               //   Ext_FinalColorForward3(l, d, color);
               //   Ext_FinalColorForward4(l, d, color);
               //   Ext_FinalColorForward5(l, d, color);
               //   Ext_FinalColorForward6(l, d, color);
               //   Ext_FinalColorForward7(l, d, color);
               //   Ext_FinalColorForward8(l, d, color);
               //   Ext_FinalColorForward9(l, d, color);
               //  Ext_FinalColorForward10(l, d, color);
               //  Ext_FinalColorForward11(l, d, color);
               //  Ext_FinalColorForward12(l, d, color);
               //  Ext_FinalColorForward13(l, d, color);
               //  Ext_FinalColorForward14(l, d, color);
               //  Ext_FinalColorForward15(l, d, color);
               //  Ext_FinalColorForward16(l, d, color);
               //  Ext_FinalColorForward17(l, d, color);
               //  Ext_FinalColorForward18(l, d, color);
               //  Ext_FinalColorForward19(l, d, color);
               //  Ext_FinalColorForward20(l, d, color);
               //  Ext_FinalColorForward21(l, d, color);
               //  Ext_FinalColorForward22(l, d, color);
               //  Ext_FinalColorForward23(l, d, color);
               //  Ext_FinalColorForward24(l, d, color);
               //  Ext_FinalColorForward25(l, d, color);
               //  Ext_FinalColorForward26(l, d, color);
               //  Ext_FinalColorForward27(l, d, color);
               //  Ext_FinalColorForward28(l, d, color);
               //  Ext_FinalColorForward29(l, d, color);
            }

            void ChainFinalGBufferStandard(inout Surface s, inout ShaderData d, inout half4 GBuffer0, inout half4 GBuffer1, inout half4 GBuffer2, inout half4 outEmission, inout half4 outShadowMask)
            {
               //   Ext_FinalGBufferStandard0(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard1(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard2(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard3(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard4(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard5(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard6(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard7(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard8(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //   Ext_FinalGBufferStandard9(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard10(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard11(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard12(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard13(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard14(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard15(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard16(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard17(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard18(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard19(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard20(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard21(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard22(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard23(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard24(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard25(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard26(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard27(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard28(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
               //  Ext_FinalGBufferStandard29(s, d, GBuffer0, GBuffer1, GBuffer2, outEmission, outShadowMask);
            }



         

         ShaderData CreateShaderData(VertexToPixel i
                  #if NEED_FACING
                     , bool facing
                  #endif
         )
         {
            ShaderData d = (ShaderData)0;
            d.clipPos = i.pos;
            d.worldSpacePosition = i.worldPos;

            d.worldSpaceNormal = normalize(i.worldNormal);
            d.worldSpaceTangent = normalize(i.worldTangent.xyz);
            d.tangentSign = i.worldTangent.w;
            float3 bitangent = cross(i.worldTangent.xyz, i.worldNormal) * d.tangentSign * -1;
            

            d.TBNMatrix = float3x3(d.worldSpaceTangent, bitangent, d.worldSpaceNormal);
            d.worldSpaceViewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

            d.tangentSpaceViewDir = mul(d.TBNMatrix, d.worldSpaceViewDir);
             d.texcoord0 = i.texcoord0;
             d.texcoord1 = i.texcoord1;
            // d.texcoord2 = i.texcoord2;

            // #if %TEXCOORD3REQUIREKEY%
            // d.texcoord3 = i.texcoord3;
            // #endif

            // d.isFrontFace = facing;
            // #if %VERTEXCOLORREQUIREKEY%
            // d.vertexColor = i.vertexColor;
            // #endif

            // these rarely get used, so we back transform them. Usually will be stripped.
            #if _HDRP
                // d.localSpacePosition = mul(unity_WorldToObject, float4(GetCameraRelativePositionWS(i.worldPos), 1)).xyz;
            #else
                // d.localSpacePosition = mul(unity_WorldToObject, float4(i.worldPos, 1)).xyz;
            #endif
            // d.localSpaceNormal = normalize(mul((float3x3)unity_WorldToObject, i.worldNormal));
            // d.localSpaceTangent = normalize(mul((float3x3)unity_WorldToObject, i.worldTangent.xyz));

            // #if %SCREENPOSREQUIREKEY%
            // d.screenPos = i.screenPos;
            // d.screenUV = (i.screenPos.xy / i.screenPos.w);
            // #endif


            // #if %EXTRAV2F0REQUIREKEY%
            // d.extraV2F0 = i.extraV2F0;
            // #endif

            // #if %EXTRAV2F1REQUIREKEY%
            // d.extraV2F1 = i.extraV2F1;
            // #endif

            // #if %EXTRAV2F2REQUIREKEY%
            // d.extraV2F2 = i.extraV2F2;
            // #endif

            // #if %EXTRAV2F3REQUIREKEY%
            // d.extraV2F3 = i.extraV2F3;
            // #endif

            // #if %EXTRAV2F4REQUIREKEY%
            // d.extraV2F4 = i.extraV2F4;
            // #endif

            // #if %EXTRAV2F5REQUIREKEY%
            // d.extraV2F5 = i.extraV2F5;
            // #endif

            // #if %EXTRAV2F6REQUIREKEY%
            // d.extraV2F6 = i.extraV2F6;
            // #endif

            // #if %EXTRAV2F7REQUIREKEY%
            // d.extraV2F7 = i.extraV2F7;
            // #endif

            return d;
         }
         

         
         #if _PASSSHADOW
            float3 _LightDirection;
         #endif

         // vertex shader
         VertexToPixel Vert (VertexData v)
         {
           
           VertexToPixel o = (VertexToPixel)0;

           UNITY_SETUP_INSTANCE_ID(v);
           UNITY_TRANSFER_INSTANCE_ID(v, o);
           UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


#if !_TESSELLATION_ON
           ChainModifyVertex(v, o, _Time);
#endif

            o.texcoord0 = v.texcoord0;
            o.texcoord1 = v.texcoord1;
           // o.texcoord2 = v.texcoord2;

           // #if %TEXCOORD3REQUIREKEY%
           // o.texcoord3 = v.texcoord3;
           // #endif

           // #if %VERTEXCOLORREQUIREKEY%
           // o.vertexColor = v.vertexColor;
           // #endif
           
           VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);
           o.worldPos = TransformObjectToWorld(v.vertex.xyz);
           o.worldNormal = TransformObjectToWorldNormal(v.normal);
           o.worldTangent = float4(TransformObjectToWorldDir(v.tangent.xyz), v.tangent.w);


          #if _PASSSHADOW
              // Define shadow pass specific clip position for Universal
              o.pos = TransformWorldToHClip(ApplyShadowBias(o.worldPos, o.worldNormal, _LightDirection));
              #if UNITY_REVERSED_Z
                  o.pos.z = min(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #else
                  o.pos.z = max(o.pos.z, o.pos.w * UNITY_NEAR_CLIP_VALUE);
              #endif
          #elif _PASSMETA
              o.pos = MetaVertexPosition(float4(v.vertex.xyz, 0), v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
          #else
              o.pos = TransformWorldToHClip(o.worldPos);
          #endif


          // #if %SCREENPOSREQUIREKEY%
          // o.screenPos = ComputeScreenPos(o.pos, _ProjectionParams.x);
          // #endif

          #if _PASSFORWARD || _PASSGBUFFER
              float2 uv1 = v.texcoord1.xy;
              OUTPUT_LIGHTMAP_UV(uv1, unity_LightmapST, o.lightmapUV);
               o.texcoord1.xy = uv1;
              OUTPUT_SH(o.worldNormal, o.sh);
          #endif

          #ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
              #if _BAKEDLIT
                 half3 vertexLight = 0;
              #else
                 half3 vertexLight = VertexLighting(o.worldPos, o.worldNormal);
              #endif
              half fogFactor = ComputeFogFactor(o.pos.z);
              o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
          #endif

          #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
             o.shadowCoord = GetShadowCoord(vertexInput);
          #endif

           return o;
         }


         

         // fragment shader
         half4 Frag (VertexToPixel IN
            #ifdef _DEPTHOFFSET_ON
              , out float outputDepth : SV_Depth
            #endif
            #if NEED_FACING
               , bool facing : SV_IsFrontFace
            #endif
         ) : SV_Target
         {
           UNITY_SETUP_INSTANCE_ID(IN);
           UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

           ShaderData d = CreateShaderData(IN
                  #if NEED_FACING
                     , facing
                  #endif
               );
           Surface l = (Surface)0;

           #ifdef _DEPTHOFFSET_ON
              l.outputDepth = outputDepth;
           #endif

           l.Albedo = half3(0.5, 0.5, 0.5);
           l.Normal = float3(0,0,1);
           l.Occlusion = 1;
           l.Alpha = 1;

           ChainSurfaceFunction(l, d);

           #ifdef _DEPTHOFFSET_ON
              outputDepth = l.outputDepth;
           #endif

           return float4(PackNormalOctRectEncode(TransformWorldToViewDir(d.worldSpaceNormal, true)), 0.0, 0.0);

         }

         ENDHLSL

      }


      


      

   }
   
   
   CustomEditor "ShaderCrew.SeeThroughShader.SeeThroughShaderEditor"
}
