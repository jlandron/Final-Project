using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float speed = 2.5f;
    private float range = 7f;
    private Transform target;
    private SpriteRenderer spriteRenderer;

    private int m_DefaultHealth = 3;
    private int m_Health;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(target != null);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance to target
        float distance = Vector2.Distance(transform.position, target.position);
        // Get direction to raycast
        Vector2 rayDirection = transform.position - target.transform.position;
        // If within range, check for line of sight and chase
        if (distance < range)
        {
            RaycastHit2D hit = Physics2D.Raycast(target.transform.position, rayDirection.normalized, range);
            if (hit.collider.gameObject == target.gameObject)
            {
                // Move towards target with speed
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.smoothDeltaTime);
                // Rotate towards target
                //Vector2 targetPostition = new Vector2(this.transform.position.x, target.position.y);
                //transform.LookAt(targetPostition);
            }
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
            Destroy(gameObject);
        }
    }
}
