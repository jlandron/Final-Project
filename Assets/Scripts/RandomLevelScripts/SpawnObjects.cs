using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.RandomRoom
{
    public class SpawnObjects : MonoBehaviour
    {
        public GameObject[] gameObjects;
        public int maxSpawnNum = 10;

        void Start()
        {
            int rand = Random.Range(5, maxSpawnNum);
            for(int i = 2; i < rand; i++)
            {
                Instantiate(gameObjects[Random.Range(0,gameObjects.Length)], transform.position, transform.rotation);
            }
        }
    }
}
