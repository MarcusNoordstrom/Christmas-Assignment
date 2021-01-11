using System.IO;
using UnityEditor;
using UnityEngine;
using UserInput;

namespace TileSystem {
    public class Tile : MonoBehaviour {
        public GameObject CreateTile(Color color, string tileName)
        {
            if (File.Exists($"Assets/Resources/Tiles/{tileName}.prefab"))
                return AssetDatabase.LoadAssetAtPath($"Assets/Resources/Tiles/{tileName}.prefab", typeof(GameObject)) as GameObject;
        
            GameObject tempTile = new GameObject();
            tempTile.name = tileName;
            tempTile.AddComponent<SpriteRenderer>().color = color;
            tempTile.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/BaseTileSprite.png", typeof(Sprite));
            tempTile.AddComponent<BoxCollider2D>();
            tempTile.AddComponent<Paint>();
        
            PrefabUtility.SaveAsPrefabAsset(tempTile, $"Assets/Resources/Tiles/{tileName}.prefab");
            Destroy(tempTile);
            return AssetDatabase.LoadAssetAtPath($"Assets/Resources/Tiles/{tileName}.prefab", typeof(GameObject)) as GameObject;
        }
    }
}