using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPracticeMiniGameController : MiniGameController
{
    /*
     * 
     * An aiming minigaming where you have to shoot down targets
     * 
     */


    public GameObject clickableSquareObject;
    public Spawner goodTargetSpawner;
    public Spawner badTargetSpawner;

    // Update is called once per frame
    public override void Update()
    {

        base.Update();

        switch (miniGameControllerState)
        {

            case MINI_GAME_CONTROLLER_STATES.STARTING:

                // Once the game has started, set the game to in progress
                miniGameControllerState = MINI_GAME_CONTROLLER_STATES.IN_PROGRESS;

                // Spawn the clickable square object in the spawn location defined in the controller prefab
                goodTargetSpawner.BeginSpawn();
                badTargetSpawner.BeginSpawn();

                break;

            case MINI_GAME_CONTROLLER_STATES.IN_PROGRESS:

                // Once the square is clicked, the game is "won" and we can end
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    goodTargetSpawner.EndSpawn();
                    badTargetSpawner.EndSpawn();
                    miniGameControllerState = MINI_GAME_CONTROLLER_STATES.TEARDOWN;
                }

                break;

            case MINI_GAME_CONTROLLER_STATES.FINISHED:

                // When the game is over, destroy the objects
                Destroy(gameObject);

                break;

        }
    }
}
