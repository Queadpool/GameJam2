using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitState : IBaseState
{
    private ZombieStateController _zombieController = null;
    private Animator _animController = null;
    private int _health = 0;

    public ZombieHitState(ZombieStateController controller)
    {
        _zombieController = controller;
        _animController = controller.AnimController;
        _health = controller.Health;
    }

    public void Enter()
    {
        Debug.Log("HIT");
        _animController.SetBool("Blocked", true);
    }

    public void Update()
    {
        if (_health <= 0)
        {
            _zombieController.ChangeState(ZombieStateController.EZombieState.DEAD);
        }
    }

    public void Exit()
    {
        _animController.SetBool("Blocked", false);
    }
}
