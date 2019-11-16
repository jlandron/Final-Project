using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Movable
{
    public class Inventory : MonoBehaviour
    {
        public Text FlareDisplay = null;

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
                    Invoke("RescanPathfinding", 3);
                }
            }

            // Drop Flare
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (FlareCount > 0)
                {
                    Instantiate(FlarePrefab, transform.position, Quaternion.identity);
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

            UpdateUI();
        }

        public void UpdateUI()
        {
            if (FlareDisplay != null)
            {
                switch (FlareCount)
                {
                    case 3:
                        FlareDisplay.text = "x3";
                        break;
                    case 2:
                        FlareDisplay.text = "x2";
                        break;
                    case 1:
                        FlareDisplay.text = "x1";
                        break;
                    case 0:
                        FlareDisplay.text = "x0";
                        break;
                }
            }
        }

        void RescanPathfinding() {
            AstarPath.active.Scan();
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
