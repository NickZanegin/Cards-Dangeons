using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    public float _bordSize;
    public List<Card> _cards;

    [SerializeField] private int _playerPosition;
    [SerializeField] private float _padding;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private NextRoom _nextRoom;
    [SerializeField] private Transform _board;
    [SerializeField] private Card _card;
    [SerializeField] private Card _player;

    [HideInInspector] public bool _havePlayer;

    private List<int> _enemyIndex;
    private float Row;

    public delegate List<int> EnemyIndex(int _playerPosition);
    public event EnemyIndex indexEnemy;

    public delegate Card Enemys(Transform currentBulidPoint, Card newCard);
    public event Enemys spawnEnemy;

    public delegate Card Drop(Transform currentBulidPoint, Card newCard);
    public event Drop dropSpawn;

    private void Awake()
    {
        _havePlayer = false;
        _enemyIndex = indexEnemy.Invoke(_playerPosition);
    }

    private void Start()
    {
        _move.move += ListCheck;
        _nextRoom.newRoom += BordSpawn;
        BordSpawn();
    }

    private void BordSpawn()
    {
        Row = 0;
        for (int y = 0; y < _bordSize; y++)
        {   
            Transform currentPoint = _board;
            Card lastCard = _card;

            for (int x = 0; x < _bordSize; x++)
            {
                Card newCard = SpawnCard(currentPoint, _card);
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
                return spawnEnemy?.Invoke(currentBulidPoint, newCard);
            }
        }
        if( _havePlayer == false && _cards.Count == _playerPosition)
        {
            return Instantiate(_player, GetSpawnPoint(currentBulidPoint, newCard), Quaternion.identity, _board);
        }
        else
        {
            return dropSpawn.Invoke(currentBulidPoint,newCard);
        }  
    }

    public Vector3 GetSpawnPoint(Transform currentSegment, Card newCard)
    {
        return new Vector3(currentSegment.position.x + newCard.transform.localScale.x + _padding, newCard.transform.position.y + Row , _board.position.z);
    }

    private void ListCheck(Vector3 OldPlayerPosition, Vector3 Direction)
    {
        for(int i = 0; i < _cards.Count; i++)
        {
            if(_cards[i] == null)
            {
                _cards.Remove(_cards[i]);
            }
        }
    }
}
