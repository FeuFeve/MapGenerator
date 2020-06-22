using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    // Tiles data
    GameTile[,] gameTiles;

    // Tilemaps
    public Tilemap terrainTilemap;
    public Tilemap featuresTilemap;

    // Terrain tiles
    public Tile[] oceanTiles;
    public Tile[] coastalTiles;
    public Tile[] grassTiles;
    public Tile[] hillTiles;
    public Tile[] mountainTiles;

    // Feature tiles
    public Tile[] forestTiles;

    // Map global parameters
    public int mapWidth;
    public int mapHeight;

    public int seed;
    public Vector2 offset;

    // Terrain parameters
    public float terrrainNoiseScale;
    [Range(0, 8)]
    public int terrainOctaves;
    [Range(0, 1)]
    public float terrainPersistance;
    public float terrainLacunarity;

    // Terrain parameters
    public float featuresNoiseScale;
    [Range(0, 8)]
    public int featuresOctaves;
    [Range(0, 1)]
    public float featuresPersistance;
    public float featuresLacunarity;

    // Automatically update the map when a parameter is changed in the editor
    public bool autoUpdate;


    public void GenerateMap()
    {
        gameTiles = new GameTile[mapWidth, mapHeight];

        GenerateTerrain(seed);
        GenerateFeatures(seed + 1);
    }

    public void GenerateTerrain(int seed)
    {
        terrainTilemap.ClearAllTiles();
        
        System.Random prng = new System.Random(seed);

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, terrrainNoiseScale, terrainOctaves, terrainPersistance, terrainLacunarity, offset);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (noiseMap[x, y] < 0.45)
                {
                    gameTiles[x, y] = new GameTile("ocean");
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), oceanTiles[prng.Next(0, oceanTiles.Length)]);
                }
                else if (noiseMap[x, y] < 0.55)
                {
                    gameTiles[x, y] = new GameTile("coast");
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), coastalTiles[prng.Next(0, coastalTiles.Length)]);
                }
                else if (noiseMap[x, y] < 0.75)
                {
                    gameTiles[x, y] = new GameTile("grass");
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), grassTiles[prng.Next(0, grassTiles.Length)]);
                }
                else if (noiseMap[x, y] < 0.85)
                {
                    gameTiles[x, y] = new GameTile("hill");
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), hillTiles[prng.Next(0, hillTiles.Length)]);
                }
                else
                {
                    gameTiles[x, y] = new GameTile("mountain");
                    terrainTilemap.SetTile(new Vector3Int(x, y, 0), mountainTiles[prng.Next(0, mountainTiles.Length)]);
                }
            }
        }
    }

    public void GenerateFeatures(int seed)
    {
        featuresTilemap.ClearAllTiles();

        System.Random prng = new System.Random(seed);

        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, featuresNoiseScale, featuresOctaves, featuresPersistance, featuresLacunarity, offset);

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (noiseMap[x, y] < 0.4)
                {
                    if (!gameTiles[x, y].isWater && !gameTiles[x, y].isImpassable)
                    {
                        featuresTilemap.SetTile(new Vector3Int(x, y, 0), forestTiles[prng.Next(0, forestTiles.Length)]);
                    }
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
        if (mapWidth > 200)
        {
            mapWidth = 200;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (mapHeight > 200)
        {
            mapHeight = 200;
        }

        if (terrainLacunarity < 1)
        {
            terrainLacunarity = 1;
        }
        if (featuresLacunarity < 1)
        {
            featuresLacunarity = 1;
        }
    }
}
