using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMeshAndBasemapByPositionIndex : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;


        [Space(10)]
        public bool exportHoles = false;
        public int mapsResolution = 512;
              

        [Header("Chunk count is 4x4")]
        [Range(0, 3)]
        public int positionX;

        [Range(0, 3)] 
        public int positionY;




        int chunkCountHorizontal = 4;
        int chunkCountVertical = 4;


        void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh from terrain by position///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
            Mesh terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, chunkCountHorizontal, chunkCountVertical, positionX, positionY, true, TerrainToMesh.Normal.CalculateFromMesh);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;




            //2. Export basemap textures by position///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Texture2D diffuseTexture = terrainData.TerrainToMesh().ExportBasemapDiffuseTexture(mapsResolution, chunkCountHorizontal, chunkCountVertical, positionX, positionY, exportHoles, false);  //alpha channel will contain holesmap
            Texture2D normalTexture = terrainData.TerrainToMesh().ExportBasemapNormalTexture(mapsResolution, chunkCountHorizontal, chunkCountVertical, positionX, positionY, false);
            
            Texture2D maskTexture = terrainData.TerrainToMesh().ExportBasemapMaskTexture(mapsResolution, chunkCountHorizontal, chunkCountVertical, positionX, positionY, false);       //contains metallic(R), occlusion(G) and smoothness(A)



            //3. Create material and assign exported basemaps/////////////////////////////////////////////////////////////////////////////////////////////////

            Material material = new Material(Shader.Find("Standard"));


            material.SetTexture("_MainTex", diffuseTexture);    //Prop names are defined inside shader
            material.SetTexture("_BumpMap", normalTexture);

            if (maskTexture != null)
            {
                material.SetTexture("_MetallicGlossMap", maskTexture);
                material.EnableKeyword("_METALLICGLOSSMAP");
            }


            if (normalTexture != null)
            {
                material.EnableKeyword("_NORMALMAP");
            }


            if (exportHoles)
            {
                material.SetFloat("_Mode", 1);
                material.EnableKeyword("_ALPHATEST_ON");
            }


            GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}