using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    private TerrainChunk[,] world;

    // Use this for initialization
    void Start()
    {

        var settings = new TerrainChunkSettings(65, 65, 100, 50);
        world = new TerrainChunk[4, 4];

        for (var i = 0; i < 1; i++)
        {
            for (var j = 0; j < 1; j++)
            {
                TerrainChunk terrain = new TerrainChunk(settings, i, j, 0);
                terrain.CreateTerrain();
                world[i, j] = terrain;
            }
        }

        if (world[0, 0])
        {
            TerrainChunk terrain = world[1, 1];
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = new Vector3(50, terrain.GetHeight(0, 0), 50);
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.gravity = new Vector3(0, Physics.gravity.magnitude - 1.0f, 0);

        }
    }
}
