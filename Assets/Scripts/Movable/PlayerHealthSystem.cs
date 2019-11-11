using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.RandomRoom;

    namespace Game.Movable {
    public class PlayerHealthSystem : MonoBehaviour {
        public Text m_HealthDisplay = null;

        private int m_DefaultHealth = 3;
        private float m_hitTime = 0;
        private float m_timeBetweenHits = 1;
        private int m_Health;

        [SerializeField]
        GameObject deadPrefab;

        // Start is called before the first frame update
        void Start( ) {
            m_Health = m_DefaultHealth;
            UpdateUI( );
        }
        private void Update( ) {
            m_hitTime += Time.deltaTime;
            UpdateUI();
        }
        public void IncrementHealth( ) {
            if( m_Health < m_DefaultHealth ) {
                m_Health++;
            }
        }

        public void DecrementHealth( ) {
            if( m_Health > 1 ) {
                m_Health--;
            } else {
                if(deadPrefab != null)
                {
                    Instantiate(deadPrefab, transform.position, Quaternion.identity);
                }
                this.gameObject.transform.position = FindObjectOfType<LevelGenerator>( ).spawnLocation;
                m_Health = m_DefaultHealth;
            }
        }

        public void UpdateUI( ) {
            if( m_HealthDisplay != null ) {
                m_HealthDisplay.text = "Health: " + m_Health;
            }
        }

        private void OnTriggerEnter2D( Collider2D collision ) {
            if( m_hitTime > m_timeBetweenHits ) {
                if( collision.gameObject.tag == "Enemy" ) {
                    m_hitTime = 0;
                    DecrementHealth( );
                }
            }
        }
    }
}
