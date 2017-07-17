using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour {

    
	// Use this for initialization
	void Start () {

        var settings = new TerrainChunkSettings(65, 65, 100, 50);
        for (var i = 0; i < 4; i++)
        {
            for (var j = 0; j < 4; j++)
            {
                new TerrainChunk(settings, i, j, 0).CreateTerrain();
            }
        }

    }



// Update is called once per frame
void Update () {
		
	}
}
