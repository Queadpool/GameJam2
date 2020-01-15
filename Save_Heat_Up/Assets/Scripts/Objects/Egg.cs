using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject _defeat = null;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie" || (other.tag == "HammerZombie") || (other.tag == "Zombig"))
        {
            Destroy(LevelManager.Instance.Player.gameObject.GetComponent<PlayerMovement>());
            _defeat.SetActive(true);
        }
    }
}
