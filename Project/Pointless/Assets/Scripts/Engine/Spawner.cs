using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public bool singleUse;
    
    public float xMinBound = 0f;
    public float xMaxBound = 0f;
    public float yMinBound = 0f;
    public float yMaxBound = 0f;

    public float spawnRate = 1f;
    public float spawnPercentage = 100f;

    public GameObject spawnObject;

    protected GameObject lastSpawnedObject;
    protected bool spawned = false;

    private float timeSinceLastSpawn = 0f;

    protected enum SPAWN_STATES
    {
        ON,
        OFF
    }

    protected SPAWN_STATES spawnState = SPAWN_STATES.OFF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        spawned = false;

        switch (spawnState) {

            case SPAWN_STATES.ON:

                timeSinceLastSpawn += Time.deltaTime;

                if (timeSinceLastSpawn > spawnRate)
                {
                    timeSinceLastSpawn = 0;

                    if(Random.Range(1, 100) <=  spawnPercentage)
                    {
                        spawned = true;

                        float xPos =  Random.Range(xMinBound, xMaxBound);
                        float yPos =  Random.Range(yMinBound, yMaxBound);

                        lastSpawnedObject = Instantiate(spawnObject,
                                                        new Vector3(gameObject.transform.position.x + xPos,
                                                                    gameObject.transform.position.y + yPos, 0),
                                                        gameObject.transform.rotation);
                    }
                }

                break;
        }

    }

    public void BeginSpawn()
    {
        spawnState = SPAWN_STATES.ON;
    }

    public void EndSpawn()
    {
        spawnState = SPAWN_STATES.OFF;
    }

    public GameObject GetLastSpawned()
    {
        return lastSpawnedObject;
    }
}
