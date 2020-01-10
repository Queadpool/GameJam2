using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMoveState : IBaseState
{
    private ZombieStateController _zombieController = null;
    private Animator _animController = null;
    private NavMeshAgent _nav = null;

    public ZombieMoveState(ZombieStateController controller)
    {
        _zombieController = controller;
        _animController = controller.AnimController;
        _nav = controller.Nav;
    }

    public void Enter()
    {
        //_nav.SetDestination(_zombieController.transform.forward);
    }

    public void Update()
    {
        _zombieController.transform.position += _zombieController.transform.forward * Time.deltaTime;
    }

    public void Exit()
    {

    }
}
