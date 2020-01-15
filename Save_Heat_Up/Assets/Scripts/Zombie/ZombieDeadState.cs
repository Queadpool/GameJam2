using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : IBaseState
{
    private ZombieStateController _zombieController = null;

    public ZombieDeadState(ZombieStateController controller)
    {
        _zombieController = controller;
    }

    public void Enter()
    {
        Debug.Log("DEAD");
        LevelManager.Instance.AddScore(_zombieController);
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
