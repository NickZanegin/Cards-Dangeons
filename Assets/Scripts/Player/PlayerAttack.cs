using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerWeapon _playerWeapon;
    [SerializeField] private PlayerDeath _death;
    [SerializeField] private PlayerHp _playerUI;
    [SerializeField] private DropWeapon _brocken;

    private Player _player;
    private Enemy _enemy;
    
    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
    }

    public void Damage(Card Enemy)
    {
        float _strength = _playerWeapon.WeaponDamage();
        _enemy = Enemy.gameObject.GetComponent<Enemy>();
        float _playerDamage = _strength;
        float _enemyDamage = _enemy._enemyHP;
        _strength -= _enemy._enemyHP;
        _enemy._enemyHP -= _playerDamage;
        
        if(_enemy._enemyHP <= 0)
        {
            _move.Move(_enemy);
        }
        
        if(_player.gameObject.TryGetComponent<Weapon>(out Weapon component))
        {
            component.Damage = _strength;
            if (component.Damage <= 0) 
            {
                Destroy(component);
                _brocken.BrokenWeapon(_player);
            }
        }
        else
        {
            _player.gameObject.GetComponent<Player>()._playerHP -= _enemyDamage;
            if (_player.gameObject.GetComponent<Player>()._playerHP <= 0)
            {
                _death.Lose();
            }
        }
        _playerUI.AppdatePlyerUi(_player);
    }
}
