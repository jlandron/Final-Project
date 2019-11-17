using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class DestroyAfterTime : MonoBehaviour
    {
        private float _timeToExpire = 0f;
        public float lifeTime = 3f;
        private void Update()
        {
            _timeToExpire += Time.deltaTime;
            if (_timeToExpire > lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

