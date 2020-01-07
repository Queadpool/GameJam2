using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QQ/Database")]
public class Database : ScriptableObject
{
    [SerializeField] private GameObject _zombie0 = null;
    [SerializeField] private GameObject _zombie1 = null;

    public GameObject Zombie0 { get { return _zombie0; } }
    public GameObject Enemy1 { get { return _zombie1; } }
}