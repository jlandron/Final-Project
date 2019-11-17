﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Movable {
    public class ReticleControl : MonoBehaviour {
        private Vector3 mouseCoords;
        public float MouseSensitivity = 0.2f;
        private Camera _camera;

        private void Start( ) {
            _camera = FindObjectOfType<Camera>( );
        }
        void Update( ) {
            mouseCoords = Input.mousePosition;
            mouseCoords = _camera.ScreenToWorldPoint( mouseCoords );
            this.gameObject.transform.position = Vector2.Lerp( transform.position, mouseCoords, MouseSensitivity );
        }
    }
}
