using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSquare : MonoBehaviour
{
    /*
     * 
     * Simple clickable square object for testing the
     * minigame controller implementation
     * 
     */

    public GameObject squareSpriteObject;

    public enum SQUARE_STATES
    {
        SPAWN,
        UNCLICKED,
        CLICKED
    }

    public SQUARE_STATES squareState = SQUARE_STATES.SPAWN;

    public void Start()
    {

    }

    public void Update()
    {
        switch(squareState)
        {
            case SQUARE_STATES.SPAWN:

                squareState = SQUARE_STATES.UNCLICKED;

                break;

        }
    }

    public void OnMouseDown()
    {
        squareState = SQUARE_STATES.CLICKED;
    }

    public bool IsClicked()
    {
        return squareState == SQUARE_STATES.CLICKED;
    }
}
