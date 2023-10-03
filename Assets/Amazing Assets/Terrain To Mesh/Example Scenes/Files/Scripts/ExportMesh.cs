using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMesh : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;


        void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh from terrain////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
            Mesh terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, TerrainToMesh.Normal.CalculateFromMesh);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;




            //2. Assign material////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Material material = new Material(Shader.Find("Standard"));

            GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}
