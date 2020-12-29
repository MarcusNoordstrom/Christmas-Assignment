using UnityEngine;

[RequireComponent(typeof(Map))]
public class LevelEditor : MonoBehaviour {
    private Map Map => GetComponent<Map>();

    private void Awake()
    {
        Map.GenerateEmptyMap();
    }
}