using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemyText : MonoBehaviour
{
    [SerializeField]
    private float destroyTime = 3f;

    private Vector3 offset = new Vector3(0, 1);
    //alter this vector to add more/less randomization
    public Vector2 RandomizedIntensity = new Vector2(0.5f, 0); 
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizedIntensity.x, RandomizedIntensity.x),
            Random.Range(-RandomizedIntensity.y, RandomizedIntensity.y));
    }
}
