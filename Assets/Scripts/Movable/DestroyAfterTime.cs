using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private float timeToExpire = 0f;
    public float lifeTime = 3f;
    private void Update()
    {
        timeToExpire += Time.deltaTime;
        if (timeToExpire > lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
