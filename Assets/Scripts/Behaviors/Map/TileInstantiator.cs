using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInstantiator : MonoBehaviour
{
    public TileDisplay tileDisplay;
    public Tile[,] tiles;

    public void InstantiateAllTiles(Tile[,] tiles, Vector2 mapCenter)
    {
        int width = tiles.GetLength (0);
		int height = tiles.GetLength (1);

        for (int y = 0; y < height; y++) 
        {
			for (int x = 0; x < width; x++) 
            {
                InstantiateTile(tiles[x, y], x - (int)mapCenter.x, y - (int)mapCenter.y);
			}
		}
    }

    private void InstantiateTile(Tile tile, int x, int y)
    {
        TileDisplay newTileDisplay = Instantiate(tileDisplay, this.transform);
        newTileDisplay.SetTile(tile, x, y);
    }
}
