using System;
using System.Collections.Generic;
using System.IO;
using MapLogic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace MapSave {
    [Serializable]
    public class SaveSystem : MonoBehaviour {
        Map Map => FindObjectOfType<Map>();
        List<MapData> _data = new List<MapData>();
    
        public void SaveMap() {
            _data = new List<MapData>();
            foreach (var a in Map._data) {
                Debug.Log(a.name);
            }
        
            for (int x = 0; x < Map.mapWidth; x++) {
                for (int y = 0; y < Map.mapHeight; y++) {
                    var tile = Map._data[x, y];
                    _data.Add(new MapData(tile.name, tile.transform.position));
                }
            }
            var saveData = JsonUtility.ToJson(new MapDataList(_data), true);
            File.WriteAllText($"Assets/Maps/Map.json", saveData);
        }

        public void LoadMap() {
            //_map.ResetMap(true);
            MapDataList mapData = JsonUtility.FromJson<MapDataList>(File.ReadAllText("Assets/Maps/Map.json"));

            for (int i = 0; i < Map._tileHolder.transform.childCount; i++) {
                Destroy(Map._tileHolder.transform.GetChild(i).gameObject);
            }
        
            foreach (var tile in mapData.MapDatas) {
                var toSpawn = AssetDatabase.LoadAssetAtPath($"Assets/Resources/Tiles/{tile.TileName}.prefab", typeof(GameObject)) as GameObject;
            
                //Debug.Log(toSpawn.name);
            
                Instantiate(toSpawn, tile.TilePosition, quaternion.identity, Map._tileHolder.transform).name = toSpawn.name;
                var xFloat = toSpawn.transform.position.x - 0.5f;
                var yFloat = toSpawn.transform.position.y - 0.5f;
                var xPos = (int) xFloat;
                var yPos = (int) yFloat;
                RefreshMapData(xPos, yPos, toSpawn);
            }
        }
    
        //TODO: Connect tile from toSpawn has position 0.5, 0.5 -> 1.5, 1.5 -> 2.5, 2.5 , delete ,5 and you get 0, 0, -> 1, 1, etc. Use that to connect to Data[x,y]
        //Still didn't solve the nullreference upon saving a second time? Probably an issue with the name of instantiated objects ending with (Clone).. ?
        //It was not the issue either. The _map._data is the issue here, it's length is only 1 after loading a map. Should be map size with all the tiles in the slots of it.
        void RefreshMapData(int x, int y, GameObject tile) {
            Map._data[x, y] = tile;
        }
    }
}

