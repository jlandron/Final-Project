using System.Collections;
using UnityEngine;
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
        private bool showSignal = false;

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

        private void Start()
        {
            signalMeshRenderer = signal.GetComponent<MeshRenderer>();
            //signal.gameObject.SetActive( false );
            SetScale(0);
            wantToShow.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _inRange = true;
                //signal.gameObject.SetActive( true );
                FadeIn();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _inRange = false;
                //signal.gameObject.SetActive( false );
                FadeOut();
            }
        }
        private void Update()
        {
            if (_inRange && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Showing Text");
                wantToShow.gameObject.SetActive(true);
                Invoke("DisableText", 5);
            }
            if (showSignal)
            {

            }
        }

        void DisableText()
        {
            Debug.Log("Disabling Text");
            wantToShow.gameObject.SetActive(false);
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