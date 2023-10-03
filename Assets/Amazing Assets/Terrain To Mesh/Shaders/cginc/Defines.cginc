#ifndef TERRAIN_TO_MESH_DEFINES_CGINC
#define TERRAIN_TO_MESH_DEFINES_CGINC



#if defined(_T2M_LAYER_COUNT_3) 

    #define NEED_PAINT_MAP_2

#elif defined(_T2M_LAYER_COUNT_4)

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3

#elif defined(_T2M_LAYER_COUNT_5)

    #define NEED_SPLAT_MAP_1

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4

#elif defined(_T2M_LAYER_COUNT_6)

    #define NEED_SPLAT_MAP_1

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5

#elif defined(_T2M_LAYER_COUNT_7)

    #define NEED_SPLAT_MAP_1

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6

#elif defined(_T2M_LAYER_COUNT_8)

    #define NEED_SPLAT_MAP_1

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7

#elif defined(_T2M_LAYER_COUNT_9)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8

#elif defined(_T2M_LAYER_COUNT_10)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9

#elif defined(_T2M_LAYER_COUNT_11)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10

#elif defined(_T2M_LAYER_COUNT_12)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10
    #define NEED_PAINT_MAP_11

#elif defined(_T2M_LAYER_COUNT_13)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2
    #define NEED_SPLAT_MAP_3

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10
    #define NEED_PAINT_MAP_11
    #define NEED_PAINT_MAP_12

#elif defined(_T2M_LAYER_COUNT_14)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2
    #define NEED_SPLAT_MAP_3

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10
    #define NEED_PAINT_MAP_11
    #define NEED_PAINT_MAP_12
    #define NEED_PAINT_MAP_13

#elif defined(_T2M_LAYER_COUNT_15)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2
    #define NEED_SPLAT_MAP_3

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10
    #define NEED_PAINT_MAP_11
    #define NEED_PAINT_MAP_12
    #define NEED_PAINT_MAP_13
    #define NEED_PAINT_MAP_14

#elif defined(_T2M_LAYER_COUNT_16)

    #define NEED_SPLAT_MAP_1
    #define NEED_SPLAT_MAP_2
    #define NEED_SPLAT_MAP_3

    #define NEED_PAINT_MAP_2
    #define NEED_PAINT_MAP_3
    #define NEED_PAINT_MAP_4
    #define NEED_PAINT_MAP_5
    #define NEED_PAINT_MAP_6
    #define NEED_PAINT_MAP_7
    #define NEED_PAINT_MAP_8
    #define NEED_PAINT_MAP_9
    #define NEED_PAINT_MAP_10
    #define NEED_PAINT_MAP_11
    #define NEED_PAINT_MAP_12
    #define NEED_PAINT_MAP_13
    #define NEED_PAINT_MAP_14
    #define NEED_PAINT_MAP_15

#endif


#if defined(_T2M_TEXTURE_SAMPLE_TYPE_ARRAY)
    
    #define T2M_DECLARE_LAYER(l)                float4 _T2M_Layer_##l##_MapsUsage; float4 _T2M_Layer_##l##_uvScaleOffset;   float4 _T2M_Layer_##l##_ColorTint; float4 _T2M_Layer_##l##_MetallicOcclusionSmoothness; int _T2M_Layer_##l##_SmoothnessFromDiffuseAlpha; 
    #define T2M_DECALRE_NORMAL(l)               float _T2M_Layer_##l##_NormalScale;
    #define T2M_DECALRE_MASK(l)                 float4 _T2M_Layer_##l##_MaskMapRemapMin; float4 _T2M_Layer_##l##_MaskMapRemapMax;


    #define T2M_UNPACK_SPLATMAP(uv,index)            UNITY_SAMPLE_TEX2DARRAY(_T2M_SplatMaps2DArray, float3(uv, index));
	#define T2M_UNPACK_PAINTMAP(uv,index,sum,splat)	 float4 paintColor##index = (_T2M_Layer_##index##_MapsUsage.x > 0.5 ? UNITY_SAMPLE_TEX2DARRAY(_T2M_DiffuseMaps2DArray, float3(uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw, paintMapUsageIndex)) : float4(1, 1, 1, 1));  paintMapUsageIndex += _T2M_Layer_##index##_MapsUsage.x > 0.5 ? 1 : 0;  sum += paintColor##index * _T2M_Layer_##index##_ColorTint * splat;
	#define T2M_UNPACK_NORMAL_MAP(index,uv,sum,splat) sum += TerrainToMeshNormalStrength(UnpackNormal(UNITY_SAMPLE_TEX2DARRAY(_T2M_NormalMaps2DArray, float3(uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw, normalMapUsageIndex))), _T2M_Layer_##index##_NormalScale) * splat; normalMapUsageIndex += 1;
	#define T2M_UNPACK_MASK(index,uv,sum,splat)       sum += TerrainToMeshRemap(UNITY_SAMPLE_TEX2DARRAY(_T2M_MaskMaps2DArray, float3(uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw, maskMapUsageIndex)), _T2M_Layer_##index##_MaskMapRemapMin, _T2M_Layer_##index##_MaskMapRemapMax) * splat; maskMapUsageIndex += 1;

