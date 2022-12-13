using UnityEngine;


    public class ParentTowerBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public ParentTowerClass thisTower;
        [HideInInspector]
        public Unit unit;
        [HideInInspector]
        public ParentTowerClass thatTower;
        [HideInInspector]
        public bool allowPathRemoval;
        [HideInInspector]
        public Civilization playerCivilization;

        private void Awake()
        {
            this.allowPathRemoval = false;
            this.SetLevel();
            this.thisTower = this.gameObject.GetComponent<ParentTowerClass>();
        }

        private void Start() => this.playerCivilization = LevelDetails.Instance.playerCivilization;

        public void SetLevel()
        {
            this.thisTower = this.gameObject.GetComponent<ParentTowerClass>();
            this.thisTower.level = this.thisTower.towerHealth < 0 || this.thisTower.towerHealth > 10 ? this.thisTower.level : (this.thisTower.level = 1);
            this.thisTower.level = this.thisTower.towerHealth < 20 || this.thisTower.towerHealth > 30 ? this.thisTower.level : (this.thisTower.level = 2);
            this.thisTower.level = this.thisTower.towerHealth < 30 || this.thisTower.towerHealth > this.thisTower.maxHealth ? this.thisTower.level : 3;
            this.ChangeTowerMaterial(this.thisTower.civilization);
        }

        public void IncreaseHealth(Unit unit, ParentTowerClass thistower)
        {
            Debug.Log((object)"health increased");
            this.thisTower.towerHealth = this.thisTower.towerHealth == this.thisTower.maxHealth ? this.thisTower.maxHealth : (this.thisTower.towerHealth += unit.unitHealth);
            this.SetLevel();
            NonRangedTowerBehavior component;
            if (this.thisTower.towerHealth != this.thisTower.maxHealth || !this.thisTower.selfTower.TryGetComponent<NonRangedTowerBehavior>(out component))
                return;
            component.MaxLevelCollidingBehaviour();
        }

        public void DecreaseHealth(Unit unit, ParentTowerClass thisTower)
        {
            Debug.Log((object)"health  decreased");
            if (thisTower.towerHealth > 0)
            {
                thisTower.towerHealth -= unit.unitHealth;
            }
            else
            {
                if (thisTower.towerHealth > 0)
                    return;
                if (ServernGameBridge.Instance.onlineGame)
                    ServernGameBridge.Instance.ChangeTowerCivilizationOnServer(LevelDetails.Instance.Towers.IndexOf(this.gameObject), (int)unit.civilization);
                else
                    this.ConvertTower(unit.civilization);
            }
        }

        public void ConvertTower(Civilization toCivilization)
        {
            if (ServernGameBridge.Instance.onlineGame)
            {
                PathMaker.Instance.RemoveAllOutgoingPathsOnConversion(this.gameObject);
                this.thisTower.civilization = toCivilization;
                this.thisTower.towerHealth = 0;
                this.thisTower.GetComponent<ObjectDefiner>().civilization = toCivilization;
                this.ChangeTowerMaterial(toCivilization);
            }
            else if (this.thisTower.civilization == LevelDetails.Instance.playerCivilization)
            {
                if (this.thisTower.civilization == toCivilization)
                    return;
                if (this.thisTower.transform.TryGetComponent<NonRangedTowerBehavior>(out NonRangedTowerBehavior _))
                {
                    PathMaker.Instance.RemoveAllOutgoingPathsOnConversion(this.gameObject);
                    if (!ServernGameBridge.Instance.onlineGame)
                    {
                        if ((bool)(Object)this.thisTower.GetComponent<EnemyAi>())
                            this.thisTower.GetComponent<EnemyAi>().enabled = true;
                        else
                            this.thisTower.gameObject.AddComponent<EnemyAi>();
                    }
                }
                this.thisTower.civilization = toCivilization;
                this.thisTower.towerHealth = 0;
                this.thisTower.GetComponent<ObjectDefiner>().civilization = toCivilization;
                this.ChangeTowerMaterial(toCivilization);
            }
            else
            {
                if (this.thisTower.civilization == LevelDetails.Instance.playerCivilization || toCivilization != LevelDetails.Instance.playerCivilization)
                    return;
                if (this.thisTower.transform.TryGetComponent<NonRangedTowerBehavior>(out NonRangedTowerBehavior _))
                {
                    PathMaker.Instance.RemoveAllOutgoingPathsOnConversion(this.gameObject);
                    if ((bool)(Object)this.thisTower.GetComponent<EnemyAi>())
                        this.thisTower.GetComponent<EnemyAi>().enabled = false;
                }
                this.thisTower.civilization = toCivilization;
                this.thisTower.towerHealth = 0;
                this.thisTower.GetComponent<ObjectDefiner>().civilization = toCivilization;
                this.ChangeTowerMaterial(toCivilization);
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log((object)"tower detected unit");
            Unit component;
            if (!collider.transform.TryGetComponent<Unit>(out component))
                return;
            Debug.Log((object)"tower detected  component");
            this.thisTower = this.gameObject.GetComponent<ParentTowerClass>();
            if (!((Object)this.thisTower.selfTower == (Object)component.unitEndingTower))
                return;
            Debug.Log((object)"ending tower tower detected  component");
            if (this.thisTower.civilization == component.civilization)
            {
                if (component.unitType == UnitType.Archer)
                    return;
                Debug.Log((object)"health increase funciton called");
                this.IncreaseHealth(component, this.thisTower);
                ObjectPooler.instance.ReturnObjectToPool(collider.gameObject);
            }
            else
            {
                Debug.Log((object)"health decrease funciton called");
                ObjectPooler.instance.ReturnObjectToPool(collider.gameObject);
                this.DecreaseHealth(component, this.thisTower);
            }
            collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public void ChangeTowerMaterial(Civilization civilization)
        {
            if (this.thisTower.civilization == Civilization.none)
                return;
            switch (civilization)
            {
                case Civilization.Green:
                    this.thisTower.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
                case Civilization.Red:
                    this.thisTower.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                case Civilization.Blue:
                    this.thisTower.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;
                case Civilization.yellow:
                    this.thisTower.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    break;
            }
        }

        public void StatsUpdate(bool civilize)
        {
            if (civilize)
                --GameManager.Instance.currentSize;
            else
                ++GameManager.Instance.currentSize;
        }
    }

