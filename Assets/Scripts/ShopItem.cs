using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{
    public Text powerupName;
    public Text powerupDescription;
    public Text tokenCost;
    public int id;
    public Image thumbnail;
    public Button itemLock;
    public Button selected;

    public void Initialize(int i, Sprite thumb, string name, string description, int cost)
    {
        if (i <= 1)
        {
            this.name = "time"; //identify timer based powerups. need to be disabled in chaos mode.
        }
        else
        {
            this.name = "Powerup Item";
        }
        thumbnail.sprite = thumb;
        powerupName.text = name;
        powerupDescription.text = description;
        id = i;
        tokenCost.text = cost.ToString();
    }
    public void AddPowerup()
    {
        if (FindObjectOfType<ComboMatch>().AddPowerup(this.id))
        {
            selected.gameObject.SetActive(true);
        }
        //Destroy(this);
    }
    public void Deselect()
    {
        FindObjectOfType<ComboMatch>().Deselect(this.id);
        selected.gameObject.SetActive(false);
    }
}
