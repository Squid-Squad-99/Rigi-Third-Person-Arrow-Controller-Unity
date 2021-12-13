using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher{

    public class ApplyGravity : MonoBehaviour
    {
        [Header("Setting")]
        public float GravityScale = 1;

        // reference
        private Rigidbody _rigidbody;

        private void Awake() {
            // set reference
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            // apply gravity
            _rigidbody.AddForce(GravityScale * 9.8f * Vector3.down, ForceMode.Acceleration);
        }
    }

}

