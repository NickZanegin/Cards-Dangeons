using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private RayCastsCheck _rayCast;
    [SerializeField] private CardsMove _cardsMove;
    [SerializeField] private int _waitTime;
    
    private Player _player;

    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
    }
    public void Move(Card _selectedСard)
    {
        Vector3 OldPlayerPosition = _player.transform.position;
        _player.transform.position = _selectedСard.transform.position;
        _rayCast.PlayerPosition();
        Destroy(_selectedСard.gameObject); // Change this.
        Vector3 Direction = OldPlayerPosition - _selectedСard.transform.position;
        _cardsMove.Move(OldPlayerPosition, Direction);
    }  
}
