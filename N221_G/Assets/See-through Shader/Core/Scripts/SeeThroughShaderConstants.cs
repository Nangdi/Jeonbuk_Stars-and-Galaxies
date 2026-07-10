using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShaderCrew.SeeThroughShader
{
    public static class SeeThroughShaderConstants
    {
        //public static readonly int NUMBER_OF_PROPERTIES_PLAYERDATA = 6;
        public static readonly string PLAYERS_POS_SHADER_PROPERTY_NAME = "_PlayersPosVectorArray";
        public static readonly string PLAYERS_DATA_SHADER_PROPERTY_NAME = "_PlayersDataFloatArray";
        public static readonly string PLAYERS_POS_ARRAY_COUNT_PROPERTY_NAME = "_ArrayLength";

#if (UNITY_WEBGL)
        public static readonly int PLAYERS_POS_ARRAY_LENGTH = 20;
        public static readonly int PLAYERS_DATA_ARRAY_LENGTH = 150;
        public static readonly int ZONES_DATA_ARRAY_LENGTH = 500;   
#else
        public static readonly int PLAYERS_POS_ARRAY_LENGTH = 100;
        public static readonly int PLAYERS_DATA_ARRAY_LENGTH = 500;
        public static readonly int ZONES_DATA_ARRAY_LENGTH = 1000;
#endif
        public static readonly string ZONES_DATA_SHADER_PROPERTY_NAME = "_ZDFA"; // _ZonesDataFloatArray



        public static readonly string ZONES_DATA_COUNT_SHADER_PROPERTY_NAME = "_ZonesDataCount";

        public static readonly string STS_INSTANCE_NAME = "STS Instance of";



        // PROPERTIES
        public static readonly string PROPERTY_IS_REPLACEMENT_SHADER = "_IsReplacementShader";
        public static readonly string PROPERTY_DISSOLVE_TEX = "_DissolveTex";
        public static readonly string PROPERTY_DISSOLVE_TEX_GLOBAL = "_DissolveTexGlobal";
        public static readonly string PROPERTY_DISSOLVE_MASK = "_DissolveMask";
        public static readonly string PROPERTY_DISSOLVE_MASK_GLOBAL = "_DissolveMaskGlobal";
        public static readonly string PROPERTY_OBSTRUCTION_CURVE = "_ObstructionCurve";
        public static readonly string PROPERTY_OBSTRUCTION_CURVE_GLOBAL = "_ObstructionCurveGlobal";
        public static readonly string PROPERTY_DISSOLVE_COLOR = "_DissolveColor";
        public static readonly string PROPERTY_DISSOLVE_COLOR_GLOBAL = "_DissolveColorGlobal";
        public static readonly string PROPERTY_GLOBAL = "Global";

        // KEYWORDS
        public static readonly string KEYWORD_REPLACEMENT = "_REPLACEMENT";


    }
}