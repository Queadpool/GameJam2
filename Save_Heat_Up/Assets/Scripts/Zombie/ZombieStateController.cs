using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using QQ.Utils;

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
    [SerializeField] private int _health = 10;


    public EZombieState CurrentState { get { return _currentState; } }
    public Animator AnimController { get { return _animController; } }
    public NavMeshAgent Nav { get { return _nav; } }
    public int Health { get { return _health; } }

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(EZombieState.DEAD);
        }
    }

    public void ChangeState(EZombieState nextState)
    {
        _states[CurrentState].Exit();
        _states[nextState].Enter();
        _currentState = nextState;
    }
       
    public void IsBunkerReached()
    {
        transform.LookAt(LevelManager.Instance.Egg);
    }

    public void IsSpikesTouched()
    {
        _health -= 5;
    }
}
