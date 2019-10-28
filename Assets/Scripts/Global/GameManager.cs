using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance = null;

    [SerializeField]
    private int hitPoint;
    private bool _isPaused;
    //for prototyping only!!!!!! DELETE FOR BUILD!!!
    private int _numScenes;

    private void Awake( ) {
        if( _instance == null ) {
            _instance = this;
        } else {
            Destroy( this );
        }
    }
    private void Start( ) {
        Screen.SetResolution( 1600, 900, false );
        _numScenes = SceneManager.sceneCount;
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
        //for prototyping only!!!!!! DELETE FOR BUILD!!!
        if( Input.GetKeyDown( KeyCode.Minus ) ) {
            int prevScene = SceneManager.GetActiveScene( ).buildIndex - 1;
            SceneManager.LoadScene( prevScene );
        }
        if( Input.GetKeyDown( KeyCode.Equals ) ) {
            int nextScene = SceneManager.GetActiveScene( ).buildIndex + 1;
            SceneManager.LoadScene( nextScene );
        }
    }

}