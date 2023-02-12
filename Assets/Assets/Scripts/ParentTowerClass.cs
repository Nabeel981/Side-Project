using UnityEngine;

namespace TowerWar
{

    [RequireComponent(typeof(ObjectDefiner))]
    public class ParentTowerClass : PathableObjects
    {
        [HideInInspector]
        public ObjectDefiner objDefiner;
        public int level;
        public int towerHealth;
        public TowerType towerType;
        public Civilization civilization;
        public GameObject prefab;
        public GameObject selfTower;
        public int maxHealth = 64;
        public PathInfo[] paths;
        public int maxHealthCollisions;

        public void Awake()
        {
            this.objDefiner = this.GetComponent<ObjectDefiner>();
            this.paths = new PathInfo[3];
            this.selfTower = this.gameObject;
        }
    }

}