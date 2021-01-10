using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {noiseTexture, noiseTiles, TerrainTiles}

    public DrawMode drawMode;

    public MapDisplay display;
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

    public TerrainTile[] terrainTiles;
    public bool autoUpdate = false;
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

        if (drawMode == DrawMode.TerrainTiles)
        {
            Sprite[,] spriteMap = SpriteMapFromNoiseMap(noiseMap);
            display.DrawTileMap(spriteMap);
        }
            
        else 
            display.DrawNoiseMap(noiseMap, drawMode);
    }

    private Sprite[,] SpriteMapFromNoiseMap(float[,] noiseMap)
    {   
        Sprite[,] spriteMap = new Sprite[mapWidth, mapHeight];

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                foreach (TerrainTile terrainTile in terrainTiles)
                {
                    if (noiseMap [x, y] >= terrainTile.minHeight)
                        spriteMap[x, y] = terrainTile.sprite;
                }
            }
        }

        return spriteMap;
    }

    private void OnValidate() 
    {
        if (octaves < 0)
            octaves = 0;
        if (lacunarity < 0)
            lacunarity = 0;
        if (noiseScale < 0)
            noiseScale = 0;
    }

    [System.Serializable]
    public struct TerrainTile
    {
        public Sprite sprite;

        [Range(0,1)]
        public float minHeight;
    }
}
