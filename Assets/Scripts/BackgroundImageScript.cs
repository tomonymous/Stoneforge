using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImageScript : MonoBehaviour
{
    public Sprite[] backgroundImages;
    public Image backgroundImage;
    // Start is called before the first frame update
    void Start()
    {
        int v = PlayerPrefs.GetInt("Background",0);
        backgroundImage.sprite = backgroundImages[v];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
