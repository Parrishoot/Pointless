using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    /*
     * 
     * Top Level Interactable parent class
     * 
     */
    public virtual void Interact()
    {
        Debug.Log("Interacted!");
    }

}
