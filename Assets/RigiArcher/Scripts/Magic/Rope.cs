using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.Magic{

    public class Rope : MonoBehaviour
    {
        [Header("Debug setting")]
        [SerializeField] float SuckAcceleration = 8f;
        [SerializeField] float InitialSpeed = 13f;
        [SerializeField] float ToInitSpeedSmoothTime = 0.05f;
        [SerializeField] float SuckInitTime = 0.1f;
        [SerializeField] float MaxHeightSpeed = 6f;

        [Header("reference")]
        [SerializeField] Transform _headPos;
        [SerializeField] LineRenderer _lineRenderer;
        private Rigidbody _rigidbody;
        private ApplyGravity _applyGravity;
        public Transform HeadPos => _headPos;

        // data
        private Transform _shooterTransform;
        private Vector3 _targetPosition;
        private float _speed;
        private bool _moveRope = false;
        private bool _originEnableGravity;
    

        public void ShootTo(Vector3 targetPosition, float Speed, Transform RopeGunTransform, RigiArcherObject magicUser){
            _targetPosition = targetPosition;
            _speed = Speed;
            _shooterTransform = RopeGunTransform;
            _headPos.position = _shooterTransform.position;
            _moveRope = true;

            _applyGravity = magicUser.GetComponent<ApplyGravity>();
            _rigidbody = magicUser.GetComponent<Rigidbody>();
            _originEnableGravity = _applyGravity.enabled;

            _applyGravity.enabled = false; 
        }

        private void OnDestroy() {
            // set back gravity
            _applyGravity.enabled = _originEnableGravity;
        }

        private void Update() {
            if(_moveRope) MoveRope();
            if(_shooterTransform != null && _headPos != null){
                // move line
                _lineRenderer.SetPosition(0, _shooterTransform.position);
                _lineRenderer.SetPosition(1, _headPos.position);
            }

        }

        private void FixedUpdate() {
            // if(_attachWall) SuckUserToRopeHead();
        }

        private void MoveRope(){
            // Move  rope head
            Vector3 direction = (_targetPosition - _headPos.position).normalized;
            _headPos.position += direction * _speed * Time.deltaTime;
        }

        private Vector3 f1;
        private IEnumerator SuckUserToRopeHead(){
            // init suck state
            Vector3 direction;
            direction = (_headPos.position - _rigidbody.transform.position).normalized;
            do
            {
                Vector3 velocity = Vector3.SmoothDamp(_rigidbody.velocity, direction * InitialSpeed, ref f1, ToInitSpeedSmoothTime);
                _rigidbody.velocity = velocity;
                yield return null;
            } while (_rigidbody.velocity != direction * InitialSpeed);
            yield return new WaitForSeconds(SuckInitTime);

            while(true){
                direction = (_headPos.position - _rigidbody.transform.position).normalized;
                if(direction.y > 1f && _rigidbody.velocity.y >= MaxHeightSpeed) direction.y = 0;
                _rigidbody.AddForce(direction * SuckAcceleration, ForceMode.Acceleration);

                // ADD gravity if above rop head
                if(_rigidbody.transform.position.y >= HeadPos.position.y){
                    _rigidbody.AddForce(Vector3.down * _applyGravity.GravityScale * 9.8f, ForceMode.Acceleration);
                }
                yield return new WaitForFixedUpdate();
            }
            
        }

        private void OnTriggerEnter(Collider other) {
            // hit wall, stop moving
            _moveRope = false;
            StartCoroutine(SuckUserToRopeHead());
        }

    }

}
