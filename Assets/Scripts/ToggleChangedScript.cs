using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleChangedScript : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject infoPanel;
    public Image thumbnail;
    public Sprite[] stoneSprites;


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
        if (PlayerPrefs.GetInt("Skin " + skinNumber + " Unlocked") != 1) //Locks skin 1 if not purchased from shop
        {
            lock1.SetActive(true);
        }
        if (PlayerPrefs.GetInt(toggleGroup.name + "Unlocked") != 1) //Locks skin 2 if not earned
        {
            lock2.SetActive(true);
        }
        if (PlayerPrefs.GetInt(toggleGroup.name + "Chaos") != 1) //Locks skin 3 if not earned
        {
            lock3.SetActive(true);
        }
        foreach (Toggle t in toggles)
        {
            if(int.Parse(t.name) == PlayerPrefs.GetInt(toggleGroup.name + "Enabled")) //Check to see if toggle should be selected.
            {
                t.isOn = true;
            }
        }
    }

    public void ToggleChanged(bool value)
    {
        
        int v = int.Parse(currentSelection.name);
        PlayerPrefs.SetInt(toggleGroup.name +"Enabled", v);
        FindObjectOfType<ToggleBackgroundScript>().SetLocks();
    }

    public void popUp(Button b)
    {
        if(b.name == "1")
        {
            infoPanel.SetActive(true);
            thumbnail.sprite = stoneSprites[0];
        }
        else if(b.name == "2")
        {
            infoPanel.SetActive(true);
            thumbnail.sprite = stoneSprites[1];
        }
        else if(b.name == "3")
        {
            infoPanel.SetActive(true);
            thumbnail.sprite = stoneSprites[2];
        }
    }
    IEnumerator HidePopUp(GameObject popUp)
    {
        yield return new WaitForSeconds(1);
        popUp.SetActive(false);
    }
}
