using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleControl : MonoBehaviour
{
    private Vector3 mouseCoords;
    public float MouseSensitivity = 0.1f;
    void Update()
    {
        mouseCoords = Input.mousePosition;
        mouseCoords = Camera.main.ScreenToWorldPoint( mouseCoords );
        this.gameObject.transform.position = Vector2.Lerp( transform.position, mouseCoords, MouseSensitivity );
    }
}
