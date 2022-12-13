
    public class RangedTowerBehaviorParent : ParentTowerBehaviour
    {
        public RangedTowerClassParent thisRangedTower;
        public float speed;
        public float towerRange = 100f;

        private void Awake() => this.thisRangedTower = this.GetComponent<RangedTowerClassParent>();

        public float MoveunitSpeed()
        {
            switch (this.gameObject.GetComponent<RangedTowerClassParent>().level)
            {
                case 1:
                    return this.speed = 3f;
                case 2:
                    return this.speed = 2.2f;
                case 3:
                    return this.speed = 1.4f;
                default:
                    return this.speed = 0.1f;
            }
        }

        public void SetRange()
        {
            switch (this.thisRangedTower.level)
            {
                case 1:
                    this.towerRange = 2f;
                    break;
                case 2:
                    this.towerRange = 3f;
                    break;
                case 3:
                    this.towerRange = 4f;
                    break;
            }
        }
    }

