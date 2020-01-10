using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnState : IBaseState
{
    private ZombieStateController _zombieController = null;
    private Animator _animController = null;

    public ZombieSpawnState(ZombieStateController controller)
    {
        _zombieController = controller;
        _animController = controller.AnimController;
    }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {
        _animController.SetTrigger("Spawned");
    }
}
