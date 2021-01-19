using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseOnPlane, TileMap}
    public DrawMode drawMode;
    public MapDisplay display;

    public NoiseData noiseData;
    public TerrainData terrainData;

    public void GenerateMap() 
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

        if (drawMode == DrawMode.NoiseOnPlane)
        {
            display.DisplayNoiseOnPlane(noiseMap);
        }
        else if (drawMode == DrawMode.TileMap)
        {
            Tile[,] tileMap = GenerateTileMap(noiseMap);
            display.DisplayTileMap(tileMap);
        }
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