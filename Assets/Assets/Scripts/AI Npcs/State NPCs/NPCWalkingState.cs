using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkingState : MonoBehaviour
{
    public Vector3 currentPos, previousPos, randomMovementSpot;

    public NPCInteractionState interactionState; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public NPCState PossibleInteractionInRange()
    {
        return interactionState;

    }

    public void patrolling()
    {

        if (currentPos == previousPos)
        {


            randomMovementSpot = new Vector3(transform.position.x + Random.Range(.05f, 1.5f), transform.position.y, transform.position.z + Random.Range(.05f, 1.5f));
        }
        transform.LookAt(randomMovementSpot);
        currentPos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, randomMovementSpot, .05f);
        previousPos = transform.position;
    }
}
