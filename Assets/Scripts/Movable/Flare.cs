using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    public float Duration = 100f;
    private float TimeToExpire = 0f;
    public GameObject ExplosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        TimeToExpire = Time.time + Duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > TimeToExpire)
        {
            Destroy(this.gameObject);
        }
    }
}
