using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{

    public float changeSpeed = 5f;
    public InputManager inputManager;
    public SpriteRenderer screenSpriteRenderer;

    private float targetAlpha = 0f;
    private float alphaMod = 1f;
    

    private enum SCREEN_STATES
    {
        SPAWN,
        IDLE,
        FADE_OUT,
        FINISHED
    }

    private SCREEN_STATES screenState = SCREEN_STATES.SPAWN;

    // Start is called before the first frame update
    void Start()
    {
        Color temp = screenSpriteRenderer.color;
        temp.a = targetAlpha;
        screenSpriteRenderer.color = temp;
    }

    // Update is called once per frame
    void Update()
    {

        bool interact = inputManager.InteractPressed();
        Color temp = screenSpriteRenderer.color;

        switch (screenState)
        {

            // When spawning in, set the target opacity to 1
            case SCREEN_STATES.SPAWN:
                
                targetAlpha = 1f;
                alphaMod = 1f;

                if (temp.a == 1f)
                {
                    screenState = SCREEN_STATES.IDLE;
                }

                break;
            
            // Wait for the interaction to despawn
            case SCREEN_STATES.IDLE:
                break;

            // Begin the fade-out for despawn
            case SCREEN_STATES.FADE_OUT:
                targetAlpha = 0f;
                alphaMod = -1f;

                if (temp.a == 0f)
                {
                    screenState = SCREEN_STATES.FINISHED;
                }

                break;

            // Once the opacity hits 0, mark the screen as ready to be despawned
            case SCREEN_STATES.FINISHED:
                break;
        }

        // If the current opacity is not the target opacity, change the opacity
        if (temp.a != targetAlpha)
        {
            temp.a += (changeSpeed * alphaMod * Time.deltaTime);
            temp.a = Mathf.Clamp(temp.a, 0f, 1f);
            screenSpriteRenderer.color = temp;
        }
    }

    public void FadeOut()
    {
        screenState = SCREEN_STATES.FADE_OUT;
    }

    public bool IsIdle()
    {
        return screenState == SCREEN_STATES.IDLE;
    }

    public bool IsFinished()
    {
        return screenState == SCREEN_STATES.FINISHED;
    }
}
