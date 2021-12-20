using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.RigAim{

    public class RigAim : MonoBehaviour
    {
        [SerializeField] RigAimManager.RigAimIdEnum _rigAimId;        
        public RigAimManager.RigAimIdEnum RigAimId => _rigAimId;

        [Header("Reference")]
        [SerializeField] Transform _targetPos;

        // data
        Transform _aimObject;

        public void Aim(Transform aimObject){
            _aimObject = aimObject;
        }

        private void Update() {
            if(_aimObject != null){
                // set target position to aim object's position
                _targetPos.position = _aimObject.position;
            }
        }
    }

}
