using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform roomDock;
    public Transform screenDock;
    public float cameraMoveSpeed = 5f;
    public InputManager inputManager;

    private Transform cameraTransform;
    private Transform targetTransform;

    private enum CAMERA_STATES
    {
        ROOM,
        SCREEN
    }

    private CAMERA_STATES cameraState = CAMERA_STATES.ROOM; 

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        targetTransform = roomDock;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current and target X coord locations
        float currentX = cameraTransform.position.x;
        float targetX = targetTransform.position.x;

        // Gradually approach that new location
        cameraTransform.position = new Vector3(currentX + (targetX - currentX) * cameraMoveSpeed * Time.deltaTime,
                                               cameraTransform.position.y,
                                               cameraTransform.position.z);
    }

    public void SwapStates()
    {
        // Set the target location to be either the room or the screen
        switch (cameraState)
        {
            case CAMERA_STATES.ROOM:
                targetTransform = screenDock;
                cameraState = CAMERA_STATES.SCREEN;
                break;

            case CAMERA_STATES.SCREEN:
                targetTransform = roomDock;
                cameraState = CAMERA_STATES.ROOM;
                break;
        }
    }
}
