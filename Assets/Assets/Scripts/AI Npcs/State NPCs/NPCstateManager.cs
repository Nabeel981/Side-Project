using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCstateManager : MonoBehaviour , IStateMachineManager
{
    public UnitOwner owner;
    NPCState CurrentState;
    



    void Update()
    {
        RunStateMachine();
    }
    public void RunStateMachine()
    {
      NPCState  nextState = CurrentState?.RunCurrentState();

        if (nextState != null) SwitchToNextState(nextState);

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
