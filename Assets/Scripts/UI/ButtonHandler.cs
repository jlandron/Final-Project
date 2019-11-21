using Game.Core;
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
                FindObjectOfType<SavingWrapper>().LoadScene();
                timeSinceClick = 0;
            }
        }

        public void QuitGame()
        {
            if (timeSinceClick > minTimeBetweenPresses)
            {
                Application.Quit();
                timeSinceClick = 0;
            }
        }

        public void GoToMenu()
        {
            if (timeSinceClick > minTimeBetweenPresses)
            {
                FindObjectOfType<SavingWrapper>().Save();
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
                timeSinceClick = 0;
            }
        }
        public void ResumeGame()
        {
            if (timeSinceClick > minTimeBetweenPresses)
            {
                FindObjectOfType<PauseMenu>().ActivateObjects(false);
                timeSinceClick = 0;
            }
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
