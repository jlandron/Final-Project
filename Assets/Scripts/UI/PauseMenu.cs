using Game.Movable;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        RectTransform[] children;
        [SerializeField]
        private bool _isPaused;
        [SerializeField]
        private Text scrapText;

        void Start()
        {
            ActivateObjects(false);
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!_isPaused)
                {
                    ActivateObjects(true);
                }
                else
                {
                    ActivateObjects(false);
                }
            }
            if (_isPaused)
            {
                scrapText.text = "Scrap Available " + FindObjectOfType<Inventory>().scrapCount;
            }
        }
        internal void ActivateObjects(bool active)
        {
            GetComponent<Image>().enabled = active;
            foreach (var child in children)
            {
                child.gameObject.SetActive(active);
            }
            _isPaused = active;
            Time.timeScale = active ? 0 : 1;
        }


    }
}
