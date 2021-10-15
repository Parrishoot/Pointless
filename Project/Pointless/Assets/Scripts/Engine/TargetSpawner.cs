using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : Spawner
{

    
    // Update is called once per frame
    public override void Update()
    {

        base.Update();

        switch (spawnState) {

            case SPAWN_STATES.ON:

                if (spawned)
                {
                    GameObject lastObject = GetLastSpawned();
                    Target target = lastObject.GetComponent<Target>();

                    float currentPostition = gameObject.transform.position.x;

                    target.setEdgeForce(lastObject.transform.position.x,
                                        currentPostition + xMinBound,
                                        currentPostition + xMaxBound);
                }

                break;
        }

    }
}
