using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Movable
{
    public class BombPickup : MonoBehaviour
    {
        private Inventory inventory;

        private void Start()
        {
            inventory = GetComponent<Inventory>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {

            }
        }
    }
}

