using UnityEngine;

public class DropeHP : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private Board _board;
    
    private Player _player;
    private float _maxHp;

    private void Start()
    {
        _maxHp = 50;
        _player = _board.GetComponentInChildren<Player>();
        _ray.hp += Boost;
    }

    private void Boost(Card Card)
    {
        if(_player._playerHP < _maxHp)
        {
            Healing hp = Card.gameObject.GetComponent<Healing>();
            _player._playerHP += hp._healScore;
        }
        if(_player._playerHP >= _maxHp)
        {
            _player._playerHP = 50;
        }
    }
}
