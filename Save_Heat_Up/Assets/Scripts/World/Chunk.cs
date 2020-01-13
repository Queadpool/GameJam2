using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    [SerializeField] private Material _cubeMaterial;
    [SerializeField] private GameObject _chunk;
    public GameObject _Chunk { get { return _chunk; } }
    public Block[,,] _chunkData = null;


    private  void BuildChunk()
    {
        _chunkData = new Block[World._chunkSize, World._chunkSize, World._chunkSize];

        //Create blocks
        for (int z = 0; z < World._chunkSize; z++)
        {
            for (int y = 0; y < World._chunkSize; y++)
            {
                for (int x = 0; x < World._chunkSize; x++)
                {
                    Vector3 pos = new Vector3(x, y, z);

                    int worldX = (int)(x + _chunk.transform.position.x);
                    int worldY = (int)(y + _chunk.transform.position.y);
                    int worldZ = (int)(z + _chunk.transform.position.z);

                    if (_chunkData[x, y, z] == null)
                    {
                        if (Utils.fBM3D(worldX, worldY, worldZ, 0.05f, 3) < 0.42f)
                        {
                            _chunkData[x, y, z] = new Block(Block.BlockType.AIR,
                                                pos,
                                                _chunk.gameObject,
                                                this);
                        }                  
                        else if (worldY <= Utils.GenerateStoneHeight(worldX, worldZ))
                        {
                            if (Utils.fBM3D(worldX, worldY, worldZ, 0.03f, 2) < 0.38f && worldY < 70 )
                            {
                                _chunkData[x, y, z] = new Block(Block.BlockType.DIAMOND,
                                                    pos,
                                                    _chunk.gameObject,
                                                    this);
                            }
                            else
                            {
                                _chunkData[x, y, z] = new Block(Block.BlockType.STONE,
                                                    pos,
                                                    _chunk.gameObject,
                                                    this);
                            }
                        }
                        else if (worldY < Utils.GenerateHeight(worldX, worldZ))
                        {
                            _chunkData[x,y,z] = new Block(Block.BlockType.DIRT,
                                                pos,
                                                _chunk.gameObject,
                                                this);
                        }
                        else if (worldY == Utils.GenerateHeight(worldX, worldZ))
                        {
                            _chunkData[x, y, z] = new Block(Block.BlockType.GRASS,
                                                pos,
                                                _chunk.gameObject,
                                                this);
                        }
                        else if (worldY == Utils.GenerateHeight(worldX, worldZ) + 1 && Utils.fBM3D(worldX, worldY, worldZ, 0.03f, 2) < 0.41f)
                        {
                            _chunkData[x, y, z] = new Block(Block.BlockType.WOOD,
                                                pos,
                                                _chunk.gameObject,
                                                this);
                        }
                        else
                        {
                            //if (y > 0)
                            //{

                            //    if (_chunkData[x, y - 1, z].GetBlocktype() == Block.BlockType.WOOD)
                            //    {
                            //        _chunkData[x, y, z] = new Block(Block.BlockType.WOOD,
                            //                            pos,
                            //                            _chunk.gameObject,
                            //                            this);
                            //    }
                            //}

                                _chunkData[x, y, z] = new Block(Block.BlockType.AIR,
                                                pos,
                                                _chunk.gameObject,
                                                this);
                        }
                    }
                    //Debug.Log("x:" + x + " y:" + y + " z:" + z + " block type :" + _chunkData[x, y, z].GetBlocktype());
                }
            }
        }
    }

    public void DrawChunk()
    {
        //Draw blocks
        for (int z = 0; z < World._chunkSize; z++)
        {
            for (int y = 0; y < World._chunkSize; y++)
            {
                for (int x = 0; x < World._chunkSize; x++)
                {
                    _chunkData[x, y, z].Draw();
                }
            }
        }

        CombineQuads();
    }

    private void UpdateBlock(int x, int y, int z)
    {
        _chunkData[x, y, z].Draw();
        
        Vector3 thisBlock = new Vector3(x, y, z);

    }

    public void EditBlock(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);
        _chunkData[x, y, z].SetType(Block.BlockType.AIR);
        UpdateBlock(x, y, z);

    }


    public Chunk (Vector3 position, Material c)
    {
        _chunk = new GameObject(World.BuildChunkName(position));
        _chunk.transform.position = position;
        _cubeMaterial = c;
        BuildChunk();
    }

    private void CombineQuads()
    {
        //1. Combine all children meshes
        MeshFilter[] meshFilters = _chunk.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }

        //2. Create a new mesh on the parent object
        MeshFilter mf = (MeshFilter) _chunk.gameObject.AddComponent(typeof(MeshFilter));
        mf.mesh = new Mesh();

        //3. Add combined meshes on children as the parent's mesh
        mf.mesh.CombineMeshes(combine);

        //4. Create a renderer for the parent
        MeshRenderer renderer = _chunk.gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = _cubeMaterial;

        //5. Delete all uncombined children
        foreach (Transform quad in _chunk.transform)
        {
            GameObject.Destroy(quad.gameObject);
        }
    }
}
