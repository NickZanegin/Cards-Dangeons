using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCastsCheck : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private EnemySpawn _enemySpawn;
    [SerializeField] private DropWeapon _drope;
    [SerializeField] private Card[] _availableCards;

    [SerializeField] private int _waitTime;
    private Vector3 _player;
    private Vector3[] RaycastVector;
    private bool _moveble = true;
    private bool _finishRoom = false;

    public delegate void Attack(Card Enemy);
    public event Attack playerAttack;

    public delegate void Move(Card MoveCard);
    public event Move playerMove;

    public delegate void Drop(Weapon weapon, Player player);
    public event Drop playerDrop;

    public delegate void HP(Card hp);
    public event HP hp;

    public delegate void ChestOpen(Vector3 chestPosition, Card chest);
    public event ChestOpen chestOpen;

    public delegate void NextRoom();
    public event NextRoom roomchenge;

    public delegate void HpUi(Player player);
    public event HpUi hpUi;

    private void Start()
    {
        PlayerPosition();
        RaycastVector = new Vector3[] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
        _enemySpawn.finish += PlayerWin;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            PlayerAction(hit.collider.gameObject.GetComponent<Card>());
        }
    }

    private void PlayerAction(Card RayCard)
    {
        if (MoveCheck(NeighboringСards(_player), RayCard) && RayCard != null)
        {
            if (RayCard.gameObject.GetComponent<Enemy>() && _moveble)
            {
                playerAttack?.Invoke(RayCard);
                _moveble = false;
                StartCoroutine(WaitAttack());
            }
            if (RayCard.gameObject.GetComponent<Weapon>())
            {
                Player player = _board.GetComponentInChildren<Player>();
                var weapon = RayCard.gameObject.GetComponent<Weapon>();
                playerDrop?.Invoke(weapon,player);
            }
            if (RayCard.gameObject.GetComponent<Healing>())
            {
                hp?.Invoke(RayCard);
                hpUi?.Invoke(_board.GetComponentInChildren<Player>());
            }
            if (RayCard.gameObject.GetComponent<Chest>())
            {
                Vector3 chestPoint = RayCard.transform.position;
                chestOpen?.Invoke(chestPoint, RayCard);
                _moveble = false;
                StartCoroutine(WaitAttack());
            }
            if (RayCard.gameObject.GetComponent<EmptyCard>() && _moveble)
            {
                playerMove?.Invoke(RayCard);
            }
        }
        if (_finishRoom && RayCard != null)
        {
            _moveble = false;
            if(RayCard.gameObject.GetComponent<Door>())
            {
                roomchenge?.Invoke();
                _finishRoom = false;
                _moveble = true;
            }
            
        }
    }
    private bool MoveCheck(Card[] _availeble, Card RayCard)
    {
        for (int i = 0; i < _availeble.Length; i++)
        {
            if (_availeble[i] == RayCard)
            {
                return true;
            }
        }
            return false;
    }

    public Card[] NeighboringСards(Vector3 RayPoint)
    {
        for (int i = 0; i < RaycastVector.Length; i++)
        {
            Ray ray = new Ray(RayPoint, RaycastVector[i]);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<Card>())
                {
                    _availableCards[i] = hit.collider.gameObject.GetComponent<Card>();
                }
            }
        }
        return _availableCards;
    }

    public void PlayerPosition()
    {
        _player = _board.GetComponentInChildren<Player>().transform.position;
    }

    private void PlayerWin()
    {
        _finishRoom = true;
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(_waitTime);
        _moveble = true;
    }
}
