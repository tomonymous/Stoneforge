using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpShopScript : MonoBehaviour
{
    public GameObject infoPanel;
    public RectTransform shoplist;
    public string[] powerups;
    public int[] tokenPrices;
    public Sprite[] powerupSprites;
    public GameObject powerUpShopItem;

    void Start()
    {
        for (int i = 0; i < powerups.Length; i++) //Instantiate the Shop.
        {
            GameObject s = Instantiate(powerUpShopItem, shoplist);
            PowerUpShopItem item = powerUpShopItem.GetComponent<PowerUpShopItem>();
            item.Initialize(powerups[i], powerupSprites[i]);
            item.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select(Button b)
    {

    }
}