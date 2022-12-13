using System.Collections.Generic;
using UnityEngine;

    public class LevelDetails : MonoBehaviour
    {
        public int levelNumber = 1;
        public int totalTowers = 2;
        public int totalCivilizations = 2;
        public int totalPLayers = 2;
        public Civilization playerCivilization;
        public List<GameObject> Towers;

        public static LevelDetails Instance { get; private set; }

        private void OnEnable()
        {
            if ((Object)LevelDetails.Instance == (Object)null)
                LevelDetails.Instance = this;
            else
                Object.Destroy((Object)this.gameObject);
        }

        private void Awake()
        {
        }

        public void AiBehavior()
        {
            if (!ServernGameBridge.Instance.onlineGame)
                return;
            foreach (GameObject tower in this.Towers)
            {
                EnemyAi component;
                if (tower.TryGetComponent<EnemyAi>(out component))
                    component.enabled = false;
            }
        }
    }

