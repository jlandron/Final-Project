using UnityEngine;

namespace Game.RandomRoom {
    public class SpawnObject : MonoBehaviour {
        public GameObject[] gameObjects;
        void Start( ) {
            int rand = Random.Range( 0, gameObjects.Length );

            GameObject instance = Instantiate( gameObjects[ rand ], transform.position, transform.rotation );
            instance.transform.parent = transform;
        }
    }
}