using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class NPCstateManager : MonoBehaviour, IStateMachineManager
{
    public UnitOwner owner;
    public NPCState CurrentState;
    public Animator thisAnimator;



    void Update()
    {

        RunStateMachine();
    }
    public void RunStateMachine()
    {
        NPCState nextState = CurrentState?.RunCurrentState();
        if (nextState == null)
        {

            nextState = CurrentState;
        }
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

    public void HandleAnimations()
    {
        if (thisAnimator.GetBool("ismoving") == true)
        {
            thisAnimator.SetBool("ismoving", false);
        }

        thisAnimator.SetBool("ismoving", false);
    }

}

