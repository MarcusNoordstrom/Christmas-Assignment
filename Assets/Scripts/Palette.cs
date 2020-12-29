using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Palette : MonoBehaviour {
    Tile _tile;
    public static List<GameObject> TilePalette;
    
    private void Awake()
    {
        TilePalette = new List<GameObject>();
        _tile = (Tile)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Tile/BaseTile.prefab", typeof(Tile));
        TilePalette.Add(_tile.CreateTile(Color.green, "Grass"));
    }

    public void AddToPalette(Color color, string tileName)
    {
        TilePalette.Add(_tile.CreateTile(color, tileName));
    }

    // public void ChangeTileInPalette(Color newColor, string oldTileName, string newTileName) {
    //     foreach (var tile in TilePalette) {
    //         if (tile.name == oldTileName) {
    //             tile.name = newTileName;
    //             tile.GetComponent<SpriteRenderer>().color = newColor;
    //         }
    //     }   
    // }
}