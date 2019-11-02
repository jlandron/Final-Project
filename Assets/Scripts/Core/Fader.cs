using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

    [SerializeField]
    private Image image;

    public IEnumerator FadeOut( float time ) {
        DontDestroyOnLoad( image );
        float alphaValue = 0;
        while( alphaValue <= 1 ) {
            alphaValue += Time.deltaTime / time;
            Color c = new Color( 0, 0, 0, alphaValue );
            image.color = c;
            yield return null;
        }
    }
    public IEnumerator FadeIn( float time ) {
        float alphaValue = 1;
        while( alphaValue >= 0 ) {
            alphaValue -= Time.deltaTime / time;
            Color c = new Color( 0, 0, 0, alphaValue );
            image.color = c;
            yield return null;
        }
    }

    public void FadeOutImmeduate( ) {
        Color c = new Color( 0, 0, 0, 1 );
        image.color = c;
    }
}


