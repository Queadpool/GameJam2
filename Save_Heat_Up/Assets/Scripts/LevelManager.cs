using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PlayerMovement _player = null;
    [SerializeField] private Transform _egg = null;
    [SerializeField] private int _score = 0;

    public PlayerMovement Player { get { return _player; } }
    public Transform Egg { get { return _egg; } }
    public int Score { get { return _score; } }

    public void AddScore(ZombieStateController controller)
    {
        _score++;
        Debug.Log("Score : " + _score);

        Destroy(controller.gameObject);
    }
}
