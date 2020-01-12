using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BiomeAttributes", menuName = "Biome Attributes")]
public class BiomeAttributes : ScriptableObject
{
    public string _biomeName;

    public int _solidGroundheight = 0;
    public int _terrainHeight = 0;
    public float _terrainScale = 0;

    [Header("Trees")]
    [SerializeField] private float _treeZoneScale = 1.3f;
    [SerializeField] private float _treeZoneThreshold = 0.6f;

    [SerializeField] private Lode[] lodes;


    [System.Serializable]
    public class Lode
    {
        public string nodeName;
        public byte blocID;
        public int minHeight;
        public int maxHeight;
        public float scale;
        public float threshold;
        public float noiseOffset;
    }
}
