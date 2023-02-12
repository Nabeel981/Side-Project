using UnityEngine;

namespace TowerWar
{

    public class UnitBehavior : MonoBehaviour
    {
        public Unit thisUnit;
        public Unit thatUnit;
        public GameObject tugPointPrefab;

        public void UnitDecreaseHealth(Unit thatUnit, Collider collider)
        {
            if (this.thisUnit.civilization == thatUnit.civilization)
                return;
            if (this.thisUnit.unitHealth > 0)
                this.thisUnit.unitHealth -= thatUnit.unitHealth;
            if (this.thisUnit.unitHealth <= 0)
            {
                switch (this.thisUnit.unitType)
                {
                    case UnitType.melee:
                        this.thisUnit.unitHealth = 1;
                        break;
                    case UnitType.tank:
                        this.thisUnit.unitHealth = 2;
                        break;
                }
                ObjectPooler.instance.ReturnObjectToPool(this.thisUnit.gameObject);
            }
            if (thatUnit.unitHealth > 0)
                return;
            switch (thatUnit.unitType)
            {
                case UnitType.melee:
                    this.thisUnit.unitHealth = 1;
                    break;
                case UnitType.tank:
                    this.thisUnit.unitHealth = 2;
                    break;
            }
            ObjectPooler.instance.ReturnObjectToPool(collider.gameObject);
        }

        public bool AllowCollisionCheck(Unit thatUnit) => this.thisUnit.spawnPos == thatUnit.destinationpPos && this.thisUnit.destinationpPos == thatUnit.spawnPos && thatUnit.civilization != this.thisUnit.civilization;

        private void Awake() => this.thisUnit = this.GetComponent<Unit>();

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Unit>(out Unit _))
            {
                Unit component = collider.gameObject.GetComponent<Unit>();
                Debug.Log((object)"unit collided with another unit");
                if (!this.AllowCollisionCheck(component))
                    return;
                this.UnitDecreaseHealth(component, collider);
            }
            else
            {
                if (!collider.gameObject.TryGetComponent<ParentTowerBehaviour>(out ParentTowerBehaviour _))
                    return;
                Debug.Log((object)"unit collided with tower");
            }
        }

        public void ObstacleHitBehavior() => this.thisUnit.destinationpPos = this.thisUnit.unitStartingTower.transform.position;

        //public void TugPointInfo(TugPoint tugPoint)
        //{
        //    tugPoint.Towers.Add(this.thisUnit.unitStartingTower);
        //    tugPoint.Towers.Add(this.thisUnit.unitEndingTower);
        //    tugPoint.tugPath = this.thisUnit.onPath;
        //}
    }

}