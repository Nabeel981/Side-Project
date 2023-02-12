using UnityEngine;

namespace TowerWar
{

    public class ObstacleBehavior : MonoBehaviour
    {
        public Obstacles thisObstacle;

        private void Awake() => this.thisObstacle = this.gameObject.GetComponent<Obstacles>();

        private void Start()
        {
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!(bool)(Object)collider.GetComponent<Unit>())
                return;
            this.ReduceUnitHealthandobject(collider.GetComponent<Unit>());
        }

        public void ReduceUnitHealthandobject(Unit thisUnit)
        {
            this.thisObstacle.obstacleHealth -= thisUnit.unitHealth;
            this.PickObstacle(thisUnit);
        }

        public void PickObstacle(Unit thisUnit)
        {
            this.thisObstacle.coinsOrLogs.Peek().transform.parent = thisUnit.transform;
            this.thisObstacle.coinsOrLogs.Pop();
        }

        public void DestroyObstacle()
        {
            if (this.thisObstacle.obstacleHealth != 0)
                return;
            Object.Destroy((Object)this.gameObject);
        }
    }

}