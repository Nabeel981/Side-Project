using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCState : MonoBehaviour
{

    public abstract  NPCState RunCurrentState();
    
}
