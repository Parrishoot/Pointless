using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour 
{
    // Setting up my KeyCodes in this one manager for 
    // ease of use
    public KeyCode interactKey = KeyCode.E;
    public KeyCode gameKey = KeyCode.Q;


    public bool InteractPressed()
    {
        return Input.GetKeyDown(interactKey);
    }

}
