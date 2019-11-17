using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool _isPaused;

    [SerializeField] GameObject[] peristantObjectPrefabs;
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource audioSource;

    static bool hasSpawned = false;

    private void Awake( ) {
        
        if( !hasSpawned ) {
            SpawnPersistantObjects();
            hasSpawned = true;
        }
    }
    private void Start( ) {
        Screen.SetResolution( 1920, 1080, true );
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        audioSource.Play();
        audioSource.volume = 0.4f;
        audioSource.loop = false;
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
        if (!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
            audioSource.Play();
        }
    }
    private void SpawnPersistantObjects( ) {
        foreach( GameObject gameObject in peristantObjectPrefabs ) {
            GameObject persistantObject = Instantiate( gameObject );
            DontDestroyOnLoad( persistantObject );
        }
    }
}
