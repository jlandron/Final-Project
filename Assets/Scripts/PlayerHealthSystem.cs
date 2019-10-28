using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
    public Text m_HealthDisplay = null;

    private int m_DefaultHealth = 3;
    private int m_Health;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = m_DefaultHealth;
        UpdateUI();
    }

    public void IncrementHealth()
    {
        if (m_Health < m_DefaultHealth)
        {
            m_Health++;
            UpdateUI();
        }
    }

    public void DecrementHealth()
    {
        if (m_Health > 1)
        {
            m_Health--;
            UpdateUI();
        }
        else
        {
            SceneManager.LoadScene("HealthSystem");
        }
    }

    public void UpdateUI()
    {
        m_HealthDisplay.text = "Health: " + m_Health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            DecrementHealth();
        }
    }
}
