using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace RigiArcher.RigAim{

    public class RigAimManager : MonoBehaviour
    {
        [Header("Contorled component")]
        [SerializeField] Rig _rig;

        [Header("Setting")]
        [SerializeField] float _weightSmoothTime;

        public enum RigAimIdEnum{
            LeftHand,
        }

        private Dictionary<RigAimIdEnum, RigAim> _rigAimMap = new Dictionary<RigAimIdEnum, RigAim>();

        private void Awake() {
            // populate rigAim Map
            RigAim[] rigAims = GetComponentsInChildren<RigAim>();
            foreach(RigAim rigAim in rigAims){
                _rigAimMap.Add(rigAim.RigAimId, rigAim);
            }
        }

        private float _currentChangeVelocity;
        private void SmoothChangeRigWeight(float targetValue){
            if(isSmoothChanging) StopCoroutine("ChangeWeightRoutine");
            StartCoroutine("ChangeWeightRoutine", targetValue);
        }

        private bool isSmoothChanging = false;
        private IEnumerator ChangeWeightRoutine(float targetValue){
            isSmoothChanging = true;
            while(true){
                float value = Mathf.SmoothDamp(_rig.weight, targetValue, ref _currentChangeVelocity, _weightSmoothTime);
                _rig.weight = value;
                if(_rig.weight == targetValue) break;
                yield return null;
            }
            isSmoothChanging = false;
        }


        public void Aim(Transform aimObject, RigAimIdEnum rigAimId){
            SmoothChangeRigWeight(0.8f);
            _rigAimMap[rigAimId].Aim(aimObject);
        }

        public void UnAim(){
            SmoothChangeRigWeight(0f);
        }
    }

}
