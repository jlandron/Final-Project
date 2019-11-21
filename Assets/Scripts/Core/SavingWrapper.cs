using Game.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 2f;

        private bool wasDeleted = false;
        public bool WasDeleted { get => wasDeleted; private set => wasDeleted = value; }

        public void LoadScene()
        {
            wasDeleted = false;
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmeduate();
            int sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            Save();
            SceneManager.LoadScene(sceneToLoad);
            Load();
            StartCoroutine(FindObjectOfType<Fader>().FadeIn(fadeInTime));
        }
#if UNITY_EDITOR
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }
        }
#endif
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
            wasDeleted = false;
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
            WasDeleted = true;
        }
    }
}