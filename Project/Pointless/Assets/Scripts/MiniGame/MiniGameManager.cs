using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    /*
     * 
     * High level manager for all mini game controllers 
     * 
     */

    public CameraController cameraController;
    public InputManager inputManager;
    public GameObject screenObject;
    public Transform screenSpawn;
    public Transform miniGameControllerSpawn;

    private ScreenController screenController = null;
    private MiniGameController miniGameController;

    private enum MINI_GAME_STATES {
        IDLE,
        SETUP,
        STARTING,
        IN_PROGRESS,
        FINISHING
    }

    private MINI_GAME_STATES miniGameState = MINI_GAME_STATES.IDLE;

    // Update is called once per frame
    void Update()
    {
        switch(miniGameState)
        {
            case MINI_GAME_STATES.IDLE:
                
                break;

            case MINI_GAME_STATES.SETUP:
                
                // Spawn the screen object and move the camera
                SpawnScreen();
                cameraController.SwapStates();
                miniGameState = MINI_GAME_STATES.STARTING;

                break;

            case MINI_GAME_STATES.STARTING:

                // Once the screen is fully spawned
                if(screenController.IsIdle())
                {
                    // Begin the current mini game
                    miniGameState = MINI_GAME_STATES.IN_PROGRESS;
                    miniGameController.BeginGame();
                }

                break;

            case MINI_GAME_STATES.IN_PROGRESS:

                // Once the game is finished, begin the ending process
                if (miniGameController.IsFinished())
                {
                    miniGameState = MINI_GAME_STATES.FINISHING;
                }
                break;

            case MINI_GAME_STATES.FINISHING:

                // Despawn the scren
                screenController.FadeOut();
                screenController = null;

                // Move the camera back
                cameraController.SwapStates();

                // Wait for the next mini game to begin
                miniGameState = MINI_GAME_STATES.IDLE;

                break;

        }
    }

    // Spawn the screen object 
    private void SpawnScreen()
    {
        GameObject screen = Instantiate(screenObject, screenSpawn.position, screenSpawn.rotation);
        screenController = screen.GetComponent<ScreenController>();
    }

    public void BeginGame(GameObject gameControllerObject)
    {
        // If there isnt already a minigame running
        if(miniGameState == MINI_GAME_STATES.IDLE)
        {
            // Spawn the mini game controller for the specific object
            MiniGameController gameController = Instantiate(gameControllerObject,
                                                            miniGameControllerSpawn.position,
                                                            miniGameControllerSpawn.rotation).GetComponent<MiniGameController>();
            
            // Set the controller instance and move to the setup state
            miniGameController = gameController;

            miniGameState = MINI_GAME_STATES.SETUP;
        }
    }
}
