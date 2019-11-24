using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class LoadLevel : MonoBehaviour
    {


        [SerializeField] int sceneToLoad = -1;
        [SerializeField] float fadeOutTime = 3f;
        [SerializeField] float fadeInTime = 3f;

        private Fader _fader;
        private SavingWrapper _saver;
        private void Start()
        {
            _fader = FindObjectOfType<Fader>();
            _saver = FindObjectOfType<SavingWrapper>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        public void DoLoad(int level)
        {

            sceneToLoad = level;
            StartCoroutine(Transition());
        }
        private IEnumerator Transition()
        {
            GetComponent<Collider2D>().enabled = false;
            if (sceneToLoad < 0)
            {
                Debug.Log("Scene to load not set");
                yield break;
            }
            if (sceneToLoad >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Scene does not exist");
                yield break;
            }

            DontDestroyOnLoad(gameObject);
            if (!_fader)
            {
                Debug.Log("fader not found!");
            }
            yield return _fader.FadeOut(fadeOutTime);

            _saver.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            _saver.Load();

            yield return new WaitForSeconds(fadeInTime);
            yield return _fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }
    }
}
