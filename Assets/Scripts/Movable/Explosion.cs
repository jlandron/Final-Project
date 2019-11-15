using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        // Collide with Tiles Layer
        if(collision.gameObject.tag == "Tiles")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
