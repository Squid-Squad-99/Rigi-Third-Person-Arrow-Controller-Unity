using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.Magic{

    /// <summary>
    /// we use left hand to cast a magic, ext. rope
    /// </summary>
    public class MagicManager : MonoBehaviour
    {
        public enum MagicIdEnum{
            Rope,
        }
        
        [Header("Data")]
        [SerializeField] MagicIdEnum _currentEquipedMagic;

        private Dictionary<MagicIdEnum, IMagic> _magicMap = new Dictionary<MagicIdEnum, IMagic>();


        private void Awake() {
            // populate magic map
            IMagic[] magics = GetComponentsInChildren<IMagic>();
            foreach(IMagic magic in magics){
                _magicMap.Add(magic.MagicId, magic);
            }
        }
        
        private void Equip(MagicIdEnum magicId){

        }
    }

}
