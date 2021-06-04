using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Card
{
    public float _playerHP;
    public float _playerDamage;

    private void Start()
    {
        _playerHP = 20;
    }
}
