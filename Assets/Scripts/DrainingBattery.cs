﻿using UnityEngine;
using UnityEngine.UI;

public class DrainingBattery : MonoBehaviour {
    private Image _image;
    private void Start( ) {
        _image = GetComponent<Image>( );
    }
    public void DrainOverTime( float percentage ) {
        _image.transform.localScale = new Vector3( ( percentage ), 1, 1 );
        // add change color later
        if( percentage < 0.25f ) {
            _image.color = Color.red;
        } else {
            _image.color = Color.green;
        }
    }
}
