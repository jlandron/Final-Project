using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Core;

namespace Game.UI {

    public class ButtonHandler : MonoBehaviour {
        public void LoadLevel( ) {
            Debug.Log( "Loading next Level" );
            int nextScene = SceneManager.GetActiveScene( ).buildIndex + 1;
            FindObjectOfType<LoadLevel>( ).DoLoad( nextScene );
        }

        public void QuitGame( ) {
            Application.Quit( );
        }
    }
}
