using UnityEngine;


    public class RangedBehavior : MonoBehaviour
    {
      //  private ProjectileType projectileType;
       // private MotionType motionType;
        public GameObject projectile;
        public float launchVelocity = 700f;

        public void DetectEnemyTypeAndProjectileMotion(
          GameObject startingGameObject,
          GameObject endingGameObject)
        {
            switch (startingGameObject.GetComponent<ObjectType>())
            {
                case ObjectType.Tower:
                 //   this.motionType = MotionType.projectileMotion;
                    break;
                case ObjectType.Unit:
               //     this.motionType = MotionType.LinearMotion;
                    break;
            }
        }
    }
