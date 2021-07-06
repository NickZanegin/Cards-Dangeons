using UnityEngine;
using TMPro;

public class RoomCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _roomTextUi;
    [SerializeField] private RayCastsCheck _ray;
    private int _roomNuber = 1;

    private void Start()
    {
        _ray.roomchenge += RoomCount;
    }

    private void RoomCount()
    {
        _roomNuber++;
        _roomTextUi.text = "Room " + _roomNuber.ToString();
    }
}
