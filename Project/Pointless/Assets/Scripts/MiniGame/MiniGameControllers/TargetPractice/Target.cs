using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    /*
     * 
     * Simple clickable square object for testing the
     * minigame controller implementation
     * 
     */

    public GameObject squareSpriteObject;
    public float fadeSpeed;

    public float minXForce = 0f;
    public float maxXForce = 0f;

    public float minYForce = 0f;
    public float maxYForce = 0f;

    public enum SQUARE_STATES
    {
        SPAWN,
        UNCLICKED,
        FADE_OUT,
        CLICKED
    }

    public SQUARE_STATES squareState = SQUARE_STATES.SPAWN;

    private float rotationXSpeed;
    private float rotationZSpeed;
    private float rotationYSpeed;

    private float maxRotationSpeed = 400f;
    private float minRotationSpeed = 500f;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D squareRigidbody;

    public void Start()
    {
        rotationXSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        rotationYSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        rotationZSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);

        spriteRenderer = squareSpriteObject.GetComponent<SpriteRenderer>();

        squareRigidbody = GetComponent<Rigidbody2D>();
        squareRigidbody.AddForce(new Vector2(Random.Range(minXForce, maxXForce),
                                             Random.Range(minYForce, maxYForce)),
                                             ForceMode2D.Impulse);
    }

    public void Update()
    {
        switch (squareState)
        {
            case SQUARE_STATES.SPAWN:

                squareState = SQUARE_STATES.UNCLICKED;

                break;

            case SQUARE_STATES.UNCLICKED:

                // If the Target is offscreen, destroy it
                Vector3 worldPos = Camera.main.WorldToViewportPoint(transform.position);

                if (worldPos.y < 0.0f)
                {
                    Destroy(gameObject);
                }

                break;

            case SQUARE_STATES.FADE_OUT:

                // Rotate the object
                gameObject.transform.Rotate(rotationXSpeed * Time.deltaTime, rotationYSpeed * Time.deltaTime, rotationZSpeed * Time.deltaTime);

                // Slowly fade Out
                Color temp = spriteRenderer.color;
                temp.a -= (fadeSpeed * Time.deltaTime / 10);
                temp.a = Mathf.Max(temp.a, 0);
                spriteRenderer.color = temp;

                // Scale down with fade
                spriteRenderer.transform.localScale = new Vector3(Mathf.Min(temp.a * 2, 1), 
                                                                  Mathf.Min(temp.a * 2, 1), 
                                                                  1);

                // Once the object is no longer visible, move to final state
                if (temp.a == 0f)
                {
                    squareState = SQUARE_STATES.CLICKED;
                }

                break;

        }
    }

    public void OnMouseDown()
    {
        squareRigidbody.AddForce(new Vector2(0f, -400f), ForceMode2D.Impulse);
        squareState = SQUARE_STATES.FADE_OUT;
    }

    public bool IsClicked()
    {
        return squareState == SQUARE_STATES.CLICKED;
    }

    // Quick and dirty solution for handling targets that spawn too close to the edge
    // This is to cheat the random launch direction and make sure that they launch
    // Towards the center
    public void setEdgeForce(float xPos, float xMinPos, float xMaxPos)
    {
        float totalLength = xMaxPos - xMinPos;

        // If they're close to the left edge, don't launch left
        if (Mathf.Abs(xPos - xMinPos) / totalLength < .25f)
        {
            minXForce = 0f;
        }

        // Same for the right edge
        else if (Mathf.Abs(xPos - xMaxPos) / totalLength < .25f)
        {
            maxXForce = 0f;
        }
    }
}
