using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Recharge : MonoBehaviour {
    [SerializeField]
    private DrainingBattery battery;
    [SerializeField]
    private float maxCharge = 100;
    [SerializeField]
    private float currentCharge;
    [SerializeField]
    private Light2D light;
    private bool _charging;

    void Start( ) {
        currentCharge = maxCharge;
        _charging = false;
    }

    void Update( ) {
        if( currentCharge > 0f && !_charging ) {
            light.gameObject.SetActive( true );
            currentCharge -= .05f;
            
        } else if( _charging ) {
            light.gameObject.SetActive( true );
            if( currentCharge < maxCharge ) {
                currentCharge += 0.5f;
            }
        } else {
            light.gameObject.SetActive( false );
        }
        battery.SetLevel( currentCharge / maxCharge );
        SetIntensity( );
    }

    private void SetIntensity( ) {
        if( currentCharge / maxCharge <= 0.25f ) {
            light.intensity = ( ( 4 * currentCharge ) / maxCharge );
        } else {
            light.intensity = 1f;
        }
    }

    private void OnTriggerEnter2D( Collider2D collision ) {
        if( collision.gameObject.tag == "LightSource" ) {
            _charging = true;
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if( collision.gameObject.tag == "LightSource" ) {
            _charging = false;
        }
    }

}
