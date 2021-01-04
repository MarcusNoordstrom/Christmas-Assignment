using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour {
    GameObject[,] _data;
    [SerializeField] int mapWidth, mapHeight;
    GameObject _tileHolder;

     void Awake() {
        Initialize();
    }

     void Initialize() {
        _data = new GameObject[mapWidth,mapHeight];
    }

    public void ResetMap()
    {
        foreach (var go in _data) {
            Destroy(go.gameObject);
            Destroy(_tileHolder);
        }
        GenerateEmptyMap();
    }
    
    public void GenerateEmptyMap() {
        Debug.Log(Palette.TilePalette[0].gameObject.name);
        Debug.Log(Palette.TilePalette[1].gameObject.name);
        
        _tileHolder = new GameObject("Tile Holder");
        for (var x = 0; x < mapWidth; x++) {
            for (var y = 0; y < mapHeight; y++) {
                var tile = Instantiate(Palette.TilePalette[0], new Vector3(x + 0.5f, y + 0.5f), quaternion.identity, _tileHolder.transform);
                _data[x, y] = tile;
            }
        }
    }
}