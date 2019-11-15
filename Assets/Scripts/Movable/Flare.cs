using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flare : MonoBehaviour
{
    public float Duration = 100f;
    private float TimeToExpire = 0f;
    private ParticleSystem flareParticles;
    private Light2D light;
    // Update is called once per frame

    void Update()
    {
        TimeToExpire += Time.deltaTime;
        if (TimeToExpire > Duration)
        {
            Destroy(this.gameObject);
        }
        //TODO: fade flare after 25% or some amount
    }
}
