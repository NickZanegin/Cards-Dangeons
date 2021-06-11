using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawn : MonoBehaviour
{
    [SerializeField] private List<Card> _drop;
    [SerializeField] private Board _board;
    [SerializeField] private BoardSpawner _spawner;
    [SerializeField] private CardsMove _cardsMove;
    [SerializeField] private OpenChest _open;

    private void Awake()
    {
        _spawner.dropSpawn += Spawner;
    }

    private void Start()
    {
        _cardsMove.angle += AngleSpawn;
        _open.open += Spawn;
    }

    private Card Spawner(Transform currentBulidPoint, Card newCard)
    {
        //Debug.Log("Check");
        return Spawn(_spawner.GetSpawnPoint(currentBulidPoint, newCard));
    }

    private Card Spawn(Vector3 currentBulidPoint)
    {
        return Instantiate(_drop[Random.Range(0, 3)], currentBulidPoint, Quaternion.identity, _board.transform);
    }

    public void AngleSpawn(Vector3 Postition)
    {
        Card NewCard = Instantiate(_drop[Random.Range(0, 3)], Postition, Quaternion.identity, _board.transform);
        _spawner._cards.Add(NewCard);
    }
}
