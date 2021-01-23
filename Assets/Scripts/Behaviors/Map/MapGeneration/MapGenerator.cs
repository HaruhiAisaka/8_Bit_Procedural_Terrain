using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseOnPlane, TileMap}
    public DrawMode drawMode;

    [Header("Components")]
    public MapDisplay display;
    public TileInstantiator tileInstantiator;
    public NoiseDataUI noiseDataUI;

    [Header("MapData")]
    public NoiseData noiseData;
    public TerrainData terrainData;

    void Start()
    {
        noiseData = new NoiseData(40, 30, 0, 2, .5f, 2, true);
        noiseDataUI.UpdateUI(noiseData);
        GenerateAndDisplayMap();
    }

    public void GenerateAndDisplayMap()
    {
        float[,] noiseMap = GenerateNoiseMap();

        if (drawMode == DrawMode.NoiseOnPlane)
        {
            display.DisplayNoiseOnPlane(noiseMap);
        }
        else if (drawMode == DrawMode.TileMap)
        {
            Tile[,] tileMap = GenerateTileMap(noiseMap);
            display.SetMapCenter(tileMap);
            display.DisplayTileMap(tileMap);
        }
    }

    private float[,] GenerateNoiseMap() 
    {
        float[,] noiseMap = 
            Noise.GenerateNoise(
                noiseData.mapWidth, 
                noiseData.mapHeight, 
                noiseData.seed, 
                noiseData.noiseScale, 
                noiseData.octaves, 
                noiseData.persistance, 
                noiseData.lacunarity, 
                noiseData.offset);
        
        float[,] falloffMap = 
            FalloffGenerator.GenerateFalloffMap(noiseData.mapWidth, noiseData.mapHeight);
        
        if (noiseData.falloff)
        {
            for (int y = 0; y < noiseData.mapHeight; y++)
            {
                for (int x = 0; x < noiseData.mapWidth; x++)
                {
                    noiseMap[x, y] = noiseMap[x, y] - falloffMap[x, y];
                }
            }
        }

        // noiseMap = falloffMap;

        return noiseMap;
    }

    private Tile[,] GenerateTileMap(float[,] noiseMap)
    {
        Tile[,] tileMap = new Tile[noiseData.mapWidth, noiseData.mapHeight];
        for (int y = 0; y < noiseData.mapHeight; y++)
        {
            for (int x = 0; x < noiseData.mapWidth; x++)
            {
                tileMap[x, y] = terrainData.MapHeightToTile(noiseMap[x, y]);
            }
        }
        return tileMap;
    }
}

public class NoiseData
{ 
    public int mapWidth;

    public int mapHeight;

    public float noiseScale = 30f;

    public int seed;

    [Range (0,10)]
    public int octaves;

    [Range(0,1)]
    public float persistance;

    public float lacunarity;

    public Vector2 offset = new Vector2(0,0);
    public bool falloff;

    public NoiseData (
        int mapWidth,
        int mapHeight,
        int seed,
        int octaves,
        float persistance,
        float lacunarity,
        bool falloff
    )
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.seed = seed;
        this.octaves = octaves;
        this.persistance = persistance;
        this.lacunarity = lacunarity;
        this.falloff = falloff;
    }

	void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}

}