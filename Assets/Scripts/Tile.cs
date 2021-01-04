using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour {
    Sprite _tileBaseSprite;

    void Awake() {
        _tileBaseSprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/BaseTileSprite.png", typeof(Sprite));
    }

    public GameObject CreateTile(Color color, string tileName)
    {
        GameObject tile = new GameObject {name = tileName};
        tile.AddComponent<SpriteRenderer>().color = color;
        tile.GetComponent<SpriteRenderer>().sprite = _tileBaseSprite;
        return tile;
    }
}