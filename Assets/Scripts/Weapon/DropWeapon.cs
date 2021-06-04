using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropWeapon : MonoBehaviour
{
    public void Drop(Weapon weapon, Player player)
    {
        if((player.TryGetComponent<Weapon>(out Weapon component) == false))
        {
            player.gameObject.AddComponent<Weapon>();
            if(weapon.TryGetComponent<Sword>(out Sword sword))
            {
                var _image = player.GetComponentInChildren<Canvas>();
                var image = _image.GetComponentInChildren<Sword>();
                image.gameObject.GetComponent<Image>().enabled = true;
            }
            if(weapon.TryGetComponent<Axe>(out Axe axe))
            {
                axe.gameObject.SetActive(true);
            }
            if (weapon.TryGetComponent<Stick>(out Stick stick))
            {
                stick.gameObject.SetActive(true);
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
