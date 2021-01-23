using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInstantiator : MonoBehaviour
{
    public TileDisplay tileDisplay;
    private Dictionary<Vector2,TileDisplay> instantiatedTileDisplays = new Dictionary<Vector2, TileDisplay>();
    

    public void InstantiateAllTiles(Tile[,] tiles, Vector2 mapCenter)
    {
        int width = tiles.GetLength (0);
		int height = tiles.GetLength (1);

        for (int y = 0; y < height; y++) 
        {
			for (int x = 0; x < width; x++) 
            {
                InstantiateTile(tiles[x, y], new Vector2(x, y) - mapCenter);
			}
		}
    }

    public bool ShowTile(Vector2 position)
    {
        bool tileDisplayExists = instantiatedTileDisplays.ContainsKey(position);
        if (tileDisplayExists)
        {
            TileDisplay selectedTile = instantiatedTileDisplays[position];
            selectedTile.gameObject.SetActive(true);
        }
        return tileDisplayExists;
    }

    public void InstantiateTile(Tile tile, Vector2 position)
    {
        TileDisplay newTileDisplay = Instantiate(tileDisplay, this.transform);
        // instantiatedTileDisplays.Add(position, newTileDisplay);
        newTileDisplay.SetTile(tile, position);
    }
}
