using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpShopScript : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI description;
    public TextMeshProUGUI keyCost;
    public TextMeshProUGUI tokenCost;
    public TextMeshProUGUI title;
    public Image thumbnail;

    public TextMeshProUGUI redKeyTotal;

    public RectTransform shoplist;
    public string[] powerups;
    public string[] powerupDescriptions;
    public int[] powerupCosts;
    public int[] tokenPrices;
    public Sprite[] powerupSprites;

    [Header("Prefabs")]
    public GameObject powerUpShopItem;

    void Start()
    {
        redKeyTotal.text = PlayerPrefs.GetInt("BlueKeys", 0).ToString("000000000");
        infoPanel.SetActive(false);
        for (int i = 0; i < powerups.Length; i++) //Instantiate the Shop.
        {
            GameObject s = Instantiate(powerUpShopItem, shoplist);
            PowerUpShopItem item = powerUpShopItem.GetComponent<PowerUpShopItem>();
            item.Initialize(powerups[i], powerupSprites[i], i);
            item.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitInfo()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        infoPanel.SetActive(false);
    }

    public void SelectPowerup(int i)
    {
        FindObjectOfType<AudioManager>().Play("switch");
        description.text = powerupDescriptions[i];
        keyCost.text = powerupCosts[i].ToString("00000");
        tokenCost.text = tokenPrices[i].ToString("00");
        title.text = powerups[i];
        thumbnail.sprite = powerupSprites[i];
        infoPanel.SetActive(true);
    }
}