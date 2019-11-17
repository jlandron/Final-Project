using Game.Core;
using Game.Saving;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Movable
{
    public class Inventory : MonoBehaviour, ISaveable
    {
        public Text FlareDisplay = null;

        public GameObject BombPrefab;
        public int BombCount = 1;
        public int BombMax = 1;

        public GameObject FlarePrefab;
        public int FlareCount = 3;
        public int FlareMax = 3;

        public GameObject AttachedBomb;
        //savable object for inventory, any new items should be added to object and ISaveable methods updated
        [System.Serializable]
        internal class SerializableInventory
        {
            internal int flareCount, bombCount;
            internal SerializableInventory(int flare, int bomb)
            {
                flareCount = flare;
                bombCount = bomb;
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
                if (BombCount < 1)
                {
                    BombCount++;
                    Destroy(collision.gameObject);
                }
            }
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

        void RescanPathfinding()
        {
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

        public void SetAllToZero()
        {
            BombCount = 0;
            FlareCount = 0;
            FindObjectOfType<SavingWrapper>().Save();
        }

        public object CaptureState()
        {
            return new SerializableInventory(FlareCount, BombCount);
        }

        public void RestoreState(object state)
        {
            SerializableInventory inventory = (SerializableInventory)state;
            BombCount = inventory.bombCount;
            FlareCount = inventory.flareCount;
        }
    }
}
