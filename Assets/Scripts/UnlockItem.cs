using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockItem : MonoBehaviour
{
    public Text achievement;
    public Text description;
    public Image thumbnail;

    public void Initialize(Sprite i, string a, string d)
    {
        //Debug.Log(i + ", " + a + ", " + d);
        thumbnail.sprite = i;
        achievement.text = a;
        description.text = d;
    }
}
