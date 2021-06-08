using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{   
    public List<Card> _cards;
    [SerializeField] private float _playerPosition;
    [SerializeField] private float _bordSize, _padding, _enemyNumbers;
    [SerializeField] private Transform _board;
    [SerializeField] private Card _card;
    [SerializeField] private Card _player;
    [SerializeField] private Enemy _enemy;

    private List<int> _enemyIndex;
    private float Row = 0;

    private void Awake()
    {
        _enemyIndex = new List<int>();
        EnemySpawnPoint();
        BordSpawn();
    }

    private void BordSpawn()
    {
        
        for (int y = 0; y < _bordSize; y++)
        {   
            Transform currentPoint = _board;
            Card lastCard = _card;

            for (int x = 0; x < _bordSize; x++)
            {
                Card newCard = SpawnCard(currentPoint,_card);
                _cards.Add(newCard);


                currentPoint = newCard.transform;
                lastCard = newCard;
            }
            Row = lastCard.transform.localPosition.y;
        }
    }

    private Card SpawnCard(Transform currentBulidPoint, Card newCard)
    {
        for(int i = 0; i < _enemyIndex.Count; i++)
        {
            if (_cards.Count == _enemyIndex[i])
            {
                return Instantiate(_enemy, GetSpawnPoint(currentBulidPoint, newCard), Quaternion.identity, _board);
            }
        }
        
        if(_cards.Count == _playerPosition)
        {
            return Instantiate(_player, GetSpawnPoint(currentBulidPoint, newCard), Quaternion.identity, _board);
        }
        else
        {
            return Instantiate(_cards[Random.Range(0, 3)], GetSpawnPoint(currentBulidPoint, newCard), Quaternion.identity, _board);
        }
            
    }

    private Vector3 GetSpawnPoint(Transform currentSegment, Card newCard)
    {
        return new Vector3(currentSegment.position.x + newCard.transform.localScale.x + _padding, newCard.transform.position.y + Row , _board.position.z);
    }

    private void EnemySpawnPoint()
    {
        for(int i = 0; i < _enemyNumbers; i++)
        {
            int index = Random.Range(3, 15);
            if(index != _playerPosition)
            {
                _enemyIndex.Add(index);
            }
            else
            {
                i--;
            }  
        }
    }

    public void AngleSpawn(Vector3 Postition)
    {
        Instantiate(_cards[Random.Range(0, 3)], Postition, Quaternion.identity, _board);
    }


}
