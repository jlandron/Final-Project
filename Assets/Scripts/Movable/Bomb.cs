using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


namespace Game.Movable
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField]
        private float delay = 1.5f;
        public GameObject ExplosionRadius;
        public ParticleSystem ExplosionParticles;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("Explode", delay);
        }

        private void Explode()
        {
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
            Instantiate(ExplosionRadius, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}