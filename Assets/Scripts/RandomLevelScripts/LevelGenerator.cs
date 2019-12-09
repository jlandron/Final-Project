using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Movable;
using System.Collections.Generic;
using Pathfinding;

namespace Game.RandomRoom
{
    public class LevelGenerator : MonoBehaviour
    {

        public Transform[] startingPositions;
        public GameObject[] rooms; // index 0 --> closed, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRBT
        public GameObject entryRoom;
        public GameObject endPoint;
        public Vector2 spawnLocation;

        private int direction;
        private bool stopGeneration;
        private int downCounter;

        public float moveIncrement;
        private float timeBtwSpawn;
        public float startTimeBtwSpawn;

        public LayerMask whatIsRoom;
        public int minX;
        public int maxX;
        public int minY;
        public GameObject player;
        [SerializeField]
        private int maxNumEnemies;

        public bool StopGeneration { get => stopGeneration; private set => stopGeneration = value; }

        private void Start()
        {
            int randStartingPos = Random.Range(1, startingPositions.Length - 1);
            transform.position = startingPositions[randStartingPos].position;
            spawnLocation = new Vector2(transform.position.x, transform.position.y);
            Instantiate(entryRoom, transform.position, Quaternion.identity);
            player.transform.position = transform.position;
            direction = Random.Range(1, 5);
        }

        private void Update()
        {
            if (Debug.isDebugBuild)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            if (timeBtwSpawn <= 0 && StopGeneration == false)
            {
                Move();
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }

        private void Move()
        {

            if (direction == 1 || direction == 2)
            { // Move right !

                if (transform.position.x < maxX)
                {
                    downCounter = 0;
                    Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                    transform.position = pos;

                    int randRoom = Random.Range(1, 4);
                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                    // Makes sure the level generator doesn't move left !
                    direction = Random.Range(1, 6);
                    if (direction == 3)
                    {
                        direction = 1;
                    }
                    else if (direction == 4)
                    {
                        direction = 5;
                    }
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 3 || direction == 4)
            { // Move left !

                if (transform.position.x > minX)
                {
                    downCounter = 0;
                    Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                    transform.position = pos;

                    int randRoom = Random.Range(1, 4);
                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                    direction = Random.Range(3, 6);
                }
                else
                {
                    direction = 5;
                }

            }
            else if (direction == 5)
            { // MoveDown
                downCounter++;
                if (transform.position.y > minY)
                {
                    // Now I must replace the room BEFORE going down with a room that has a DOWN opening, so type 3 or 5
                    Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);

                    if (previousRoom.GetComponent<Room>().type != 4 && previousRoom.GetComponent<Room>().type != 2)
                    {

                        // My problem : if the level generation goes down TWICE in a row, there's a chance that the previous room is just 
                        // a LRB, meaning there's no TOP opening for the other room ! 

                        if (downCounter >= 2)
                        {
                            previousRoom.GetComponent<Room>().DestroyRoom();
                            Instantiate(rooms[4], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            previousRoom.GetComponent<Room>().DestroyRoom();
                            int randRoomDownOpening = Random.Range(2, 5);
                            if (randRoomDownOpening == 3)
                            {
                                randRoomDownOpening = 2;
                            }
                            Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                        }

                    }

                    Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                    transform.position = pos;

                    // Makes sure the room we drop into has a TOP opening !
                    int randRoom = Random.Range(3, 5);
                    Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 6);
                }
                else
                {
                    StopGeneration = true;
                    Invoke("MoveThings", 1);
                }
            }
        }
        /**
         * Used to place objects around the map after generation of all tiles.  
         */
        private void MoveThings()
        {
            Invoke("FinishGeneration", 1);
            endPoint.transform.position = transform.position;
        }
        private void FinishGeneration()
        {
            AIPath[] pathfinders = GameObject.FindObjectsOfType<AIPath>();
            foreach (var finder in pathfinders)
            {
                finder.enabled = false;
            }
            AstarPath.active.Scan(); //level is built, scan graph
            foreach (var finder in pathfinders)
            {
                finder.enabled = true;
            }
            //scan for the number of enamies and delete over the max
            //List<EnemyBehavior> enemies = new List<EnemyBehavior>(FindObjectsOfType<EnemyBehavior>());
            //while(enemies.Count > maxNumEnemies)
            //{
            //    EnemyBehavior enemyBehavior = enemies[0];
            //    enemies.RemoveAt(0);
            //    Destroy(enemyBehavior.gameObject);
            //}
        }
    }
}
