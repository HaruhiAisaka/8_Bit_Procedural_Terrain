using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public GameObject tileMap;
    public Tile tile;

    private Tile[] tiles;

    public void DrawNoiseMap(float[,] noiseMap, MapGenerator.DrawMode drawMode) 
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * height + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        if (drawMode == MapGenerator.DrawMode.noiseTexture)
        {
            texture.SetPixels(colorMap);
            texture.Apply();

            textureRenderer.sharedMaterial.mainTexture = texture;
            textureRenderer.transform.localScale = new Vector3(width, 1, height);
        }
        else if (drawMode == MapGenerator.DrawMode.noiseTiles)
        {
            InstantiateTilesByColor(width, height, colorMap);
        }

    }

    private void InstantiateTilesByColor(int width, int height, Color[] colorMap)
    {
        tiles = new Tile[width * height];
        GameObject newTileMap = Instantiate(tileMap, this.transform);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile newTile = InstantiateTile(x, y, newTileMap);
                newTile.GetComponent<SpriteRenderer>().color = colorMap[y * height + x];
                tiles[y * height + x] = newTile;
            }
        }
    }

    
    public void DrawTileMap(Sprite[,] spriteMap)
    {
        int width = spriteMap.GetLength(0);
        int height = spriteMap.GetLength(1);

        tiles = new Tile[width * height];
        GameObject newTileMap = Instantiate(tileMap, this.transform);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile newTile = InstantiateTile(x, y, newTileMap);
                newTile.GetComponent<SpriteRenderer>().sprite = spriteMap[x, y];
                tiles[y * height + x] = newTile;
            }
        }
    }

    private Tile InstantiateTile(int x, int y, GameObject tileMap)
    {
        Tile newTile = Instantiate(tile, tileMap.transform);
        newTile.transform.localPosition = new Vector3(x, y, 0);
        return newTile;
    }
}
