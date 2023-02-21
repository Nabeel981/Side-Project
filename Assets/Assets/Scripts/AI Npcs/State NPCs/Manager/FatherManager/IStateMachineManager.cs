using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachineManager 
{
    public void RunStateMachine();

    public void StopStateMachine();
    public void SwitchToNextState(NPCState nextState);
}
