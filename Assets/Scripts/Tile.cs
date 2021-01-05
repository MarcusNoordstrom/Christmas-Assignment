using System.IO;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour {
    //MESH FILTER, MESH COLLIDER, MESH RENDERER
    public GameObject CreateTile(Color color, string tileName)
    {
        if (File.Exists($"Assets/Tiles/{tileName}.prefab"))
            return AssetDatabase.LoadAssetAtPath($"Assets/Tiles/{tileName}.prefab", typeof(GameObject)) as GameObject;
        
        GameObject tempTile = new GameObject();
        tempTile.name = tileName;
        tempTile.AddComponent<SpriteRenderer>().color = color;
        tempTile.GetComponent<SpriteRenderer>().sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/BaseTileSprite.png", typeof(Sprite));
            
        PrefabUtility.SaveAsPrefabAsset(tempTile, $"Assets/Tiles/{tileName}.prefab");
        Destroy(tempTile);
        return AssetDatabase.LoadAssetAtPath($"Assets/Tiles/{tileName}.prefab", typeof(GameObject)) as GameObject;
    }
}