using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : MonoBehaviour
{
    [SerializeField] private Card _door;
    [SerializeField] private EnemySpawn _enemy;
    [SerializeField] private RayCastsCheck _ray;

    private void Start()
    {
        _enemy.finish += DoorsSpawn;
        _ray.roomchenge += DoorInvise;
    }

    private void DoorsSpawn()
    {
        _door.gameObject.SetActive(true);
    }

    private void DoorInvise()
    {
        _door.gameObject.SetActive(false);
    }
}
