using UnityEngine;
namespace Game.Movable {
    public class EnemyBehavior : MonoBehaviour {
        private float speed = 2.5f;
        private float range = 7f;
        private Transform target;
        private SpriteRenderer spriteRenderer;

        private int m_DefaultHealth = 3;
        private int m_Health;
        //keeps the ray from hitting 3 times extreamly fast
        private float m_hitTime = 0;
        private float m_timeBetweenHits = 1;

        // Start is called before the first frame update
        void Start( ) {
            target = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<Transform>( );
            spriteRenderer = GetComponent<SpriteRenderer>( );
            Debug.Assert( target != null );
            m_Health = m_DefaultHealth;
        }

        // Update is called once per frame
        void Update( ) {
            // Calculate distance to target
            float distance = Vector2.Distance( transform.position, target.position );
            // Get direction to raycast
            Vector2 rayDirection = transform.position - target.transform.position;
            // If within range, check for line of sight and chase
            if( distance < range ) {
                // Move towards target with speed
                //TODO: add wall colision detection and avoidence
                transform.position = Vector2.MoveTowards( transform.position, target.position, speed * Time.smoothDeltaTime );
                // Rotate towards target
                //Vector2 targetPostition = new Vector2(this.transform.position.x, target.position.y);
                //transform.LookAt(targetPostition);
            }
            m_hitTime += Time.deltaTime;
        }

        public void DecrementHealth( ) {
            if( m_hitTime > m_timeBetweenHits ) {
                if( m_Health > 1 ) {
                    m_hitTime = 0;
                    m_Health--;
                    Debug.Log( "Enemy: ouch!" );
                } else {
                    Destroy( gameObject );
                }
            }
        }
    }

}