using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float _enemyNumbers;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private Board _board;
    [SerializeField] private BoardSpawner _boardSpawner;


    public delegate void RoomFinish();
    public event RoomFinish finish;

    private void Awake()
    {
        _boardSpawner.indexEnemy += EnemySpawnPoint;
        _boardSpawner.spawnEnemy += SpawnEnemys;
    }

    private void Start()
    {
        _attack.playerMove += QuantityEnemies;
    }

    private Card SpawnEnemys( Transform currentBulidPoint, Card newCard)
    {
        return Instantiate(_enemy, _boardSpawner.GetSpawnPoint(currentBulidPoint, newCard), Quaternion.identity, _board.transform);
    }

    private List<int> EnemySpawnPoint(int _playerPosition)
    {
        List<int> _enemyIndex = new List<int>();
        for (int i = 0; i < _enemyNumbers; i++)
        {
            int index = Random.Range(0, 12);
            if (index != _playerPosition)
            {
                _enemyIndex.Add(index);
            }
            else
            {
                i--;
            }
        }
        return _enemyIndex;

    }

    private void QuantityEnemies(Card card)
    {
        _enemyNumbers--;
        if (_enemyNumbers == 0)
        {
            finish?.Invoke();
        }
    }
}
