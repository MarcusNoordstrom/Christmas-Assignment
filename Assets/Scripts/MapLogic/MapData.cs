using System;
using UnityEngine;

namespace MapLogic {
    [Serializable]
    public class MapData {
        public string TileName;
        public Vector3 TilePosition;

        public MapData(string tileName, Vector3 tilePosition) {
            TileName = tileName;
            TilePosition = tilePosition;
        }
    }
}