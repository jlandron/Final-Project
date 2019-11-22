using UnityEngine;


namespace GAME.Movable {
    public class Shaker : MonoBehaviour {
        [SerializeField]
        private Vector2 shakeDelta;
        [SerializeField]
        private float duration;
        [SerializeField]
        private float omega;

        private float _timeLeft;
        private Vector3 _originalPosition;
        private bool _isShaking = false;
        private float _shakeAmount = 1f;

        private float nextHarmonic = 0f;
        private float _shakeSpeed = 1f;

        public bool IsShaking { get => _isShaking; set => _isShaking = value; }
        public Vector3 OriginalPosition { get => _originalPosition; set => _originalPosition = value; }

        private void Start( ) {
            omega = 90 * Mathf.PI;
        }
        
        // Update is called once per frame
        void Update( ) {

            if( IsShaking ) {

                
                if( _timeLeft <= 0.1 ) {
                    _isShaking = false;
                    return;
                }
                _timeLeft -= Time.deltaTime;
                Vector3 LerpTo = Vector3.Lerp( transform.localPosition, _originalPosition + Random.insideUnitSphere * shakeDelta.x, _shakeSpeed );
                LerpTo.z = transform.localPosition.z;
                transform.localPosition = LerpTo;
            }
        }
        

        internal void StartShaking( Vector2 vector2, float l_duration ) {

            if( !IsShaking ) {
                OriginalPosition = transform.position;
            }
            shakeDelta = vector2;
            duration = l_duration;
            _timeLeft = duration;
            IsShaking = true;
        }
    }
}

