using System.Collections.Generic;
using TMPro;
using UnityEngine;


    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler instance;
        private List<GameObject> ObjectPool;
        public GameObject _melee;
        public TextMeshProUGUI totalUnits;
        public int unitsSize;
        public int _size = 1;

        public void Start() => this.ObjectPoolerGlobal();

        private void Update()
        {
        }

        public GameObject StartSpawning(GameObject prefab, GameObject startingTower)
        {
            GameObject objectFromPool = this.GetObjectFromPool(prefab);
            objectFromPool.transform.position = startingTower.transform.position;
            return objectFromPool;
        }

        public void ObjectPoolerGlobal()
        {
            Debug.Log((object)"global  pool made");
            if ((Object)ObjectPooler.instance != (Object)null)
                return;
            ObjectPooler.instance = this;
            this.ObjectPool = new List<GameObject>();
            GameObject melee = this._melee;
            int size = this._size;
            this.GrowPool(melee);
        }

        private void GrowPool(GameObject prefab)
        {
            for (int index = 0; index < this.ObjectPool.Count; ++index)
            {
                Debug.Log((object)"unit made");
                ServernGameBridge.Instance.MovethistoScene(Object.Instantiate<GameObject>(prefab));
                prefab.SetActive(false);
                this.ObjectPool.Add(prefab);
            }
        }

        public GameObject GetObjectFromPool(GameObject prefab)
        {
            Debug.Log((object)prefab.name.ToString());
            for (int index = 0; index < this.ObjectPool.Count; ++index)
            {
                if (prefab.name.ToString() + "(Clone)" == this.ObjectPool[index].name.ToString() && !this.ObjectPool[index].activeInHierarchy)
                {
                    this.ObjectPool[index].SetActive(true);
                    return this.ObjectPool[index];
                }
            }
            return this.IncreasePoolSize(prefab);
        }

        public void ReturnObjectToPool(GameObject _activeObject)
        {
            Debug.Log((object)"unit retured to pool as inactive");
            _activeObject.SetActive(false);
            this.ObjectPool.Add(_activeObject);
        }

        public GameObject IncreasePoolSize(GameObject prefab)
        {
            ++this.unitsSize;
            this.totalUnits.text = this.unitsSize.ToString() + " units";
            Debug.Log((object)"unit made");
            GameObject gameObject = Object.Instantiate<GameObject>(prefab);
            gameObject.SetActive(false);
            this.ObjectPool.Add(gameObject);
            return gameObject;
        }
    }
