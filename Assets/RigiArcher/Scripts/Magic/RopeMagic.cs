using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.Magic{

    public class RopeMagic : MonoBehaviour, IMagic
    {
        public MagicManager.MagicIdEnum MagicId => _magicId;
        [SerializeField] MagicManager.MagicIdEnum _magicId;
    }

}
