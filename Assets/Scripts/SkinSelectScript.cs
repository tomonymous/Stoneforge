using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkinSelectScript : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject infoPanelBackground;
    public GameObject selectedLock;
    public Image thumbnail;
    public TextMeshProUGUI wallet;
    public TextMeshProUGUI price;

    // Start is called before the first frame update
    void Start()
    {
        wallet.text = PlayerPrefs.GetInt("BlueKeys", 0).ToString("000000");
        infoPanelBackground.SetActive(false);
        infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        infoPanelBackground.SetActive(false);
    }

    public void Buy()
    {
        int walletNumber = PlayerPrefs.GetInt("BlueKeys", 0);
        int keyCost = int.Parse(price.text);
        if(walletNumber >= keyCost)
        {
            PlayerPrefs.SetInt(thumbnail.name + " Unlocked", 1);
            FindObjectOfType<AudioManager>().Play("Buy");
            PlayerPrefs.SetInt("BlueKeys", walletNumber - keyCost);
            wallet.text = PlayerPrefs.GetInt("BlueKeys", 0).ToString("000000");
            selectedLock.SetActive(false);
            CloseInfoPanel();
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("CantAfford");
        }
    }
}
