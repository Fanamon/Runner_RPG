using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMeshAndSplatmap : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;

        [Space(10)]
        public bool exportHoles = false;
        public bool createFallbackTextures;

        void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh with edge fall/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Mesh terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, TerrainToMesh.Normal.CalculateFromMesh);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;




            //2. Export Splatmap material from terrain/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Material splatmapMaterial = terrainData.TerrainToMesh().ExportSplatmapMaterial(exportHoles);

            GetComponent<Renderer>().sharedMaterial = splatmapMaterial;




            //3. Fallback for Splatmap material

            if (createFallbackTextures)
            {
                Texture2D fallbackDiffuse = terrainData.TerrainToMesh().ExportBasemapDiffuseTexture(1024, exportHoles, false);
                Texture2D fallbackNormal = terrainData.TerrainToMesh().ExportBasemapNormalTexture(1024, false);

                splatmapMaterial.SetTexture(Utilities.GetMaterailPropMainTex(), fallbackDiffuse);
                splatmapMaterial.SetTexture(Utilities.GetMaterailPropBumpMap(), fallbackNormal);
            }
        }
    }
}