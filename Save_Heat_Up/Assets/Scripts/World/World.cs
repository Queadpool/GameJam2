using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] Material _textureAtlas = null;
    public static int _columnHeight = 4;
    public static int _chunkSize = 16;
    public static int _worldSize = 8;
    public static Dictionary<string, Chunk> _chunks = null;

    public string[] blockTypes { get { return new string[] { "GRASS", "WOOD", "DIRT", "STONE", "DIAMOND"}; }}

    public static string BuildChunkName(Vector3 v)
    {
        return (int)v.x + "_" + (int)v.y + "_" + (int)v.z;
    }

    IEnumerator BuildChunkColumn()
    {
        for (int i = 0; i < _columnHeight; i++)
        {
            Vector3 chunkPosition = new Vector3(this.transform.position.x,
                                                i * _chunkSize,
                                                this.transform.position.z);
            Chunk c = new Chunk(chunkPosition, _textureAtlas);
            c._Chunk.transform.parent = this.transform;
            _chunks.Add(c._Chunk.name, c);
        }

        foreach(KeyValuePair<string, Chunk> c in _chunks)
        {
            c.Value.DrawChunk();
            yield return null;
        }
    }

    IEnumerator BuildWorld()
    {
        for (int z = 0; z < _worldSize; z++)
        {
            for (int x = 0; x < _worldSize; x++)
            {
                for (int y = 0; y < _columnHeight; y++)
                {
                    Vector3 chunkPosition = new Vector3(x * _chunkSize, y * _chunkSize, z * _chunkSize);
                    Chunk c = new Chunk(chunkPosition, _textureAtlas);
                    c._Chunk.transform.parent = this.transform;
                    _chunks.Add(c._Chunk.name, c);
                }
            }
        }

        foreach(KeyValuePair<string, Chunk> c in _chunks)
        {
            c.Value.DrawChunk();
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _chunks = new Dictionary<string, Chunk>();
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
        StartCoroutine(BuildWorld());
    }
}
