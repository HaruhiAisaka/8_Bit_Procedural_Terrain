using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Tile tile;

    public void SetTile(Tile tile, int x, int y)
    {
        this.tile = tile;
        this.transform.localPosition = new Vector3(x, y, 0);
        spriteRenderer.sprite = tile.sprite;
    }
}