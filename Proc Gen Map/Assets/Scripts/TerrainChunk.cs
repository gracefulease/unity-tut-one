using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

public class TerrainChunk : ScriptableObject
{
    public int X { get; private set; }

    public int Z { get; private set; }

    private Terrain Terrain { get; set; }

    private TerrainChunkSettings Settings { get; set; }

    public float noiseSeed;
    private NoiseProvider NoiseProvider { get; set; }

    public TerrainChunk(TerrainChunkSettings _settings, int _X, int _Z, int _noiseSeed)
    {
        Settings = _settings;
        X = _X;
        Z = _Z;
        noiseSeed = _noiseSeed;
        NoiseProvider = new NoiseProvider();
    }


    public void CreateTerrain()
    {
        var terrainData = new TerrainData();
        terrainData.heightmapResolution = Settings.HeightmapResolution;
        terrainData.alphamapResolution = Settings.AlphamapResolution;

        if (noiseSeed != 0)
            noiseSeed = Time.fixedTime;

        var heightmap = GetHeightmap();
        terrainData.SetHeights(0, 0, heightmap);
        terrainData.size = new Vector3(Settings.Length, Settings.Height, Settings.Length);
        

        var newTerrainGameObject = Terrain.CreateTerrainGameObject(terrainData);
        newTerrainGameObject.transform.position = new Vector3(X * Settings.Length, 0, Z * Settings.Length);
        Terrain = newTerrainGameObject.GetComponent<Terrain>();
        Terrain.activeTerrain.heightmapMaximumLOD = 1;
       // Terrain.activeTerrain.heightmapPixelError = 64;
        Terrain.Flush();
    }

    private float[,] GetHeightmap()
    {
        var heightmap = new float[Settings.HeightmapResolution, Settings.HeightmapResolution];

        for (var zRes = 0; zRes < Settings.HeightmapResolution; zRes++)
        {
            for (var xRes = 0; xRes < Settings.HeightmapResolution; xRes++)
            {
                var xCoordinate = X + (float)xRes / (Settings.HeightmapResolution - 1);
                var zCoordinate = Z + (float)zRes / (Settings.HeightmapResolution - 1);

                heightmap[zRes, xRes] = NoiseProvider.GetValue(xCoordinate, zCoordinate, noiseSeed);
            }
        }

        return heightmap;
    }
}

public class NoiseProvider
{

    public float GetValue(float x, float z, float noiseSeed)
    {
        return Mathf.PerlinNoise(noiseSeed + x, noiseSeed + z);
    }
}
