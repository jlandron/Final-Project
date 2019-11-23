using Game.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{

    public class ButtonHandler : MonoBehaviour
    {

        public Toggle[] resolutionToggles;
        public Toggle fullscreenToggle;
        public int[] screenWidths;
        int activeScreenResIndex;

        private float minTimeBetweenPresses = 1f;
        private float timeSinceClick = 0f;
        private void Start()
        {
            fullscreenToggle.isOn = GameManager.instance.isFullscreen;
        }
        public void LoadLevel()
        {
            if (timeSinceClick > minTimeBetweenPresses)
            {
                Debug.Log("Loading next Level");
                timeSinceClick = 0;
                FindObjectOfType<SavingWrapper>().Save();
                FindObjectOfType<SavingWrapper>().LoadNextScene();
                FindObjectOfType<SavingWrapper>().Load();
            }
        }

        public void QuitGame()
        {

            timeSinceClick = 0;
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

        }

        public void GoToMenu()
        {
            FindObjectOfType<SavingWrapper>().Save();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        public void ResumeGame()
        {

            FindObjectOfType<PauseMenu>().ActivateObjects(false);

        }
        public void DeleteSaveFile()
        {
            if (timeSinceClick > minTimeBetweenPresses)
            {
                Debug.Log("Save file deleted");
                FindObjectOfType<SavingWrapper>().Delete();
                timeSinceClick = 0;
            }
        }
        public void SetScreenResolution(int i)
        {
            if (resolutionToggles[i].isOn)
            {
                activeScreenResIndex = i;
                float aspectRatio = 16 / 9f;
                Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
                PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
                PlayerPrefs.Save();
            }
        }

        public void SetFullscreen(bool isFullscreen)
        {
            for (int i = 0; i < resolutionToggles.Length; i++)
            {
                resolutionToggles[i].interactable = !isFullscreen;
            }

            if (isFullscreen)
            {
                Resolution[] allResolutions = Screen.resolutions;
                Resolution maxResolution = allResolutions[allResolutions.Length - 1];
                Screen.SetResolution(maxResolution.width, maxResolution.height, true);
            }
            else
            {
                SetScreenResolution(activeScreenResIndex);
            }

            PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
            PlayerPrefs.Save();
        }

        private void Update()
        {
            timeSinceClick += Time.deltaTime;
        }
        public void SetMasterVolume(float value)
        {
            FindObjectOfType<GameManager>().GetComponent<AudioSource>().volume = value;
        }
    }
}
