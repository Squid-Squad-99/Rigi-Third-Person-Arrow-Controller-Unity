using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.MeshSocket;

namespace RigiArcher.Magic{

    /// <summary>
    /// we use left hand to cast a magic, ext. rope
    /// provide Equip Method to equip magic to character
    /// 1. basic casting animation
    /// </summary>
    public class MagicManager : MonoBehaviour
    {
        public enum MagicIdEnum{
            None,
            Rope,
        }

        [Header("Setting")]
        [SerializeField] MagicIdEnum _InitEquippedMagicId;
        [SerializeField] float _AnimLayerSmoothTime;
        
        [Header("Data")]
        [SerializeField] MagicIdEnum _currentEquippedMagicId;
        public MagicBase CurrentEquipedMagic => _magicMap[_currentEquippedMagicId];

        // data
        private Dictionary<MagicIdEnum, MagicBase> _magicMap = new Dictionary<MagicIdEnum, MagicBase>();

        // reference
        private MeshSocketManager _meshSocketManager;
        private RigiArcherObject _rigiArcherObject;
        private Animator _animator;
        private int _leftArmAnimLayerIndex;
        private int _animParamCastMagicId;



        private void Awake() {
            // reference
            _meshSocketManager = GetComponent<MeshSocketManager>();
            _animator = GetComponent<Animator>();
            _rigiArcherObject = GetComponent<RigiArcherObject>();
            _leftArmAnimLayerIndex = _animator.GetLayerIndex("LeftArm");
            _animParamCastMagicId = Animator.StringToHash("CastMagic");

            // populate magic map
            MagicBase[] magics = GetComponentsInChildren<MagicBase>(true);
            foreach(MagicBase magic in magics){
                _magicMap.Add(magic.MagicId, magic);
            }

            // set to init equipped maigic
            Equip(_InitEquippedMagicId);
        }
        
        public void Equip(MagicIdEnum magicId){
            // check is None
            if(magicId == MagicIdEnum.None) Debug.LogWarning("can't equip none type magic");

            // set to current magic
            _currentEquippedMagicId = magicId;

            //equip magic
            CurrentEquipedMagic.Equip(_rigiArcherObject);

            // hold to left hand
            _meshSocketManager.Attach(CurrentEquipedMagic.transform, MeshSocketManager.SocketIdEnum.LeftHand);
        }

        public void CastMagic(){
            // check have equip 
            if(_currentEquippedMagicId == MagicIdEnum.None) Debug.LogWarning("[Magic Manger] no magic equip");

            if(CurrentEquipedMagic.CanCastMagic())
            {
                // hook magic's events
                CurrentEquipedMagic.CastMagicFinish.AddListener(OnCastMagicFinish);
                CurrentEquipedMagic.SpellFinish.AddListener(OnSpellMagicFinish);
                // play cast magic animation for character
                PlayCastMagicAnimation();
                // cast magic
                CurrentEquipedMagic.CastMagic();
            }
            else
            {
                Debug.Log("[MagicManager] can't cast magic, (not cool down yet");
            }

        }  

        public void CancelMagic(){
            CurrentEquipedMagic.CancelMagic();
        } 

        private void PlayCastMagicAnimation(){
            // play cast magic animation
            _animator.SetBool(_animParamCastMagicId, true);
            // enable anim left hand layer active
            SetLeftHandLayerActive(true);
        }

        private void OnCastMagicFinish(){
            // set anim param 
            _animator.SetBool(_animParamCastMagicId, false);
        }

        private void OnSpellMagicFinish()
        {
            // set anim
            SetLeftHandLayerActive(false);
        }

        private bool _haveSetLayerRoutine = false;
        private void SetLeftHandLayerActive(bool enable)
        {
            if (_haveSetLayerRoutine)
            {
                StopCoroutine("SetLeftHandLayerActiveRoutine");
            }

            _haveSetLayerRoutine = true;
            StartCoroutine("SetLeftHandLayerActiveRoutine", enable);
        }

        private float _layerChangeVel, _targetValue;
        private IEnumerator SetLeftHandLayerActiveRoutine(bool enable){
            float currentLayerWeight = _animator.GetLayerWeight(_leftArmAnimLayerIndex);
            float _targetValue = enable ? 1 : 0;
            while(true){
                float weight = Mathf.SmoothDamp(currentLayerWeight, _targetValue, ref _layerChangeVel, _AnimLayerSmoothTime);
                _animator.SetLayerWeight(_leftArmAnimLayerIndex, weight);
                currentLayerWeight = weight;
                if(Mathf.Abs(_targetValue - weight) <= 0.01) break;
                yield return null;
            }
            _haveSetLayerRoutine = false;
        }
    }

}
