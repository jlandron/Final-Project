﻿using Game.Core;
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
        private string insufficientScrapTextToShow = "Insufficient Scrap";

        private AudioSource audioData;

        [SerializeField]
        public bool flareUpgrade = false;
        [SerializeField]
        public bool batteryUpgrade = false;
        [SerializeField]
        public bool weaponUpgrade = false;

        private void Start()
        {
            audioData = GetComponent<AudioSource>();
            signalMeshRenderer = signal.GetComponent<MeshRenderer>();
            player = FindObjectOfType<Inventory>();
            SetScale(0);
            wantToShow.text = "";
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
                DeactivateText();
            }
        }
        private void Update()
        {
            if (_inRange && Input.GetKeyDown(KeyCode.E))
            {
                audioData.Play();
                Debug.Log("Showing Text");
                wantToShow.text = textToShow;
                if(flareUpgrade)
                {
                    if(player.scrapCount >= 20)
                    {
                        wantToShow.text = "Flare Capacity Increased + 3";
                        player.FlareMax += 3;
                        player.FlareCount = player.FlareMax;

                        player.scrapCount -= 20;
                    }
                    else
                    {
                        wantToShow.text = insufficientScrapTextToShow;
                    }
                }
                else if (batteryUpgrade)
                {
                    if (player.scrapCount >= 10)
                    {
                        wantToShow.text = "Battery Capacity Increased + 10%";
                        player.gameObject.GetComponent<Recharge>().maxCharge += 10;

                        player.scrapCount -= 10;

                    }
                    else
                    {
                        wantToShow.text = insufficientScrapTextToShow;
                    }
                }
                else if (weaponUpgrade)
                {
                    if (player.scrapCount >= 30)
                    {
                        wantToShow.text = "Weapon Damage Increased";
                        player.gameObject.GetComponent<WeaponControl>().damage += 1f;

                        player.scrapCount -= 30;
                    }
                    else
                    {
                        wantToShow.text = insufficientScrapTextToShow;
                    }
                }
                else if (isLevelEnd)
                {
                    if (player.HasLevelKey)
                    {
                        wantToShow.text = textToShow;
                    }
                    else
                    {
                        wantToShow.text = alternateTextToShow;
                    }
                    Invoke("LoadNextLevel", timeToShowText);
                }
            }

        }
        void LoadNextLevel()
        {
            if (isLevelEnd && !loading)
            {
                FindObjectOfType<SavingWrapper>().LoadNextScene();
                loading = true;
            }
        }

        void DeactivateText()
        {

            if (wantToShow != null)
            {
                Debug.Log("Disabling Text");
                wantToShow.text = "";
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