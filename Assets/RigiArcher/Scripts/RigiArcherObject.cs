using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;

namespace RigiArcher{

    public class RigiArcherObject : MonoBehaviour{
        private GetAimPoint _getAimPoint;

        public Vector3 AimPoint => _getAimPoint.AimPoint;

        private void Awake() {
            _getAimPoint = GetComponent<GetAimPoint>();
            Debug.Assert(_getAimPoint != null, "[RigiArcherObject] Cnat get GetAimPoint Component");
        }
        
    }

}
