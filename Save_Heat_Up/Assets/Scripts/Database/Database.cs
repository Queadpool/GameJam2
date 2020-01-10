using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QQ/Database")]
public class Database : ScriptableObject
{
    [SerializeField] private GameObject _zombie = null;
    [SerializeField] private GameObject _hammerZombie = null;
    [SerializeField] private GameObject _zombig = null;

    public GameObject Zombie { get { return _zombie; } }
    public GameObject Hamemr_Zombie { get { return _hammerZombie; } }
    public GameObject Zombig { get { return _zombig; } }
}