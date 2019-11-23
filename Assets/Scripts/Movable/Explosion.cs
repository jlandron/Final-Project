using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Movable
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem dirtParticles;
        public AudioClip explosionSound;
        [SerializeField]
        private float areaOfEffect;
        [SerializeField]
        private LayerMask whatIsDestructable;

        void Start()
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructable);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                switch (objectsToDamage[i].tag)
                {
                    case "Tiles":
                        if (dirtParticles != null)
                        {
                            Instantiate(dirtParticles, objectsToDamage[i].transform.position, Quaternion.identity);
                        }
                        Destroy(objectsToDamage[i].gameObject);
                        break;
                    case "Enemy":
                        objectsToDamage[i].GetComponent<EnemyBehavior>().DecrementHealth(3);
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
