using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RigiArcher.Magic{

    /// <summary>
    /// magic basic logic
    /// cold down time, casting time, cast end event, spell end event
    /// </summary>
    public abstract class MagicBase : MonoBehaviour
    {

        public MagicManager.MagicIdEnum MagicId => _magicId;
        [SerializeField] MagicManager.MagicIdEnum _magicId;

        [Header("Magic Basic Setting")]
        [SerializeField] float _coolDownTime;
        [SerializeField] float _castMagicTime;

        [Header("Magic Basic data")]
        [SerializeField] float _coolDownTimeDelta;
        [SerializeField] float _castMagicTimeDelta;
        [SerializeField] bool _isCasting;
        [SerializeField] bool _isSpelling;
        [SerializeField] bool _isCoolDowning;


        // event is in per cast magic basic, this should be clean every time spell finish
        public UnityEvent CastMagicFinish;
        public UnityEvent SpellFinish;

        public float CoolDownTime => _coolDownTime;
        public float CoolDownTimeDelta => _coolDownTimeDelta;
        public float CastMagicTime => _castMagicTime;
        public float CastMagicTimeDelta => _castMagicTimeDelta;

        public bool IsCasting => _isCasting;
        public bool IsSpelling => _isSpelling;
        public bool IsCoolDowning => _isCoolDowning;

        // data
        protected RigiArcherObject _magicUser;
        protected Vector3 _AimPointAtCast;

        protected abstract void UseMagic();
        public abstract void CancelMagic();

        public bool CanCastMagic()
        {
            return CoolDownTimeDelta <= 0;
        }

        public void CastMagic()
        {
            StartCoroutine(CastMagicRoutine());
        }

        private IEnumerator CastMagicRoutine()
        {
            // count down cast time
            _castMagicTimeDelta = _castMagicTime;
            _isCasting = true;
            _AimPointAtCast = _magicUser.AimPoint;

            while(_castMagicTimeDelta > 0)
            {
                _castMagicTimeDelta -= Time.deltaTime;
                yield return null;
            }
            // finish casting, invoke event
            _isCasting = false;
            CastMagicFinish.Invoke();

            // use magic
            _isSpelling = true;
            SpellFinish.AddListener(() => _isSpelling = false);
            UseMagic();

            // count down cool down
            _coolDownTimeDelta = _coolDownTime;
            _isCoolDowning = true;
            while(_coolDownTimeDelta > 0)
            {
                _coolDownTimeDelta -= Time.deltaTime;
                yield return null;
            }
            // finish cool down
            _isCoolDowning = false;
        }

        protected virtual void Awake()
        {
            // clean up event every time spell end
            SpellFinish.AddListener(CleanUpEvent);
        }

        private void CleanUpEvent()
        {
            CastMagicFinish.RemoveAllListeners();
            SpellFinish.RemoveAllListeners();
            // add clean up event for next time
            SpellFinish.AddListener(CleanUpEvent);
        }

        public virtual void Equip(RigiArcherObject magicUser){
            _magicUser = magicUser;
            gameObject.SetActive(true);
        }
        public virtual void UnEquip(){
            _magicUser = null;
            gameObject.SetActive(false);
        }
    }

}
