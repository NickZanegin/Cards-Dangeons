using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    void Start()
    {
        _enemyHP = 5;
        _coins = Random.Range(1, 10);
    }
}
