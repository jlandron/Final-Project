using System.Collections;
using UnityEngine;
namespace Game.Core {
    public class Fader : MonoBehaviour {

        private CanvasGroup _canvasGroup;

        private void Start( ) {
            _canvasGroup = GetComponent<CanvasGroup>( );
        }
        public IEnumerator FadeOut( float time ) {
            float alphaValue = 0;
            while( alphaValue <= 1 ) {
                alphaValue += Time.deltaTime / time;
                _canvasGroup.alpha = alphaValue;
                yield return null;
            }
        }
        public IEnumerator FadeIn( float time ) {
            float alphaValue = 1;
            while( alphaValue >= 0 ) {
                alphaValue -= Time.deltaTime / time;
                _canvasGroup.alpha = alphaValue;
                yield return null;
            }
        }

        public void FadeOutImmeduate( ) {
            _canvasGroup.alpha = 1;
        }
    }
}


