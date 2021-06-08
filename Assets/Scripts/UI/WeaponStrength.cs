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
    public void AppdateStrengthUi(Player _player)
    {
        Strength _text = _player.GetComponentInChildren<Strength>();
        _strength = _text.gameObject.GetComponent<TextMeshProUGUI>();
        float Uistrength = _player.gameObject.GetComponent<Weapon>().Damage;
        _strength.text = Uistrength.ToString();
    }
}
