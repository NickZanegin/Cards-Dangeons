using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Board _board;

    private TextMeshProUGUI _hp;

    public void AppdatePlyerUi(Player _player)
    {
        HP _text = _player.GetComponentInChildren<HP>();
        _hp = _text.gameObject.GetComponent<TextMeshProUGUI>();
        float UiHp = _player._playerHP;
        _hp.text = UiHp.ToString();
    }
}
