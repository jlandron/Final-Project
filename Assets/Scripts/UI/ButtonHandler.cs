using Game.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{

    public class ButtonHandler : MonoBehaviour
    {

        private float minTimeBetweenPresses = 1f;
        private float timeSinceClick = 0f;
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
        private void Update()
        {
            timeSinceClick += Time.deltaTime;
        }
    }
}
