using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerUpShopItem : MonoBehaviour
{
    public TextMeshProUGUI powerUpName;
    public Image thumbnail;
    public Image bought;
    public int id;


    public void Initialize(string n, Sprite thumb, int i, bool b)
    {
        powerUpName.text = n;
        thumbnail.sprite = thumb;
        id = i;
        if (b)
        {
            bought.gameObject.SetActive(false);
        }
        else
        {
            bought.gameObject.SetActive(false);
        }
    }


    public void Select()
    {
        FindObjectOfType<PowerUpShopScript>().SelectPowerup(id);
    }

    public void Buy()
    {
        bought.gameObject.SetActive(true);
    }

    public void Sell()
    {
        bought.gameObject.SetActive(false);
    }
}
