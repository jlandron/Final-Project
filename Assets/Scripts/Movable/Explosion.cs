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
        void Start()
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            // Collide with Tiles Layer
            if (collision.gameObject.tag == "Tiles")
            {
                if(dirtParticles != null)
                {
                    Instantiate(dirtParticles, collision.transform.position, Quaternion.identity);
                }
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
            //TODO: Consider explosion effecting enemies/player as well
        }
    }
}
