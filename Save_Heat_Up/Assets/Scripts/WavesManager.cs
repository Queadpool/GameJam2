using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Transform _spawn0 = null;
    [SerializeField] private Transform _spawn1 = null;
    [SerializeField] private Transform _spawn2 = null;
    [SerializeField] private Transform _spawn3 = null;
    private Timer _timer = null;
    [SerializeField] private float _timerSpawn = 5.0f;
    private Transform _spawn = null;

    // Start is called before the first frame update
    void Start()
    {
        _timer = new Timer();
        _timer.ResetTimer(_timerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer.TimeLeft <= 0)
        {
            _timer.ResetTimer(_timerSpawn);

            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        ChooseSpawn();

        GameObject zombie = DatabaseManager.Instance.Database.Zombie0;
        if (zombie == null)
        {
            Debug.LogError("Missing target Reference");
        }
        else
        {
            GameObject newZombie = Instantiate(zombie);
            newZombie.transform.position = _spawn.position;
        }
    }

    private void ChooseSpawn()
    {
        int spawnValue = Random.Range(0, 4);

        switch (spawnValue)
        {
            case 0:
                {
                    _spawn = _spawn0;
                    break;
                }
            case 1:
                {
                    _spawn = _spawn1; 
                    break;
                }
            case 2:
                {
                    _spawn = _spawn2; 
                    break;
                }
            case 3:
                {
                    _spawn = _spawn3;
                    break;
                }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spawn0.position, 2);
        Gizmos.DrawWireSphere(_spawn1.position, 2);
        Gizmos.DrawWireSphere(_spawn2.position, 2);
        Gizmos.DrawWireSphere(_spawn3.position, 2);
    }
}
