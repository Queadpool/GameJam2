using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class ZombieHarrow : MonoBehaviour
{
    [SerializeField] private AudioSource _audio = null;
    [SerializeField] private int _health = 5;
    private Timer _timer = null;
    private float _timerHit = 0f;
    [SerializeField] private float _timerZombieHit = 1f;
    [SerializeField] private float _timerHammerZombieHit = 2f;
    [SerializeField] private float _timerZombigHit = 3f;
    private int _hit = 0;
    [SerializeField] private int _hitZombie = 1;
    [SerializeField] private int _hitHammerZombie = 3;
    [SerializeField] private int _hitZombig = 5;

    private void Start()
    {
        _timer = new Timer();
        _timer.ResetTimer(_timerHit);
    }

    private void Update()
    {
        if (_timer.TimeLeft <= 0f)
        {
            _timer.ResetTimer(_timerHit);
            _health -= _hit;

            if (_hit > 0)
            {
                _audio.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie" || (other.tag == "HammerZombie") || (other.tag == "Zombig"))
        {
            other.GetComponent<ZombieStateController>().ChangeState(ZombieStateController.EZombieState.HIT);
        }

        if (other.tag == "Zombie")
        {
            _timerHit = _timerZombieHit;
            _hit = _hitZombie;
        }

        if (other.tag == "HammerZombie")
        {
            _timerHit = _timerHammerZombieHit;
            _hit = _hitHammerZombie;
        }

        if (other.tag == "Zombig")
        {
            _timerHit = _timerZombigHit;
            _hit = _hitZombig;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zombie" || (other.tag == "HammerZombie") || (other.tag == "Zombig"))
        {
            if (_health <= 0)
            {
                other.GetComponent<ZombieStateController>().ChangeState(ZombieStateController.EZombieState.MOVE);
                Destroy(gameObject);
            }
        }
    }
}
