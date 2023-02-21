using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    public NPCWalkingState walkingState;
    public bool FindNextPosition;
    public override NPCState RunCurrentState()
    {
        //code needs to be here?
        return walkingState;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
