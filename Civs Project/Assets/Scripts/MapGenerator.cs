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

    public int seed;
    public Vector2 offset;

    // Terrain parameters
    public float terrrainNoiseScale;
    public int terrainOctaves;
    [Range(0, 1)]
    public float terrainPersistance;
    public float terrainLacunarity;

    // Automatically update the map when a parameter is changed in the editor
    public bool autoUpdate;


    public void GenerateMap()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        terrainTilemap.ClearAllTiles();

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, terrrainNoiseScale, terrainOctaves, terrainPersistance, terrainLacunarity, offset);

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

    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (terrainOctaves < 0)
        {
            terrainOctaves = 0;
        }
        if (terrainLacunarity < 1)
        {
            terrainLacunarity = 1;
        }
    }
}
