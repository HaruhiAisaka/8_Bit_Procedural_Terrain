using UnityEngine;

public class TileDisplay : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Tile tile;

    public void SetTile(Tile tile, Vector2 position)
    {
        this.tile = tile;
        this.transform.localPosition = (Vector3)position;
        spriteRenderer.sprite = tile.sprite;
    }
}