using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher{

    public class GetAimPoint : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] LayerMask _aimLayerMask = new LayerMask();
        [SerializeField] Transform _debugTransform;

        [Header("Data")]
        [SerializeField] Vector3 _aimPoint;
        public Vector3 AimPoint => _aimPoint;

        private void Update() {
            Vector2 screenCenterPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPos);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, float.MaxValue,_aimLayerMask)){
                _aimPoint = hit.point;
                _debugTransform.position = _aimPoint;
            }
        }     
    }

}
