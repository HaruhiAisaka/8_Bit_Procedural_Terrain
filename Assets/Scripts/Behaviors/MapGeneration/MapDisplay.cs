using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public GameObject tileMap;
    public Tile tile;
    private Tile[] tiles;

    public void DisplayNoiseOnPlane(float[,] noiseMap)
    {
        int width = noiseMap.GetLength (0);
		int height = noiseMap.GetLength (1);

		Texture2D texture = new Texture2D (width, height);
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) 
        {
			for (int x = 0; x < width; x++) 
            {
				colorMap[y * width + x] = 
                    Color.Lerp(Color.black, Color.white, noiseMap [x, y]);
			}
		}

		texture.SetPixels(colorMap);
		texture.Apply();

		textureRenderer.sharedMaterial.mainTexture = texture;
		textureRenderer.transform.localScale = new Vector3 (width, 1, height);
    }

    public void DisplayTileMap(Sprite[,] spriteMap)
    {
        int width = spriteMap.GetLength (0);
		int height = spriteMap.GetLength (1);

        GameObject newTileMap = Instantiate(tileMap);

        for (int y = 0; y < height; y++) 
        {
			for (int x = 0; x < width; x++) 
            {
                Instantiate(tile, newTileMap.transform);
                tile.transform.localPosition = new Vector3(x, y, 0);
                tile.GetComponent<SpriteRenderer>().sprite = spriteMap[x, y];
			}
		}
    }
}
