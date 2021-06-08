using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private Board _board;
    [SerializeField] private PlayerWeapon _playerWeapon;

    private Player _player;
    private Enemy _enemy;

    public delegate void Move(Card MoveCard);
    public event Move playerMove;

    public delegate void Lose();
    public event Lose playerLose;

    public delegate void PlayerUi(Player player);
    public event PlayerUi playerUi;

    public delegate void Brocken(Player player);
    public event Brocken brock;

    public delegate void EnemyUi(Enemy enemy);
    public event EnemyUi enemyUi;

    private void Start()
    {
        _player = _board.GetComponentInChildren<Player>();
        _ray.playerAttack += Damage;
    }

    public void Damage(Card Enemy)
    {
        float _strength = _playerWeapon.WeaponDamage();
        _enemy = Enemy.gameObject.GetComponent<Enemy>();
        float _playerDamage = _strength;
        float _enemyDamage = _enemy._enemyHP;
        _strength -= _enemy._enemyHP;
        _enemy._enemyHP -= _playerDamage;

        if (_enemy._enemyHP <= 0)
        {
            playerMove?.Invoke(_enemy);
        }

        if (_player.gameObject.TryGetComponent<Weapon>(out Weapon component))
        {
            component.Damage = _strength;
            if (component.Damage <= 0)
            {
                Destroy(component);
                brock?.Invoke(_player);
            }
        }
        else
        {
            _player.gameObject.GetComponent<Player>()._playerHP -= _enemyDamage;
            if (_player.gameObject.GetComponent<Player>()._playerHP <= 0)
            {
                playerLose?.Invoke();
            }
        }
        playerUi?.Invoke(_player);
        enemyUi?.Invoke(Enemy.gameObject.GetComponent<Enemy>());
    }
}

