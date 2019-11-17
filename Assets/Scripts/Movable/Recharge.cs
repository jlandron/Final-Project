using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Game.Movable {
    public class Recharge : MonoBehaviour {
        [SerializeField]
        private DrainingBattery battery;
        [SerializeField]
        private float maxCharge = 100;
        [SerializeField]
        private float currentCharge;
        [SerializeField]
        private Light2D _light;
        private bool _charging;
        private bool batteryCharged = true;

        public bool BatteryCharged { get => batteryCharged; private set => batteryCharged = value; }

        void Start( ) {
            currentCharge = maxCharge;
            _charging = false;
        }

        void Update( ) {
            BatteryCharged = true;
            if( currentCharge > 0f && !_charging ) {
                _light.gameObject.SetActive( true );
                currentCharge -= .05f;

            } else if( _charging ) {
                _light.gameObject.SetActive( true );
                if( currentCharge < maxCharge ) {
                    currentCharge += 0.5f;
                }
            } else {
                BatteryCharged = false;
                _light.gameObject.SetActive( false );
            }
            battery.SetLevel( currentCharge / maxCharge );
            SetIntensity( );
        }

        private void SetIntensity( ) {
            if( currentCharge / maxCharge <= 0.25f ) {
                _light.intensity = ( ( 4 * currentCharge ) / maxCharge );
            } else {
                _light.intensity = 1f;
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
}
