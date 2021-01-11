using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {NoiseTexture, TerrainTiles}

    public DrawMode drawMode;
    public bool falloff;

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

    [System.Serializable]
    public struct TerrainTile
    {
        public Sprite sprite;

        [Range(0,1)]
        public float minHeight;
    }
}