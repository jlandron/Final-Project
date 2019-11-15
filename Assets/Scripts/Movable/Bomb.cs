using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float delay = 1.5f;
    private float timeToExplode = 0f;
    public GameObject ExplosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        timeToExplode = Time.time + delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToExplode)
        {
            Instantiate(ExplosionRadius, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
