using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private RayCastsCheck _ray;

    private TextMeshProUGUI _hp;

    private void Start()
    {
        _attack.playerUi += AppdatePlyerUi;
        _ray.hpUi += AppdatePlyerUi;
    }
    public void AppdatePlyerUi(Player _player)
    {
        HP _text = _player.GetComponentInChildren<HP>();
        _hp = _text.gameObject.GetComponent<TextMeshProUGUI>();
        float UiHp = _player._playerHP;
        _hp.text = UiHp.ToString();
    }
}
