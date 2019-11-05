using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Movable {
    public class PressEToInteract : MonoBehaviour {
        [SerializeField]
        private Text signal = null;
        [SerializeField]
        private Text wantToShow = null;
        [SerializeField]
        private bool _inRange = false;

        private void Start( ) {
            signal.gameObject.SetActive( false );
            wantToShow.gameObject.SetActive( false );
        }

        private void OnTriggerEnter2D( Collider2D collision ) {
            if( collision.gameObject.tag == "Player" ) {
                _inRange = true;
                signal.gameObject.SetActive( true );
            }
        }
        private void Update( ) {
            if( _inRange && Input.GetKeyDown( KeyCode.E ) ) {
                Debug.Log( "Showing Text" );
                wantToShow.gameObject.SetActive( true );
                Invoke( "ShowText", 5 );
            }
        }
        private void OnTriggerExit2D( Collider2D collision ) {
            if( collision.gameObject.tag == "Player" ) {
                _inRange = false;
                signal.gameObject.SetActive( false );
            }
        }
        void ShowText( ) {
            Debug.Log( "Disabling Text" );
            wantToShow.gameObject.SetActive( false );
        }
    }

}