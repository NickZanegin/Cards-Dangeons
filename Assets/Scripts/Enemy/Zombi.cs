using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi : Enemy
{
    void Start()
    {
        _enemyHP = 35;
        _coins = Random.Range(5, 30);
    }
}
