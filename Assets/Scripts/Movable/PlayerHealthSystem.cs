﻿using Game.RandomRoom;
using UnityEngine;
using UnityEngine.UI;
using Game.Saving;
using Game.Core;
using GAME.Movable;

namespace Game.Movable
{
    public class PlayerHealthSystem : MonoBehaviour, ISaveable
    {
        public Text m_HealthDisplay = null;

        private int m_DefaultHealth = 3;
        private float m_hitTime = 0;
        private float m_timeBetweenHits = 1;
        private int m_Health = 3;


        [SerializeField]
        private GameObject[] scrapPieces;
        [SerializeField]
        private int maxScrapDropped = 5;

        [System.Serializable]
        internal class SerializablePlayerHealth
        {
            internal int currentHealth, maxHealth;
            internal SerializablePlayerHealth(int _health, int _maxHealth)
            {
                currentHealth = _health;
                maxHealth = _maxHealth;
            }
        }

        void Start()
        {
            UpdateUI();
        }
        private void Update()
        {
            m_hitTime += Time.deltaTime;
            UpdateUI();
        }
        public void IncrementHealth()
        {
            if (m_Health < m_DefaultHealth)
            {
                m_Health++;
            }
        }

        public void DecrementHealth()
        {
            if (m_Health > 1)
            {
                m_Health--;
            }
            else
            {
                Vector2 deathPos = new Vector2(transform.position.x, transform.position.y);
                FindObjectOfType<Fader>().FadeOutImmeduate();
                try
                {
                    gameObject.transform.position = FindObjectOfType<LevelGenerator>().spawnLocation;
                }
                catch (System.Exception)
                {

                }
                DropScrapOnDeath(deathPos);
                m_Health = m_DefaultHealth;
                //Rougelike, death means you lose everything current save is overwritten
                GetComponent<Inventory>().SetAllToZero();
                StartCoroutine(FindObjectOfType<Fader>().FadeIn(2));
            }
        }

        private void DropScrapOnDeath(Vector2 deathPos)
        {
            int numDropped = Random.Range(1, maxScrapDropped);
            for (int i = 0; i < numDropped; i++)
            {
                Instantiate(scrapPieces[Random.Range(0, scrapPieces.Length)], deathPos, Quaternion.identity);
            }
        }

        public void UpdateUI()
        {
            if (m_HealthDisplay != null)
            {
                switch (m_Health)
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_hitTime > m_timeBetweenHits)
            {
                if (collision.CompareTag("Enemy"))
                {
                    m_hitTime = 0;
                    Camera.main.GetComponent<Shaker>().StartShaking(new Vector2(0.5f, 0.5f), 0.75f);
                    DecrementHealth();
                }
            }
        }
        private void OnParticleCollision(GameObject other)
        {
            if (m_hitTime > m_timeBetweenHits)
            {
                if (other.CompareTag("EnemyGun"))
                {
                    m_hitTime = 0;
                    DecrementHealth();
                }
            }
        }

        public object CaptureState()
        {
            return new SerializablePlayerHealth(this.m_Health, this.m_DefaultHealth);
        }

        public void RestoreState(object state)
        {
            SerializablePlayerHealth health = (SerializablePlayerHealth)state;
            this.m_Health = health.currentHealth;
            this.m_DefaultHealth = health.maxHealth;
        }
    }
}
