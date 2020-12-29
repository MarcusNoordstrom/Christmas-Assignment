using Unity.Mathematics;
using UnityEngine;

public class Map : MonoBehaviour {
    public GameObject[,] Data;
    [SerializeField][HideInInspector] int mapWidth, mapHeight;
    
    GameObject tileHolder;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize() {
        Data = new GameObject[mapWidth,mapHeight];
    }

    public void ResetMap()
    {
        foreach (var go in Data) {
            Destroy(go.gameObject);
            Destroy(tileHolder);
        }
        GenerateEmptyMap();
    }
        
    public void GenerateEmptyMap() {
        tileHolder = new GameObject("Tile Holder");
        for (var x = 0; x < mapWidth; x++) {
            for (var y = 0; y < mapHeight; y++) {
                var tile = Instantiate(Palette.TilePalette[0], new Vector3(x + 0.5f, y + 0.5f), quaternion.identity, tileHolder.transform);
                Data[x, y] = tile;
            }
        }
    }
}