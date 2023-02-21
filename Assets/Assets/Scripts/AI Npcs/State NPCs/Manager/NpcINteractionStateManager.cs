using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcINteractionStateManager : MonoBehaviour, IStateMachineManager
{
    NPCState CurrentState;
   public List<NPCState> States;


    void Update()
    {
        RunStateMachine();
    }
    public void RunStateMachine()
    {
        NPCState nextState = CurrentState?.RunCurrentState();

        if (nextState != null) SwitchToNextState(nextState);

    }
    public void ChooseInteractionType()
    {
        // this.GetComponent<NPCstateManager>().owner = UnitOwner.Player;
        switch (this.GetComponent<NPCstateManager>().owner)
        {

            case UnitOwner.Player:


                break;

            case UnitOwner.Ai:


                break;



        }


    }
    public void SwitchToNextState(NPCState nextState)
    {
        CurrentState = nextState;

    }

    public void StopStateMachine()
    {
        throw new System.NotImplementedException();
    }
}
