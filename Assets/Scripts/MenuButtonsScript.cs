using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsScript : MonoBehaviour
{
    public Button resumeButton;
    void Start()
    {
        if(PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            AudioManager.instance.tutorial = true;
            SceneManager.LoadScene(1);
        }
        if (PlayerPrefs.GetInt("GameSaved", 0) == 1)
        {
            resumeButton.gameObject.SetActive(true);
        }
        else
        {
            resumeButton.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(1);
    }
    public void TutorialOLD()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(2);
    }

    public void Tutorial()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        AudioManager.instance.tutorial = true;
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        AudioManager.instance.resume = true;
        SceneManager.LoadScene(1);
    }
    public void Customise()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(3);
    }
    public void Menu()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(0);
    }
    public void Stats()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(4);
    }


}
