using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSprite : MonoBehaviour
{
    public float delay = 5f;
    private float timePassed = 0f;

    private void Update()
    {
        if(timePassed < delay)
        {
            timePassed += Time.deltaTime;
        }
        else if(timePassed >= delay)
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
