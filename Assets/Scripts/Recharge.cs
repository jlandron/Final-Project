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
            battery.DrainOverTime( currentCharge / maxCharge );
        } else if( _charging ) {
            light.gameObject.SetActive( true );
        } else {
            light.gameObject.SetActive( false );
        }
    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        if( collision.gameObject.tag == "LightSource" ) {
            _charging = true;
            if( currentCharge < maxCharge ) {
                currentCharge += 0.5f;
            }
        }
    }

    private void OnTriggerExit2D( Collider2D collision ) {
        if( collision.gameObject.tag == "LightSource" ) {
            _charging = false;
        }
    }

}
