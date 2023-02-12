using UnityEngine;

namespace TowerWar { 
    public class NonRangedTowerBehavior : ParentTowerBehaviour
{
    public NonRangedTowerClass thisNonRangedTower;
    public PathableObjects thisPathableObject;
    [HideInInspector]
    public ObjectPooler objectPooler;

    private void Awake()
    {
        this.SetLevel();
        this.thisNonRangedTower = this.gameObject.GetComponent<NonRangedTowerClass>();
        this.thisPathableObject = this.gameObject.GetComponent<PathableObjects>();
        this.InvokeRepeating("IdleTower", 0.0f, 3f);
    }

    public void IdleTower()
    {
        this.thisPathableObject = this.gameObject.GetComponent<PathableObjects>();
        if (this.thisNonRangedTower.outgoingPaths != 0 || this.thisPathableObject.incomingPaths != 0)
            return;
        if (this.thisNonRangedTower.towerHealth >= this.thisTower.maxHealth)
        {
            int towerHealth = this.thisNonRangedTower.towerHealth;
        }
        else
            this.IdleIncreaseHealth();
    }

    private int IdleIncreaseHealth()
    {
        ++this.thisNonRangedTower.towerHealth;
        this.SetLevel();
        return this.thisNonRangedTower.towerHealth;
    }

    public void ResetPathsOnConversion()
    {
        this.thisNonRangedTower.outgoingPaths = 0;
        this.thisNonRangedTower.targetTowers.Clear();
    }

    public int MaxLevelCollidingBehaviour()
    {
        if (this.thisNonRangedTower.outgoingPaths <= 0)
            return this.thisNonRangedTower.maxHealthCollisions;
        return this.thisNonRangedTower.maxHealthCollisions++;
    }
}
}