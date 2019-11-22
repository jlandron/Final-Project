using System.Collections;
using UnityEngine;
namespace Game.Movable
{
    public class WeaponControl : MonoBehaviour
    {
        public AudioClip laserSound;
        private AudioSource audioData;

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
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            difference.Normalize();
            difference.Set(difference.x, difference.y, 0);
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gun.transform.rotation = rotation;
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