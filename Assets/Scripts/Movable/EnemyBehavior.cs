using Pathfinding;
using UnityEngine;

namespace Game.Movable
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField]
        private float targetRange = 5f;
        [SerializeField]
        private float tetherDistence = 14f;
        
        private Transform target;
        private SpriteRenderer spriteRenderer;
        private ParticleSystem trail;

        [SerializeField]
        private int m_DefaultHealth = 5;
        [SerializeField]
        private int m_Health;
        //keeps the laser from hitting 3 times extreamly fast
        private float m_hitTime = 0;
        private float m_timeBetweenHits = 0.5f;

        [SerializeField]
        private GameObject hitText;
        [SerializeField]
        private bool isDead = false;

        public AIPath aIPath;
        [SerializeField]
        private string[] hitStrings = { "Ouch!", "Beep", "Boop", "BZZZZ" };
        private bool isChasing = false;
        [SerializeField]
        public float radius = 20;

        private IAstarAI ai;
        private AIDestinationSetter aIDestinationSetter;

        
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            aIPath = GetComponent<AIPath>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            aIDestinationSetter = GetComponent<AIDestinationSetter>();
            trail = GetComponentInChildren<ParticleSystem>();
            ai = GetComponent<IAstarAI>();
            Debug.Assert(target != null);
            m_Health = m_DefaultHealth;

        }
        // Update is called once per frame
        void Update()
        {
            if(ai == null)
            {
                ai = GetComponent<IAstarAI>();
            }
            if (!isDead)
            {
                DoChasing();
                CheckFlipSprite();
                m_hitTime += Time.deltaTime;
                //keep the enemy from rotating due to collisions
                this.gameObject.transform.rotation = Quaternion.identity;
            }
            else
            {
                aIPath.enabled = false;
            }
            
        }

        private void CheckFlipSprite()
        {
            if (aIPath.desiredVelocity.x >= 0.01f) //moving right
            {

                spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f); //flip 
            }
            else if (aIPath.desiredVelocity.x <= -0.01f) //moving left
            {
                spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        private void DoChasing()
        {
            if (Vector2.Distance(transform.position, target.position) <= targetRange && !isChasing)
            {
                ai.maxSpeed = 2;
                isChasing = true;
                aIDestinationSetter.enabled = true;
            }
            else if (!isChasing || Vector2.Distance(transform.position, target.position) > tetherDistence)
            {
                isChasing = false;
                aIDestinationSetter.enabled = false;
                DoWandering();
            }
        }
 
        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("Particle Collision");
            DecrementHealth();
        }
        

        public void DecrementHealth(int amount = 1)
        {
            if (m_hitTime > m_timeBetweenHits)
            {
                if (m_Health > 1)
                {
                    m_hitTime = 0;
                    m_Health -= amount;
                    if (hitText != null)
                    {
                        ShowText();
                    }
                }
                else if(!isDead)
                {
                    isDead = true;
                    gameObject.tag = "Dead";
                    GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                    trail.gameObject.SetActive(false);
                    GetComponent<DeadSprite>().enabled = true;
                }
            }
        }

        private void ShowText()
        {
            GameObject go = Instantiate(hitText, transform.position, Quaternion.identity);
            go.GetComponent<TextMesh>().color = Color.red;
            int stringChoice = Random.Range(0, hitStrings.Length);
            go.GetComponent<TextMesh>().text = hitStrings[stringChoice];
        }

        Vector3 PickRandomPoint()
        {
            Vector3 point = Random.insideUnitSphere * radius;

            point.y = 0;
            point += transform.position;
            return point;
        }
        void DoWandering()
        {
            ai.maxSpeed = 1;
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath))
            {
                ai.destination = PickRandomPoint();
                ai.SearchPath();
            }
        }
    }
}