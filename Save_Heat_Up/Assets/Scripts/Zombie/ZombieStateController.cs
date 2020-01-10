using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieStateController : MonoBehaviour
{
    public enum EZombieState
    {
        SPAWN,
        MOVE,
        HIT,
        DEAD
    }

    [SerializeField] private EZombieState _currentState = EZombieState.SPAWN;
    [SerializeField] private Animator _animController = null;
    [SerializeField] private NavMeshAgent _nav = null;

    public EZombieState CurrentState { get { return _currentState; } }
    public Animator AnimController { get { return _animController; } }
    public NavMeshAgent Nav { get { return _nav; } }

    Dictionary<EZombieState, IBaseState> _states = null;

    private void Start()
    {
        _states = new Dictionary<EZombieState, IBaseState>();
        _states.Add(EZombieState.SPAWN, new ZombieSpawnState(this));
        _states.Add(EZombieState.MOVE, new ZombieMoveState(this));
        _states.Add(EZombieState.HIT, new ZombieHitState(this));
        _states.Add(EZombieState.DEAD, new ZombieDeadState(this));
        _states[CurrentState].Enter();
    }

    private void Update()
    {
        _states[CurrentState].Update();
    }

    public void ChangeState(EZombieState nextState)
    {
        _states[CurrentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }
}
