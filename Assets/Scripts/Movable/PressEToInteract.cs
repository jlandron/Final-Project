using Game.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Game.Movable
{
    public class PressEToInteract : MonoBehaviour
    {
        [SerializeField]
        private GameObject signal = null;
        [SerializeField]
        private Text wantToShow = null;
        [SerializeField]
        private bool _inRange = false;
        [SerializeField]
        private float timeToShowText = 2.5f;
        [SerializeField]
        private string textToShow = "BLANK TEXT";
        [SerializeField]
        private bool isLevelEnd = false;

        private MeshRenderer signalMeshRenderer;

        [SerializeField]
        private float changeTimeSeconds = 1f;
        [SerializeField]
        private float startScale = 0;
        [SerializeField]
        private float endScale = 0.4f;
        private float _startScale;
        private float _endScale;
        float changeRate = 0;
        float timeSoFar = 0;
        bool fading = false;
        bool loading = false;
        private Inventory player;
        private string alternateTextToShow = "You must find the key to move on";

        private void Start()
        {
            signalMeshRenderer = signal.GetComponent<MeshRenderer>();
            player = FindObjectOfType<Inventory>();
            SetScale(0);
            wantToShow.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inRange = true;
                FadeIn();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _inRange = false;
                FadeOut();
            }
        }
        private void Update()
        {
            if (_inRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Showing Text");
                wantToShow.gameObject.SetActive(true);
                if (isLevelEnd)
                {
                    if (player.HasLevelKey)
                    {
                        wantToShow.text = textToShow;
                    }
                    else
                    {
                        wantToShow.text = alternateTextToShow;
                    }
                }
                else
                {
                    wantToShow.text = textToShow;
                }
                Invoke("DoNextSteps", timeToShowText);
            }

        }

        void DoNextSteps()
        {

            if (isLevelEnd && !loading && player.HasLevelKey)
            {
                int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
                FindObjectOfType<LoadLevel>().DoLoad(nextScene);
                loading = true;
            }
            
            else if (wantToShow != null)
            {
                Debug.Log("Disabling Text");
                wantToShow.gameObject.SetActive(false);
            }
        }

        public void FadeIn()
        {
            _startScale = startScale;
            _endScale = endScale;

            timeSoFar = 0;
            fading = true;
            StartCoroutine(FadeCoroutine());
        }

        public void FadeOut()
        {
            _startScale = endScale;
            _endScale = startScale;

            timeSoFar = 0;
            fading = true;
            StartCoroutine(FadeCoroutine());
        }

        IEnumerator FadeCoroutine()
        {
            changeRate = (_endScale - _startScale) / changeTimeSeconds;
            SetScale(_startScale);
            while (fading)
            {
                timeSoFar += Time.deltaTime;

                if (timeSoFar > changeTimeSeconds)
                {
                    fading = false;
                    SetScale(_endScale);
                    yield break;
                }
                else
                {

                    SetScale(signalMeshRenderer.transform.localScale.x + (changeRate * Time.deltaTime));
                }

                yield return null;
            }
        }

        public void SetScale(float scale)
        {
            Vector2 scaleVec = new Vector2(Mathf.Clamp(scale, startScale, endScale), Mathf.Clamp(scale, startScale, endScale));
            signalMeshRenderer.transform.localScale = scaleVec;
        }
    }

}