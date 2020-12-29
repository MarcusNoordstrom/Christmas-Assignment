using UnityEngine;

public class Tile : MonoBehaviour{
    public GameObject CreateTile(Color color, string tileName)
    {
        var tile = this.gameObject;
        tile.name = tileName;
        tile.GetComponent<SpriteRenderer>().color = color;
        return tile;
    }
}