using Game.RandomRoom;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Movable
{
    public class PlayerHealthSystem : MonoBehaviour
    {
        public Text HealthDisplay = null;

        private int _defaultHealth = 3;
        private float _hitTime = 0;
        private float _timeBetweenHits = 1;
        private int _health;

        [SerializeField]
        GameObject deadPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _health = _defaultHealth;
            UpdateUI();
        }
        private void Update()
        {
            _hitTime += Time.deltaTime;
            UpdateUI();
        }
        public void IncrementHealth()
        {
            if (_health < _defaultHealth)
            {
                _health++;
            }
        }

        public void DecrementHealth()
        {
            if (_health > 1)
            {
                _health--;
            }
            else
            {
                if (deadPrefab != null)
                {
                    Instantiate(deadPrefab, transform.position, Quaternion.identity);
                }
                try
                {
                    gameObject.transform.position = FindObjectOfType<LevelGenerator>().spawnLocation;
                }
                catch (System.Exception)
                {

                }

                _health = _defaultHealth;
            }
        }

        public void UpdateUI()
        {
            if (HealthDisplay != null)
            {
                switch (_health)
                {
                    case 3:
                        HealthDisplay.text = "STABLE";
                        HealthDisplay.color = new Color(0, 255, 0);
                        break;
                    case 2:
                        HealthDisplay.text = "DAMAGED";
                        HealthDisplay.color = new Color(255, 215, 0);
                        break;
                    case 1:
                        HealthDisplay.text = "CRITICAL";
                        HealthDisplay.color = new Color(255, 0, 0);
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hitTime > _timeBetweenHits)
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    _hitTime = 0;
                    DecrementHealth();
                }
            }
        }
    }
}
