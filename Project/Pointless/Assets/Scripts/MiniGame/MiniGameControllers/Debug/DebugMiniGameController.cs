using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMiniGameController : MiniGameController
{
    /*
     * 
     * Example implementation for a simple game
     * (FOR TESTING)
     * 
     */


    public GameObject clickableSquareObject;
    public Transform squareSpawn;

    private DebugSquare debugSquare;
    private GameObject debugSquareGameObject;

    // Update is called once per frame
    void Update()
    {
        switch (miniGameControllerState)
        {
            case MINI_GAME_CONTROLLER_STATES.IDLE:
                
                break;

            case MINI_GAME_CONTROLLER_STATES.STARTING:
                
                // Once the game has started, set the game to in progress
                miniGameControllerState = MINI_GAME_CONTROLLER_STATES.IN_PROGRESS;

                // Spawn the clickable square object in the spawn location defined in the controller prefab
                debugSquareGameObject = Instantiate(clickableSquareObject,
                                                    squareSpawn.position,
                                                    squareSpawn.rotation);
                debugSquare = debugSquareGameObject.GetComponent<DebugSquare>();

                break;

            case MINI_GAME_CONTROLLER_STATES.IN_PROGRESS:
                
                // Once the square is clicked, the game is "won" and we can end
                if(debugSquare.IsClicked())
                {
                    miniGameControllerState = MINI_GAME_CONTROLLER_STATES.FINISHED;
                }
                
                break;

            case MINI_GAME_CONTROLLER_STATES.FINISHED:
                
                // When the game is over, destroy the objects
                Destroy(debugSquareGameObject);
                Destroy(gameObject);
                
                break;

        }
    }
}
