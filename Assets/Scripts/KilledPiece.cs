
using UnityEngine;
using UnityEngine.UI;

public class KilledPiece : MonoBehaviour
{
    public bool moving;
    public bool arrived;
    Vector2 moveDir;
    Vector2 startPoint;
    float speed = 14f;
    RectTransform rect;
    Image img;


    public void Initialize(Sprite piece, Vector2 start, Vector2 end)
    {
        moving = true;
        arrived = false;
        startPoint.x = start.x;
        startPoint.y = start.y;
        moveDir = end;


        img = GetComponent<Image>();
        rect = GetComponent < RectTransform >();
        img.sprite = piece;
        rect.anchoredPosition = start;

    }

    // Update is called once per frame
    void Update()
    {
        if (!moving) return;
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, moveDir, Time.deltaTime * speed);
        
        if(Vector2.Distance(rect.anchoredPosition, moveDir) < 8f)
        {
            arrived = true;
            rect.anchoredPosition = new Vector2(Screen.width + 64f, Screen.height + 64f);
            moving = false;
        }

    }
}
