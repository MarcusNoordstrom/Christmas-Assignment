using System;
using UnityEngine;

[RequireComponent(typeof(Map))]
public class LevelEditor : MonoBehaviour {
    private Map Map => GetComponent<Map>();

    private void Awake()
    {
        
    }

    void Start() {
        Map.GenerateEmptyMap();
    }
}