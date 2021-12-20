using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.Magic{

    public abstract class MagicBase : MonoBehaviour
    {
        public abstract MagicManager.MagicIdEnum MagicId { get; }

        public abstract float ColdDownTime {get;}
        public abstract float ColdDownTimeDelta {get;}
        public abstract float CastMagicTime {get;}
        public abstract void CastMagic();

        protected virtual void Start() {
            gameObject.SetActive(false);
        }
        public virtual void Equip(){
            gameObject.SetActive(true);
        }
        public virtual void UnEquip(){
            gameObject.SetActive(false);
        }
    }

}
