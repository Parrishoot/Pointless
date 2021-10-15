using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController: MonoBehaviour
{
    /*
     * 
     * Top level minigame controller parent class
     * 
     */
    public InputManager inputManager;
    public ScreenController screenController;

    // To keep track of the current mini game state
    public enum MINI_GAME_CONTROLLER_STATES
    {
        SETUP,
        STARTING,
        IN_PROGRESS,
        TEARDOWN,
        FINISHING,
        FINISHED
    }

    // Set the starting state to IDLE
    public MINI_GAME_CONTROLLER_STATES miniGameControllerState = MINI_GAME_CONTROLLER_STATES.SETUP;

    public virtual void Update()
    {
        switch(miniGameControllerState)
        {
            case MINI_GAME_CONTROLLER_STATES.SETUP:

                if (screenController.IsIdle())
                {
                    miniGameControllerState = MINI_GAME_CONTROLLER_STATES.STARTING;
                }
                break;

            case MINI_GAME_CONTROLLER_STATES.TEARDOWN:

                screenController.FadeOut();
                miniGameControllerState = MINI_GAME_CONTROLLER_STATES.FINISHING;
                break;

            case MINI_GAME_CONTROLLER_STATES.FINISHING:

                if (screenController.IsFinished())
                {
                    miniGameControllerState = MINI_GAME_CONTROLLER_STATES.FINISHED;
                }
                break;

        }
    }

    // Once the game begins, set the state to starting
    // (The rest of the game logic will take place in the child objects)
    public void BeginGame()
    {
        miniGameControllerState = MINI_GAME_CONTROLLER_STATES.STARTING;
    }

    // Return true when the game state is finished
    public bool IsFinished()
    {
        return miniGameControllerState == MINI_GAME_CONTROLLER_STATES.TEARDOWN;
    }
}
