using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMeshAndBasemap : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;

        [Space(10)]
        public bool exportHoles = false;
        public int mapsResolution = 512;


        void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh from terrain////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Mesh terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, TerrainToMesh.Normal.CalculateFromMesh);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;




            //2. Export basemap textures////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Texture2D diffuseTexture = terrainData.TerrainToMesh().ExportBasemapDiffuseTexture(mapsResolution, exportHoles, false);  //Basemaps's alpha channel contains holesmap, if 'exportHoles' is enabled
            Texture2D normalTexture = terrainData.TerrainToMesh().ExportBasemapNormalTexture(mapsResolution, false);

            Texture2D maskTexture = terrainData.TerrainToMesh().ExportBasemapMaskTexture(mapsResolution, false);       //Contains metallic(R), occlusion(G) and smoothness(A)



            //3. Create material and assign exported basemaps/////////////////////////////////////////////////////////////////////////////////////////////////

            Material material = new Material(Shader.Find("Standard"));


            material.SetTexture("_MainTex", diffuseTexture);    //Prop names are defined inside shader
            material.SetTexture("_BumpMap", normalTexture);

            if (maskTexture != null)
            {
                material.SetTexture("_MetallicGlossMap", maskTexture);
                material.EnableKeyword("_METALLICGLOSSMAP");
            }

            if(normalTexture != null)
            {
                material.EnableKeyword("_NORMALMAP");
            }


            if(exportHoles)
            {
                material.SetFloat("_Mode", 1);
                material.EnableKeyword("_ALPHATEST_ON");
            }


            GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}