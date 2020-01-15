using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombie" || (other.tag == "HammerZombie") || (other.tag == "Zombig"))
        {
            other.GetComponent<ZombieStateController>().IsSpikesTouched();
            Destroy(gameObject);
        }
    }
}
