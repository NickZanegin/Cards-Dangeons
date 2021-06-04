using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Board _board;

    private Player _player;

    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
    }

    public float WeaponDamage()
    {
        if(_player.gameObject.TryGetComponent<Weapon>(out Weapon component))
        {
            return _player.gameObject.GetComponent<Weapon>().Damage;
        }
        else
        {
            return _player.gameObject.GetComponent<Player>()._playerHP;
        }
    }
}
