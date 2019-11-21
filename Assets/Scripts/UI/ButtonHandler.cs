using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Core;

namespace Game.UI {

    public class ButtonHandler : MonoBehaviour {
        public void LoadLevel( ) {
            Debug.Log( "Loading next Level" );
            FindObjectOfType<SavingWrapper>().LoadScene();
        }

        public void QuitGame( ) {
            Application.Quit( );
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
            Debug.Log("Save file deleted");
            FindObjectOfType<SavingWrapper>().Delete(); ;
        }
    }
}
