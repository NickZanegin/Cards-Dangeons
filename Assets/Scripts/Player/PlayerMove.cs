using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private RayCastsCheck _rayCast;
    [SerializeField] private PlayerAttack _enemyDeath;
    [SerializeField] BoardSpawner _list;
    
    private Player _player;

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
        Vector3 OldPlayerPosition = _player.transform.position;
        _player.transform.position = _selectedСard.transform.position;
        _rayCast.PlayerPosition();
        _list._cards.Remove(_selectedСard);
        Destroy(_selectedСard.gameObject);
        Vector3 Direction = OldPlayerPosition - _selectedСard.transform.position;
        move?.Invoke(OldPlayerPosition, Direction);
    }  
}
