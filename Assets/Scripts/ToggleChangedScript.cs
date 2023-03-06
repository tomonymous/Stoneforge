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
    public GameObject popUp1;
    public GameObject popUp2;
    public GameObject popUp3;
    public Text levelText;
    public Text movesText;
    public Text chaosText;


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
        if(b.name.Substring(0,5) == "LEVEL")
        {
            levelText.text = "GET TO" + System.Environment.NewLine + b.name + System.Environment.NewLine + "TO UNLOCK";
            popUp1.SetActive(true);
            StartCoroutine(HidePopUp(popUp1));
        }
        else if(b.name.Substring(b.name.Length - 5) == "MOVES" && b.name.Substring(0, 5) != "CHAOS")
        {
            movesText.text = "GET TO" + System.Environment.NewLine + b.name + System.Environment.NewLine + "TO UNLOCK";
            popUp2.SetActive(true);
            StartCoroutine(HidePopUp(popUp2));
        }
        else if(b.name.Substring(b.name.Length - 4) == "GOLD")
        {
            chaosText.text = "FORGE" + System.Environment.NewLine + b.name + " IN" + System.Environment.NewLine + "CHAOS MODE";
            popUp3.SetActive(true);
            StartCoroutine(HidePopUp(popUp3));
        }
        else if(b.name.Substring(0,5) == "CHAOS")
        {
            chaosText.text = b.name.Substring(6) + System.Environment.NewLine +"IN" + System.Environment.NewLine + "CHAOS MODE";
            popUp3.SetActive(true);
            StartCoroutine(HidePopUp(popUp3));
        }
    }
    IEnumerator HidePopUp(GameObject popUp)
    {
        yield return new WaitForSeconds(1);
        popUp.SetActive(false);
    }
}
