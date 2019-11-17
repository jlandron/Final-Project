using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movable
{
    public class DeadSprite : MonoBehaviour
    {
        public float delay = 5f;
        private float _timePassed = 0f;

        private void Update()
        {
            if (_timePassed < delay)
            {
                _timePassed += Time.deltaTime;
            }
            else if (_timePassed >= delay)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                GetComponent<Collider2D>().isTrigger = true;
                Invoke("DestroySprite", 5);
            }
        }
        void DestroySprite()
        {
            Destroy(this.gameObject);
        }
    }
}

