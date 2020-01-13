using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMoveState : IBaseState
{
    private ZombieStateController _zombieController = null;
    private Animator _animController = null;
    private NavMeshAgent _nav = null;
    private int _health = 0;

    public ZombieMoveState(ZombieStateController controller)
    {
        _zombieController = controller;
        _animController = controller.AnimController;
        _nav = controller.Nav;
        _health = controller.Health;
    }

    public void Enter()
    {
        Debug.Log("MOVE");
    }

    public void Update()
    {
        _zombieController.transform.position += _zombieController.transform.forward * Time.deltaTime;

        if(_health == 0)
        {
            _zombieController.ChangeState(ZombieStateController.EZombieState.DEAD);
        }
    }

    public void Exit()
    {

    }
}
