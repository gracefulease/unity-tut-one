using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int width = 10;
    public int length = 10;
    // Use this for initialization
    void Start()
    {
        
        for (var i = 0; i < 1; i++)
        {
            for (var j = 0; j < 1; j++)
            {
                TerrainChunk terrain = new TerrainChunk(i,j,width,length);
                terrain.CreateTerrain();
            }
        }
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");  
        player.transform.position = new Vector3(50, 50, 50);
        

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
