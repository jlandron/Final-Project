using UnityEngine;

public class OptionalSpawn : MonoBehaviour {
    private void Awake( ) {
        int rand = Random.Range( 0, 2 );
        switch( rand ) {
            case 0:
            gameObject.SetActive( true );
            break;
            case 1:
            gameObject.SetActive( false );
            break;
            default:
            break;
        }
    }
}
