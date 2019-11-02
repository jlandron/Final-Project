using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDungeon : MonoBehaviour
{
    [SerializeField] float fadeOutTime = 3f;
    [SerializeField] float fadeInTime = 3f;

    private Fader _fader;
    [SerializeField]
    private int sceneToLoad = -1;
    private void Start( ) {
        sceneToLoad = SceneManager.GetActiveScene( ).buildIndex + 1;
    }
    private void OnTriggerEnter2D( Collider2D collision ) {
        if(collision.gameObject.tag == "Player" ) {
            StartCoroutine( Transition( ) );
        }
    }
    private IEnumerator Transition( ) {
        if( sceneToLoad < 0 ) {
            Debug.Log( "Scene to load not set" );
            yield break;
        }

        DontDestroyOnLoad( gameObject );
        _fader = FindObjectOfType<Fader>( );
        if( !_fader ) {
            Debug.Log( "fader not found!" );
        }
        yield return _fader.FadeOut( fadeOutTime );

        yield return SceneManager.LoadSceneAsync( sceneToLoad );
        
        yield return new WaitForSeconds( fadeInTime );
        yield return _fader.FadeIn( fadeInTime );
        Destroy( gameObject );
    }
}
