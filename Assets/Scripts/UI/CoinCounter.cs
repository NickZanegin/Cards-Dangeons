using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinTextUi;
    [SerializeField] private PlayerAttack _drop;
    private int _coinValue = 0;

    private void Start()
    {
        _drop.playerMove += EnemyDropCoins;
    }

    private void EnemyDropCoins(Card enemy)
    {
        int _coins = enemy.gameObject.GetComponent<Enemy>()._coins;
        CoinUiappdate(_coins);
    }

    private void CoinUiappdate(int _coinDpor)
    {
        _coinValue += _coinDpor;
        _coinTextUi.text = _coinValue.ToString();
    }
}
