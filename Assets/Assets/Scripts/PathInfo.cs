using UnityEngine;


    public class PathInfo : MonoBehaviour
    {
        [SerializeField]
        public GameObject endingTower;
        [SerializeField]
        public GameObject startingTower;
        public int enable;
        [HideInInspector]
        public ObjectPooler objectPooler;
        public float spawnTimer;
        public bool hasTugpoint;
        public GameObject tugPoint;
        public Civilization civilization;
        public float speed;
        public GameObject selfPath;
        public Unit movingUnit;

        private void Start()
        {
            this.selfPath = this.gameObject;
            this.InvokeRepeating("PoolMaker", 1f, this.speed = this.MoveunitSpeed());
        }

        public void PoolMaker()
        {
            NonRangedTowerClass component;
            if (!this.startingTower.TryGetComponent<NonRangedTowerClass>(out component) || component.outgoingPaths <= 0)
                return;
            GameObject prefab = component.prefab;
            this.movingUnit = ObjectPooler.instance.StartSpawning(prefab, this.startingTower).GetComponent<Unit>();
            if (component.towerType == TowerType.archerTower || component.towerType == TowerType.archerTower || component.towerType == TowerType.archerTower || component.towerType == TowerType.archerTower)
                return;
            this.movingUnit.spawnPos = this.startingTower.transform.position;
            this.movingUnit.onPath = this.gameObject;
            this.movingUnit.destinationpPos = this.endingTower.transform.position;
            this.movingUnit.unitStartingTower = this.startingTower;
            this.movingUnit.unitEndingTower = this.endingTower;
            this.ChangeUnitPrefab(component.civilization, this.movingUnit);
            if (component.maxHealthCollisions > 0 && component.GetComponent<ObjectDefiner>().pathType == PathType.pathmaker)
            {
                --component.maxHealthCollisions;
                this.Invoke("poolMaker", 0.5f);
            }
            this.speed = this.MoveunitSpeed();
            Debug.Log((object)this.speed);
        }

        public void ChangeUnitPrefab(Civilization civilization, Unit unit)
        {
            switch (civilization)
            {
                case Civilization.Green:
                    unit.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
                case Civilization.Red:
                    unit.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                case Civilization.Blue:
                    unit.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;
                case Civilization.yellow:
                    unit.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
                case Civilization.none:
                    return;
            }
            unit.GetComponent<Unit>().civilization = civilization;
            unit.GetComponent<ObjectDefiner>().civilization = civilization;
        }

        public float MoveunitSpeed()
        {
            switch (this.startingTower.GetComponent<NonRangedTowerClass>().level)
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

        public void TugPointMaker() => this.movingUnit.makeTugPoint = true;
    }
