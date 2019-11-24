using UnityEngine;

namespace Game.Movable
{
    public class HealthBehavior : MonoBehaviour
    {
        [SerializeField]
        private float StartHealth = 3f;
        [SerializeField]
        private float _currentHealth;
        [SerializeField]
        private GameObject deathParticlesEffects;
        [SerializeField]
        private GameObject[] scrapPieces;
        [SerializeField]
        private int maxScrapDropped = 5;
        //keeps this object from being hit many times extreamly fast
        private float m_hitTime = 0;
        private float m_timeBetweenHits = 0.5f;
        private bool isDead = false;

        private void Awake()
        {
            _currentHealth = StartHealth;
            m_hitTime = m_timeBetweenHits;
        }
        private void Update()
        {
            m_hitTime += Time.deltaTime;
            if (_currentHealth <= 0)
            {
                isDead = true;
            }
            if (isDead)
            {
                int numDropped = Random.Range(1, maxScrapDropped);
                if (scrapPieces.Length > 0)
                {
                    for (int i = 0; i < numDropped; i++)
                    {
                        Instantiate(scrapPieces[Random.Range(0, scrapPieces.Length)], transform.position, Quaternion.identity);

                    }
                }
                if (deathParticlesEffects != null)
                {
                    Instantiate(deathParticlesEffects, transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("PlayerGun"))
            {
                Debug.Log("Particle Collision");
                DecrementHealth();
            }
        }

        public void DecrementHealth(int amount = 1)
        {
            if (m_hitTime > m_timeBetweenHits)
            {
                m_hitTime = 0;
                _currentHealth -= amount;
                EnemyBehavior enemy = GetComponent<EnemyBehavior>();
                if(enemy != null)
                {
                    enemy.ShowText();
                }
            }
        }
    }
}
