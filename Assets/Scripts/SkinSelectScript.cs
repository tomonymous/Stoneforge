using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelectScript : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject infoPanelBackground;
    public TextMeshProUGUI wallet;

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
}
