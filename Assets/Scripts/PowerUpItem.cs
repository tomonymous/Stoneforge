using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpItem : MonoBehaviour
{
    public Button itemButton;
    public Button priceButton;
    public GameObject powerUpItem;
    public Image glow;
    public bool temp;
    public int id;
    public int tokenCost;
    bool tut;

    public void Initialize(int i, Sprite image, int p, bool t)
    {
        tut= false;
        if (i <= 1)
        {
            this.name = "time";
        }
        else
        {
            this.name = "powerup";
        }
        id = i;
        itemButton.image.sprite = image;
        itemButton.name = i.ToString();
        priceButton.name = "Price" + i;
        tokenCost = p;
        temp = t;
        if (t)
        {
            tokenCost = 0;
            glow.gameObject.SetActive(true);
        }
        else
        {
            glow.gameObject.SetActive(false);
        }
    }

    public void Initialize(int i, Sprite image, int p, bool t, bool tutorial)
    {
        tut = tutorial;
        if (i <= 1)
        {
            this.name = "time";
        }
        else
        {
            this.name = "powerup";
        }
        id = i;
        itemButton.image.sprite = image;
        itemButton.name = i.ToString();
        priceButton.name = "Price" + i;
        tokenCost = p;
        temp = t;
        if (t)
        {
            tokenCost = 0;
            glow.gameObject.SetActive(true);
        }
        else
        {
            glow.gameObject.SetActive(false);
        }
    }

    public void ItemClick()
    {
        StartCoroutine(IClick());
    }
    public void ItemBuy()
    {
        FindObjectOfType<ComboMatch>().ItemBuy(priceButton, temp);
        if (temp)
        {
            FindObjectOfType<ComboMatch>().haveTempPowerUp = false;
            priceButton.interactable = false;
            StartCoroutine(DestroyDelay());
        }
    }
    private IEnumerator IClick()
    {
        if (!tut)
        {
            WaitForSeconds displayPause = new WaitForSeconds(2f);
            itemButton.gameObject.SetActive(false);
            TextMeshProUGUI price = priceButton.GetComponentInChildren<TextMeshProUGUI>();
            price.text = tokenCost.ToString();
            priceButton.gameObject.SetActive(true);
            //TextMeshProUGUI description = descriptionPanel.GetComponentInChildren<TextMeshProUGUI>();
            //description.text = powerupDescriptions[int.Parse(b.name)].ToString();
            //descriptionPanel.SetActive(true);
            //latestTokenDescription = int.Parse(b.name);
            yield return displayPause;
            itemButton.gameObject.SetActive(true);
            //if (latestTokenDescription == int.Parse(b.name))
            //{
            //    descriptionPanel.SetActive(false);
            //}
            priceButton.gameObject.SetActive(false);
        }
        else if(FindObjectOfType<ComboMatch>().tutorialPhase>14)
        {
            WaitForSeconds displayPause = new WaitForSeconds(2f);
            itemButton.gameObject.SetActive(false);
            TextMeshProUGUI price = priceButton.GetComponentInChildren<TextMeshProUGUI>();
            price.text = tokenCost.ToString();
            priceButton.gameObject.SetActive(true);
            yield return displayPause;
            itemButton.gameObject.SetActive(true);
            priceButton.gameObject.SetActive(false);
        }
    }
    private IEnumerator DestroyDelay()
    {
        WaitForSeconds animationWait = new WaitForSeconds(1f);
        yield return animationWait;
        Destroy(powerUpItem);
    }
}
