using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System;
using System.IO;

using UnityEngine.Rendering;

namespace ShaderCrew.SeeThroughShader
{
    public class SeeThroughShaderEditor : ShaderGUI
    {

        public enum BlendMode
        {
            Opaque,
            Cutout,
            Transparent
        }
        public enum AlphaModes
        {
            Opaque,
            Blend,
            Add,
            PreMultiply
        }

        public enum STSInteractionMode
        {
            PlayerBased,
            Independent
        }

        public enum ObstructionMode
        {
            None,
            AngleOnly,
            ConeOnly,
            AngleAndCone,
            CylinderOnly,
            AngleAndCylinder,
            Circle,
            Curve
        }

        public enum FloorMode
        {
            Manual,
            PlayerPosition
        }

        public enum CeilingMode
        {
            Manual,
            PlayerPosition
        }

        public enum CeilingBlendMode
        {
            Additive,
            Subtractive
        }

        public enum ZoningMode
        {
            Additive,
            Subtractive
        }

        private static class Styles
        {
            // Standard
            public static GUIContent albedoText = EditorGUIUtility.TrTextContent("Albedo", "Albedo (RGB) and Transparency (A)");
            public static GUIContent alphaCutoffText = EditorGUIUtility.TrTextContent("Alpha Cutoff", "Threshold for alpha cutoff");
            public static GUIContent metallicMapText = EditorGUIUtility.TrTextContent("Metallic", "Metallic (R) and Smoothness (A)");

            public static GUIContent smoothnessText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness value");
            public static GUIContent smoothnessScaleText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness scale factor");
            public static GUIContent smoothnessMapChannelText = EditorGUIUtility.TrTextContent("Source", "Smoothness texture and channel");

            public static GUIContent normalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map");

            public static GUIContent heightMapText = EditorGUIUtility.TrTextContent("Height Map", "Height Map (G)");

            public static GUIContent emissionText = EditorGUIUtility.TrTextContent("Emission", "Emission (RGB)");
            public static GUIContent occlusionMapText = EditorGUIUtility.TrTextContent("Occlusion", "Occlusion (G)");

            public static GUIContent detailMaskText = EditorGUIUtility.TrTextContent("Detail Mask", "Mask for Secondary Maps (A)");
            public static GUIContent detailAlbedoText = EditorGUIUtility.TrTextContent("Detail Albedo x2", "Albedo (RGB) multiplied by 2");
            public static GUIContent detailNormalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map");
            public static GUIContent uvSetLabel = EditorGUIUtility.TrTextContent("UV Set");


            // See-through Shader
            public static GUIContent dissolveText = EditorGUIUtility.TrTextContent("Dissolve Effect Texture", "Dissolve Effect Texture");
            public static GUIContent dissolveSizeText = EditorGUIUtility.TrTextContent("Dissolve Texture Scale", "Dissolve Texture Scale");
            public static GUIContent dissolveEmissionText = EditorGUIUtility.TrTextContent("Strength", "Dissolve Emission Strength");
            public static GUIContent dissolveEmissionBoosterText = EditorGUIUtility.TrTextContent("Dissolve Emission Booster", "Dissolve Emission Booster");
            public static GUIContent dissolveColorSaturationText = EditorGUIUtility.TrTextContent("Saturation", "Dissolve Color Saturation");

            public static GUIContent dissolveMaskText = EditorGUIUtility.TrTextContent("Dissolve Mask", "Dissolve Mask (R)");

