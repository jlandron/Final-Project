﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    [SerializeField]
    private int hitPoint;
    private bool _isPaused;

    private void Awake( ) {
        if( _instance == null ) {
            _instance = this;
        } else {
            Destroy( this );
        }
    }
    private void Start( ) {
        Screen.SetResolution( 1600, 900, false );
    }
    private void Update( ) {
        if( SceneManager.GetActiveScene( ).name == ( "_preload" ) ) {
            SceneManager.LoadScene( 1 );
        }
        if( Input.GetKeyDown( KeyCode.Q ) ) {
            Application.Quit( );
        }
        if( Input.GetKeyDown( KeyCode.Escape ) ) {
            SceneManager.LoadScene( 0 );
        }
        if( Input.GetKeyDown( KeyCode.P ) ) {
            if( !_isPaused ) {
                _isPaused = true;
                Time.timeScale = 0;
            } else {
                _isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

}