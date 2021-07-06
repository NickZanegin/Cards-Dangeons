using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private float _scaleSpeed;
    [SerializeField] private int _waitTime;
    [SerializeField] private Board _board;
    [SerializeField] private RayCastsCheck _rayCast;
    [SerializeField] private PlayerAttack _enemyDeath;
    [SerializeField] BoardSpawner _list;
    
    private Player _player;
    private bool _moving = false;
    private Vector3 _movePoint;
    private Vector3 _direction;
    private Vector3 _oldPlayerPosition;

    public delegate void CardsMove (Vector3 OldPlayerPosition, Vector3 Direction);
    public event CardsMove move;
    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
        _rayCast.playerMove += Move;
        _enemyDeath.playerMove += Move;
    }
    public void Move(Card _selectedСard)
    {
        _movePoint = _selectedСard.transform.position;
        Destroy(_selectedСard.gameObject);
        _list._cards.Remove(_selectedСard);
        _oldPlayerPosition = _player.transform.position;
        _moving = true;
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _movePoint, Time.deltaTime * _speed);
        }
        if(_player.transform.position == _movePoint)
        {
            if (_moving)
            {
                _direction = _oldPlayerPosition - _movePoint;
                move?.Invoke(_oldPlayerPosition, _direction);
                _rayCast.PlayerPosition();
            }
            _moving = false;
        }
    }
}
