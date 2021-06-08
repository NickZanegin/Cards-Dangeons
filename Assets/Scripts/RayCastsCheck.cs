using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RayCastsCheck : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private DropWeapon _drope;
    [SerializeField] private Card[] _availableCards;

    [SerializeField] private int _waitTime;
    private Vector3 _player;
    private Vector3[] RaycastVector;
    private bool _attackeble = true;

    public delegate void Attack(Card Enemy);
    public event Attack playerAttack;

    public delegate void Move(Card MoveCard);
    public event Move playerMove;

    public delegate void Drop(Weapon weapon, Player player);
    public event Drop playerDrop;

    private void Start()
    {
        PlayerPosition();
        RaycastVector = new Vector3[] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };
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
            if (RayCard.gameObject.GetComponent<Enemy>() && _attackeble)
            {
                playerAttack?.Invoke(RayCard);
                _attackeble = false;
                StartCoroutine(WaitAttack());
            }
            if (RayCard.gameObject.GetComponent<EmptyCard>())
            {
                playerMove?.Invoke(RayCard);
            }
            if (RayCard.gameObject.GetComponent<Weapon>())
            {
                Player player = _board.GetComponentInChildren<Player>();
                var weapon = RayCard.gameObject.GetComponent<Weapon>();
                playerDrop?.Invoke(weapon,player);
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

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(_waitTime);
        _attackeble = true;
    }
}
