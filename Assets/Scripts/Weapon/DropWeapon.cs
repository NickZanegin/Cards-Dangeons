using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropWeapon : MonoBehaviour
{
    [SerializeField] private RayCastsCheck _ray;
    [SerializeField] private PlayerAttack _brock;

    private void Start()
    {
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
            }
            if(weapon.TryGetComponent<Axe>(out Axe axe))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Axe>();
                image.gameObject.GetComponent<Image>().enabled = true;
                player.gameObject.AddComponent<Axe>();
            }
            if (weapon.TryGetComponent<Stick>(out Stick stick))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Stick>();
                image.gameObject.GetComponent<Image>().enabled = true;
                player.gameObject.AddComponent<Stick>();
            }
        }
        
    }
    public void BrokenWeapon(Player _player)
    {
        var _image = _player.GetComponentInChildren<Canvas>();
        var image = _image.GetComponentInChildren<Weapon>();
        image.gameObject.GetComponent<Image>().enabled = false;
    }
}
