using UnityEngine;


    public class Projectile : MonoBehaviour
    {
        public Vector3 spawnPos;
        public Vector3 destinationPos;
       // public ProjectileType projectileType;
        public Civilization civilization;
        public int unitHealth;
        public GameObject selfProjectile;
        [HideInInspector]
        public Vector3 endingTower;

        private void Awake() => this.selfProjectile = this.gameObject;

        private void OnEnable()
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Vector3 force = this.destinationPos - this.spawnPos;
            this.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            Debug.Log((object)("Force APplied is " + (this.destinationPos - this.spawnPos).ToString()));
        }
    }

