using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RigiArcher.Magic{

    public class RopeMagic : MagicBase
    {
        [Header("Rope Gun Setting")]
        [SerializeField] float _ropeSpeed;

        [Header("Make")]
        [SerializeField] GameObject _ropePrefab;

        // data
        private Rope _rope;

        // reference
        private Rigidbody _rigidbody;
        private RigAim.RigAimManager _rigAimManager;

        public override void Equip(RigiArcherObject magicUser)
        {
            base.Equip(magicUser);

            // set reference
            _rigidbody = magicUser.GetComponent<Rigidbody>();
            _rigAimManager = magicUser.GetComponent<RigAim.RigAimManager>();
        }

        protected override void UseMagic()
        {
            // make sure have set magic user
            Debug.Assert(_magicUser != null, "[RopeMagic] use magic without magic user");

            // shoot rope
            GameObject ropeGameObject = Instantiate(_ropePrefab, Vector3.zero, _ropePrefab.transform.rotation);
            _rope = ropeGameObject.GetComponent<Rope>();
            _rope.ShootTo(_AimPointAtCast, _ropeSpeed, transform, _magicUser);

            // magic user left hand aim rope head
            _rigAimManager.Aim(_rope.HeadPos, RigAim.RigAimManager.RigAimIdEnum.LeftHand);

        }

        public override void CancelMagic()
        {
            _rigAimManager.UnAim();
            SpellFinish.Invoke();
            Destroy(_rope.gameObject);
        }


    }

}
