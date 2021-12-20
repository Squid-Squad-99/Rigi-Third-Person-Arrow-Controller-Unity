using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.RigAim{

    public class RigAimManager : MonoBehaviour
    {
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

        public void Aim(Transform aimObject, RigAimIdEnum rigAimId){
            _rigAimMap[rigAimId].Aim(aimObject);
        }
    }

}
