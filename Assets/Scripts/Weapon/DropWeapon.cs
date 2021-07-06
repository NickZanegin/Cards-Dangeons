using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropWeapon : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private PlayerAttack _brock;
    [SerializeField] private Board _board;
    private Weapon _activeWeapon;

    public delegate void Drope(Player player);
    public event Drope drope;

    private void Start()
    {
        var _player = _board.GetComponentInChildren<Player>();
        var _image = _player.GetComponentInChildren<Canvas>();
        var image = _image.GetComponentInChildren<Sword>();
        _activeWeapon = image;
        _ray.playerDrop += Drop;
        _brock.brock += BrokenWeapon;
    }
    public void Drop(Weapon weapon, Player player)
    {
        if((player.TryGetComponent<Weapon>(out Weapon component) == false))
        {
            if(weapon.TryGetComponent<Sword>(out Sword sword))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Sword>();
                image.gameObject.GetComponent<Image>().enabled = true;
                player.gameObject.AddComponent<Sword>();
                _activeWeapon = image;
            }
            if(weapon.TryGetComponent<Axe>(out Axe axe))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Axe>();
                image.gameObject.GetComponent<Image>().enabled = true;
                player.gameObject.AddComponent<Axe>();
                _activeWeapon = image;
            }
            if (weapon.TryGetComponent<Stick>(out Stick stick))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Stick>();
                image.gameObject.GetComponent<Image>().enabled = true;
                player.gameObject.AddComponent<Stick>(); 
                _activeWeapon = image;
            }
        }
        drope?.Invoke(player);
        
    }
    public void BrokenWeapon()
    {
        _activeWeapon.GetComponent<Image>().enabled = false;
    }
}
