using Game.Movable;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private bool _isPaused;

    private GameObject pauseMenu = null;

    [SerializeField] GameObject[] peristantObjectPrefabs;
    [SerializeField] AudioClip[] backgroundMusic;
    [SerializeField] AudioSource audioSource;

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
    }

    private void OnLevelWasLoaded(int level)
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }
    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic[UnityEngine.Random.Range(0, backgroundMusic.Length)];
        audioSource.Play();
        audioSource.volume = 0.4f;
        audioSource.loop = false;
    }

    internal void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.FindWithTag("PauseMenu");
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false);
            }
        }
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!_isPaused)
            {
                _isPaused = true;
                Time.timeScale = 0;

                //set up better upgrade menu
                if (pauseMenu != null)
                {
                    pauseMenu.SetActive(true);
                    pauseMenu.GetComponent<Text>().text = "Scrap Available" + FindObjectOfType<Inventory>().scrapCount;
                }
            }
            else
            {
                Unpause();
            }
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
