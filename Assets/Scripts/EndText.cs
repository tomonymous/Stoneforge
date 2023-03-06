
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    

    private void Start()
    {
    }
    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(1);
    }

    public void ToMenu()
    {
        FindObjectOfType<AudioManager>().Play("switch");
        SceneManager.LoadScene(0);
    }
    
    
}
