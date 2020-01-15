using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harrow : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Zombie")
        {
            other.GetComponent<ZombieStateController>().ChangeState(ZombieStateController.EZombieState.DEAD);
        }

        Destroy(gameObject);
    }
}
