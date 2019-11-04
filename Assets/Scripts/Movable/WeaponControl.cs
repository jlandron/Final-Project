using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public Camera m_MainCamera;
    public LayerMask m_HitLayer;
    public LineRenderer m_LineRenderer;
    public ReticleControl m_Reticle;

    public float m_LaserBeamLength = 3f;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
        m_Reticle = GetComponentInChildren<ReticleControl>( );
        m_LineRenderer = GetComponentInChildren<LineRenderer>( );
        Debug.Assert( m_LineRenderer != null);
        Debug.Assert(m_LineRenderer != null);
        m_LineRenderer.useWorldSpace = true;
        m_LineRenderer.enabled = true ;
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector2 startPoint = this.gameObject.transform.position;
        Vector2 endPoint = m_Reticle.gameObject.transform.position;
        // Get vectors for mouse and where the laser starts and ends
        //Vector2 mousePos = new Vector2(m_MainCamera.ScreenToWorldPoint(Input.mousePosition).x, m_MainCamera.ScreenToWorldPoint(Input.mousePosition).y);
        //Vector2 laserStartPos = new Vector2(m_LaserPoint.position.x, m_LaserPoint.position.y);
        //Vector2 laserEndPos = laserStartPos + (laserStartPos * m_LaserBeamLength);

        //TODO: set max shoot distance
        RaycastHit2D hit = Physics2D.Raycast( startPoint, endPoint, m_LaserBeamLength, m_HitLayer);
        
        // Hit enemy, decrease health
        if(hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyBehavior enemy = hit.transform.GetComponent<EnemyBehavior>();
                if (enemy != null)
                {
                    enemy.DecrementHealth();
                }
            }
            Debug.Log( "Raycast hit: " + hit.collider.gameObject.name );

            endPoint = hit.point;
        }
        Debug.DrawLine( startPoint, endPoint, Color.cyan );
        m_LineRenderer.SetVertexCount( 2 );
        // Render line at start and end points
        m_LineRenderer.SetPosition(0, startPoint );
        m_LineRenderer.SetPosition(1, endPoint );
    }
}