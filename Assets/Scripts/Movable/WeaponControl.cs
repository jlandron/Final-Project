using System.Collections;
using UnityEngine;
namespace Game.Movable
{
    public class WeaponControl : MonoBehaviour
    {
        public GameObject reticlePrefab;
        private GameObject currentReticle;
        private ReticleControl reticle;
        public AudioClip laserSound;
        private AudioSource audioData;

        [SerializeField]
        private ParticleSystem gun;
        [SerializeField]
        private float fireRate = 1;
        private bool canfire = true;

        // Start is called before the first frame update
        void Start()
        {
            audioData = GetComponent<AudioSource>();
            currentReticle = Instantiate(reticlePrefab, transform.position, Quaternion.identity);
            reticle = currentReticle.GetComponent<ReticleControl>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateGunRotation();
            
            if (Input.GetButton("Fire1"))
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
            Vector2 endPoint = reticle.gameObject.transform.position;
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