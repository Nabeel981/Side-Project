
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsManager : MonoBehaviour
{
    public static NPCsManager instance;
   
    /// spawn npcs 
    /// set a moving routine for them 
    /// despawn when they walk out of the map
    /// 
    ///fun idea 
    ///characters can be sspawned  on waves <summary>
    /// spawn npcs 
    /// </summary>
    public GameObject[] NpcPrefabs;

    private void OnEnable()
    {
        if(instance == null) instance = this;   
    }
    private void Awake()
    {
       
    }

 



    public void SpawnNpcs()
    {


    }

    public void DespawnNpcs()
    {


    }
    public void SetMovement()
    {


    }


}
