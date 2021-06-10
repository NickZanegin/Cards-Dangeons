using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Card
{
    [HideInInspector] public float _enemyHP;

    private void Start()
    {
        _enemyHP = 35;
    }
}
