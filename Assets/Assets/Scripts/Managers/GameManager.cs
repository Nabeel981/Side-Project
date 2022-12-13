using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerWar
{
    public class GameManager : MonoBehaviour
    {
        public GameObject[] allTowers;
        public int green;
        public int red;
        public int blue;
        public int Win;
        public int Lose;
        public int fullSize;
        public int currentSize;
        public TextMeshProUGUI endMessage;
        public Dictionary<Civilization, int> playerCivAndIDs = new Dictionary<Civilization, int>();
        public List<int> OccupiedTowers;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            this.allTowers = GameObject.FindGameObjectsWithTag("Towers");
            this.fullSize = this.allTowers.Length + this.allTowers.Length + 1;
            this.currentSize = this.allTowers.Length + 1;
            this.Win = this.fullSize;
            if ((Object)GameManager.Instance == (Object)null)
                GameManager.Instance = this;
            else
                Object.Destroy((Object)this.gameObject);
        }

        private void Start()
        {
        }

        public void GameEndDecision()
        {
            this.endMessage.text = this.Win.ToString() + ":" + this.Lose.ToString() + ":" + this.currentSize.ToString() + ":" + LevelDetails.Instance.playerCivilization.ToString();
            Debug.Log((object)"gameend deciison called");
            if (this.currentSize == this.Win)
            {
                this.endMessage.text = "you win";
                Debug.Log((object)"you win");
                Time.timeScale = 0.0f;
            }
            else
            {
                if (this.currentSize != this.Lose)
                    return;
                this.endMessage.text = "you lose";
                Debug.Log((object)"you lose");
                Time.timeScale = 0.0f;
            }
        }

        public void FixTowersFromTowers()
        {
            foreach (GameObject allTower in this.allTowers)
            {
                this.FixTowerNumbers(allTower);
                this.endMessage.text = this.Win.ToString() + ":" + this.Lose.ToString() + ":" + this.currentSize.ToString();
            }
        }

        public void FixTowerNumbers(GameObject tower)
        {
            Debug.Log((object)"fix towers is called");
            ParentTowerClass component = tower.GetComponent<ParentTowerClass>();
            if (component.civilization == LevelDetails.Instance.playerCivilization)
                --this.Win;
            if (component.civilization == LevelDetails.Instance.playerCivilization)
                return;
            ++this.Lose;
        }

        public void GameLostDecision()
        {
            if (this.green == 0)
            {
                if (this.playerCivAndIDs.Count != 1)
                    return;
                ServernGameBridge.Instance.WonOrLost(false, this.playerCivAndIDs[Civilization.Green]);
            }
            else if (this.red == 0)
            {
                if (this.playerCivAndIDs.Count != 2)
                    return;
                ServernGameBridge.Instance.WonOrLost(false, this.playerCivAndIDs[Civilization.Red]);
            }
            else
            {
                if (this.blue != 0 || this.playerCivAndIDs.Count != 3)
                    return;
                ServernGameBridge.Instance.WonOrLost(false, this.playerCivAndIDs[Civilization.Blue]);
            }
        }

        public void GameWinDecision()
        {
            if (this.green == this.allTowers.Length)
                ServernGameBridge.Instance.WonOrLost(true, this.playerCivAndIDs[Civilization.Green]);
            else if (this.red == this.allTowers.Length)
            {
                ServernGameBridge.Instance.WonOrLost(true, this.playerCivAndIDs[Civilization.Red]);
            }
            else
            {
                if (this.blue != this.allTowers.Length)
                    return;
                ServernGameBridge.Instance.WonOrLost(true, this.playerCivAndIDs[Civilization.Blue]);
            }
        }

        public void CheckAllTowers()
        {
            this.green = 0;
            this.red = 0;
            this.blue = 0;
            foreach (GameObject allTower in this.allTowers)
            {
                ParentTowerClass component;
                if (allTower.TryGetComponent<ParentTowerClass>(out component))
                {
                    switch (component.civilization)
                    {
                        case Civilization.Green:
                            ++this.green;
                            continue;
                        case Civilization.Red:
                            ++this.red;
                            continue;
                        case Civilization.Blue:
                            ++this.blue;
                            continue;
                        default:
                            continue;
                    }
                }
            }
            this.endMessage.text = "green:" + this.green.ToString() + "Red:" + this.red.ToString() + "Blue:" + this.blue.ToString();
            this.GameLostDecision();
            this.GameWinDecision();
        }
    }
}
