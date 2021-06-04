using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Board _board;

    private Canvas _image;
    private TextMeshPro _hp;

    public void AppdatePlyerUi(Player _player)
    {
        HP _text = _player.GetComponentInChildren<HP>();
        _hp = _text.gameObject.GetComponent<TextMeshPro>();
        _image = _player.GetComponentInChildren<Canvas>();
        float UiHp = _player._playerHP;
        _hp.text = UiHp.ToString();
        if(_player.TryGetComponent<Weapon>(out Weapon component) == false)
        {
            var image = _image.GetComponentInChildren<Weapon>();
            image.gameObject.SetActive(false);
        }
    }
}
