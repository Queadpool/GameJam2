using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audio = null;
    [SerializeField] private GameObject _victory = null;
    private AudioClip _music0 = null;
    private AudioClip _music1 = null;
    private AudioClip _music2 = null;
    private Timer _timer = null;
    [SerializeField] private float _timerSpawn = 60.0f;
    [SerializeField] private int _waveCounter = 0;
    private int _score = 0;
    private bool _waveCleared = true;
    private bool _canSpawn = false;

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

        _music0 = DatabaseManager.Instance.Database.MusicInGame;
        _music1 = DatabaseManager.Instance.Database.MusicWave;
        _music2 = DatabaseManager.Instance.Database.MusicCall;
        _audio.clip = _music1;
        _audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Score : " + _score);
        Debug.Log("Current wave : " + _waveCounter);

        _score = LevelManager.Instance.Score;

        if (_canSpawn)
        {
            _waveCleared = false;
            _canSpawn = false;
            SpawnZombie();
            _audio.clip = _music2;
            _audio.Play();
        }

        CheckWaveTimer();
        CheckScore();

        if (!_audio.isPlaying)
        {
            _audio.clip = _music0;
            _audio.Play();
        }
    }
    
    private void CheckScore()
    {
        switch (_score)
        {
            case 2:
                if(_waveCounter == 0)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 6:
                if (_waveCounter == 1)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 12:
                if (_waveCounter == 2)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 22:
                if (_waveCounter == 3)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 33:
                if (_waveCounter == 4)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 44:
                if (_waveCounter == 5)
                {
                    _waveCleared = true;
                    _timer.ResetTimer(_timerSpawn);
                    _waveCounter++;
                    _audio.clip = _music1;
                    _audio.Play();
                }
                break;

            case 56:
                Debug.Log("GG FRATE");
                Destroy(LevelManager.Instance.Player.gameObject.GetComponent<PlayerMovement>());
                _victory.SetActive(true);
                break;

            default:
                break;
        }
    }

    private void CheckWaveTimer()
    {
        if (_waveCleared)
        {
            if (_timer.TimeLeft <= 0)
            {
                _canSpawn = true;
            }
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
                    GameObject newHammerZombie0 = Instantiate(hammerZombie);
                    newHammerZombie0.transform.position = _oSpawn2.position;
                    newHammerZombie0.transform.Rotate(0, 180, 0);
                    break;

                case 3:
                    GameObject newZombie11 = Instantiate(zombie);
                    newZombie11.transform.position = _sSpawn0.position;
                    newZombie11.transform.Rotate(0, 90, 0);
                    GameObject newZombie12 = Instantiate(zombie);
                    newZombie12.transform.position = _sSpawn1.position;
                    newZombie12.transform.Rotate(0, 90, 0);
                    GameObject newZombie13 = Instantiate(zombie);
                    newZombie13.transform.position = _sSpawn3.position;
                    newZombie13.transform.Rotate(0, 90, 0);
                    GameObject newZombie14 = Instantiate(zombie);
                    newZombie14.transform.position = _sSpawn4.position;
                    newZombie14.transform.Rotate(0, 90, 0);
                    GameObject newZombie15 = Instantiate(zombie);
                    newZombie15.transform.position = _eSpawn2.position;
                    GameObject newZombie16 = Instantiate(zombie);
                    newZombie16.transform.position = _eSpawn3.position;
                    GameObject newZombie17 = Instantiate(zombie);
                    newZombie17.transform.position = _eSpawn4.position;
                    GameObject newHammerZombie1 = Instantiate(hammerZombie);
                    newHammerZombie1.transform.position = _eSpawn0.position;
                    GameObject newZombie18 = Instantiate(zombie);
                    newZombie18.transform.position = _oSpawn4.position;
                    newZombie18.transform.Rotate(0, 180, 0);
                    GameObject newHammerZombie2 = Instantiate(hammerZombie);
                    newHammerZombie2.transform.position = _oSpawn1.position;
                    newHammerZombie2.transform.Rotate(0, 180, 0);
                    break;

                case 4:
                    GameObject newZombig0 = Instantiate(zombig);
                    newZombig0.transform.position = _sSpawn2.position;
                    newZombig0.transform.Rotate(0, 90, 0);
                    GameObject newZombie19 = Instantiate(zombie);
                    newZombie19.transform.position = _eSpawn0.position;
                    GameObject newZombie20 = Instantiate(zombie);
                    newZombie20.transform.position = _eSpawn1.position;
                    GameObject newZombie21 = Instantiate(zombie);
                    newZombie21.transform.position = _eSpawn2.position;
                    GameObject newZombie22 = Instantiate(zombie);
                    newZombie22.transform.position = _eSpawn3.position;
                    GameObject newZombie23 = Instantiate(zombie);
                    newZombie23.transform.position = _eSpawn4.position;
                    GameObject newZombie24 = Instantiate(zombie);
                    newZombie24.transform.position = _oSpawn0.position;
                    newZombie24.transform.Rotate(0, 180, 0);
                    GameObject newZombie25 = Instantiate(zombie);
                    newZombie25.transform.position = _oSpawn1.position;
                    newZombie25.transform.Rotate(0, 180, 0);
                    GameObject newZombie26 = Instantiate(zombie);
                    newZombie26.transform.position = _oSpawn2.position;
                    newZombie26.transform.Rotate(0, 180, 0);
                    GameObject newZombie27 = Instantiate(zombie);
                    newZombie27.transform.position = _oSpawn3.position;
                    newZombie27.transform.Rotate(0, 180, 0);
                    GameObject newZombie28 = Instantiate(zombie);
                    newZombie28.transform.position = _oSpawn4.position;
                    newZombie28.transform.Rotate(0, 180, 0);
                    break;

                case 5:
                    GameObject newZombie29 = Instantiate(zombie);
                    newZombie29.transform.position = _sSpawn1.position;
                    newZombie29.transform.Rotate(0, 90, 0);
                    GameObject newZombie30 = Instantiate(zombie);
                    newZombie30.transform.position = _sSpawn3.position;
                    newZombie30.transform.Rotate(0, 90, 0);
                    GameObject newHammerZombie3 = Instantiate(hammerZombie);
                    newHammerZombie3.transform.position = _sSpawn0.position;
                    newHammerZombie3.transform.Rotate(0, 90, 0);
                    GameObject newHammerZombie4 = Instantiate(hammerZombie);
                    newHammerZombie4.transform.position = _sSpawn2.position;
                    newHammerZombie4.transform.Rotate(0, 90, 0);
                    GameObject newHammerZombie5 = Instantiate(hammerZombie);
                    newHammerZombie5.transform.position = _sSpawn4.position;
                    newHammerZombie5.transform.Rotate(0, 90, 0);
                    GameObject newZombie31 = Instantiate(zombie);
                    newZombie31.transform.position = _eSpawn0.position;
                    GameObject newZombie32 = Instantiate(zombie);
                    newZombie32.transform.position = _eSpawn4.position;
                    GameObject newHammerZombie6 = Instantiate(hammerZombie);
                    newHammerZombie6.transform.position = _eSpawn2.position;
                    GameObject newZombie33 = Instantiate(zombie);
                    newZombie33.transform.position = _oSpawn0.position;
                    newZombie33.transform.Rotate(0, 180, 0);
                    GameObject newZombie34 = Instantiate(zombie);
                    newZombie34.transform.position = _oSpawn4.position;
                    newZombie34.transform.Rotate(0, 180, 0);
                    GameObject newHammerZombie7 = Instantiate(hammerZombie);
                    newHammerZombie7.transform.position = _oSpawn2.position;
                    newHammerZombie7.transform.Rotate(0, 180, 0);
                    break;

                case 6:
                    GameObject newZombig1 = Instantiate(zombig);
                    newZombig1.transform.position = _sSpawn0.position;
                    newZombig1.transform.Rotate(0, 90, 0);
                    GameObject newZombig2 = Instantiate(zombig);
                    newZombig2.transform.position = _sSpawn4.position;
                    newZombig2.transform.Rotate(0, 90, 0);
                    GameObject newZombie35 = Instantiate(zombie);
                    newZombie35.transform.position = _eSpawn0.position;
                    GameObject newZombie36 = Instantiate(zombie);
                    newZombie36.transform.position = _eSpawn1.position;
                    GameObject newZombie37 = Instantiate(zombie);
                    newZombie37.transform.position = _eSpawn3.position;
                    GameObject newZombie38 = Instantiate(zombie);
                    newZombie38.transform.position = _eSpawn4.position;
                    GameObject newHammerZombie8 = Instantiate(hammerZombie);
                    newHammerZombie8.transform.position = _eSpawn2.position;
                    GameObject newZombie39 = Instantiate(zombie);
                    newZombie39.transform.position = _oSpawn0.position;
                    newZombie39.transform.Rotate(0, 180, 0);
                    GameObject newZombie40 = Instantiate(zombie);
                    newZombie40.transform.position = _oSpawn1.position;
                    newZombie40.transform.Rotate(0, 180, 0);
                    GameObject newZombie41 = Instantiate(zombie);
                    newZombie41.transform.position = _oSpawn2.position;
                    newZombie41.transform.Rotate(0, 180, 0);
                    GameObject newZombie42 = Instantiate(zombie);
                    newZombie42.transform.position = _oSpawn3.position;
                    newZombie42.transform.Rotate(0, 180, 0);
                    GameObject newHammerZombie9 = Instantiate(hammerZombie);
                    newHammerZombie9.transform.position = _oSpawn4.position;
                    newHammerZombie9.transform.Rotate(0, 180, 0);
                    break;

                default:
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
