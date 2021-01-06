using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Palette : MonoBehaviour {
    Tile _tile => GetComponent<Tile>();
    public static List<GameObject> TilePalette;

    void Awake()
    {
        TilePalette = new List<GameObject>();
        var tiles = Resources.LoadAll("Tiles").Cast<GameObject>().ToArray();

        if (tiles.Length <= 0) {
            AddToPalette(Color.green, "Grass");
            AddToPalette(Color.blue, "Water");
        }

        foreach (var tile in tiles) {
            AddToPalette(tile.GetComponent<SpriteRenderer>().color, tile.name);
        }
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