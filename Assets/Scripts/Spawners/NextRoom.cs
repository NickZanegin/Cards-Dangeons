using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private Board _board;
    [SerializeField] private BoardSpawner _spawner;
    private Vector3 _playerPosition;
    private Player _player;

    public delegate void NewRoom();
    public event NewRoom newRoom;

    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
        _ray.roomchenge += DeliteRoom;
        _ray.roomchenge += LoadRoom;
    }

    private void DeliteRoom()
    {
        float boardSize = 15; //кол-во объектов минус игрок.
        for(int i = 0; i < boardSize; i++)
        {
            if (_spawner._cards[0].gameObject.GetComponent<Player>() || _spawner._cards[0].gameObject == null)
            {
                Destroy(_spawner._cards[1].gameObject);
                _spawner._cards.Remove(_spawner._cards[1]);
            }
            else
            {
                Destroy(_spawner._cards[0].gameObject);
                _spawner._cards.Remove(_spawner._cards[0]);
            }
        }
    }

    private void LoadRoom()
    {
        _playerPosition = _player.transform.position;
        _spawner._havePlayer = true;
        newRoom?.Invoke();
        for(int i = 1; i < _spawner._cards.Count; i++)
        {
            if(_spawner._cards[i].transform.position == _playerPosition)
            {
                Destroy(_spawner._cards[i].gameObject);
                _spawner._cards.Remove(_spawner._cards[i]);
            }
        }
    }
}
