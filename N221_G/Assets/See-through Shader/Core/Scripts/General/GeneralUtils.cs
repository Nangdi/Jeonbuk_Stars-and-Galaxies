using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShaderCrew.SeeThroughShader
{
    public static class GeneralUtils
    {

        public static readonly List<string> STS_SHADER_LIST = new List<string>
    {
        "Custom/SeeThroughShaderHDRP2019",
        "Custom/SeeThroughShaderHDRP2020",
        "Custom/SeeThroughShaderHDRP2021",
        "Custom/SeeThroughShaderURP2019",
        "Custom/SeeThroughShaderURP2020",
        "Custom/SeeThroughShaderURP2021",
        "Custom/SeeThroughShader",
        
    };

        //public static readonly List<string> STS_REFMAT_LIST = new List<string>
        //{
        //    "refMaterial","refMaterial2","refMaterial3","refMaterial4",
        //};

        public static readonly List<string> STS_PROPERTIES_LIST = new List<string>
    {
        "_Obstruction", "_AnimationSpeed", "_AnimationEnabled", "_TransitionDuration",
        "_DissolveFallOff", "_PreviewMode", "_CircleObstructionDestroyRadius", "_CircleStrength",
        "_DissolveEmissionBooster", "_AngleStrength", "_IntrinsicDissolveStrength", "_ConeStrength",
        "_ConeObstructionDestroyRadius", "_CylinderStrength", "_CylinderObstructionDestroyRadius",
        "_Floor", "_FloorMode", "_FloorY", "_PlayerPosYOffset", "_UVs", "_hasClippedShadows", "_DissolveColorSaturation",
        "_DissolveEmission", "_TextureVisibility", "_FloorYTextureGradientLength", "_DefaultEffectRadius",
        "_TexturedEmissionEdgeStrength", "_TexturedEmissionEdge",
        "_DissolveMaskEnabled",
        "_IsometricExclusion", "_IsometricExclusionDistance", "_IsometricExclusionGradientLength",
        "_Ceiling", "_CeilingMode", "_CeilingBlendMode", "_CeilingY","_CeilingPlayerYOffset", "_CeilingYGradientLength",
        "_Mode", "_Cull",
        "_Zoning", "_ZoningMode", "_ZoningEdgeGradientLength", "_IsZoningRevealable", "_SyncZonesWithFloorY", "_SyncZonesFloorYOffset",
        "_PreviewIndicatorLineThickness",
        "_CurveStrength", "_CurveObstructionDestroyRadius",
    };

        public static readonly List<string> STS_KEYWORDS_LIST = new List<string>
    {
        "_DISSOLVEMASK",
        "_ZONING",
        "_OBSTRUCTION_CURVE",
        "_PLAYERINDEPENDENT"
    };


        public class UnityVersionRenderPipelineShaderInfo
        {
            public string unityVersion;
            public string renderPipeline;
            public string versionAndRPCorrectedShader;

            public UnityVersionRenderPipelineShaderInfo(string unityVersion, string renderPipeline, string shader)
            {
                this.unityVersion = unityVersion;
                this.renderPipeline = renderPipeline;
                this.versionAndRPCorrectedShader = shader;
            }
        }
        public static UnityVersionRenderPipelineShaderInfo getUnityVersionAndRenderPipelineCorrectedShaderString()
        {
            string unityVersion;
            string renderPipeline;
            string shaderString;
            unityVersion = Application.unityVersion;
            if (GraphicsSettings.currentRenderPipeline)
            {
                if (GraphicsSettings.currentRenderPipeline.GetType().ToString().Contains("HighDefinition"))
                {
                    renderPipeline = "HDRP";
                    if (unityVersion.Substring(0, 4).Equals("2019"))
                    {
                        shaderString = "Custom/SeeThroughShaderHDRP2019";
                    }
                    else if (unityVersion.Substring(0, 4).Equals("2020"))
                    {
                        shaderString = "Custom/SeeThroughShaderHDRP2020";
                    }
                    else
                    {
                        shaderString = "Custom/SeeThroughShaderHDRP2021";
                    }
                }
                else
                {
                    renderPipeline = "URP";
                    if (unityVersion.Substring(0, 4).Equals("2019"))
                    {
                        shaderString = "Custom/SeeThroughShaderURP2019";
                    }
                    else if (unityVersion.Substring(0, 4).Equals("2020"))
                    {
                        shaderString = "Custom/SeeThroughShaderURP2020";
                    }
                    else
                    {
                        shaderString = "Custom/SeeThroughShaderURP2021";
                    }
                }
            }
            else
            {
                renderPipeline = "Built-in RP";
                shaderString = "Custom/SeeThroughShader";
            }


            return new UnityVersionRenderPipelineShaderInfo(unityVersion, renderPipeline, shaderString);
        }


        //public static void updateSeeThroughShaderMaterialProperties(Transform[] transforms, string seeThroughShaderName, Material referenceMaterial)
        //{

        //    Material firstInstancedMaterial = getFirstInstancedMaterial(transforms, seeThroughShaderName);
        //    List<string> namesOfChangedProperties = getNamesOfAllChangedPropertyValues(firstInstancedMaterial, referenceMaterial);
        //    if (namesOfChangedProperties.Count > 0)
        //    {
        //        foreach (Transform transform in transforms)
        //        {
        //            Renderer rendererNonLOD = transform.GetComponent<Renderer>();

        //            if (transform.GetComponent<LODGroup>() != null)
        //            {
        //                foreach (LOD lod in transform.GetComponent<LODGroup>().GetLODs())
        //                {
        //                    foreach (Renderer renderer in lod.renderers)
        //                    {
        //                        if (renderer == rendererNonLOD)
        //                        {
        //                            rendererNonLOD = null;
        //                        }
        //                        if (renderer != null && renderer.materials.Length > 0)
        //                        {
        //                            foreach (Material mat in renderer.materials)
        //                            {
        //                                if (mat != null && mat.shader.name == seeThroughShaderName)
        //                                {
        //                                    updateMaterialProperties(mat, referenceMaterial, namesOfChangedProperties);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            if (rendererNonLOD != null)
        //            {
        //                Material[] materials = rendererNonLOD.materials;
        //                if (materials.Length > 0)
        //                {
        //                    foreach (Material material in materials)
        //                    {
        //                        if (material != null && material.shader.name == seeThroughShaderName)
        //                        {
        //                            updateMaterialProperties(material, referenceMaterial, namesOfChangedProperties);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Gets all materials, including the ones in various LOD levels, which are associated with given transforms
        public static List<Material> getAllSTSMaterialsFromTransforms(Transform[] transforms, string seeThroughShaderName, Material referenceMaterial)
        {
            List<Material> materialList = new List<Material>();        

            foreach (Transform transform in transforms)
            {
                Renderer rendererNonLOD = transform.GetComponent<Renderer>();

                if (transform.GetComponent<LODGroup>() != null)
                {
                    foreach (LOD lod in transform.GetComponent<LODGroup>().GetLODs())
                    {
                        foreach (Renderer renderer in lod.renderers)
                        {
                            if (renderer == rendererNonLOD)
                            {
                                rendererNonLOD = null;
                            }
                            if (renderer != null && renderer.materials.Length > 0)
                            {
                                foreach (Material material in renderer.materials)
                                {
                                    if (material != null && material.shader.name == seeThroughShaderName)
                                    {
                                        if(!materialList.Contains(material))
                                        {
                                            materialList.Add(material);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (rendererNonLOD != null)
                {
                    Material[] materials = rendererNonLOD.materials;
                    if (materials.Length > 0)
                    {
                        foreach (Material material in materials)
                        {
                            if (material != null && material.shader.name == seeThroughShaderName)
                            {
                                if (!materialList.Contains(material))
                                {
                                    materialList.Add(material);
                                }
                            }
                        }
                    }
                }
            }

            return materialList;
        }

        public static void updateSeeThroughShaderMaterialPropertiesAndKeywords(Transform[] transforms, string seeThroughShaderName, Material referenceMaterial)
        {

            Material firstInstancedMaterial = getFirstInstancedMaterial(transforms, seeThroughShaderName);
            List<string> namesOfChangedProperties = getNamesOfAllChangedPropertyValues(firstInstancedMaterial, referenceMaterial);
            List<string> namesOfChangedKeywords = getNamesOfAllChangedKeywordValues(firstInstancedMaterial, referenceMaterial);
            if (namesOfChangedProperties.Count > 0 || namesOfChangedKeywords.Count > 0)
            {
                List<Material> allSTSMaterials = getAllSTSMaterialsFromTransforms(transforms, seeThroughShaderName, referenceMaterial);
                foreach (Material material in allSTSMaterials)
                {
                    if (namesOfChangedProperties.Count > 0)
                    {
                        updateMaterialProperties(material, referenceMaterial, namesOfChangedProperties);
                    }

                    if (namesOfChangedKeywords.Count > 0)
                    {
                        updateMaterialKeywords(material, referenceMaterial, namesOfChangedKeywords);
                    }
                }
            }
        }

        public static void updateSeeThroughShaderMaterialKeywords(Transform[] transforms, string seeThroughShaderName, Material referenceMaterial)
        {

            Material firstInstancedMaterial = getFirstInstancedMaterial(transforms, seeThroughShaderName);
            List<string> namesOfChangedKeywords = getNamesOfAllChangedKeywordValues(firstInstancedMaterial, referenceMaterial);
            if (namesOfChangedKeywords.Count > 0)
            {
                List<Material> allSTSMaterials = getAllSTSMaterialsFromTransforms(transforms, seeThroughShaderName, referenceMaterial);
                foreach (Material material in allSTSMaterials)
                {
                    if (namesOfChangedKeywords.Count > 0)
                    {
                        updateMaterialKeywords(material, referenceMaterial, namesOfChangedKeywords);
                    }
                }
            }
        }

        private static void updateMaterialProperties(Material instancedMaterial, Material referenceMaterial, List<string> namesOfChangedProperties)
        {
            foreach (string propertyName in namesOfChangedProperties)
            {
                if (propertyName.Equals(SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX))
                {
                    Texture disTex = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX);
                    instancedMaterial.SetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX, disTex);
                }
                else if (propertyName.Equals(SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK))
                {
                    Texture disMask = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK);
                    instancedMaterial.SetTexture(SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK, disMask);
                }
                else if (propertyName.Equals(SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE))
                {
                    Texture obstructionCurve = referenceMaterial.GetTexture(SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE);
                    instancedMaterial.SetTexture(SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE, obstructionCurve);
                }
                else if (propertyName.Equals(SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR))
                {
                    instancedMaterial.SetColorArray(SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR, referenceMaterial.GetColorArray(SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR));
                }
                else
                {
                    float temp = referenceMaterial.GetFloat(propertyName);
                    instancedMaterial.SetFloat(propertyName, temp);
                }
            }
        }


        private static void updateMaterialKeywords(Material instancedMaterial, Material referenceMaterial, List<string> namesOfChangedKeywords)
        {
            foreach (string keyword in namesOfChangedKeywords)
            {
                bool isKeywordEnabled = referenceMaterial.IsKeywordEnabled(keyword);
                if(isKeywordEnabled)
                {
                    instancedMaterial.EnableKeyword(keyword);
                } else
                {
                    instancedMaterial.DisableKeyword(keyword);
                }
            }
        }

        private static List<string> getNamesOfAllChangedPropertyValues(Material instancedMaterial, Material referenceMaterial)
        {
            if (instancedMaterial != null && referenceMaterial != null)
            {

                if (GeneralUtils.STS_SHADER_LIST.Contains(instancedMaterial.shader.name) &&
                    GeneralUtils.STS_SHADER_LIST.Contains(instancedMaterial.shader.name))
                {

                    List<string> namesOfChangedProperties = new List<string>();
                    foreach (string propertyName in GeneralUtils.STS_PROPERTIES_LIST)
                    {
                        float instancedValue = instancedMaterial.GetFloat(propertyName);
                        float referenceValue = referenceMaterial.GetFloat(propertyName);
                        if (instancedValue != referenceValue)
                        {
                            namesOfChangedProperties.Add(propertyName);
                        }
                    }
                    string dissolveTexPropertyName = SeeThroughShaderConstants.PROPERTY_DISSOLVE_TEX;
                    Texture instancedDissolveTexture = instancedMaterial.GetTexture(dissolveTexPropertyName);
                    Texture referenceDissolveTexture = referenceMaterial.GetTexture(dissolveTexPropertyName);
                    if (instancedDissolveTexture != null && referenceDissolveTexture != null)
                    {
                        if (!String.Equals(instancedDissolveTexture.name, referenceDissolveTexture.name))
                        {
                            namesOfChangedProperties.Add(dissolveTexPropertyName);
                        }
                    }


                    string dissolveMaskPropertyName = SeeThroughShaderConstants.PROPERTY_DISSOLVE_MASK;
                    Texture instancedDissolveMask = instancedMaterial.GetTexture(dissolveMaskPropertyName);
                    Texture referenceDissolveMask = referenceMaterial.GetTexture(dissolveMaskPropertyName);
                    if (instancedDissolveMask != null && referenceDissolveMask != null)
                    {
                        if (!String.Equals(instancedDissolveMask.name, referenceDissolveMask.name))
                        {
                            namesOfChangedProperties.Add(dissolveMaskPropertyName);
                        }
                    }

                    string obstructionCurvePropertyName = SeeThroughShaderConstants.PROPERTY_OBSTRUCTION_CURVE;
                    Texture instancedObstructionCurve = instancedMaterial.GetTexture(obstructionCurvePropertyName);
                    Texture referenceObstructionCurve = referenceMaterial.GetTexture(obstructionCurvePropertyName);
                    if (instancedObstructionCurve != null && referenceObstructionCurve != null)
                    {
                        if (!String.Equals(instancedObstructionCurve.name, referenceObstructionCurve.name))
                        {
                            namesOfChangedProperties.Add(obstructionCurvePropertyName);
                        }
                    }

                    string dissolveColorPropertyName = SeeThroughShaderConstants.PROPERTY_DISSOLVE_COLOR;
                    Color[] instancedColor = instancedMaterial.GetColorArray(dissolveColorPropertyName);
                    Color[] referenceColor = referenceMaterial.GetColorArray(dissolveColorPropertyName);
                    if (!instancedColor[0].Equals(referenceColor[0]))
                    {
                        namesOfChangedProperties.Add(dissolveColorPropertyName);
                    }

                    return namesOfChangedProperties;

                }
                else
                {
                    Debug.LogWarning("InstancedMaterial and/or referenceMaterial don't have the See-through Shader applied! Please check your Reference Materials if the right See-through Shader is applied!");
                    return null;
                }


            }
            else
            {
                Debug.LogWarning("InstancedMaterial and/or referenceMaterial don't have the See-through Shader applied! Please check your Reference Materials if the right See-through Shader is applied!");
                return null;
            }
        }


        private static List<string> getNamesOfAllChangedKeywordValues(Material instancedMaterial, Material referenceMaterial)
        {
            if (instancedMaterial != null && referenceMaterial != null)
            {

                if (GeneralUtils.STS_SHADER_LIST.Contains(instancedMaterial.shader.name) &&
                    GeneralUtils.STS_SHADER_LIST.Contains(instancedMaterial.shader.name))
                {

                    List<string> namesOfChangedProperties = new List<string>();
                    foreach (string keyword in GeneralUtils.STS_KEYWORDS_LIST)
                    {

                        bool instancedValue = instancedMaterial.IsKeywordEnabled(keyword);
                        bool referenceValue = referenceMaterial.IsKeywordEnabled(keyword);
                        if (instancedValue != referenceValue)
                        {
                            namesOfChangedProperties.Add(keyword);
                        }
                    }

                    return namesOfChangedProperties;

                }
                else
                {
                    Debug.LogWarning("InstancedMaterial and/or referenceMaterial don't have the See-through Shader applied! Please check your Reference Materials if the right See-through Shader is applied!");
                    return null;
                }


            }
            else
            {
                Debug.LogWarning("InstancedMaterial and/or referenceMaterial don't have the See-through Shader applied! Please check your Reference Materials if the right See-through Shader is applied!");
                return null;
            }
        }


        public static void RenameInstancedMaterialName(Material materialInstance, string referenceMaterialName)
        {
            string name = materialInstance.name;
            name = name.Replace(" (Instance)", "");
            materialInstance.name = name + " (" + SeeThroughShaderConstants.STS_INSTANCE_NAME + " '" + referenceMaterialName + "')";
        }

        private static Material getFirstInstancedMaterial(Transform[] transforms, string seeThroughShaderName)
        {
            if (transforms != null && transforms.Length > 0)
            {
                foreach (Transform transform in transforms)
                {
                    if (transform != null)
                    {
                        Renderer renderer = transform.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            Material[] materials = renderer.materials;
                            if (materials != null && materials.Length > 0)
                            {
                                foreach (Material material in materials)
                                {
                                    if (material != null && material.shader.name == seeThroughShaderName)
                                    {
                                        return material;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            return null;
        }

        public static float getFirstPropertyValueFoundInMaterialList(List<Material> materialList, string propertyString)
        {
            float id = -1;
            if (materialList != null && materialList.Count > 0)
            {
                foreach (Material mat in materialList)
                {
                    if (mat != null)
                    {
                        if (!mat.HasProperty(propertyString) || mat.GetFloat(propertyString) == 0)
                        {
                        }
                        else
                        {
                            if (id == -1)
                            {
                                id = mat.GetFloat(propertyString);
                            }
                            else
                            {
                                Debug.Assert(id == mat.GetFloat(propertyString), "Materials have different Ids! Bug?");
                            }

                        }
                    }
                }
            }
            else
            {
                Debug.Assert(materialList.Count > 0, "Empty Material List. No materials with the See-through Shader could be found. It seems like you didn't apply the " +
                    "See-through Shader correctly. Please check if the See-through Shader is on the GameObjects during runtime. Are your GlobalShaderReplacement and/or" +
                    " GroupShaderReplacement settings correct? Maybe you selected the wrong layermask?");
            }
            if (id == -1)
            {
                id = IdGenerator.Instance.Id;
            }
            return id;
        }


        public static List<string> MaterialsNoApplyListToNameList(List<Material> materialExemptions)
        {
            List<string> materialNoApplyNames;
            if (materialExemptions != null && materialExemptions.Count > 0)
            {
                materialNoApplyNames = new List<string>();
                foreach (Material mat in materialExemptions)
                {
                    if (!materialNoApplyNames.Contains(mat.name))
                    {
                        materialNoApplyNames.Add(mat.name);
                    }
                }
                return materialNoApplyNames;
            }
            else
            {
                return null;
            }
        }


        public static void AddIfSeeThroughShaderMaterial(GameObject gameObject, string seeThroughShaderName, List<Material> listMaterial)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null && renderer.materials.Length > 0)
            {
                if (gameObject.gameObject.GetComponent<SeeThroughShaderPlayer>() == null || !gameObject.gameObject.GetComponent<SeeThroughShaderPlayer>().isActiveAndEnabled)// && item.gameObject.GetComponent<SeeThroughShaderExemption>() == null)
                {
                    for (int j = 0; j < renderer.materials.Length; j++)
                    {
                        if (renderer.materials[j] != null && renderer.materials[j].shader.name == seeThroughShaderName)
                        {
                            listMaterial.Add(renderer.materials[j]);
                        }
                    }
                }
            }
        }


        public static void adjustHDRPMaterial(Material original, Material STS)
        {
            if (original.HasProperty("_NormalMap"))
            {
                Texture normalMap = original.GetTexture("_NormalMap");
                if (normalMap != null)
                {
                    STS.SetTexture("_BumpMap", normalMap);
                    STS.EnableKeyword("_NORMALMAP");
                }

            }
            if (original.HasProperty("_NormalScale"))
            {
                float normalScale = original.GetFloat("_NormalScale");
                STS.SetFloat("_BumpScale", normalScale);
            }
            if (original.HasProperty("_EmissiveColorMap"))
            {
                Texture emissiveColorMap = original.GetTexture("_EmissiveColorMap");
                if (emissiveColorMap != null)
                {
                    STS.SetTexture("_EmissionMap", emissiveColorMap);
                    STS.EnableKeyword("_EMISSION");
                }
            }
            if (original.HasProperty("_EmissiveColor"))
            {
                Color emissiveColor = original.GetColor("_EmissiveColor");
                if (emissiveColor != null)
                {
                    STS.SetColor("_EmissionColor", emissiveColor);
                    STS.EnableKeyword("_EMISSION");
                    STS.globalIlluminationFlags = MaterialGlobalIlluminationFlags.AnyEmissive;
                }
            }
        }

        public static void adjustURPMaterial(Material original, Material STS)
        {
            if (original.HasProperty("_MetallicGlossMap"))
            {
                if (original.IsKeywordEnabled("_METALLICSPECGLOSSMAP"))
                {
                    STS.EnableKeyword("_METALLICGLOSSMAP");
                }

            }
            if (original.HasProperty("_Smoothness"))
            {
                float smoothness = original.GetFloat("_Smoothness");
                STS.SetFloat("_GlossMapScale", smoothness);
            }
        }
    }
}