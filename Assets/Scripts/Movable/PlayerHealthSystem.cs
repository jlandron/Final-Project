using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.RandomRoom;

    namespace Game.Movable {
    public class PlayerHealthSystem : MonoBehaviour {
        public Text m_HealthDisplay = null;
        public Text m_HealthDisplayStatus = null;

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
                try
                {
                    this.gameObject.transform.position = FindObjectOfType<LevelGenerator>().spawnLocation;
                }
                catch (System.Exception)
                {

                }
                
                m_Health = m_DefaultHealth;
            }
        }

        public void UpdateUI( ) {
            if( m_HealthDisplay != null ) {
                switch(m_Health)
                {
                    case 3:
                        m_HealthDisplay.text = "STABLE";
                        m_HealthDisplay.color = new Color(0, 255, 0);
                        break;
                    case 2:
                        m_HealthDisplay.text = "DAMAGED";
                        m_HealthDisplay.color = new Color(255, 215, 0);
                        break;
                    case 1:
                        m_HealthDisplay.text = "CRITICAL";
                        m_HealthDisplay.color = new Color(255, 0, 0);
                        break;
                }
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
