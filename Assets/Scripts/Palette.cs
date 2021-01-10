using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Palette : MonoBehaviour {
    Tile _tile => GetComponent<Tile>();
    public static List<GameObject> TilePalette;

    void Awake()
    {
        TilePalette = new List<GameObject>();
        RefreshPalette();
    }

    public void RefreshPalette() {
        TilePalette.Clear();
        var tiles = Resources.LoadAll("Tiles").Cast<GameObject>().ToArray();

        if (tiles.Length <= 0) {
            AddToPalette(Color.green, "Grass");
            AddToPalette(Color.blue, "Water");
        }

        foreach (var tile in tiles) {
            AddToPalette(tile.GetComponent<SpriteRenderer>().color, tile.name);
        }
        AssetDatabase.Refresh();
    }

     public void AddToPalette(Color color, string tileName)
     { 
         TilePalette.Add(_tile.CreateTile(color, tileName));
     }

     public void RemoveFromPalette(string tileName)
     {
         var count = TilePalette.Count;
         int indexToRemoveAt = 100;
         for (int i = 0; i < count; i++) {
             if (TilePalette[i].name == tileName) {
                 indexToRemoveAt = i;
             }
         }
         TilePalette.RemoveAt(indexToRemoveAt);
         
         string tileToRemove = HoverOver.SelectedTile.name.Remove(HoverOver.SelectedTile.name.Length - 7, 7);
        
         if (File.Exists($"Assets/Resources/Tiles/{tileToRemove}.prefab")) {
             File.Delete($"Assets/Resources/Tiles/{tileToRemove}.prefab");
             File.Delete($"Assets/Resources/Tiles/{tileToRemove}.prefab.meta");
         }
         else {
             throw new Exception($"ERROR! Tile: {tileToRemove} prefab couldn't be deleted. Because it doesn't exist.");
         }
         
         
         //TODO: This throws: "Collection was modified, cannot continue enumeration"
         // foreach (var tile in TilePalette) {
         //     if (tile.name == tileName) {
         //         TilePalette.Remove(tile);
         //     }
         // }
     }

     //TODO: FIX SO WE CAN CHANGE TILE FROM PALETTE INSTEAD OF UI!
     public void ChangeTileInPalette(GameObject tileToChange, Color newColor, string newName)
     {
         GameObject tilePrefab = null;
         foreach (var tile in TilePalette) {
             if (tile.name == tileToChange.name) {
                 if (File.Exists($"Assets/Resources/Tiles/{tile.name}.prefab"))
                     tileToChange = AssetDatabase.LoadAssetAtPath($"Assets/Resources/Tiles/{tile.name}.prefab", typeof(GameObject)) as GameObject;
             }
         }
         tileToChange.GetComponent<SpriteRenderer>().color = newColor;
         RefreshPalette();
     }
}