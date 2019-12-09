﻿using System.Collections;
using UnityEngine;
namespace Game.Movable
{
    public class WeaponControl : MonoBehaviour
    {
        public AudioClip laserSound;
        private AudioSource audioData;

        public float damage = 1f;

        [SerializeField]
        private ParticleSystem gun;
        [SerializeField]
        private float fireRate = 1;

        private bool canfire = true;
        private Recharge recharge;

        // Start is called before the first frame update
        void Start()
        {
            audioData = GetComponent<AudioSource>();
            recharge = GetComponent<Recharge>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateGunRotation();
           
            if (recharge.BatteryCharged && Input.GetMouseButtonDown(0))
            {
                if (canfire)
                {
                    StartCoroutine(HandleShoot());
                }
            }

        }

        private void UpdateGunRotation()
        {
            Vector2 startPoint = gameObject.transform.position;
            Vector2 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (endPoint - startPoint);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gun.transform.rotation = rotation;
            Debug.DrawLine(startPoint, endPoint, Color.cyan);
        }

        private IEnumerator HandleShoot()
        {
            audioData.clip = laserSound;
            audioData.Play();
            gun.Emit(1);
            canfire = false;
            yield return new WaitForSeconds(fireRate);
            canfire = true;
        }
    }
}