using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseOnPlane, TileMap}
    public DrawMode drawMode;

    public MapDisplay display;

    [Header("Noise Options")]
    public int mapWidth;

    public int mapHeight;

    public float noiseScale;

    public int seed;

    [Range (0,10)]
    public int octaves;

    [Range(0,1)]
    public float persistance;

    public float lacunarity;

    public Vector2 offset;
    public bool falloff;


    [Header("Terrain Tiles")]
    public TerrainTile[] terrainTiles;

    public void GenerateMap() 
    {
        float[,] noiseMap = 
            Noise.GenerateNoise(
                mapWidth, 
                mapHeight, 
                seed, 
                noiseScale, 
                octaves, 
                persistance, 
                lacunarity, 
                offset);
        
        float[,] falloffMap = 
            FalloffGenerator.GenerateFalloffMap(mapWidth, mapHeight);
        
        // noiseMap = falloffMap;
        
        if (falloff)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    noiseMap[x, y] = noiseMap[x, y] - falloffMap[x, y];
                }
            }
        }

        if (drawMode == DrawMode.NoiseOnPlane)
        {
            display.DisplayNoiseOnPlane(noiseMap);
        }
        else if (drawMode == DrawMode.TileMap)
        {
            Sprite[,] tileMap = GenerateTileMap(noiseMap);
            display.DisplayTileMap(tileMap);
        }
    }

    private Sprite[,] GenerateTileMap(float[,] noiseMap)
    {
        Sprite[,] spriteMap = new Sprite[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                spriteMap[x, y] = MapHeightToSprite(noiseMap[x, y]);
            }
        }
        return spriteMap;
    }

    private Sprite MapHeightToSprite(float height)
    {
        Sprite sprite = terrainTiles[0].sprite;
        foreach (TerrainTile terrainTile in terrainTiles)
        {
            if (height > terrainTile.minHeight)
                sprite = terrainTile.sprite;
        }
        return sprite;
    }

    [System.Serializable]
    public struct TerrainTile
    {
        public Sprite sprite;

        [Range(0,1)]
        public float minHeight;
    }
}