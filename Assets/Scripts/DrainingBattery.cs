using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainingBattery : MonoBehaviour
{
    [SerializeField]
    private float TotalHp = 100;
    [SerializeField]
    public float CurrentHp;
    
    void Start()
    {
        CurrentHp = TotalHp;
    }

    void Update()
    {
        if (CurrentHp > 0f)
        {
            DamageOverTime();
        }
        if (CurrentHp < 0f)
        {
            CurrentHp = TotalHp;
        }
    }

    void DamageOverTime()
    {
        CurrentHp -= .05f;
        transform.localScale = new Vector3((CurrentHp / TotalHp), 1, 1);
        // add change color later
    }

}
