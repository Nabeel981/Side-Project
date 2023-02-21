using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GatheingInteractionState : NPCInteractionState
{
  public  bool taskDone = false;
    bool start = true;
    public NPCIdleState idleState;
    public float timecheck = 0;

    private void Start()
    {
        taskDone = false;
        start = true;
    }
    public override  NPCState RunCurrentState()
    {
        ///check which state to return
        ///  StartCoroutine(GatheringFood());
        ///  
        if (start)
        {
            taskDone = false;

            StartCoroutine(GatheringFood());
            start = false;
           
        }
        
        if (taskDone)
        {
            // taskDone = false;
            start = true;
            
            return idleState;
          
        }
        else
        {
            return this;
          //  return new NPCIdleState();

        }
    }

public IEnumerator  GatheringFood()
    {
        Debug.Log("im gathering");
       
        yield return new WaitForSeconds(5);
        taskDone = true;   
    }

    public void HasTimePassed( )
    {
        timecheck += Time.deltaTime;
        if (timecheck > 5)
        {
            
        }

    }
}
