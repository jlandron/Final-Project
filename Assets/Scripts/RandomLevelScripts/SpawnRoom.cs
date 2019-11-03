using UnityEngine;

public class SpawnRoom : MonoBehaviour {
    public LayerMask whatIsRoom;
    public LevelGenerator levelGenerator;
    private bool _hasSpawned = false;

    // Update is called once per frame
    void Update( ) {
        if( !_hasSpawned && levelGenerator.StopGeneration) {
            Collider2D roomDetector = Physics2D.OverlapCircle( transform.position, 1, whatIsRoom );
            if( roomDetector == null ) {
                int randRoom = Random.Range( 0, levelGenerator.rooms.Length );
                Instantiate( levelGenerator.rooms[ randRoom ], transform.position, Quaternion.identity ); 
            }
            int randBackgound = Random.Range( 0, levelGenerator.backgrounds.Length );
            Instantiate( levelGenerator.backgrounds[ randBackgound ], transform.position, Quaternion.identity );
            _hasSpawned = true;
        }
        
    }
}
