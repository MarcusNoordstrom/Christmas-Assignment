using TileSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace MapLogic {
    public class Map : MonoBehaviour {
        public GameObject[,] _data;
        public int mapWidth, mapHeight;
        public GameObject _tileHolder;

        void Awake() {
            Initialize();
        }

        void Initialize() {
            _data = new GameObject[mapWidth, mapHeight];
            _tileHolder = new GameObject("Tile Holder");
        }

        public void ResetMap(bool destroyOnly)
        {
            foreach (var go in _data) {
                Destroy(go.gameObject);
            
            }
            Destroy(_tileHolder);
            Initialize();
            if (destroyOnly) return;
            GenerateEmptyMap();
        }

        public void RefreshMap() {
            foreach (var tile in _data) {
                if (tile.name == HoverOver.SelectedTile.name) {
                    tile.GetComponent<SpriteRenderer>().color = HoverOver.SelectedTile.GetComponent<RawImage>().color;
                }
            }
        }

        public void GenerateEmptyMap() {
            for (var x = 0; x < mapWidth; x++) {
                for (var y = 0; y < mapHeight; y++) {
                    var tile = Instantiate(Palette.TilePalette[0], new Vector3(x + 0.5f, y + 0.5f), quaternion.identity, _tileHolder.transform);
                    tile.name = tile.name.Remove(tile.name.Length - 7, 7);
                    _data[x, y] = tile;
                }
            }
        }
    }
}