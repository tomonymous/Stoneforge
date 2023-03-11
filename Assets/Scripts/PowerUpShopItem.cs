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
    public int id;


    public void Initialize(string n, Sprite thumb, int i)
    {
        powerUpName.text = n;
        thumbnail.sprite = thumb;
        id = i;
    }


    public void Select()
    {
        FindObjectOfType<PowerUpShopScript>().SelectPowerup(id);
    }
}
