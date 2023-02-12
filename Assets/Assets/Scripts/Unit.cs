using UnityEngine;

namespace TowerWar
{

    public class Unit : MonoBehaviour
    {
        public Vector3 spawnPos;
        public Vector3 destinationpPos;
        public UnitType unitType;
        public Civilization civilization;
        public int unitHealth;
        public GameObject onPath;
        public GameObject unitStartingTower;
        public GameObject unitEndingTower;
        public bool makeTugPoint;

        private void Awake()
        {
            switch (this.unitType)
            {
                case UnitType.melee:
                    this.unitHealth = 1;
                    break;
                case UnitType.tank:
                    this.unitHealth = 2;
                    break;
                case UnitType.Archer:
                    this.unitHealth = 1;
                    break;
            }
        }

       // private void OnEnable() => ServernGameBridge.Instance.MovethistoScene(this.gameObject);

        public void Start()
        {
        }

        private void FixedUpdate()
        {
            if (this.unitType == UnitType.Archer || this.unitType == UnitType.Cannon)
                return;
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.destinationpPos, 1f * Time.deltaTime);
        }

        private void OnDisable() => this.makeTugPoint = false;
    }
}