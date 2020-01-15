using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QQ/Database")]
public class Database : ScriptableObject
{
    [SerializeField] private GameObject _zombie = null;
    [SerializeField] private GameObject _hammerZombie = null;
    [SerializeField] private GameObject _zombig = null;
    [SerializeField] private GameObject _harrow = null;
    [SerializeField] private GameObject _spikes = null;
    [SerializeField] private GameObject _zombieHarrow = null;
    [SerializeField] private GameObject _zombieWall = null;
    [SerializeField] private AudioClip _musicInGame = null;
    [SerializeField] private AudioClip _musicWave = null;
    [SerializeField] private AudioClip _musicCall = null;

    public GameObject Zombie { get { return _zombie; } }
    public GameObject Hammer_Zombie { get { return _hammerZombie; } }
    public GameObject Zombig { get { return _zombig; } }
    public GameObject Harrow { get { return _harrow; } }
    public GameObject Spikes { get { return _spikes; } }
    public GameObject ZombieHarrow { get { return _zombieHarrow; } }
    public GameObject ZombieWall { get { return _zombieWall; } }
    public AudioClip MusicInGame { get { return _musicInGame; } }
    public AudioClip MusicWave { get { return _musicWave; } }
    public AudioClip MusicCall { get { return _musicCall; } }
}