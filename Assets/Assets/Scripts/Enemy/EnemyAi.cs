using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerWar
{
    public class EnemyAi : MonoBehaviour
    {
        [HideInInspector]
        private GameObject[] allTowers;
        public GameObject[] nearest;
        public NonRangedTowerClass nonRangedTowerClass;

        private void Start()
        {
            this.nonRangedTowerClass = this.GetComponent<NonRangedTowerClass>();
            this.InvokeRepeating("GetClosestEnemy", (float)UnityEngine.Random.Range(1, 3), 1f);
        }

        public void GetClosestEnemy()
        {
            if (!this.AvailablePaths())
                return;
            this.allTowers = GameManager.Instance.allTowers;
            this.nearest = ((IEnumerable<GameObject>)this.allTowers).OrderBy<GameObject, float>((Func<GameObject, float>)(go => (this.transform.position - go.transform.position).sqrMagnitude)).ToArray<GameObject>();
            this.CivilizationCheck(this.gameObject, this.nearest);
        }

        public void DefencivePath(NonRangedTowerClass thisTower, NonRangedTowerClass thatTower)
        {
            Debug.Log((object)thisTower.selfTower);
            for (int index = 0; index < thisTower.targetTowers.Count; ++index)
            {
                GameObject targetTower = thisTower.targetTowers[index];
                if ((bool)(UnityEngine.Object)thatTower.selfTower)
                    return;
            }
            int fromTower = LevelDetails.Instance.Towers.IndexOf(this.gameObject);
            int toTower = LevelDetails.Instance.Towers.IndexOf(thatTower.selfTower);
            if (!PathMaker.PathHurdlesCheck(this.gameObject, thatTower.selfTower))
                return;
            PathMaker.Instance.CreatePath(fromTower, toTower);
        }

        public void CivilizationCheck(GameObject gameObject, GameObject[] nearest)
        {
            Debug.Log((object)"civlization check");
            foreach (GameObject endingTower in nearest)
            {
                NonRangedTowerClass component1 = gameObject.GetComponent<NonRangedTowerClass>();
                if (!((UnityEngine.Object)endingTower == (UnityEngine.Object)gameObject))
                {
                    NonRangedTowerClass component2;
                    if (endingTower.TryGetComponent<NonRangedTowerClass>(out component2))
                    {
                        if (!component1.targetTowers.Contains(component2.selfTower) && !component2.targetTowers.Contains(component1.selfTower))
                        {
                            int fromTower = LevelDetails.Instance.Towers.IndexOf(this.gameObject);
                            int toTower = LevelDetails.Instance.Towers.IndexOf(endingTower);
                         //   if (ServernGameBridge.Instance.onlineGame)
                              //  ServernGameBridge.Instance.MakePathOnServer(fromTower, toTower);
                           // else if (PathMaker.PathHurdlesCheck(this.gameObject, endingTower))
                                PathMaker.Instance.CreatePath(fromTower, toTower);
                        }
                    }
                    else
                    {
                        ObjectDefiner component3;
                        if (endingTower.TryGetComponent<ObjectDefiner>(out component3) && component3.pathType == PathType.pathable)
                        {
                            int fromTower = LevelDetails.Instance.Towers.IndexOf(this.gameObject);
                            int toTower = LevelDetails.Instance.Towers.IndexOf(endingTower);
                          //  ServernGameBridge.Instance.MakePathOnServer(fromTower, toTower);
                            Debug.Log((object)" path made ");
                           // if (ServernGameBridge.Instance.onlineGame)
                           //     ServernGameBridge.Instance.MakePathOnServer(fromTower, toTower);
                         //   else if (PathMaker.PathHurdlesCheck(this.gameObject, endingTower))
                                PathMaker.Instance.CreatePath(fromTower, toTower);
                        }
                    }
                }
            }
        }

        public bool AvailablePaths() => this.nonRangedTowerClass.outgoingPaths < this.nonRangedTowerClass.level;

        public float DifficultySet() => 2f;
    }
}