using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Camera m_MainCamera;
    public LayerMask m_HitLayer;
    public Transform m_LaserPoint;
    public LineRenderer m_LineRenderer;
    public GameObject m_Reticle;

    public float m_LaserBeamLength = 3f;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
        m_LaserPoint = transform.FindChild("Laser Point");
        Debug.Assert(m_LaserPoint != null);
        Debug.Assert(m_LineRenderer != null);
        m_LineRenderer.enabled = false;

        m_Reticle = Resources.Load("Prefabs/Reticle") as GameObject;
        Instantiate(m_Reticle);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            m_LineRenderer.enabled = false;
        }
    }

    public void Shoot()
    {
        m_LineRenderer.enabled = true;
        // Get vectors for mouse and where the laser starts and ends
        Vector2 mousePos = new Vector2(m_MainCamera.ScreenToWorldPoint(Input.mousePosition).x, m_MainCamera.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 laserStartPos = new Vector2(m_LaserPoint.position.x, m_LaserPoint.position.y);
        Vector2 laserEndPos = laserStartPos + (laserStartPos * m_LaserBeamLength);

        RaycastHit2D hit = Physics2D.Raycast(laserStartPos, mousePos - laserStartPos, m_LaserBeamLength, m_HitLayer);
        Debug.DrawLine(laserStartPos, (mousePos - laserStartPos) * 100, Color.cyan);
        // Hit enemy, decrease health
        if(hit != null && hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyBehavior enemy = hit.transform.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    enemy.DecrementHealth();
                }
            }

            laserEndPos = hit.point;
        }

        // Render line at start and end points
        m_LineRenderer.SetPosition(0, laserStartPos);
        m_LineRenderer.SetPosition(1, laserEndPos);
    }
}