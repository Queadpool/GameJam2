using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Transform _cam;
    [SerializeField] World _world = null;

    [SerializeField] private Transform _highlightBlock = null;
    [SerializeField] private Transform _placeBlock = null;
    [SerializeField] private Text _selectedBlockText = null;
    [SerializeField] int _selectedBlockIndex = 0;


    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _sprintSpeed = 6f;
    [SerializeField] private float _jumpForce = 6f;

    [SerializeField] private float _mouseVelocity = 2f;

    [SerializeField] private float _playerWidth = 0.1f;
    [SerializeField] private float _boundsTolerance = 0.1f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isSprinting = false;

    [SerializeField] private float _checkIncrement = 0.025f;
    [SerializeField] private float _reach = 8f;

    private int _stoneInventory = 0;
    private int _woodInventory = 0;
    private int _sommbieInventory = 0;

    private Block[,,] _lastPos = null;

    private float _gravity = -9.8f;

    private float _horizontal = 0;
    private float _vertical = 0;
    private float _mouseHorizontal = 0;
    private float _mouseVertical = 0;
    private Vector3 _velocity = new Vector3(0,0,0);

    private float _verticalMomentum = 0;
    private bool _jumpRequest = false;



    private void Start()
    {
        _cam = gameObject.GetComponentInChildren<Camera>().transform;
        _cam.rotation = this.transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _selectedBlockText.text = _world.blockTypes[_selectedBlockIndex] + " block selected";
    }

    private void FixedUpdate()
    {
        CalculateVelocity();

        if (_jumpRequest)
        {
            Jump();
        }

        transform.Rotate(Vector3.up * _mouseHorizontal);
        _cam.Rotate(Vector3.right * -_mouseVertical);
        transform.Translate(_velocity, Space.World);
    }

    private void Update()
    {
        GetPlayerInputs();
    }

    private void GetPlayerInputs()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _mouseHorizontal = Input.GetAxis("Mouse X");
        _mouseVertical = Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Sprint"))
        {
            _isSprinting = true;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            _isSprinting = false;
        }

        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _jumpRequest = true;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            if (scroll > 0)
            {
                _selectedBlockIndex++;
            }
            if (scroll < 0)
            {
                _selectedBlockIndex--;
            }

            if (_selectedBlockIndex > (_world.blockTypes.Length - 1))
            {
                _selectedBlockIndex = 0;
            }
            if (_selectedBlockIndex < 0)
            {
                _selectedBlockIndex = (_world.blockTypes.Length - 1);
            }

            _selectedBlockText.text = _world.blockTypes[_selectedBlockIndex] + " block selected";

        }

        PlaceCursorBlocks();

        if (_highlightBlock.gameObject.activeSelf)
        {
            //Destroy block
            if (Input.GetMouseButtonDown(1))
            {
                List<string> updates = new List<string>();

                Block[,,] chunks;

                int xChunck = Mathf.FloorToInt(_highlightBlock.transform.position.x);
                int yChunck = Mathf.FloorToInt(_highlightBlock.transform.position.y);
                int zChunck = Mathf.FloorToInt(_highlightBlock.transform.position.z);

                string nName = null;

                if (Utils.GetChunk(xChunck, yChunck, zChunck) != null)
                {
                    nName = GetChunk(xChunck, yChunck, zChunck);
                    updates.Add(nName);

                    if (Utils.GetPositionInChunk(xChunck) == 0)
                        updates.Add(Utils.GetChunk(xChunck - World._chunkSize, yChunck, zChunck));
                    if (Utils.GetPositionInChunk(xChunck) == World._chunkSize - 1)
                        updates.Add(Utils.GetChunk(xChunck + World._chunkSize, yChunck, zChunck));

                    if (Utils.GetPositionInChunk(yChunck) == 0)
                        updates.Add(Utils.GetChunk(xChunck, yChunck - World._chunkSize, zChunck));
                    if (Utils.GetPositionInChunk(yChunck) == World._chunkSize - 1)
                        updates.Add(Utils.GetChunk(xChunck, yChunck + World._chunkSize, zChunck));

                    if (Utils.GetPositionInChunk(zChunck) == 0)
                        updates.Add(Utils.GetChunk(xChunck, yChunck, zChunck - World._chunkSize));
                    if (Utils.GetPositionInChunk(zChunck) == World._chunkSize - 1)
                        updates.Add(Utils.GetChunk(xChunck, yChunck, zChunck + World._chunkSize));



                    foreach (string cName in updates)
                    {
                        Chunk nChunk;

                        if (World._chunks.TryGetValue(cName, out nChunk))
                        {
                            chunks = nChunk._chunkData;
                            //if (chunks[Utils.GetPositionInChunk(xCheck), Utils.GetPositionInChunk(yCheck), Utils.GetPositionInChunk(zCheck)].GetBlocktype() == Block.BlockType.WOOD)
                            DestroyImmediate(nChunk._Chunk.GetComponent<MeshFilter>());
                            DestroyImmediate(nChunk._Chunk.GetComponent<MeshRenderer>());
                            DestroyImmediate(nChunk._Chunk.GetComponent<Collider>());

                            if (chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].GetBlocktype() == Block.BlockType.WOOD) 
                            {
                                _woodInventory += 1;
                            }
                            else if (chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].GetBlocktype() == Block.BlockType.STONE)
                            {
                                _stoneInventory += 1;
                            }
                            chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].SetType(Block.BlockType.AIR);
                            nChunk.DrawChunk();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }

            if (_placeBlock.gameObject.activeSelf)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Block[,,] chunks;

                    int xChunck = Mathf.FloorToInt(_placeBlock.transform.position.x);
                    int yChunck = Mathf.FloorToInt(_placeBlock.transform.position.y);
                    int zChunck = Mathf.FloorToInt(_placeBlock.transform.position.z);

                    string nName = null;

                    if (Utils.GetChunk(xChunck, yChunck, zChunck) != null)
                    {
                        nName = GetChunk(xChunck, yChunck, zChunck);

                        Chunk nChunk;

                        if (World._chunks.TryGetValue(nName, out nChunk))
                        {
                            chunks = nChunk._chunkData;
                            //if (chunks[Utils.GetPositionInChunk(xCheck), Utils.GetPositionInChunk(yCheck), Utils.GetPositionInChunk(zCheck)].GetBlocktype() == Block.BlockType.WOOD)
                            DestroyImmediate(nChunk._Chunk.GetComponent<MeshFilter>());
                            DestroyImmediate(nChunk._Chunk.GetComponent<MeshRenderer>());
                            DestroyImmediate(nChunk._Chunk.GetComponent<Collider>());

                            if (_world.blockTypes[_selectedBlockIndex] == "WOOD")
                            {
                                chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].SetType(Block.BlockType.WOOD);
                            }
                            else if (_world.blockTypes[_selectedBlockIndex] == "STONE")
                            {
                                chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].SetType(Block.BlockType.STONE);
                            }
                            else if (_world.blockTypes[_selectedBlockIndex] == "GRASS")
                            {
                                chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].SetType(Block.BlockType.GRASS);
                            }
                            else if (_world.blockTypes[_selectedBlockIndex] == "DIRT")
                            {
                                chunks[Utils.GetPositionInChunk(xChunck), Utils.GetPositionInChunk(yChunck), Utils.GetPositionInChunk(zChunck)].SetType(Block.BlockType.DIRT);
                            }
                            nChunk.DrawChunk();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }
    }



    private void PlaceCursorBlocks()
    {
        float step = _checkIncrement;
        Vector3 lastPos = new Vector3();

        while (step < _reach)
        {
            Vector3 blockPos = _cam.position + (_cam.forward * step);


            if (CheckVoxel(blockPos.x, blockPos.y, blockPos.z))
            {
                _highlightBlock.position = new Vector3(Mathf.FloorToInt(blockPos.x), Mathf.FloorToInt(blockPos.y), Mathf.FloorToInt(blockPos.z));

                _placeBlock.position = lastPos;

                _highlightBlock.gameObject.SetActive(true);
                _placeBlock.gameObject.SetActive(true);
                return;
            }

            lastPos = new Vector3(Mathf.FloorToInt(blockPos.x), Mathf.FloorToInt(blockPos.y), Mathf.FloorToInt(blockPos.z));

            step += _checkIncrement;
        }
        _highlightBlock.gameObject.SetActive(false);
        _placeBlock.gameObject.SetActive(false);
    }
        private void CalculateVelocity()
    {
        // Affect vertical momentum with gravity
        if (_verticalMomentum > _gravity)
        {
            _verticalMomentum += Time.fixedDeltaTime * _gravity;
        }

        // If we're sprinting, use sprint multiplier
        if (_isSprinting)
        {
            _velocity = ((transform.forward * _vertical) + (transform.right * _horizontal)) * Time.fixedDeltaTime * _sprintSpeed;
        }
        else
        {
            _velocity = ((transform.forward * _vertical) + (transform.right * _horizontal)) * Time.fixedDeltaTime * _walkSpeed;
        }

        // Apply vertical momentum
        _velocity += Vector3.up * _verticalMomentum * Time.fixedDeltaTime;

        if ((_velocity.z > 0 && front) || (_velocity.z < 0 && back))
        {
            _velocity.z = 0;
        }
        if ((_velocity.x > 0 && right) || (_velocity.x < 0 && left))
        {
            _velocity.x = 0;
        }

        if (_velocity.y < 0)
        {
            _velocity.y = CheckDownSpeed(_velocity.y);
        }
        else if (_velocity.y > 0)
        {
            _velocity.y = CheckUpSpeed(_velocity.y);
        }
    }

    private float CheckDownSpeed (float downSpeed)
    {
        if (CheckVoxel(transform.position.x, transform.position.y + downSpeed, transform.position.z))
        {
            _isGrounded = true;
            return 0;
        }
        else
        {
            _isGrounded = false;
            return downSpeed;
        }
    }

    private float CheckUpSpeed(float upSpeed)
    {
        if  (CheckVoxel(transform.position.x , transform.position.y + 2f + upSpeed, transform.position.z))
        {
            return 0;
        }
        else
        {
            return upSpeed;
        }
    }

    private void Jump()
    {
        _verticalMomentum = _jumpForce;
        _isGrounded = false;
        _jumpRequest = false;
    }

    private bool front
    {
        get
        {
            if  (
                CheckVoxel(transform.position.x, transform.position.y, transform.position.z) ||
                CheckVoxel(transform.position.x, transform.position.y +1f, transform.position.z) 
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool back
    {
        get
        {
            if (
                CheckVoxel(transform.position.x, transform.position.y, transform.position.z) ||
                CheckVoxel(transform.position.x, transform.position.y + 1f, transform.position.z)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool right
    {
        get
        {
            if (
                CheckVoxel(transform.position.x, transform.position.y, transform.position.z) ||
                CheckVoxel(transform.position.x, transform.position.y + 1f, transform.position.z)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private bool left
    {
        get
        {
            if (
                CheckVoxel(transform.position.x, transform.position.y, transform.position.z) ||
                CheckVoxel(transform.position.x, transform.position.y + 1f, transform.position.z)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool CheckVoxel(float x, float y, float z)
    {
        Block[,,] chunks;

        int xCheck = Mathf.FloorToInt(x);
        int yCheck = Mathf.FloorToInt(y);
        int zCheck = Mathf.FloorToInt(z);

        string nName = null;
        if (Utils.GetChunk(xCheck, yCheck, zCheck) != null)
        {
            nName = GetChunk(xCheck, yCheck, zCheck);
        }

        Chunk nChunk;

        if (World._chunks.TryGetValue(nName, out nChunk))
        {
            chunks = nChunk._chunkData;
        }
        else
        {
            return false;
        }

        int xPos = Utils.GetPositionInChunk(xCheck);
        int yPos = Utils.GetPositionInChunk(yCheck);
        int zPos = Utils.GetPositionInChunk(zCheck);


        try
        {
            return chunks[xPos, yPos, zPos].IsSolid;
        }
        catch (System.IndexOutOfRangeException) { }

        return false;
    }

    private string GetChunk(int x, int y, int z)
    {
        int xChunk = Mathf.FloorToInt(x / World._chunkSize) * 16;
        int yChunk = Mathf.FloorToInt(y / World._chunkSize) * 16;
        int zChunk = Mathf.FloorToInt(z / World._chunkSize) * 16;

        Vector3 chunkPos = new Vector3(xChunk, yChunk, zChunk);


        return World.BuildChunkName(chunkPos);
    }

}
