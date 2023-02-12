using System.Collections.Generic;
using UnityEngine;
namespace TowerWar{

    public class NonRangedTowerClass : ParentTowerClass
    {
        public int outgoingPaths;
        public List<GameObject> targetTowers;
        public int enable;
    }
}