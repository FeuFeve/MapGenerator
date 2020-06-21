using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Tilemap
    public Tilemap terrainTilemap;

    // Terrain tiles
    public Tile water;
    public Tile grass;
    public Tile mountain;
    public Tile snow;

    // Map global parameters
    public int mapWidth;
    public int mapHeight;

    // Terrain parameters
    public float noiseScale;

    public bool autoUpdate;


    public void GenerateMap()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        terrainTilemap.ClearAllTiles();

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (noiseMap[x, y] < 0.4)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), water);
                }
                else if (noiseMap[x, y] < 0.6)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), grass);
                }
                else if (noiseMap[x, y] < 0.8)
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), mountain);
                }
                else
                {
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), snow);
                }
            }
        }
    }
}
