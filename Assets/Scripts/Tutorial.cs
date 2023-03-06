using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject panel;
    public Text instruction;
    public string[] sentences;
    public Button ok;
    public GameObject menu;
    public GameObject redRing;
    public TextMeshProUGUI okButtonText;
    RectTransform rectPanel;
    RectTransform rectRing;
    int tutorialPhase;

    void Start()
    {
        menu.SetActive(false);
        redRing.SetActive(false);
        rectPanel = panel.GetComponent<RectTransform>();
        rectRing = redRing.GetComponent<RectTransform>();
        rectPanel.anchoredPosition = new Vector2(0f, -330f);
        tutorialPhase =0;
        instruction.text = sentences[tutorialPhase];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void okClicked()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        tutorialPhase += 1;
        if(tutorialPhase == 1)
        {
            rectPanel.anchoredPosition = new Vector2(0f, -970f);
        }
        if (tutorialPhase == 2)
        {
            rectPanel.anchoredPosition = new Vector2(-260f, 800f);
            rectRing.anchoredPosition = new Vector2(-390f, 480f);
            rectRing.sizeDelta = new Vector2(512f, 350f);
            redRing.SetActive(true);
        }
        if (tutorialPhase == 3)
        {
            redRing.SetActive(false);
            rectPanel.anchoredPosition = new Vector2(0f, -970f);
        }

        if (tutorialPhase == 4)
        {
            rectPanel.anchoredPosition = new Vector2(0f, 350f);
        }
        if (tutorialPhase == 5)
        {
            rectPanel.anchoredPosition = new Vector2(0f, -750f);
        }
        if (tutorialPhase == 7)
        {
            rectPanel.anchoredPosition = new Vector2(260f, 800f);
            rectRing.anchoredPosition = new Vector2(165f, 490f);
            rectRing.sizeDelta = new Vector2(400f, 350f);
            redRing.SetActive(true);
        }
        if (tutorialPhase == 8)
        {
            redRing.SetActive(false);
            rectPanel.anchoredPosition = new Vector2(0f, 0f);
        }
        if (tutorialPhase == 9)
        {
            rectPanel.anchoredPosition = new Vector2(0f, -970f);
        }
        if (tutorialPhase == 11)
        {
            rectPanel.anchoredPosition = new Vector2(0f, -0f);
        }
        if (tutorialPhase == 13)
        {
            rectPanel.anchoredPosition = new Vector2(0f, -970f);
            rectRing.anchoredPosition = new Vector2(0f, -490f);
            rectRing.sizeDelta = new Vector2(400f, 350f);
            redRing.SetActive(true);
        }
        if(tutorialPhase == 14)
        {
            
        }
        if (tutorialPhase == 15)
        {
            redRing.SetActive(false);
            rectPanel.anchoredPosition = new Vector2(0f, -0f);
        }
        if (tutorialPhase == 3 || tutorialPhase == 6 || tutorialPhase == 14)
        {
            okButtonText.text = "GOT IT!";
        }
        else
        {
            okButtonText.text = "OK";
        }
        instruction.text = sentences[tutorialPhase];
        FindObjectOfType<ComboMatch>().tutorialPhaseChange(tutorialPhase);
        if(tutorialPhase == 15)
        {
            menu.SetActive(true);
        }
    }
}
