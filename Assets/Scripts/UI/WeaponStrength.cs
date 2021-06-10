using UnityEngine;
using TMPro;

public class WeaponStrength : MonoBehaviour
{
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private DropWeapon _drop;

    private TextMeshProUGUI _strength;

    private void Start()
    {
        _attack.playerUi += AppdateStrengthUi;
        _drop.drope += AppdateStrengthUi;

    }
    private void AppdateStrengthUi(Player _player)
    {
        Strength _text = _player.GetComponentInChildren<Strength>();
        _strength = _text.gameObject.GetComponent<TextMeshProUGUI>();

        if (_player.gameObject.TryGetComponent<Weapon>(out Weapon component))
        {
            float Uistrength = _player.gameObject.GetComponent<Weapon>().Damage;
            if(Uistrength >= 0)
            {
                _strength.text = Uistrength.ToString();
            }
            else
            {
                _strength.text = 0.ToString();
            }
        }

    }
}
