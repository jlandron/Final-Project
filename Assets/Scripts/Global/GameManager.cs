using Game.Movable;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GameObject[] peristantObjectPrefabs;
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource audioSource;

    public bool isFullscreen = false;

    static bool hasSpawned = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (!hasSpawned)
        {
            SpawnPersistantObjects();
            hasSpawned = true;
        }
        isFullscreen = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
    }


    private void Start()
    {
        
        Screen.SetResolution(1920, 1080, isFullscreen);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Length)];
        audioSource.Play();
        audioSource.volume = 0.4f;
        audioSource.loop = false;
    }

    

    private void FixedUpdate()
    {

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        switch (buildIndex)
        {
            case 0:
                SceneManager.LoadScene(1);
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        
        if (!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic[UnityEngine.Random.Range(0, backgroundMusic.Length)];
            audioSource.Play();
        }
    }
    private void SpawnPersistantObjects()
    {
        foreach (GameObject gameObject in peristantObjectPrefabs)
        {
            GameObject persistantObject = Instantiate(gameObject);
            DontDestroyOnLoad(persistantObject);
        }
    }

}
