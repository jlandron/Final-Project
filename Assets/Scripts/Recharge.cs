using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    [SerializeField]
    public DrainingBattery battery;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckTrigger(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckTrigger(collision.gameObject);
    }

    private void CheckTrigger(GameObject g)
    {
        if (g.name == "LightSource")
        {
            if (battery.CurrentHp < 100)
            {
                battery.CurrentHp += 0.5f ;
            }
        }
    }

}
