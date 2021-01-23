using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Vector2 mapCenter;
    public Renderer textureRenderer;
    
    public TileInstantiator tileInstantiator;

    public void DisplayNoiseOnPlane(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
		int height = noiseMap.GetLength(1);

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

    public void DisplayTileMap(Tile[,] tileMap)
    {
        tileInstantiator.InstantiateAllTiles(tileMap, mapCenter);
    }

    public void SetMapCenter(Tile[,] tileMap)
    {
        int width = tileMap.GetLength(0);
		int height = tileMap.GetLength(1);
        mapCenter = new Vector2((int)width/2, (int)height/2);
    }
}
