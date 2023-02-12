using UnityEngine;

namespace TowerWar
{

    public class ObjectDefiner : MonoBehaviour
    {
        public ObjectType objectType;
        public UserType userType;
        public Civilization civilization;
        public PathType pathType;

        private void Awake()
        {
            if ((bool)(Object)this.GetComponent<PathableObjects>() && !(bool)(Object)this.GetComponent<NonRangedTowerBehavior>())
            {
                this.pathType = PathType.pathable;
            }
            else
            {
                if (!(bool)(Object)this.GetComponent<NonRangedTowerBehavior>() || !(bool)(Object)this.GetComponent<PathableObjects>())
                    return;
                this.pathType = PathType.pathmaker;
            }
        }
    }

}