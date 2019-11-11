using System.Collections;
using UnityEngine;
namespace Game.Movable
{
    public class WeaponControl : MonoBehaviour
    {
        public ReticleControl m_Reticle;

        [SerializeField]
        private ParticleSystem gun;
        [SerializeField]
        private float fireRate = 1;
        private bool canfire = true;

        // Start is called before the first frame update
        void Start()
        {
            m_Reticle = GetComponentInChildren<ReticleControl>();
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
            Vector2 endPoint = m_Reticle.gameObject.transform.position;
            Vector2 direction = (endPoint - startPoint);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gun.transform.rotation = rotation;
            Debug.DrawLine(startPoint, endPoint, Color.cyan);
        }

        private IEnumerator HandleShoot()
        {
            gun.Emit(1);
            canfire = false;
            yield return new WaitForSeconds(fireRate);
            canfire = true;
        }
    }
}