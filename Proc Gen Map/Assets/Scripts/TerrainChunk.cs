using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class TerrainChunkSettings
{
    public int HeightmapResolution { get; private set; }
    public int AlphamapResolution { get; private set; }

    public int Length { get; private set; }
    public int Height { get; private set; }

    public TerrainChunkSettings(int _hmres, int _amres, int _length, int _height)
    {
        HeightmapResolution = _hmres;
        AlphamapResolution = _amres;
        Length = _length;
        Height = _height;
    }
}

public class TerrainChunk : MonoBehaviour
{
    public int X { get; private set; }
    public int Z { get; private set; }

    public int width { get; private set; }
    public int length { get; private set; }

    private Terrain Terrain { get; set; }

    private TerrainChunkSettings Settings { get; set; }

    private float[,] heightmap;
    public float noiseSeed;
    private NoiseProvider NoiseProvider { get; set; }

    public TerrainChunk(int _X, int _Z, int _width, int _length)
    {
        X = _X;
        Z = _Z;
        width = _width;
        length = _length;
        noiseSeed = 1.0f;
        NoiseProvider = new NoiseProvider();
    }


    public MeshData CreateTerrain()
    {
        float[,] heightMap = new float[width, length];

        for (int x = X; x < X + width; x++)
        {
            for (int z = Z; z < Z + length; z++)
            {
                heightMap[x, z] = NoiseProvider.GetValue(x, z, noiseSeed);
            }
        }

        MeshData meshData = new MeshData(width, length);
        int vertexIndex = 0;

        for (int x =1; x < width; x++)
        {
            for (int z = 1; z < length; z++)
            {
                meshData.vertices[vertexIndex] = new Vector3(X + x, heightmap[x, z], Z + z);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, z / (float)length);

                if(x < width - 1 && z < length - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width +1, vertexIndex, vertexIndex + 1);
                }


                vertexIndex++;
            }
        }

        return meshData;

    }
}
    
public class NoiseProvider
{

    public float GetValue(float x, float z, float noiseSeed)
    {
        return Mathf.PerlinNoise(noiseSeed + x, noiseSeed + z) + (Mathf.PerlinNoise(noiseSeed + x*10, noiseSeed + z*10)/10);
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshLength)
    {
        vertices = new Vector3[meshWidth * meshLength];
        uvs = new Vector2[meshWidth * meshLength];
        triangles = new int[(meshWidth - 1) * (meshLength - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }


}
