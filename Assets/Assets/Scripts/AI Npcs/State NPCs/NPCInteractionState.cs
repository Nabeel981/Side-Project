using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionState : NPCState
{
    public Decision decision;
    public Animation Animation;


    public override NPCState RunCurrentState()
    {
        ///check which state to return
        return this;
    }

   


    public void DoInteraction()
    {


        //when done change state to idle

    }
}
