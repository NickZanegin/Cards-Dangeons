using UnityEngine;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private PlayerAttack _attack;

    private TextMeshProUGUI _hp;

    private void Start()
    {
        _attack.enemyUi += AppdateEnemyUi;
    }
    public void AppdateEnemyUi(Enemy enemy)
    {
        HP _text = enemy.GetComponentInChildren<HP>();
        _hp = _text.gameObject.GetComponent<TextMeshProUGUI>();
        float UiHp = enemy._enemyHP;
        _hp.text = UiHp.ToString();
    }
}
