using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool _isPaused;

    [SerializeField] GameObject[] peristantObjectPrefabs;

    static bool hasSpawned = false;

    private void Awake( ) {
        
        if( !hasSpawned ) {
            SpawnPersistantObjects();
            hasSpawned = true;
        }
    }
    private void Start( ) {
        Screen.SetResolution( 1920, 1080, true );
    }
    private void Update( ) {
        if( SceneManager.GetActiveScene( ).name == ( "_preload" ) ) {
            SceneManager.LoadScene(1);
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

        if( Debug.isDebugBuild ) {
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
    private void SpawnPersistantObjects( ) {
        foreach( GameObject gameObject in peristantObjectPrefabs ) {
            GameObject persistantObject = Instantiate( gameObject );
            DontDestroyOnLoad( persistantObject );
        }
    }
}
