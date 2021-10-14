using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameInteractable : Interactable
{
    /*
     * 
     * Specific interactable child class
     * for minigame instantiation on 
     * interact
     * 
     */

    public InputManager inputManager;
    public MiniGameManager miniGameManager;

    public GameObject miniGameControllerObject;

    public void Update()
    {
        // If the interact key is pressed, call the interact override
        // TODO: Add trigger on Player to only trigger this if within range
        if (Input.GetKeyDown(inputManager.interactKey))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        // Begin the minigame using the specific controller for this object
        miniGameManager.BeginGame(miniGameControllerObject);
    }

}
