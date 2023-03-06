using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePieces : MonoBehaviour
{
    public static MovePieces instance;
    ComboMatch game;

    NodePiece moving;
    Point newIndex;
    Vector2 mouseStart;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        game = GetComponent<ComboMatch>();
    }


    void Update()
    {
        if(moving != null)
        {
            Vector2 dir = ((Vector2)Input.mousePosition - mouseStart);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            newIndex = Point.clone(moving.index);
            Point add = Point.zero;
            if(dir.magnitude > 32 && moving.bombStatus == 0) //if our mouse if 32 pixels away from the starting position 
            {
                //make add either (1,0) | (-1, 0) | (0, 1) | (0, -1) depending on direction of the mouse
                if (aDir.x > aDir.y)
                    add = (new Point((nDir.x > 0) ? 1 : -1, 0));
                else if (aDir.y > aDir.x)
                    add = (new Point(0, (nDir.y > 0) ? -1 : 1));
            }
            newIndex.add(add);

            Vector2 pos = game.getPositionFromPoint(moving.index);
            if (!newIndex.Equals(moving.index))
                pos += Point.mult(new Point(add.x, -add.y), 16).ToVector();
            moving.MovePositionTo(pos);
        }
    }

    public void StartTheGame()
    {
        game.StartPlaying();
    }

    public void Pause()
    {
        game.gameRunning = false;
    }
    public void Resume()
    {
        game.gameRunning = true;
        if (game.timerBroken == 0)
        {
            if (!game.zenMode)
            {
                CountdownTimer.timerPaused = false;
            }
        }
    }

    public void EndGame()
    {
        game.gameRunning = true;
        game.EndTheGame();
    }

    public void MovePiece(NodePiece piece)
    {
        if (moving != null || game.gameEnd || !game.gameRunning || game.tutorialStop) return; //   - can't make move if gameOver
        if (game.armedBombStatus > 0 && game.armedBombIndexX < 0)
        {
            piece.bombStatus = game.armedBombStatus;  //set piece as armed.
            game.armedBombIndexX = piece.index.x;
            game.armedBombIndexY = piece.index.y;  //let Game know  its position
        }
        if (game.magicTouch)
        {
            game.magicIndexX = piece.index.x;
            game.magicIndexY = piece.index.y;
        }
            moving = piece;
            mouseStart = Input.mousePosition;
    }

    public void DropPiece()
    {
        if (moving == null || game.gameEnd || !game.gameRunning) return;
        if (!newIndex.Equals(moving.index))
        {
            game.FlipPieces(moving.index, newIndex, true);
            FindObjectOfType<AudioManager>().Play("swap");
        }
        else { 
            game.ResetPiece(moving);
        }
        moving = null;
    }
}
