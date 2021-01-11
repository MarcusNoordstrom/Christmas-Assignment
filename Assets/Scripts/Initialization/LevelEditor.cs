using MapLogic;
using UnityEngine;

namespace Initialization {
    [RequireComponent(typeof(Map))]
    public class LevelEditor : MonoBehaviour {
        Map Map => GetComponent<Map>();
    
        void Start() {
            Map.GenerateEmptyMap();
        }
    }
}