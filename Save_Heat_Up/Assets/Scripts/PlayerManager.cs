using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerMovement _player = null;

    public PlayerMovement Player { get { return _player; } }
}
