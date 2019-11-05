using UnityEngine;

public class DDOL : MonoBehaviour {
    private static DDOL _instance = null;

    private void Awake( ) {
        DontDestroyOnLoad( gameObject );
        if( _instance == null ) {
            _instance = this;
        } else {
            Destroy( this.gameObject );
        }
    }
}
