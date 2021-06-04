using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastsCheck : MonoBehaviour
{
    [HideInInspector] public Vector3 _player;

    [SerializeField] private int _waitTime;

    [SerializeField] private Board _board;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerAttack _attack;
    [SerializeField] private DropWeapon _drope;
    [SerializeField] private Card[] _availableCards;

    private Vector3[] RaycastVector = new Vector3[] { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    private bool _attackeble = true;

    private void Start()
    {
        PlayerPosition();
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
                _attack.Damage(RayCard);
                _attackeble = false;
                StartCoroutine(WaitAttack());
            }
            if (RayCard.gameObject.GetComponent<EmptyCard>())
            {
                _move.Move(RayCard);
            }
            if (RayCard.gameObject.GetComponent<Weapon>())
            {
                Player player = _board.GetComponentInChildren<Player>();
                var weapon = RayCard.gameObject.GetComponent<Weapon>();
                _drope.Drop(weapon, player);
            }
            
        }
    // if(RayCard.gameObject.GetComponent<Door>())
     //{ 
            //NextRoom(RayCard);
     //}
    }
    private bool MoveCheck(Card[] _availeble, Card RayCard)
    {
        for (int i = 0; i < _availeble.Length; i++)
        {
            if(_availeble[i] == RayCard)
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
