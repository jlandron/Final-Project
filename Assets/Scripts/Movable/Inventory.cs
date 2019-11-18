using Game.Core;
using Game.Saving;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

namespace Game.Movable
{
    public class Inventory : MonoBehaviour, ISaveable
    {
        public Text FlareDisplay = null;
        public Text ScrapDisplay = null;

        public GameObject BombPrefab;
        public int BombCount = 1;
        public int BombMax = 1;

        public GameObject FlarePrefab;
        public int FlareCount = 3;
        public int FlareMax = 3;

        public int scrapCount = 0;

        public GameObject AttachedBomb;
        //savable object for inventory, any new items should be added to object and ISaveable methods updated
        [System.Serializable]
        internal class SerializableInventory
        {
            internal int flareCount, bombCount, scrapCount;
            internal SerializableInventory(int flare, int bomb, int scrap)
            {
                flareCount = flare;
                bombCount = bomb;
                scrapCount = scrap;
            }
        }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("BombPickup"))
            {
                if (IncrementBombCount())
                {
                    Destroy(collision.gameObject);
                }
            }
            if (collision.CompareTag("FlarePickup"))
            {
                if (IncrementFlareCount())
                {
                    Destroy(collision.gameObject);
                }
            }
            if (collision.CompareTag("Scrap"))
            {
                scrapCount++;
                Destroy(collision.gameObject);
            }
        }
        public void UpdateUI()
        {
            if (FlareDisplay != null)
            {
                FlareDisplay.text = "x" + FlareCount;
            }
            if (ScrapDisplay != null)
            {
                ScrapDisplay.text = "x" + scrapCount;
            }
        }

        void RescanPathfinding()
        {
            AIPath[] pathfinders = GameObject.FindObjectsOfType<AIPath>();
            foreach (var finder in pathfinders)
            {
                finder.enabled = false;
            }
            var graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
            foreach (var finder in pathfinders)
            {
                finder.enabled = true;
            }
        }
        public bool IncrementBombCount()
        {
            if (BombCount < BombMax)
            {
                BombCount++;
                return true;
            }
            return false;
        }

        public bool DecrementBombCount()
        {
            if (BombCount > 0)
            {
                BombCount--;
                return true;
            }
            return false;
        }

        public bool IncrementFlareCount()
        {
            if (FlareCount < FlareMax)
            {
                FlareCount++;
                return true;
            }
            return false;
        }

        public bool DecrementFlareCount()
        {
            if (FlareCount > 0)
            {
                FlareCount--;
                return true;
            }
            return false;
        }

        public void SetAllToZero()
        {
            BombCount = 0;
            FlareCount = 0;
            if (scrapCount > 5)
            {
                scrapCount -= 5;
            }
            else
            {
                scrapCount = 0;
            }
            FindObjectOfType<SavingWrapper>().Save();
        }

        public object CaptureState()
        {
            return new SerializableInventory(FlareCount, BombCount, scrapCount);
        }

        public void RestoreState(object state)
        {
            SerializableInventory inventory = (SerializableInventory)state;
            BombCount = inventory.bombCount;
            FlareCount = inventory.flareCount;
            scrapCount = inventory.scrapCount;
        }
    }
}