#else

    #define T2M_DECLARE_LAYER(l)                UNITY_DECLARE_TEX2D_NOSAMPLER(_T2M_Layer_##l##_Diffuse);   float4 _T2M_Layer_##l##_uvScaleOffset;   float4 _T2M_Layer_##l##_ColorTint; float4 _T2M_Layer_##l##_MetallicOcclusionSmoothness; int _T2M_Layer_##l##_SmoothnessFromDiffuseAlpha;
    #define T2M_DECALRE_NORMAL(l)               UNITY_DECLARE_TEX2D_NOSAMPLER(_T2M_Layer_##l##_NormalMap); float _T2M_Layer_##l##_NormalScale;
    #define T2M_DECALRE_MASK(l)                 UNITY_DECLARE_TEX2D_NOSAMPLER(_T2M_Layer_##l##_Mask);      float4 _T2M_Layer_##l##_MaskMapRemapMin; float4 _T2M_Layer_##l##_MaskMapRemapMax;

    #define T2M_UNPACK_SPLATMAP(uv,index)            UNITY_SAMPLE_TEX2D_SAMPLER(_T2M_SplatMap_##index, _T2M_SplatMap_0, uv);
	#define T2M_UNPACK_PAINTMAP(uv,index,sum,splat)	 float4 paintColor##index = UNITY_SAMPLE_TEX2D_SAMPLER(_T2M_Layer_##index##_Diffuse, _T2M_Layer_0_Diffuse, uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw);	sum += paintColor##index * _T2M_Layer_##index##_ColorTint * splat;
	#define T2M_UNPACK_NORMAL_MAP(index,uv,sum,splat) sum += TerrainToMeshNormalStrength(UnpackNormal(UNITY_SAMPLE_TEX2D_SAMPLER(_T2M_Layer_##index##_NormalMap, _T2M_Layer_0_Diffuse, uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw)), _T2M_Layer_##index##_NormalScale) * splat;
	#define T2M_UNPACK_MASK(index,uv,sum,splat)       sum += TerrainToMeshRemap(UNITY_SAMPLE_TEX2D_SAMPLER(_T2M_Layer_##index##_Mask, _T2M_Layer_0_Diffuse, uv * _T2M_Layer_##index##_uvScaleOffset.xy + _T2M_Layer_##index##_uvScaleOffset.zw), _T2M_Layer_##index##_MaskMapRemapMin, _T2M_Layer_##index##_MaskMapRemapMax) * splat;

#endif


#define T2M_UNPACK_METALLIC_OCCLUSION_SMOOTHNESS(index,sum,splat)   sum += float4(lerp(GammaToLinearSpaceExact(_T2M_Layer_##index##_MetallicOcclusionSmoothness.r), _T2M_Layer_##index##_MetallicOcclusionSmoothness.r, IsGammaSpace()), _T2M_Layer_##index##_MetallicOcclusionSmoothness.g, 0, lerp(_T2M_Layer_##index##_MetallicOcclusionSmoothness.a, paintColor##index.a, _T2M_Layer_##index##_SmoothnessFromDiffuseAlpha)) * splat;


#endif
 