using System.Collections.Generic;
using UnityEngine;


    public class Obstacles : PathableObjects
    {
        public ObstacleType obstacleType;
        public bool interactable;
        public int obstacleHealth;
        public int obstacleDestructionLevel;
        public Stack<GameObject> coinsOrLogs;
    }
