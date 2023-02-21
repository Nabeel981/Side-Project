using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class NPCInteractionState : NPCState , IStateMachineManager
{
    public NPCState CurrentState;
  //  public List<NPCState> States;
    public GatheingInteractionState gatheing;
    public bool toskDone = false;
    bool stort = true;
  //  public Decision decision;
   // public Animation Animation;
    //public NpcINteractionStateManager npcINteractionStateManager;

    private void Start()
    {
       
        
    }
    public override NPCState RunCurrentState()
    {
        ///check which state to return
        ///




        if (stort)
        {
            toskDone = false;

            RunStateMachine();
            stort = false;

        }

        if (toskDone)
        {
            // taskDone = false;
            stort = true;

            return CurrentState;

        }
        else
        {
            return this;
            //  return new NPCIdleState();

        }
    }

   


    public void DoInteraction()
    {


        //when done change state to idle

    }







    public void RunStateMachine()
    {
        NPCState nextState = CurrentState?.RunCurrentState();
        if (nextState == null)
        {
            nextState = gatheing;
        }

        if (nextState != null) SwitchToNextState(nextState);
       
    }
    public NPCState ChooseInteractionType()
    {
        // this.GetComponent<NPCstateManager>().owner = UnitOwner.Player;
        //switch (this.GetComponent<NPCstateManager>().owner)
        //{

        //    case UnitOwner.Player:

        //        return 
        //        break;

        //    case UnitOwner.Ai:


        //        break;



        //}

        return new GatheingInteractionState();
    }
    public void SwitchToNextState(NPCState nextState)
    {
        CurrentState = nextState;
        toskDone = true;

    }

    public void StopStateMachine()
    {
        throw new System.NotImplementedException();
    }
}
