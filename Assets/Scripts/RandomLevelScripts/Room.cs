using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.RandomRoom {
    public class Room : MonoBehaviour {
        public int type;

        public void DestroyRoom( ) {
            Destroy( gameObject );
        }
    }
}