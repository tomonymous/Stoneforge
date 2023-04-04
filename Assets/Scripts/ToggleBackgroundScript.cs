using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleBackgroundScript : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgroundImages;
    public ToggleGroup toggleGroup;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;


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
        //Debug.Log(PlayerPrefs.GetInt("Background"));
        foreach (Toggle t in toggles)
        {
            if (int.Parse(t.name) == PlayerPrefs.GetInt("Background")) //Check to see if toggle should be selected.
            {
                //Debug.Log(int.Parse(t.name)+", "+ PlayerPrefs.GetInt("Background"));
                t.isOn = true;
            }
        }
        SetLocks();


    }


    public void ToggleChanged(bool value)
    {
        FindObjectOfType<AudioManager>().Play("black");
        int v = int.Parse(currentSelection.name);
        PlayerPrefs.SetInt("Background", v);
        backgroundImage.sprite = backgroundImages[v];
    }

    public void SetLocks()
    {
        if (PlayerPrefs.GetInt("Background1Lock", 0) != 1)
        {
            lock1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Background2Lock", 0) != 1)
        {
            lock2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Background3Lock", 0) != 1)
        {
            lock3.SetActive(true);
        }
        for (int i = 1; i < 4; i++)
        {
            bool allEnabled = false;
            for (int p = 0; p <= 8; p++)
            {
                
                if (PlayerPrefs.GetInt("Piece" + p + "Enabled") == i || p==7)
                {
                    //Debug.Log("Lock " + i + " Piece" + p);
                    allEnabled = true;
                }
                else
                {
                    allEnabled = false;
                    break;
                }
            }
            if (allEnabled && i == 1)
            {
                lock1.SetActive(false);
                if(PlayerPrefs.GetInt("Background1Lock", 0) != 1)
                {
                    FindObjectOfType<AudioManager>().Play("Confusion");
                    PlayerPrefs.SetInt("Background1Lock", 1);
                }
                break;
            }
            if (allEnabled && i == 2)
            {
                lock2.SetActive(false);
                if (PlayerPrefs.GetInt("Background2Lock", 0) != 1)
                {
                    FindObjectOfType<AudioManager>().Play("Confusion");
                    PlayerPrefs.SetInt("Background2Lock", 1);
                }
                break;
            }
            if (allEnabled && i == 3)
            {
                lock3.SetActive(false);
                if (PlayerPrefs.GetInt("Background3Lock", 0) != 1)
                {
                    FindObjectOfType<AudioManager>().Play("Confusion");
                    PlayerPrefs.SetInt("Background3Lock", 1);
                }
                break;
            }

        }
    }


}
