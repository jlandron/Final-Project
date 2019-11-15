using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movable
{
    public class Inventory : MonoBehaviour
    {
        public GameObject BombPrefab;
        public int BombCount = 1;
        public int BombMax = 1;

        public GameObject FlarePrefab;
        public int FlareCount = 3;
        public int FlareMax = 3;

        public GameObject AttachedBomb;

        // Update is called once per frame
        void Update()
        {
            // Drop Bomb
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (BombCount > 0)
                {
                    Instantiate(BombPrefab, AttachedBomb.transform.position, Quaternion.identity);
                    DecrementBombCount();
                }
            }

            // Drop Flare
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (FlareCount > 0)
                {
                    //Instantiate(FlarePrefab, transform.position, Quaternion.identity);
                    DecrementFlareCount();
                }
            }

            // Display Bomb on back of Robot
            if (BombCount > 0)
            {
                AttachedBomb.SetActive(true);
            }
            else
            {
                AttachedBomb.SetActive(false);
            }
        }

        public void IncrementBombCount()
        {
            if (BombCount < BombMax)
            {
                BombCount++;
            }
        }

        public void DecrementBombCount()
        {
            if (BombCount > 0)
            {
                BombCount--;
            }
        }

        public void IncrementFlareCount()
        {
            if (FlareCount < FlareMax)
            {
                FlareCount++;
            }
        }

        public void DecrementFlareCount()
        {
            if (FlareCount > 0)
            {
                FlareCount--;
            }
        }
    }
}
