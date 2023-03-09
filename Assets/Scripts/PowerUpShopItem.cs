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


    public void Initialize(string n, Sprite thumb)
    {
        powerUpName.text = n;
        thumbnail.sprite = thumb;
    }
}
