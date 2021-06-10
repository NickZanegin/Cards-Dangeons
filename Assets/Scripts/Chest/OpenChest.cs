using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private BoardSpawner _spawner;

    public delegate Card Open(Vector3 currentBulidPoint);
    public event Open open;


    private void Start()
    {
        _ray.chestOpen += DropSpawn;
    }

    private void DropSpawn(Vector3 chestPosition, Card chest)
    {
        Destroy(chest.gameObject);
        Card newCard = open?.Invoke(chestPosition);
        _spawner._cards.Add(newCard);
       
    }
}
