using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.Magic{

    public class RopeMagic : MagicBase
    {
        public override MagicManager.MagicIdEnum MagicId => _magicId;

        public override float ColdDownTime => _coldDownTime;

        public override float ColdDownTimeDelta => _coldDownTimeDelta;

        public override float CastMagicTime => _castMagicTime;

        [SerializeField] MagicManager.MagicIdEnum _magicId;
        [SerializeField] float _coldDownTime;
        [SerializeField] float _coldDownTimeDelta;
        [SerializeField] float _castMagicTime;

        public override void CastMagic()
        {
            
        }

        public override void Equip()
        {
            throw new System.NotImplementedException();
        }

        public override void UnEquip()
        {
            throw new System.NotImplementedException();
        }
    }

}
