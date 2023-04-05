using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ToggleChangedScript : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject infoPanel;
    public GameObject infoPanelBackground;
    public Image thumbnail;
    public Sprite[] stoneSprites;
    public TextMeshProUGUI cost;
    bool startUp = false;

    public Toggle currentSelection
    {
        get
        {
            return toggleGroup.ActiveToggles().FirstOrDefault();
        }
    }
    void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        lock1.SetActive(false);
        lock2.SetActive(false);
        lock3.SetActive(false);
        int skinNumber = int.Parse(toggleGroup.name.Substring(5));
        skinNumber = skinNumber + 1;
        if (PlayerPrefs.GetInt(toggleGroup.name + "A Unlocked") != 1) //Locks skin A
        {
            lock1.SetActive(true);
        }
        if (PlayerPrefs.GetInt(toggleGroup.name + "B Unlocked") != 1) //Locks skin B
        {
            lock2.SetActive(true);
        }
        if (PlayerPrefs.GetInt(toggleGroup.name + "C Unlocked") != 1) //Locks skin C
        {
            lock3.SetActive(true);
        }
        startUp = true;
        foreach (Toggle t in toggles)
        {
            if(int.Parse(t.name) == PlayerPrefs.GetInt(toggleGroup.name + "Enabled")) //Check to see if toggle should be selected.
            {
                t.isOn = true;
            }
        }
        startUp = false;
    }

    public void ToggleChanged(bool value)
    {
        
        int v = int.Parse(currentSelection.name);
        PlayerPrefs.SetInt(toggleGroup.name +"Enabled", v);
        //Debug.Log(toggleGroup.name + "Enabled: " + v);
        if (!startUp)
        {
            FindObjectOfType<AudioManager>().Play("swap");
        }
        FindObjectOfType<ToggleBackgroundScript>().SetLocks();
    }

    public void popUp(Button b)
    {
        FindObjectOfType<AudioManager>().Play("switch");
        if (b.name == "1")
        {
            cost.text = 4.ToString("000000");
            infoPanel.SetActive(true);
            infoPanelBackground.SetActive(true);
            thumbnail.sprite = stoneSprites[0];
            thumbnail.name = toggleGroup.name + 'A';
            FindObjectOfType<SkinSelectScript>().selectedLock = lock1;
        }
        else if(b.name == "2")
        {
            cost.text = 8.ToString("000000");
            infoPanel.SetActive(true);
            infoPanelBackground.SetActive(true);
            thumbnail.sprite = stoneSprites[1];
            thumbnail.name = toggleGroup.name + 'B';
            FindObjectOfType<SkinSelectScript>().selectedLock = lock2;
        }
        else if(b.name == "3")
        {
            cost.text = 12.ToString("000000");
            infoPanel.SetActive(true);
            infoPanelBackground.SetActive(true);
            thumbnail.sprite = stoneSprites[2];
            thumbnail.name = toggleGroup.name + 'C';
            FindObjectOfType<SkinSelectScript>().selectedLock = lock3;
        }
    }


    IEnumerator HidePopUp(GameObject popUp)
    {
        yield return new WaitForSeconds(1);
        popUp.SetActive(false);
    }
}
