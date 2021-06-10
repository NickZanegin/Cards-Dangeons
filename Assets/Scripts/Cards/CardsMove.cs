using UnityEngine;

public class CardsMove : MonoBehaviour
{
    [SerializeField] private BoardSpawner _spawn;
    [SerializeField] private PlayerMove _move;

    public delegate void AnglSpawn(Vector3 OldCardPosition);
    public event AnglSpawn angle;


    private void Start()
    {
        _move.move += Move;
    }

    public void Move(Vector3 _oldPlayerPosition, Vector3 _moveDirection)
    {
        Ray ray = new Ray(_oldPlayerPosition, _moveDirection);
        RaycastHit[] hit = Physics.RaycastAll(ray);
        
        if(hit.Length != 0)
        {
            Vector3 OldCardPosition;
            OldCardPosition = hit[0].transform.position;
            hit[0].transform.position = _oldPlayerPosition;

            for(int i = 1; i < hit.Length; i++)
            {
                Vector3 NextCardPositin = hit[i].transform.position;
                hit[i].transform.position = OldCardPosition;
                OldCardPosition = NextCardPositin;
            }
            angle.Invoke(OldCardPosition);
        }
        else
        {
            angle.Invoke(_oldPlayerPosition);
        }
    }
}
