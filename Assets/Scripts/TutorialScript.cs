
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    private Queue<string> instructions;
    public Text instructionText;
    public string[] sentences;
    public Sprite[] images;
    private int currentPosition;
    public Button playButton;
    public Button nextButton;
    public Button previousButton;
    public Button menuButton;
    public Image image;

    private void Start()
    {
        currentPosition = 0;
        playButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        previousButton.gameObject.SetActive(false);
        string instruction = sentences[currentPosition];
        image.sprite = images[currentPosition];
        instructionText.text = instruction;
    }
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(1);
    }

    public void ToMenu()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(0);
    }


    public void DisplayNextSentence()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        currentPosition++;


        if (currentPosition == 0) //but this would never happen? Can't remember why this was necessary.
        {
            menuButton.gameObject.SetActive(true);
            previousButton.gameObject.SetActive(false);
        }
        else
        {
            menuButton.gameObject.SetActive(false);
            previousButton.gameObject.SetActive(true);
        }
        string instruction = sentences[currentPosition];
        image.sprite = images[currentPosition];
        instructionText.text = instruction;
        if (currentPosition >= sentences.Length - 1)
        {
            playButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }

    }
    public void DisplayPreviousSentence()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        currentPosition--;

        playButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        string instruction = sentences[currentPosition];
        image.sprite = images[currentPosition];
        instructionText.text = instruction;
        if (currentPosition < 1)
        {
            previousButton.gameObject.SetActive(false);
            menuButton.gameObject.SetActive(true);
        }
    }
}