            public static string standardShaderText = "Standard Shader Properties";
            public static string secondaryMapsText = "Secondary Maps";
            public static string advancedText = "Advanced Options";
            public static string renderingMode = "Rendering Mode";
            public static string stsShaderText = "See-through Shader Properties";
            public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));

        }
        private bool showContentDissolveArea = true;
        private bool showContentInteractionOptionsArea = true;
        private bool showContentObstructionOptionsArea = true;
        private bool showContentAnimationArea = true;
        private bool showContentZoningArea = true;
        private bool showContentDebugArea = true;

        public enum SmoothnessMapChannel
        {
            SpecularMetallicAlpha,
            AlbedoAlpha,
        }

        MaterialProperty isReferenceMaterialMat = null;

        // Standard
        MaterialProperty blendMode = null;
        MaterialProperty cullMode = null;
        //MaterialProperty alphaMode = null;

        MaterialProperty albedoMap = null;
        MaterialProperty albedoColor = null;
        MaterialProperty alphaCutoff = null;
        MaterialProperty metallic = null;
        MaterialProperty metallicMap = null;

        MaterialProperty smoothness = null;
        MaterialProperty smoothnessScale = null;
        MaterialProperty smoothnessMapChannel = null;

        MaterialProperty bumpScale = null;
        MaterialProperty bumpMap = null;

        MaterialProperty emissionMap = null;
        MaterialProperty emissionColor = null;

        MaterialProperty occlusionMap = null;
        MaterialProperty occlusionStrength = null;

        MaterialProperty heigtMapScale = null;
        MaterialProperty heightMap = null;

        MaterialProperty detailMask = null;
        MaterialProperty detailAlbedoMap = null;
        MaterialProperty detailNormalMap = null;
        MaterialProperty detailNormalMapScale = null;
        MaterialProperty uvSetSecondary = null;

        // See-through Shader
        MaterialProperty dissolveMap = null;
        MaterialProperty dissolveColor = null;
        MaterialProperty dissolveSize = null;
        MaterialProperty dissolveColorSaturation = null;

        MaterialProperty dissolveEmmission = null;
        MaterialProperty dissolveEmmissionBooster = null;
        MaterialProperty dissolveTexturedEmissionEdge = null;
        AnimBool dissolveTexturedEmissionEdgeAnimBool;
        MaterialProperty dissolveTexturedEmissionEdgeStrength = null;

        MaterialProperty dissolveClippedShadowsEnabled = null;

        MaterialProperty dissolveTextureAnimationEnabled = null;
        AnimBool dissolveTextureAnimationEnabledAnimBool;
        MaterialProperty dissolveTextureAnimationSpeed = null;
        MaterialProperty dissolveTransitionDuration = null;

        MaterialProperty interactionMode = null;
        //MaterialProperty centerPosition = null;

        MaterialProperty obstructionMode = null;
        MaterialProperty angleStrength = null;
        MaterialProperty coneStrength = null;
        MaterialProperty coneObstructionDestroyRadius = null;
        MaterialProperty cylinderStrength = null;
        MaterialProperty cylinderObstructionDestroyRadius = null;
        MaterialProperty circleStrength = null;
        MaterialProperty circleObstructionDestroyRadius = null;

        MaterialProperty curveStrength = null;
        MaterialProperty curveObstructionDestroyRadius = null;

        MaterialProperty dissolveFallOff = null;
        MaterialProperty dissolveMask = null;
        MaterialProperty dissolveMaskEnabled = null;
        AnimBool dissolveMaskEnabledAnimBool;
        MaterialProperty intrinsicDissolveStrength = null;


        AnimBool ceilingEnabledAnimBool;
        MaterialProperty ceilingEnabled = null;
        MaterialProperty ceilingMode = null;
        MaterialProperty ceilingBlendMode = null;
        MaterialProperty ceilingY = null;
        MaterialProperty ceilingPlayerYOffset = null;
        MaterialProperty ceilingYGradientLength = null;

        AnimBool isometricExlusionEnabledAnimBool;
        MaterialProperty isometricExlusionEnabled = null;
        MaterialProperty isometricExclusionDistance = null;
        MaterialProperty isometricExclusionGradientLength = null;

        AnimBool floorEnabledAnimBool;
        MaterialProperty floorEnabled = null;
        MaterialProperty floorMode = null;
        MaterialProperty floorY = null;
        MaterialProperty playerPosYOffset = null;
        MaterialProperty floorYTextureGradientLength = null;


        AnimBool zoningEnabledAnimBool;
        MaterialProperty zoningEnabled = null;
        MaterialProperty zoningMode = null;
        MaterialProperty zoningEdgeGradientLength = null;
        MaterialProperty zoningIsRevealable = null;
        AnimBool zoningSyncZonesWithFloorYAnimBool = null;
        MaterialProperty zoningSyncZonesWithFloorY = null;
        MaterialProperty zoningSyncZonesFloorYOffset = null;

        AnimBool debugModeEnabledAnimBool;
        MaterialProperty debugModeEnabled = null;
        MaterialProperty debugModeIndicatorLineThickness = null;

        MaterialProperty isReplacementShader = null;


        MaterialProperty defaultEffectRadius = null;

        MaterialEditor m_MaterialEditor;

        bool m_FirstTimeApply = true;

        Color oriCol;

        bool isReferenceMaterial;

        Color textColor;
        Color originalColor;


        AnimationCurve curveY;
        AnimationCurveSO curveSO;
        Texture2D curveTexture;

        MaterialProperty dissolveObstructionCurve = null;
        readonly int curveTextureResolution = 512;






        public override void OnClosed(Material material)
        {
            base.OnClosed(material);
            EditorUtility.SetDirty(curveSO);
            AssetDatabase.SaveAssets();
        }

        public void FindProperties(MaterialProperty[] props)
        {           
            isReferenceMaterialMat = FindProperty("_isReferenceMaterial", props);

            // Standard
            blendMode = FindProperty("_Mode", props);
            cullMode = FindProperty("_Cull", props);
            //alphaMode = FindProperty("_AlphaMode", props);

            albedoMap = FindProperty("_MainTex", props);
            albedoColor = FindProperty("_Color", props);
            alphaCutoff = FindProperty("_Cutoff", props);

            metallicMap = FindProperty("_MetallicGlossMap", props);
            metallic = FindProperty("_Metallic", props);

            smoothness = FindProperty("_Glossiness", props);
            smoothnessScale = FindProperty("_GlossMapScale", props, false);
            smoothnessMapChannel = FindProperty("_SmoothnessTextureChannel", props, false);

            bumpScale = FindProperty("_BumpScale", props);
            bumpMap = FindProperty("_BumpMap", props);

            heigtMapScale = FindProperty("_Parallax", props);
            heightMap = FindProperty("_ParallaxMap", props);

            occlusionStrength = FindProperty("_OcclusionStrength", props);
            occlusionMap = FindProperty("_OcclusionMap", props);

            emissionMap = FindProperty("_EmissionMap", props);
            emissionColor = FindProperty("_EmissionColor", props);

            detailMask = FindProperty("_DetailMask", props);
            detailAlbedoMap = FindProperty("_DetailAlbedoMap", props);
            detailNormalMap = FindProperty("_DetailNormalMap", props);
            detailNormalMapScale = FindProperty("_DetailNormalMapScale", props);
            uvSetSecondary = FindProperty("_UVSec", props);

            // See-through Shader
            dissolveMap = FindProperty("_DissolveTex", props);
            dissolveColor = FindProperty("_DissolveColor", props);
            dissolveSize = FindProperty("_UVs", props);
            dissolveColorSaturation = FindProperty("_DissolveColorSaturation", props);

            dissolveEmmission = FindProperty("_DissolveEmission", props);
            dissolveEmmissionBooster = FindProperty("_DissolveEmissionBooster", props);
            dissolveTexturedEmissionEdge = FindProperty("_TexturedEmissionEdge", props);
            dissolveTexturedEmissionEdgeStrength = FindProperty("_TexturedEmissionEdgeStrength", props);

            dissolveClippedShadowsEnabled = FindProperty("_hasClippedShadows", props);


            dissolveTextureAnimationEnabled = FindProperty("_AnimationEnabled", props);
            dissolveTextureAnimationSpeed = FindProperty("_AnimationSpeed", props);
            dissolveTransitionDuration = FindProperty("_TransitionDuration", props);


            interactionMode = FindProperty("_InteractionMode", props);
            //centerPosition = FindProperty("_CenterPosition", props);

            obstructionMode = FindProperty("_Obstruction", props);
            angleStrength = FindProperty("_AngleStrength", props);
            coneStrength = FindProperty("_ConeStrength", props);
            coneObstructionDestroyRadius = FindProperty("_ConeObstructionDestroyRadius", props);

            cylinderStrength = FindProperty("_CylinderStrength", props);
            cylinderObstructionDestroyRadius = FindProperty("_CylinderObstructionDestroyRadius", props);

            circleStrength = FindProperty("_CircleStrength", props);
            circleObstructionDestroyRadius = FindProperty("_CircleObstructionDestroyRadius", props);

            curveStrength = FindProperty("_CurveStrength", props);
            curveObstructionDestroyRadius = FindProperty("_CurveObstructionDestroyRadius", props);

            dissolveObstructionCurve = FindProperty("_ObstructionCurve", props);

            dissolveFallOff = FindProperty("_DissolveFallOff", props);
            dissolveMask = FindProperty("_DissolveMask", props);
            dissolveMaskEnabled = FindProperty("_DissolveMaskEnabled", props);

            intrinsicDissolveStrength = FindProperty("_IntrinsicDissolveStrength", props);


            ceilingEnabled = FindProperty("_Ceiling", props);
            ceilingMode = FindProperty("_CeilingMode", props);
            ceilingBlendMode = FindProperty("_CeilingBlendMode", props);
            ceilingY = FindProperty("_CeilingY", props);
            ceilingPlayerYOffset = FindProperty("_CeilingPlayerYOffset", props);
            ceilingYGradientLength = FindProperty("_CeilingYGradientLength", props);


            isometricExlusionEnabled = FindProperty("_IsometricExclusion", props);
            isometricExclusionDistance = FindProperty("_IsometricExclusionDistance", props);
            isometricExclusionGradientLength = FindProperty("_IsometricExclusionGradientLength", props);

            floorEnabled = FindProperty("_Floor", props);
            floorMode = FindProperty("_FloorMode", props);
            floorY = FindProperty("_FloorY", props);
            playerPosYOffset = FindProperty("_PlayerPosYOffset", props);
            floorYTextureGradientLength = FindProperty("_FloorYTextureGradientLength", props);



            zoningEnabled = FindProperty("_Zoning", props);
            zoningMode = FindProperty("_ZoningMode", props);
            zoningEdgeGradientLength = FindProperty("_ZoningEdgeGradientLength", props);
            zoningIsRevealable = FindProperty("_IsZoningRevealable", props);

            zoningSyncZonesWithFloorY = FindProperty("_SyncZonesWithFloorY", props);
            zoningSyncZonesFloorYOffset = FindProperty("_SyncZonesFloorYOffset", props);


            debugModeEnabled = FindProperty("_PreviewMode", props);
            debugModeIndicatorLineThickness = FindProperty("_PreviewIndicatorLineThickness", props);

            isReplacementShader = FindProperty("_IsReplacementShader", props);

            defaultEffectRadius = FindProperty("_DefaultEffectRadius", props);


        }


        void DoSetup(MaterialEditor materialEditor)
        {
            dissolveTexturedEmissionEdgeAnimBool = new AnimBool(false);
            dissolveTexturedEmissionEdgeAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            dissolveTextureAnimationEnabledAnimBool = new AnimBool(false);
            dissolveTextureAnimationEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            dissolveMaskEnabledAnimBool = new AnimBool(false);
            dissolveMaskEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            ceilingEnabledAnimBool = new AnimBool(false);
            ceilingEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            isometricExlusionEnabledAnimBool = new AnimBool(false);
            isometricExlusionEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            floorEnabledAnimBool = new AnimBool(false);
            floorEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            zoningEnabledAnimBool = new AnimBool(false);
            zoningEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            zoningSyncZonesWithFloorYAnimBool = new AnimBool(false);
            zoningSyncZonesWithFloorYAnimBool.valueChanged.AddListener(materialEditor.Repaint);

            debugModeEnabledAnimBool = new AnimBool(false);
            debugModeEnabledAnimBool.valueChanged.AddListener(materialEditor.Repaint);


            if (dissolveObstructionCurve == null || dissolveObstructionCurve.textureValue == null)
            {
                curveTexture = new Texture2D(curveTextureResolution, 1, TextureFormat.R8, false, true);
            }
            else
            {
                curveTexture = (Texture2D)dissolveObstructionCurve.textureValue;
            }


            if (curveSO == null)
            {
                string name = (materialEditor.target as Material).name;
                if (!name.Contains(SeeThroughShaderConstants.STS_INSTANCE_NAME)) {
                    curveSO = Resources.Load<AnimationCurveSO>("AnimationCurveScriptableObjects/" + name);
                }


                if (curveSO == null)
                {
                    curveSO = ScriptableObject.CreateInstance<AnimationCurveSO>();
                    curveSO.curve = AnimationCurve.Linear(0, 0, 1, 1);
                    if (!name.Contains(SeeThroughShaderConstants.STS_INSTANCE_NAME))
                    {
                        var dirPath = "Assets/See-through Shader/Core/Editor/Resources/AnimationCurveScriptableObjects/";
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }
                        AssetDatabase.CreateAsset(curveSO, dirPath + name + ".asset");


                    }
                } 
                else
                {
                    if (!curveSO.isBakedToTexture)
                    {
                        SaveTexture(materialEditor.target as Material);
                        //name = name.Replace(" (Instance)", "");
                        Texture2D texture = Resources.Load("Curves/" + name, typeof(Texture2D)) as Texture2D;
                        SetTextureImporterFormat(texture, true);
                        dissolveObstructionCurve.textureValue = texture;
                        //material.SetTexture("_ObstructionCurve", texture);
                        curveSO.isBakedToTexture = true;
                        EditorUtility.SetDirty(curveSO);
                        AssetDatabase.SaveAssets();                        
                    }
                }
            }

            if (obstructionMode.floatValue == (float)ObstructionMode.Curve)
            {
                CreateInitialTexture(materialEditor.target as Material);
            }

            originalColor = EditorStyles.label.normal.textColor;


            if (EditorGUIUtility.isProSkin)
            {
                textColor = Color.white;
                oriCol = EditorStyles.label.normal.textColor;
            }
            else
            {
                //textColor = EditorStyles.label.normal.textColor;
                textColor = Color.black;
                oriCol = new Color(0.9f, 0.9f, 0.9f, 1);
            }
        }
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {

            FindProperties(properties);
            m_MaterialEditor = materialEditor;
            Material material = materialEditor.target as Material;

            if (m_FirstTimeApply)
            {
                MaterialChanged(material);
                DoSetup(materialEditor);
                m_FirstTimeApply = false;

            }

            if (isReferenceMaterialMat.floatValue == 1)
            {
                isReferenceMaterial = true;
            } else
            {
                isReferenceMaterial = false;
            }
            string name = isReferenceMaterial ? "Reference Shader" : "The Shader";


            EditorUtils.usualStart(name);


            EditorStyles.label.normal.textColor = textColor;

            //EditorGUI.BeginChangeCheck();
            //m_MaterialEditor.ShaderProperty(isReferenceMaterialMat, "Is Reference Material");
            if (!material.name.Contains(SeeThroughShaderConstants.STS_INSTANCE_NAME))
            { 
                isReferenceMaterialMat.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft("Is Reference Material",
                                                      Convert.ToBoolean(isReferenceMaterialMat.floatValue)));
         
            EditorUtils.makeHorizontalSeparation();
            }
            //EditorGUI.EndChangeCheck();

            if (!isReferenceMaterial)
            {
                StandardShaderPropertiesGUI(material);
                EditorUtils.makeHorizontalSeparation();
            }
            else
            {
                BlendModePopup();
                m_MaterialEditor.ShaderProperty(cullMode, "Cull Mode");
                EditorUtils.makeHorizontalSeparation();
            }



            STSShaderPropertiesGUI(material);

            EditorUtils.makeHorizontalSeparation();




            EditorUtils.usualEnd();
            EditorStyles.label.normal.textColor = originalColor;
        }



        void StandardShaderPropertiesGUI(Material material)
        {
            EditorGUIUtility.labelWidth = 0f;

            EditorGUI.BeginChangeCheck();
            {
                GUIStyle replacementStyle = new GUIStyle();
                replacementStyle.normal.textColor = textColor;
                replacementStyle.alignment = TextAnchor.MiddleCenter;
                replacementStyle.fontStyle = FontStyle.Bold;
                replacementStyle.fontSize = 14;

                GUILayout.Label(Styles.standardShaderText, replacementStyle);
                EditorUtils.DrawUILine();
                BlendModePopup();
                m_MaterialEditor.ShaderProperty(cullMode, "Cull Mode");

                DoAlbedoArea(material);
                DoSpecularMetallicArea(material);
                DoNormalArea();
                DoHeightMapArea();
                DoOcclusionArea();
                DoDetailMaskArea();
                DoEmissionArea();
                bool shouldEmissionBeEnabled = (material.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0;
                if (albedoMap.textureValue != null ||
                    bumpMap.textureValue != null ||
                    occlusionMap.textureValue != null ||
                    heightMap.textureValue != null ||
                    metallicMap.textureValue != null ||
                    detailMask.textureValue != null ||
                    (emissionMap.textureValue != null && shouldEmissionBeEnabled))
                {
                    EditorGUI.indentLevel += 2;
                    m_MaterialEditor.TextureScaleOffsetProperty(albedoMap);
                    EditorGUI.indentLevel -= 2;
                }
                EditorGUILayout.Space();
                DoSecondaryArea();

            }
            if (EditorGUI.EndChangeCheck())
            {
                //MaterialChanged(material);
                foreach (var obj in blendMode.targets)
                    MaterialChanged((Material)obj);
            }

            EditorGUILayout.Space();

            GUILayout.Label(Styles.advancedText, EditorStyles.boldLabel);
            m_MaterialEditor.RenderQueueField();
            m_MaterialEditor.EnableInstancingField();
            m_MaterialEditor.DoubleSidedGIField();
        }

        void STSShaderPropertiesGUI(Material material)
        {
            EditorGUIUtility.labelWidth = 0f;

            EditorGUI.BeginChangeCheck();
            {
                GUIStyle replacementStyle = new GUIStyle();

                replacementStyle.normal.textColor = textColor;
                replacementStyle.alignment = TextAnchor.MiddleCenter;
                replacementStyle.fontStyle = FontStyle.Bold;
                replacementStyle.fontSize = 14;

                GUILayout.Label(Styles.stsShaderText, replacementStyle);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                if (!material.name.Contains(SeeThroughShaderConstants.STS_INSTANCE_NAME))
                {
                    float originalLabelWidht = EditorGUIUtility.labelWidth;
                    if (!isReferenceMaterial)
                    {
                        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 94;
                    }
                    else
                    {
                        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 94; //80
                    }
                    DoDissolveArea();
                    DoInteractionOptionsArea(material);
                    DoObstructionOptionsArea(material);
                    DoAnimationArea(material);
                    DoZoningArea(material);
                    DoReplacementArea(material);
                    DoDebugArea(material);
                    EditorGUIUtility.labelWidth = originalLabelWidht;
                    EditorGUILayout.Space();
                } else
                {
                    EditorGUILayout.HelpBox("This is just an Instanced Material! If you want to change the 'See-through Shader' settings, you have to do that in the associated Reference Material.", MessageType.Error);
                }

            }

            if (EditorGUI.EndChangeCheck())
            {
                MaterialChanged(material);
            }

        }

        void BlendModePopup()
        {
            EditorGUI.showMixedValue = blendMode.hasMixedValue;
            var mode = (BlendMode)blendMode.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = (BlendMode)EditorGUILayout.Popup(Styles.renderingMode, (int)mode, Styles.blendNames);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
                blendMode.floatValue = (float)mode;
            }

            EditorGUI.showMixedValue = false;
        }

        //void ObstructionModePopup()
        //{
        //    EditorGUI.showMixedValue = blendMode.hasMixedValue;
        //    var mode = (BlendMode)blendMode.floatValue;

        //    EditorGUI.BeginChangeCheck();
        //    mode = (BlendMode)EditorGUILayout.Popup(Styles.renderingMode, (int)mode, Styles.blendNames);
        //    if (EditorGUI.EndChangeCheck())
        //    {
        //        m_MaterialEditor.RegisterPropertyChangeUndo("Rendering Mode");
        //        blendMode.floatValue = (float)mode;
        //    }

        //    EditorGUI.showMixedValue = false;
        //}

        void DoAlbedoArea(Material material)
        {
            float oriLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 94;
            m_MaterialEditor.TexturePropertySingleLine(Styles.albedoText, albedoMap, albedoColor);
            EditorGUIUtility.labelWidth = oriLabelWidth;

            if (((BlendMode)material.GetFloat("_Mode") == BlendMode.Cutout))
            {
                m_MaterialEditor.ShaderProperty(alphaCutoff, Styles.alphaCutoffText.text, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
            }
        }


        void DoDetailMaskArea()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.detailMaskText, detailMask);
        }



        void DoSecondaryArea()
        {
            GUILayout.Label(Styles.secondaryMapsText, EditorStyles.boldLabel);
            m_MaterialEditor.TexturePropertySingleLine(Styles.detailAlbedoText, detailAlbedoMap);
            m_MaterialEditor.TexturePropertySingleLine(Styles.detailNormalMapText, detailNormalMap, detailNormalMapScale);
            m_MaterialEditor.TextureScaleOffsetProperty(detailAlbedoMap);
            m_MaterialEditor.ShaderProperty(uvSetSecondary, Styles.uvSetLabel.text);

        }

        void DoNormalArea()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, bumpMap, bumpMap.textureValue != null ? bumpScale : null);
        }

        void DoHeightMapArea()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.heightMapText, heightMap, heightMap.textureValue != null ? heigtMapScale : null);
        }
        void DoSpecularMetallicArea(Material material)
        {
            bool hasGlossMap = metallicMap.textureValue != null;
            m_MaterialEditor.TexturePropertySingleLine(Styles.metallicMapText, metallicMap, hasGlossMap ? null : metallic);

            bool showSmoothnessScale = hasGlossMap;
            if (smoothnessMapChannel != null)
            {
                int smoothnessChannel = (int)smoothnessMapChannel.floatValue;
                if (smoothnessChannel == (int)SmoothnessMapChannel.AlbedoAlpha)
                    showSmoothnessScale = true;
            }

            int indentation = 2;
            m_MaterialEditor.ShaderProperty(showSmoothnessScale ? smoothnessScale : smoothness, showSmoothnessScale ? Styles.smoothnessScaleText : Styles.smoothnessText, indentation);

            ++indentation;
            if (smoothnessMapChannel != null)
                m_MaterialEditor.ShaderProperty(smoothnessMapChannel, Styles.smoothnessMapChannelText, indentation);
        }


        void DoOcclusionArea()
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.occlusionMapText, occlusionMap, occlusionMap.textureValue != null ? occlusionStrength : null);
        }


        void DoEmissionArea()
        {
            if (m_MaterialEditor.EmissionEnabledProperty())
            {
                bool hadEmissionTexture = emissionMap.textureValue != null;

                m_MaterialEditor.TexturePropertyWithHDRColor(Styles.emissionText, emissionMap, emissionColor, false);

                float brightness = emissionColor.colorValue.maxColorComponent;
                if (emissionMap.textureValue != null && !hadEmissionTexture && brightness <= 0f)
                {
                    emissionColor.colorValue = Color.white;
                }

                m_MaterialEditor.LightmapEmissionFlagsProperty(MaterialEditor.kMiniTextureFieldLabelIndentLevel, true);
            }
        }

        void DoDissolveArea()
        {



            //MakeSTSSectionHeader("Dissolve Effect Texture and Styling");
            EditorGUI.indentLevel += 1;
            showContentDissolveArea = MakeSTSSectionHeaderWithFoldout("Dissolve Effect Texture and Styling", showContentDissolveArea);
            EditorGUI.indentLevel -= 1;
            if (showContentDissolveArea)
            {


                EditorGUILayout.Space();


                m_MaterialEditor.TexturePropertySingleLine(Styles.dissolveText, dissolveMap, dissolveColor);

                //EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - 94;
                if (dissolveMap.textureValue == null)
                {
                    EditorGUILayout.HelpBox("You didn't select any dissolve texture! The 'See-through Shader' effect won't work without it!", MessageType.Error);
                }
                else
                {
                    EditorGUI.indentLevel += 2;
                    m_MaterialEditor.ShaderProperty(dissolveSize, Styles.dissolveSizeText);
                    m_MaterialEditor.ShaderProperty(dissolveColorSaturation, Styles.dissolveColorSaturationText);
                    EditorGUILayout.Space();

                    EditorUtils.DrawUILineSubMenu();

                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Emission");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(dissolveEmmission, Styles.dissolveEmissionText);

                    if (dissolveEmmission.floatValue > 0)
                    {
                        m_MaterialEditor.ShaderProperty(dissolveEmmissionBooster, Styles.dissolveEmissionBoosterText);
                        EditorGUILayout.Space();
                        dissolveTexturedEmissionEdge.floatValue = Convert.ToSingle(EditorGUILayout.Toggle("Textured Emission Edge", Convert.ToBoolean(dissolveTexturedEmissionEdge.floatValue)));
                        dissolveTexturedEmissionEdgeAnimBool.target = dissolveTexturedEmissionEdge.floatValue == 1;
                        if (EditorGUILayout.BeginFadeGroup(dissolveTexturedEmissionEdgeAnimBool.faded))
                        {
                            //EditorGUI.indentLevel++;
                            m_MaterialEditor.ShaderProperty(dissolveTexturedEmissionEdgeStrength, "Strength");
                            //EditorGUI.indentLevel--;
                        }
                        EditorGUILayout.EndFadeGroup();

                    }

                    if (obstructionMode.floatValue != 6)
                    {
                        EditorGUILayout.Space();

                        EditorUtils.DrawUILineSubMenu();

                        EditorGUI.indentLevel -= 1;
                        EditorStyles.label.normal.textColor = oriCol;
                        EditorGUILayout.LabelField("Shadows");
                        EditorStyles.label.normal.textColor = textColor;
                        EditorGUI.indentLevel += 1;

                        m_MaterialEditor.ShaderProperty(dissolveClippedShadowsEnabled, "Has Clipped Shadows");

                    }


                    EditorGUI.indentLevel -= 2;

                }

                EditorGUILayout.Space();

            } else
            {

            }




        }
        void DrawBox(Rect position, Color color)
        {
            Color oldColor = GUI.color;
            GUI.color = color;
            GUI.Box(position, GUIContent.none);
            GUI.color = oldColor;
        }



        void MakeSTSSectionHeader(string name)
        {
            Rect rect2 = EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            if (EditorGUIUtility.isProSkin)
            {
                //GUI.Box(rect2, GUIContent.none);
                DrawBox(rect2, new Color(0.5f, 1.5f, 3f, 1f));
            }
            else
            {
                DrawBox(rect2, new Color(0.8f, 0.8f, 0.8f, 1));
            }

            GUILayout.Label(name, EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
        }


        bool MakeSTSSectionHeaderWithFoldout(string name, bool showContent)
        {

            //showDescription = EditorGUILayout.Foldout(showDescription, name, EditorStyles.boldLabel); //, EditorStyles.boldLabel);

            Rect rect2 = EditorGUILayout.BeginVertical();

            if (EditorGUIUtility.isProSkin)
            {
                Color lightBlue = new Color(0.5f, 0.6f, 0.7f, 1); 
                EditorUtils.DrawUILine(new Color(0.2f, 0.2f, 0.2f, 1f));
                EditorUtils.DrawUILine(lightBlue,2,0);      
            }
            else
            {
                Color lightBlue = new Color(0.6f, 0.8f, 2f, 1);
                EditorUtils.DrawUILine(rect2, new Color(0.1f, 0.1f, 0.1f, 1f), 2, -8);
                EditorUtils.DrawUILine(rect2, lightBlue, 2, -5);
            }

            EditorGUILayout.Space();
            if (EditorGUIUtility.isProSkin)
            {
                //GUI.Box(rect2, GUIContent.none);
                DrawBox(rect2, new Color(0.5f, 1.5f, 2.5f, 1f));
            }
            else
            {

                DrawBox(rect2, new Color(0.5f, 0.7f, 0.9f, 1f));
                //DrawBox(rect2, new Color(0.8f, 0.8f, 0.8f, 1));
            }
            GUIStyle style = EditorStyles.foldout;
            FontStyle previousStyle = style.fontStyle;
            style.fontStyle = FontStyle.Bold;
            Color myStyleColor = new Color(1f, 1f, 1f, 1f);

            style.normal.textColor = myStyleColor;
            style.onNormal.textColor = myStyleColor;
            style.hover.textColor = myStyleColor;
            style.onHover.textColor = myStyleColor;
            style.focused.textColor = myStyleColor;
            style.onFocused.textColor = myStyleColor;
            style.active.textColor = myStyleColor;
            style.onActive.textColor = myStyleColor;
            showContent = EditorGUILayout.Foldout(showContent, name, style); //, EditorStyles.boldLabel);
            style.fontStyle = previousStyle;
            //GUILayout.Label(name, EditorStyles.boldLabel);

            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();


            return showContent;
        }

        void DoAnimationArea(Material material)
        {

            //MakeSTSSectionHeader("Dissolve Effect Animations");
            EditorGUI.indentLevel += 1;
            showContentAnimationArea = MakeSTSSectionHeaderWithFoldout("Dissolve Effect Animations", showContentAnimationArea);
            EditorGUI.indentLevel -= 1;
            //showDescription = EditorGUILayout.Foldout(showDescription, title);
            if (showContentAnimationArea)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                EditorGUILayout.LabelField("Dissolve Texture");
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;

                GUIStyle optionStyle = new GUIStyle();
                optionStyle.normal.textColor = textColor;
                //optionStyle.fontSize = 15;
                optionStyle.fontStyle = FontStyle.Bold;
                dissolveTextureAnimationEnabled.floatValue = Convert.ToSingle(EditorGUILayout.Toggle("Animation Enabled", Convert.ToBoolean(dissolveTextureAnimationEnabled.floatValue)));
                //m_MaterialEditor.ShaderProperty(dissolveTextureAnimationEnabled,dissolveTextureAnimationEnabled.displayName);
                dissolveTextureAnimationEnabledAnimBool.target = dissolveTextureAnimationEnabled.floatValue == 1;
                if (EditorGUILayout.BeginFadeGroup(dissolveTextureAnimationEnabledAnimBool.faded))
                {
                    //EditorGUI.indentLevel++;
                    m_MaterialEditor.ShaderProperty(dissolveTextureAnimationSpeed, "Speed");
                    //EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();

                EditorGUILayout.Space();

                EditorUtils.DrawUILineSubMenu();

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                EditorGUILayout.LabelField("Enter/Exit Transition");
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;


                if (Application.isPlaying)
                {
                    GUI.enabled = false;
                    m_MaterialEditor.ShaderProperty(dissolveTransitionDuration, "Transition Duration In Seconds");
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("You can't change the transition duration while being in 'Play Mode'", MessageType.Info);

                }
                else
                {
                    m_MaterialEditor.ShaderProperty(dissolveTransitionDuration, "Transition Duration In Seconds");
                }

                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 2;
            }

          
        }


        void DoZoningArea(Material material)
        {
            float oriLabelWidth = EditorGUIUtility.labelWidth;
            //MakeSTSSectionHeader("Zoning");

            EditorGUI.indentLevel += 1;
            showContentZoningArea = MakeSTSSectionHeaderWithFoldout("Zoning", showContentZoningArea);
            EditorGUI.indentLevel -= 1;
            if (showContentZoningArea)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;

                //SeeThroughShaderEditorUtils.DrawUILineSubMenu();

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                zoningEnabled.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft("Zoning",
                                                          Convert.ToBoolean(zoningEnabled.floatValue)));
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;


                EditorGUILayout.Space();

                zoningEnabledAnimBool.target = zoningEnabled.floatValue == 1;
                if (EditorGUILayout.BeginFadeGroup(zoningEnabledAnimBool.faded))
                {
                    //EditorGUI.indentLevel++;

                    EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2;
                    m_MaterialEditor.ShaderProperty(zoningMode, "Zoning Mode");
                    EditorGUIUtility.labelWidth = oriLabelWidth;

                    m_MaterialEditor.ShaderProperty(zoningIsRevealable, "Is Zoning Revealable");

                    //if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                    //{
                    //}
                    //else
                    //{
                    //    zoningIsRevealable.floatValue = 0;
                    //}

                    m_MaterialEditor.ShaderProperty(zoningEdgeGradientLength, "Zone Edge Gradient Length");
                    makeAlwaysPositiv(zoningEdgeGradientLength);

                    //m_MaterialEditor.ShaderProperty(isometricExclusionGradientLength, "Dissolve Texture Gradient Length");
                    //makeAlwaysPositiv(isometricExclusionGradientLength);
                    //EditorGUILayout.LabelField("Zoning stuff..");
                    //EditorGUI.indentLevel--;

                    if (floorEnabled.floatValue == 1)
                    {
                        EditorGUILayout.Space();
                        EditorUtils.DrawUILineSubMenu();


                        EditorGUI.indentLevel -= 1;
                        EditorStyles.label.normal.textColor = oriCol;
                        EditorGUILayout.LabelField("Synchronization of Zones and FloorY");
                        EditorStyles.label.normal.textColor = textColor;
                        EditorGUI.indentLevel += 1;

                        zoningSyncZonesWithFloorY.floatValue = Convert.ToSingle(EditorGUILayout.Toggle("Sync Zones With FloorY",
                                                                  Convert.ToBoolean(zoningSyncZonesWithFloorY.floatValue)));


                        zoningSyncZonesWithFloorYAnimBool.target = zoningSyncZonesWithFloorY.floatValue == 1;
                        if (EditorGUILayout.BeginFadeGroup(zoningSyncZonesWithFloorYAnimBool.faded))
                        {
                            m_MaterialEditor.ShaderProperty(zoningSyncZonesFloorYOffset, "Sync FloorY Offset");
                        }
                        EditorGUILayout.EndFadeGroup();
                    }





                }
                EditorGUILayout.EndFadeGroup();
                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 2;
            }
           

        }


        void DoDebugArea(Material material)
        {
            float oriLabelWidth = EditorGUIUtility.labelWidth;

            //MakeSTSSectionHeader("Debug");

            EditorGUI.indentLevel += 1;
            showContentDebugArea = MakeSTSSectionHeaderWithFoldout("Debug", showContentDebugArea);
            EditorGUI.indentLevel -= 1;
            if (showContentDebugArea)
            {


                //makeBetweenRange(previewIndicatorLineThickness,0.1f,0.5f);


                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;

                //SeeThroughShaderEditorUtils.DrawUILineSubMenu();

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                debugModeEnabled.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft(debugModeEnabled.displayName,
                                                          Convert.ToBoolean(debugModeEnabled.floatValue)));
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;


                EditorGUILayout.Space();

                debugModeEnabledAnimBool.target = debugModeEnabled.floatValue == 1;
                if (EditorGUILayout.BeginFadeGroup(debugModeEnabledAnimBool.faded))
                {
                    //EditorGUI.indentLevel++;
                    m_MaterialEditor.ShaderProperty(debugModeIndicatorLineThickness, "Line Thickness");
                }
                EditorGUILayout.EndFadeGroup();
                EditorGUI.indentLevel -= 2;
            }

        }

        // Ensure that shader variant for global replacement exists
        void DoReplacementArea(Material material)
        {
            float oriLabelWidth = EditorGUIUtility.labelWidth;

            //MakeSTSSectionHeader("Debug");

            EditorGUI.indentLevel += 1;
            showContentDebugArea = MakeSTSSectionHeaderWithFoldout("Replacement Options", showContentDebugArea);
            EditorGUI.indentLevel -= 1;
            if (showContentDebugArea)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;
                m_MaterialEditor.ShaderProperty(isReplacementShader, "Global Replacement");
                if (isReplacementShader.floatValue == 1)
                {
                    EditorGUILayout.HelpBox("Use this material only in conjunction with the \"Global Shader Replacement\" script! For every other use case, please disable this option!", MessageType.Info);
                }
                EditorGUI.indentLevel -= 2;
                //makeBetweenRange(previewIndicatorLineThickness,0.1f,0.5f);


                EditorGUILayout.Space();

            }
            //if(isReplacementShader.floatValue == 1)
            //{
            //    isReferenceMaterial = true;
            //    isReferenceMaterialMat.floatValue = 1;
            //}

        }

        void DoInteractionOptionsArea(Material material)
        {

            if (interactionMode.floatValue == (float)STSInteractionMode.Independent)
            {
                obstructionMode.floatValue = (float)ObstructionMode.None;
            }

            //MakeSTSSectionHeader("Interaction Options");
            EditorGUI.indentLevel += 1;
            showContentInteractionOptionsArea = MakeSTSSectionHeaderWithFoldout("Interaction Options", showContentInteractionOptionsArea);
            EditorGUI.indentLevel -= 1;
            //showDescription = EditorGUILayout.Foldout(showDescription, title);
            if (showContentInteractionOptionsArea)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;

                float oriLabelWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2;
                m_MaterialEditor.ShaderProperty(interactionMode, "Interaction Mode");
                EditorGUIUtility.labelWidth = oriLabelWidth;

                //if (interactionMode.floatValue == (float)STSInteractionMode.Independent)
                //{
                //    m_MaterialEditor.ShaderProperty(centerPosition, "Center Position");
                //}
                if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                {
                    EditorGUILayout.Space();

                    EditorUtils.DrawUILineSubMenu();

                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Effect Radius Only");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(defaultEffectRadius, defaultEffectRadius.displayName);
                    makeAlwaysPositiv(defaultEffectRadius);
                }
                else
                {
                    
                }
                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 2;
            }

         


        }

        void DoObstructionOptionsArea(Material material)
        {

            //MakeSTSSectionHeader("Obstruction Options");
            EditorGUI.indentLevel += 1;
            showContentObstructionOptionsArea = MakeSTSSectionHeaderWithFoldout("Obstruction Options", showContentObstructionOptionsArea);
            EditorGUI.indentLevel -= 1;
            //showDescription = EditorGUILayout.Foldout(showDescription, title);
            if (showContentObstructionOptionsArea)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel += 2;

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                EditorGUILayout.LabelField("Obstruction Settings");
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;

                float oriLabelWidth = EditorGUIUtility.labelWidth;
                if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                {
                    
                    EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2;
                    m_MaterialEditor.ShaderProperty(obstructionMode, "Obstruction Mode");
                    EditorGUIUtility.labelWidth = oriLabelWidth;
                } else
                {
                    obstructionMode.floatValue = (float)ObstructionMode.None;
                }
                EditorGUI.indentLevel += 1;
                EditorGUI.indentLevel += 1;

                if (obstructionMode.floatValue != (float)ObstructionMode.None)
                {
                    Rect rect = EditorGUILayout.BeginVertical();
                    rect.width -= 40;
                    rect.x += 40;
                    //GUI.Box(rect, GUIContent.none);

                    if (EditorGUIUtility.isProSkin)
                    {
                        //GUI.Box(rect, GUIContent.none);
                        DrawBox(rect, new Color(1f, 1.5f, 2f, 1));

                    }
                    else
                    {
                        DrawBox(rect, new Color(0.8f, 0.8f, 0.8f, 1));
                    }
                }


                if (obstructionMode.floatValue == (float)ObstructionMode.AngleOnly ||
                    obstructionMode.floatValue == (float)ObstructionMode.AngleAndCone ||
                    obstructionMode.floatValue == (float)ObstructionMode.AngleAndCylinder)
                {
                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Angle Obstruction");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(angleStrength, "Strength");

                }

                if (obstructionMode.floatValue == (float)ObstructionMode.ConeOnly ||
                    obstructionMode.floatValue == (float)ObstructionMode.AngleAndCone)
                {
                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Cone Obstruction");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(coneStrength, "Strength");
                    m_MaterialEditor.ShaderProperty(coneObstructionDestroyRadius, "Obstruction Destroy Radius");
                    makeAlwaysPositiv(coneObstructionDestroyRadius);

                }



                if (obstructionMode.floatValue == (float)ObstructionMode.CylinderOnly ||
                    obstructionMode.floatValue == (float)ObstructionMode.AngleAndCylinder)
                {
                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Cylinder Obstruction");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(cylinderStrength, "Strength");
                    m_MaterialEditor.ShaderProperty(cylinderObstructionDestroyRadius, "Obstruction Destroy Radius");
                    makeAlwaysPositiv(cylinderObstructionDestroyRadius);

                }


                if (obstructionMode.floatValue == (float)ObstructionMode.Circle)
                {
                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Circle Obstruction");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;

                    m_MaterialEditor.ShaderProperty(circleStrength, "Strength");
                    m_MaterialEditor.ShaderProperty(circleObstructionDestroyRadius, "Obstruction Destroy Radius");
                    makeAlwaysPositiv(circleObstructionDestroyRadius);

                }


                if (obstructionMode.floatValue == (float)ObstructionMode.Curve)
                {
                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    EditorGUILayout.LabelField("Curve Obstruction");
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;
                    EditorGUI.BeginChangeCheck();
                    curveSO.curve = EditorGUILayout.CurveField(curveSO.curve, Color.red, new Rect(0, 0, 1, 1));
                    if (EditorGUI.EndChangeCheck())
                    {
                        UpdateCurveTexture(material);
                        //EditorUtility.SetDirty(curveSO);
                        //AssetDatabase.SaveAssets();
                    }
                    string name = material.name;
                    if (!name.Contains(SeeThroughShaderConstants.STS_INSTANCE_NAME))
                    {
                        if (!curveSO.isBakedToTexture)
                        {
                            EditorGUILayout.HelpBox("THIS IS ONLY A PREVIEW! TO USE THE CURVE IN BUILDS, YOU HAVE TO BAKE THE CURVE!", MessageType.Warning);
                            var rect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(new GUILayoutOption[] { }));
                            if (GUI.Button(rect, "Bake Curve"))
                            {
                                SaveTexture(material);
                                //name = name.Replace(" (Instance)", "");
                                Texture2D texture = Resources.Load("Curves/" + name, typeof(Texture2D)) as Texture2D;
                                SetTextureImporterFormat(texture, true);
                                dissolveObstructionCurve.textureValue = texture;
                                //material.SetTexture("_ObstructionCurve", texture);
                                curveSO.isBakedToTexture = true;
                                EditorUtility.SetDirty(curveSO);
                                AssetDatabase.SaveAssets();
                            }
                        }
                    }


                    m_MaterialEditor.ShaderProperty(curveStrength, "Strength");
                    m_MaterialEditor.ShaderProperty(curveObstructionDestroyRadius, "Obstruction Destroy Radius");
                    makeAlwaysPositiv(curveObstructionDestroyRadius);
                    //CurveToTexture();
                    //if (curveTexture != null)
                    //{
                    //    EditorGUI.PrefixLabel(new Rect(25, 45, 100, 15), 0, new GUIContent("Preview:"));
                    //    EditorGUI.DrawPreviewTexture(new Rect(25, 60, 100, 100), curveTexture);
                    //} else
                    //{
                    //    Debug.Log("CURVE TEX IS NULL");
                    //}
                }



                //EditorGUILayout.Space();
                if (obstructionMode.floatValue != (float)ObstructionMode.None && obstructionMode.floatValue != (float)ObstructionMode.AngleOnly)
                {
                    m_MaterialEditor.ShaderProperty(dissolveFallOff, "FallOff");
                }

                EditorGUILayout.Space();

                if (obstructionMode.floatValue > 1)
                {
                    dissolveMaskEnabled.floatValue = Convert.ToSingle(EditorGUILayout.Toggle("Use Dissolve Mask", Convert.ToBoolean(dissolveMaskEnabled.floatValue)));
                    dissolveMaskEnabledAnimBool.target = dissolveMaskEnabled.floatValue == 1;
                    if (EditorGUILayout.BeginFadeGroup(dissolveMaskEnabledAnimBool.faded))
                    {
                        EditorGUILayout.Space();
                        //EditorGUI.indentLevel++;
                        m_MaterialEditor.TexturePropertySingleLine(Styles.dissolveMaskText, dissolveMask);
                        //EditorGUI.indentLevel--;
                        if (dissolveMask.textureValue == null)
                        {
                            EditorGUILayout.HelpBox("You didn't select any dissolve mask! The 'See-through Shader' effect will only work if you select one OR disable 'Use Dissolve Mask'!", MessageType.Error);
                        }
                    }
                    EditorGUILayout.EndFadeGroup();


                }

                if (obstructionMode.floatValue != (float)ObstructionMode.None)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.EndVertical();
                }

                EditorGUI.indentLevel -= 1;

                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                EditorGUILayout.LabelField("Intrinsic Dissolve Obstruction");
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;

                m_MaterialEditor.ShaderProperty(intrinsicDissolveStrength, "Strength");

                EditorGUI.indentLevel -= 1;


                if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                {
                    EditorGUILayout.Space();

                    EditorUtils.DrawUILineSubMenu();

                    EditorGUI.indentLevel -= 1;
                    EditorStyles.label.normal.textColor = oriCol;
                    isometricExlusionEnabled.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft("Isometric Exclusion",
                                                              Convert.ToBoolean(isometricExlusionEnabled.floatValue)));
                    EditorStyles.label.normal.textColor = textColor;
                    EditorGUI.indentLevel += 1;


                    EditorGUILayout.Space();

                    isometricExlusionEnabledAnimBool.target = isometricExlusionEnabled.floatValue == 1;
                    if (EditorGUILayout.BeginFadeGroup(isometricExlusionEnabledAnimBool.faded))
                    {
                        //EditorGUI.indentLevel++;
                        m_MaterialEditor.ShaderProperty(isometricExclusionDistance, "Isometric Plane Distance");
                        m_MaterialEditor.ShaderProperty(isometricExclusionGradientLength, "Dissolve Texture Gradient Length");
                        makeAlwaysPositiv(isometricExclusionGradientLength);
                        //EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndFadeGroup();

                }
                else
                {
                    isometricExlusionEnabled.floatValue = 0;
                }


                EditorGUILayout.Space();
                EditorUtils.DrawUILineSubMenu();
                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                ceilingEnabled.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft("Ceiling",
                                                          Convert.ToBoolean(ceilingEnabled.floatValue)));
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;
                EditorGUILayout.Space();


                ceilingEnabledAnimBool.target = ceilingEnabled.floatValue == 1;
                if (EditorGUILayout.BeginFadeGroup(ceilingEnabledAnimBool.faded))
                {
                    //EditorGUI.indentLevel++;
                    EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2;
                    if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                    {

                        m_MaterialEditor.ShaderProperty(ceilingMode, "Ceiling Mode");
                    }
                    else
                    {
                        ceilingMode.floatValue = (float)CeilingMode.Manual;
                    }

                    m_MaterialEditor.ShaderProperty(ceilingBlendMode, "Blend Mode");
                    EditorGUIUtility.labelWidth = oriLabelWidth;
                    if (ceilingMode.floatValue == (float)CeilingMode.Manual)
                    {
                        m_MaterialEditor.ShaderProperty(ceilingY, "CeilingY");
                    }
                    else if (ceilingMode.floatValue == (float)CeilingMode.PlayerPosition)
                    {
                        m_MaterialEditor.ShaderProperty(ceilingPlayerYOffset, "PlayerPos Y Offset");
                    }
                    m_MaterialEditor.ShaderProperty(ceilingYGradientLength, "Dissolve Texture Gradient Length");
                    makeAlwaysPositiv(ceilingYGradientLength);
                    //EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();


                EditorGUILayout.Space();
                EditorUtils.DrawUILineSubMenu();

                //EditorGUI.indentLevel -= 1;
                //EditorStyles.label.normal.textColor = oriCol;
                //EditorGUILayout.LabelField("Floor Exclusion Settings");
                //EditorStyles.label.normal.textColor = textColor;
                //EditorGUI.indentLevel += 1;
                EditorGUI.indentLevel -= 1;
                EditorStyles.label.normal.textColor = oriCol;
                floorEnabled.floatValue = Convert.ToSingle(EditorGUILayout.ToggleLeft("Floor Exclusion",
                                                          Convert.ToBoolean(floorEnabled.floatValue)));
                EditorStyles.label.normal.textColor = textColor;
                EditorGUI.indentLevel += 1;
                floorEnabledAnimBool.target = floorEnabled.floatValue == 1;
                if (EditorGUILayout.BeginFadeGroup(floorEnabledAnimBool.faded))
                {
                    if (interactionMode.floatValue == (float)STSInteractionMode.PlayerBased)
                    {
                        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth / 2;
                        m_MaterialEditor.ShaderProperty(floorMode, "Floor Mode");
                        EditorGUIUtility.labelWidth = oriLabelWidth;
                    }
                    else
                    {
                        floorMode.floatValue = (float)FloorMode.Manual;
                    }


                    if (floorMode.floatValue == (float)FloorMode.Manual)
                    {
                        m_MaterialEditor.ShaderProperty(floorY, "FloorY");
                    }
                    else if (floorMode.floatValue == (float)FloorMode.PlayerPosition)
                    {
                        m_MaterialEditor.ShaderProperty(playerPosYOffset, "PlayerPos Y Offset");
                    }

                    m_MaterialEditor.ShaderProperty(floorYTextureGradientLength, "Floor Gradient Length");
                    makeAlwaysPositiv(floorYTextureGradientLength);
                }
                EditorGUILayout.EndFadeGroup();
                EditorGUILayout.Space();
                EditorGUI.indentLevel -= 2;

            }

        }

        static void MaterialChanged(Material material)
        {
            SetupMaterialWithBlendMode(material, (BlendMode)material.GetFloat("_Mode"));
            SetMaterialKeywords(material);
  
        }

        static SmoothnessMapChannel GetSmoothnessMapChannel(Material material)
        {
            int ch = (int)material.GetFloat("_SmoothnessTextureChannel");
            if (ch == (int)SmoothnessMapChannel.AlbedoAlpha)
                return SmoothnessMapChannel.AlbedoAlpha;
            else
                return SmoothnessMapChannel.SpecularMetallicAlpha;
        }

        static void SetMaterialKeywords(Material material)
        {
            SetKeyword(material, "_NORMALMAP", material.GetTexture("_BumpMap") || material.GetTexture("_DetailNormalMap"));
            SetKeyword(material, "_METALLICGLOSSMAP", material.GetTexture("_MetallicGlossMap"));
            SetKeyword(material, "_PARALLAXMAP", material.GetTexture("_ParallaxMap"));
            SetKeyword(material, "_DETAIL_MULX2", material.GetTexture("_DetailAlbedoMap") || material.GetTexture("_DetailNormalMap"));

            MaterialEditor.FixupEmissiveFlag(material);
            bool shouldEmissionBeEnabled = (material.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0;
            SetKeyword(material, "_EMISSION", shouldEmissionBeEnabled);
            if (material.HasProperty("_SmoothnessTextureChannel"))
            {
                SetKeyword(material, "_SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A", GetSmoothnessMapChannel(material) == SmoothnessMapChannel.AlbedoAlpha);
            }
            SetKeyword(material, "_DISSOLVEMASK", material.GetFloat("_DissolveMaskEnabled") == 1);
            SetKeyword(material, "_ZONING", material.GetFloat("_Zoning") == 1);
            SetKeyword(material, "_OBSTRUCTION_CURVE", material.GetFloat("_Obstruction") == (float)ObstructionMode.Curve);
            SetKeyword(material, "_PLAYERINDEPENDENT", material.GetFloat("_InteractionMode") == (float)STSInteractionMode.Independent);
            SetKeyword(material, "_REPLACEMENT", material.GetFloat("_IsReplacementShader") == 1);
        }
        static void SetKeyword(Material m, string keyword, bool state)
        {
            if (state)
                m.EnableKeyword(keyword);
            else
                m.DisableKeyword(keyword);
        }

        bool IsKeywordEnabled(Material m, string keyword)
        {
            return m.IsKeywordEnabled(keyword);
        }

        void makeAlwaysPositiv(MaterialProperty materialProperty)
        {
            materialProperty.floatValue = Mathf.Max(materialProperty.floatValue, 0);
        }


        void makeBetweenRange(MaterialProperty materialProperty, float min, float max)
        {
            materialProperty.floatValue = Mathf.Max(materialProperty.floatValue, min);
            materialProperty.floatValue = Mathf.Min(materialProperty.floatValue, max);
        }

        public void CurveToTexture()
        {
            if (curveTexture != null)
            {
                if (curveTexture.width != curveTextureResolution)
                    curveTexture.Reinitialize(curveTextureResolution, 1);

                curveTexture.wrapMode = TextureWrapMode.Clamp; 
                curveTexture.filterMode = FilterMode.Bilinear;

                Color[] colors = new Color[curveTextureResolution];
                for (int i = 0; i < curveTextureResolution; ++i)
                {
                    var t = (float)i / curveTextureResolution;

                    colors[i].r = curveSO.curve.Evaluate(t);
                }
                curveTexture.SetPixels(colors);
                curveTexture.Apply(false);
            }
        }

        public void SaveTexture(Material material)
        {
            if (curveTexture != null)
            {
                byte[] bytes = curveTexture.EncodeToPNG();
                var dirPath = Application.dataPath + "/See-through Shader/Core/Resources/Curves/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string name = material.name;
                //name = name.Replace(" (Instance)", "");
                File.WriteAllBytes(dirPath + name  + ".png", bytes);
                //Debug.Log(name + " saved");
            }
        }


        public void CreateInitialTexture(Material material)
        {
            if (curveTexture != null)
            {
                var dirPath = Application.dataPath + "/See-through Shader/Core/Resources/Curves/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                } else
                {
                    string name = material.name;

                    if (!File.Exists(dirPath + name + ".png"))
                    {
                        //CurveToTexture();
                        Debug.Log(name + " doesn't exist");
                        SaveTexture(material);
                        AssetDatabase.Refresh();
                        Texture2D texture = Resources.Load("Curves/" + name, typeof(Texture2D)) as Texture2D;
                        SetTextureImporterFormat(texture, true);
                        dissolveObstructionCurve.textureValue = texture;
                    }
                }

            }
        }

        public void UpdateCurveTexture(Material material)
        {
            CurveToTexture();
            //SaveTexture(material);
            if (curveTexture!=null)
            {
                //Texture2D texture = Resources.Load("Curves/" + "CurveFor" + material.name, typeof(Texture2D)) as Texture2D;
                //SetTextureImporterFormat(texture, true);
                //dissolveObstructionCurve.textureValue = null;
                dissolveObstructionCurve.textureValue = curveTexture;
                //material.SetTexture("_ObstructionCurve", curveTexture);
                curveSO.isBakedToTexture = false;
            }
        }


        public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
        {
            if (null == texture) return;

            string assetPath = AssetDatabase.GetAssetPath(texture);
            var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (tImporter != null)
            {
                tImporter.isReadable = isReadable;

                AssetDatabase.ImportAsset(assetPath);
                AssetDatabase.Refresh();
            }
        }
        public static void SetupMaterialWithBlendMode(Material material, BlendMode blendMode)
        {
            //switch (blendMode)
            //{
            //    case BlendMode.Opaque:
            //        material.SetFloat("_AlphaMode", (float)AlphaModes.Opaque);
            //        material.SetOverrideTag("RenderType", "");
            //        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            //        material.SetInt("_ZWrite", 1);
            //        //material.DisableKeyword("_ALPHATEST_ON");
            //        //material.renderQueue = -1;
            //        //material.SetColor("_Color", Color.green);
            //        break;
            //    case BlendMode.Cutout:
            //        material.SetFloat("_AlphaMode", (float)AlphaModes.Opaque);
            //        //material.SetInt("_ZWrite", 1);
            //        //material.EnableKeyword("_ALPHATEST_ON");
            //        //material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
            //        break;
            //    case BlendMode.Transparent:
            //        material.SetOverrideTag("RenderType", "Transparent");
            //        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            //        //material.SetColor("_Color", Color.blue);
            //        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //        material.SetInt("_ZWrite", 0);
            //        //material.SetFloat("_AlphaMode", (float)AlphaModes.Blend);
            //        break;
            //}
        }

    }
}

