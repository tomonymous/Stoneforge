using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpShopScript : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject buyButton;
    public GameObject keyCostPanel;
    public GameObject tokenDisplay;
    public TextMeshProUGUI description;
    public TextMeshProUGUI keyCost;
    public TextMeshProUGUI tokenCost;
    public TextMeshProUGUI title;
    public Image thumbnail;
    public Image powerUpSlotThumbnail;

    public TextMeshProUGUI redKeyTotal;

    public RectTransform shoplist;
    public string[] powerups;
    public string[] powerupDescriptions;
    public bool[] powerupOwned;
    public int[] tokenPrices;
    public Sprite[] powerupSprites;
    public Sprite[] powerupSlotSprites;
    public int selectedPowerup;

    [Header("Prefabs")]
    public GameObject powerUpShopItem;

    void Start()
    {
        //PlayerPrefs.SetInt("RedKeys", 20);
        redKeyTotal.text = PlayerPrefs.GetInt("RedKeys", 0).ToString("000000000");
        powerUpSlotThumbnail.sprite = powerupSlotSprites[PlayerPrefs.GetInt("InventorySlots", 1) - 1];
        infoPanel.SetActive(false);
        for (int i = 0; i < powerups.Length; i++) //Instantiate the Shop.
        {
            GameObject s = Instantiate(powerUpShopItem, shoplist);
            PowerUpShopItem item = powerUpShopItem.GetComponent<PowerUpShopItem>();
            item.Initialize(powerups[i], powerupSprites[i], i, true);
            item.name = i + " powerup";
            item.gameObject.SetActive(true);
            string key = powerups[i] + " Unlocked";
            if (PlayerPrefs.GetInt(key, 0) == 1 || i == 0 || i == 2) //1 is unlocked. 0 is locked.
            {
                item.GetComponent<PowerUpShopItem>().Buy();
            }
        }
        //Check and correct random instatiation error with last shop item.
        GameObject powerUp = GameObject.Find(powerups.Length - 1 + " powerup(Clone)");  
        if (powerUp == null)
        {
            GameObject s = Instantiate(powerUpShopItem, shoplist);
            PowerUpShopItem item = powerUpShopItem.GetComponent<PowerUpShopItem>();
            item.Initialize(powerups[powerups.Length - 1], powerupSprites[powerups.Length - 1], powerups.Length - 1, true);
            item.name = powerups.Length - 1 + " powerup";
            item.gameObject.SetActive(true);
            string key = powerups[powerups.Length - 1] + " Unlocked";
            if (PlayerPrefs.GetInt(key, 0) == 1) //1 is unlocked. 0 is locked.
            {
                item.GetComponent<PowerUpShopItem>().Buy();
            }
            GameObject buggedPowerUp = GameObject.Find("PowerUpShopItem(Clone)");
            Destroy(buggedPowerUp);
        }
        selectedPowerup = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitInfo()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        infoPanel.SetActive(false);
        selectedPowerup = -1;
    }

    public void SelectPowerup(int i)
    {
        FindObjectOfType<AudioManager>().Play("switch");
        description.text = powerupDescriptions[i];
        keyCost.text = (5 + 10 * PlayerPrefs.GetInt("PowerUpPurchases", 0)).ToString("00000");
        tokenCost.text = tokenPrices[i].ToString("00");
        title.text = powerups[i];
        thumbnail.sprite = powerupSprites[i];
        selectedPowerup = i;
        string key = powerups[selectedPowerup] + " Unlocked";
        if(PlayerPrefs.GetInt(key, 0) == 1 || i == 0 || i == 2)
        {
            buyButton.SetActive(false);
            keyCostPanel.SetActive(false);
        }
        else
        {
            buyButton.SetActive(true);
            keyCostPanel.SetActive(true);
        }

        tokenDisplay.SetActive(true);
        infoPanel.SetActive(true);
    }

    public void SelectExtraSlot()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        description.text = "Equip " + (PlayerPrefs.GetInt("InventorySlots", 1) + 1) + " different Power Ups in a game.";
        int cost = 10;
        keyCost.text = cost.ToString("00000");
        title.text = "Extra Slot";
        thumbnail.sprite = powerupSlotSprites[PlayerPrefs.GetInt("InventorySlots", 1) - 1];
        selectedPowerup = -2;
        int invSlotsPurchased = PlayerPrefs.GetInt("InventorySlots", 1) - 1;
        int powerUpsOwned = PlayerPrefs.GetInt("PowerUpPurchases", 0) - invSlotsPurchased + 2;
        if (PlayerPrefs.GetInt("InventorySlots", 1) > 6 || powerUpsOwned <= invSlotsPurchased + 1)
        {
            buyButton.SetActive(false);
            keyCostPanel.SetActive(false);
        }
        else
        {
            buyButton.SetActive(true);
            keyCostPanel.SetActive(true);
        }
        tokenDisplay.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void BuyPowerup()
    {

        int cost = 5 + 10 * PlayerPrefs.GetInt("PowerUpPurchases", 0);
        int wallet = PlayerPrefs.GetInt("RedKeys", 0);
        if (cost <= wallet)
        {
            if (selectedPowerup == -2) //if extra slot selected.
            {
                FindObjectOfType<AudioManager>().Play("Buy");
                PlayerPrefs.SetInt("RedKeys", wallet - cost);
                PlayerPrefs.SetInt("TotalRedKeysSpent", PlayerPrefs.GetInt("TotalRedKeysSpent", 0) + cost);
                redKeyTotal.text = PlayerPrefs.GetInt("RedKeys", 0).ToString("000000000");
                PlayerPrefs.SetInt("InventorySlots", PlayerPrefs.GetInt("InventorySlots", 1) + 1);
                powerUpSlotThumbnail.sprite = powerupSlotSprites[PlayerPrefs.GetInt("InventorySlots", 1) - 1];
                PlayerPrefs.SetInt("PowerUpPurchases", PlayerPrefs.GetInt("PowerUpPurchases", 0) + 1);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Buy");
                PlayerPrefs.SetInt("RedKeys", wallet - cost);
                PlayerPrefs.SetInt("TotalRedKeysSpent", PlayerPrefs.GetInt("TotalRedKeysSpent", 0) + cost);
                redKeyTotal.text = PlayerPrefs.GetInt("RedKeys", 0).ToString("000000000");
                string key = powerups[selectedPowerup] + " Unlocked";
                PlayerPrefs.SetInt(key, 1); //1 is unlocked. 0 is locked.
                GameObject powerUp = GameObject.Find(selectedPowerup + " powerup(Clone)");
                powerUp.GetComponent<PowerUpShopItem>().Buy();
                PlayerPrefs.SetInt("PowerUpPurchases", PlayerPrefs.GetInt("PowerUpPurchases", 0) + 1);
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("CantAfford");
        }
        QuitInfo();
    }

    public void RefundAll()
    {
        if (PlayerPrefs.GetInt("TotalRedKeysSpent", 0) > 0)
        {
            infoPanel.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Refund");
            for (int i = 0; i < powerups.Length; i++)
            {
                if (i == 0 || i == 2)
                {
                    continue;
                }
                string key = powerups[i] + " Unlocked";
                PlayerPrefs.SetInt(key, 0);
                GameObject powerUp = GameObject.Find(i + " powerup(Clone)");
                if (powerUp != null)
                {
                    powerUp.GetComponent<PowerUpShopItem>().Sell();
                }
            }
            PlayerPrefs.SetInt("RedKeys", PlayerPrefs.GetInt("RedKeys", 0) + PlayerPrefs.GetInt("TotalRedKeysSpent", 0));
            PlayerPrefs.SetInt("TotalRedKeysSpent", 0);
            PlayerPrefs.SetInt("PowerUpPurchases", 0);
            redKeyTotal.text = PlayerPrefs.GetInt("RedKeys", 0).ToString("000000000");
            PlayerPrefs.SetInt("InventorySlots", 1);
            powerUpSlotThumbnail.sprite = powerupSlotSprites[PlayerPrefs.GetInt("InventorySlots", 1) - 1];
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("CantAfford");
        }
        selectedPowerup = -1;
    }
}