using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : ScriptableObject
{
    public Tile[] tiles;

    public Tile MapHeightToTile(float height)
    {
        Tile result = tiles[0];
        foreach (Tile tile in tiles)
        {
            if (height > tile.minHeight)
                result = tile;
        }
        return result;
    }
}

[System.Serializable]
public class Tile
{
    public static float tileSize = 1;

    public Sprite sprite;

    [Range(0,1)]
    public float minHeight;

    public bool accessable;
}