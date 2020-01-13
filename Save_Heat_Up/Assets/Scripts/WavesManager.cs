using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class WavesManager : MonoBehaviour
{
    private Timer _timer = null;
    [SerializeField] private float _timerSpawn = 5.0f;
    [SerializeField] private int _waveCounter = 0;
    private int _score = 0;
    private bool _waveCleared = false;
    private bool _timerOn = false;

    [Header("South")]
    [SerializeField] private Transform _sSpawn0 = null;
    [SerializeField] private Transform _sSpawn1 = null;
    [SerializeField] private Transform _sSpawn2 = null;
    [SerializeField] private Transform _sSpawn3 = null;
    [SerializeField] private Transform _sSpawn4 = null;

    [Header("East")]
    [SerializeField] private Transform _eSpawn0 = null;
    [SerializeField] private Transform _eSpawn1 = null;
    [SerializeField] private Transform _eSpawn2 = null;
    [SerializeField] private Transform _eSpawn3 = null;
    [SerializeField] private Transform _eSpawn4 = null;

    [Header("West")]
    [SerializeField] private Transform _oSpawn0 = null;
    [SerializeField] private Transform _oSpawn1 = null;
    [SerializeField] private Transform _oSpawn2 = null;
    [SerializeField] private Transform _oSpawn3 = null;
    [SerializeField] private Transform _oSpawn4 = null;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer();
        _timer.ResetTimer(_timerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        _score = LevelManager.Instance.Score;

        if (_waveCleared == true)
        {
            SpawnZombie();
            _waveCounter++;
            _waveCleared = false;
            _timerOn = false;
        }

        if ((_score == 3) && (_timerOn == false))
        {
            _timer.ResetTimer(_timerSpawn);
            _timerOn = true;
        }

        if (_timer.TimeLeft <= 0)
        {
            _waveCleared = true;
        }
    }

    private void SpawnZombie()
    {
        GameObject zombie = DatabaseManager.Instance.Database.Zombie;
        GameObject hammerZombie = DatabaseManager.Instance.Database.Hammer_Zombie;
        GameObject zombig = DatabaseManager.Instance.Database.Zombig;
        if (zombie == null)
        {
            Debug.LogError("Missing Zombie Reference");
        }
        else if (hammerZombie == null)
        {
            Debug.LogError("Missing Hammer Zombie Reference");
        }
        else if (zombig == null)
        {
            Debug.LogError("Missing Zombig Reference");
        }
        else
        {
            switch (_waveCounter)
            {
                case 0:
                    GameObject newZombie0 = Instantiate(zombie);
                    newZombie0.transform.position = _sSpawn1.position;
                    newZombie0.transform.Rotate(0, 90, 0);
                    GameObject newZombie1 = Instantiate(zombie);
                    newZombie1.transform.position = _sSpawn3.position;
                    newZombie1.transform.Rotate(0, 90, 0);
                    break;

                case 1:
                    GameObject newZombie2 = Instantiate(zombie);
                    newZombie2.transform.position = _sSpawn0.position;
                    newZombie2.transform.Rotate(0, 90, 0);
                    GameObject newZombie3 = Instantiate(zombie);
                    newZombie3.transform.position = _sSpawn2.position;
                    newZombie3.transform.Rotate(0, 90, 0);
                    GameObject newZombie4 = Instantiate(zombie);
                    newZombie4.transform.position = _sSpawn4.position;
                    newZombie4.transform.Rotate(0, 90, 0);
                    GameObject newZombie5 = Instantiate(zombie);
                    newZombie5.transform.position = _eSpawn2.position;
                    break;

                case 2:
                    GameObject newZombie6 = Instantiate(zombie);
                    newZombie6.transform.position = _sSpawn0.position;
                    newZombie6.transform.Rotate(0, 90, 0);
                    GameObject newZombie7 = Instantiate(zombie);
                    newZombie7.transform.position = _sSpawn2.position;
                    newZombie7.transform.Rotate(0, 90, 0);
                    GameObject newZombie8 = Instantiate(zombie);
                    newZombie8.transform.position = _sSpawn4.position;
                    newZombie8.transform.Rotate(0, 90, 0);
                    GameObject newZombie9 = Instantiate(zombie);
                    newZombie9.transform.position = _eSpawn1.position;
                    GameObject newZombie10 = Instantiate(zombie);
                    newZombie10.transform.position = _eSpawn3.position;
                    GameObject newHammerZombie = Instantiate(hammerZombie);
                    newHammerZombie.transform.position = _oSpawn2.position;
                    newHammerZombie.transform.Rotate(0, 180, 0);
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sSpawn2.position, 2);
        Gizmos.DrawWireSphere(_eSpawn2.position, 2);
        Gizmos.DrawWireSphere(_oSpawn2.position, 2);
    }
}
