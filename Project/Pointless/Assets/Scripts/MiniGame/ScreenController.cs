using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{

    public float changeSpeed = 5f;
    public InputManager inputManager;

    private float targetAlpha = 0f;
    private float alphaMod = 1f;
    private SpriteRenderer spriteRenderer;

    private enum SCREEN_STATES
    {
        SPAWN,
        IDLE,
        FADE_OUT,
        DESPAWN
    }

    private SCREEN_STATES screenState = SCREEN_STATES.SPAWN;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Color temp = spriteRenderer.color;
        temp.a = targetAlpha;
        spriteRenderer.color = temp;
    }

    // Update is called once per frame
    void Update()
    {

        bool interact = inputManager.InteractPressed();
        Color temp = spriteRenderer.color;

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
                    screenState = SCREEN_STATES.DESPAWN;
                }

                break;

            // Once the opacity hits 0, despawn
            case SCREEN_STATES.DESPAWN:
                Destroy(gameObject);
                break;
        }

        // If the current opacity is not the target opacity, change the opacity
        if (temp.a != targetAlpha)
        {
            temp.a += (changeSpeed * alphaMod * Time.deltaTime);
            temp.a = Mathf.Clamp(temp.a, 0f, 1f);
            spriteRenderer.color = temp;
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
}
