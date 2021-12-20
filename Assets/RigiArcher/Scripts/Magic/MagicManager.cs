using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher.MeshSocket;

namespace RigiArcher.Magic{

    /// <summary>
    /// we use left hand to cast a magic, ext. rope
    /// provide Equip Method to equip magic to character
    /// </summary>
    public class MagicManager : MonoBehaviour
    {
        public enum MagicIdEnum{
            None,
            Rope,
        }

        [Header("Setting")]
        [SerializeField] MagicIdEnum _InitEquippedMagicId;
        [SerializeField] float _EnableAnimLayerTime;
        
        [Header("Data")]
        [SerializeField] MagicIdEnum _currentEquippedMagicId;
        public MagicBase CurrentEquipedMagic => _magicMap[_currentEquippedMagicId];

        // data
        private Dictionary<MagicIdEnum, MagicBase> _magicMap = new Dictionary<MagicIdEnum, MagicBase>();

        // reference
        private MeshSocketManager _meshSocketManager;
        private Animator _animator;
        private int _leftArmAnimLayerIndex;
        private int _AnimParamCastMagicId;



        private void Awake() {
            // reference
            _meshSocketManager = GetComponent<MeshSocketManager>();
            _animator = GetComponent<Animator>();
            _leftArmAnimLayerIndex = _animator.GetLayerIndex("LeftArm");
            _AnimParamCastMagicId = Animator.StringToHash("CastMagic");

            // populate magic map
            MagicBase[] magics = GetComponentsInChildren<MagicBase>();
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

            // hold to right hand
            _meshSocketManager.Attach(CurrentEquipedMagic.transform, MeshSocketManager.SocketIdEnum.LeftHand);
        }

        public void CastMagic(){
            // check have equip
            if(_currentEquippedMagicId == MagicIdEnum.None) Debug.LogWarning("[Magic Manger] no magic equip");

            if(CurrentEquipedMagic.ColdDownTimeDelta <= 0){
                // play cast magic animation (visual)
                StartCoroutine(PlayCastMagicAnimation());
                // cast magic with cast time (logic)
                StartCoroutine(CastingMagicWithCastTime());
            }

        }

        private IEnumerator CastingMagicWithCastTime(){
            yield return new WaitForSeconds(CurrentEquipedMagic.CastMagicTime);
            CurrentEquipedMagic.CastMagic();
        }

        private IEnumerator PlayCastMagicAnimation(){
            // enable left w  rm layer
            yield return EnableLeftHandLayer();
            // play cast magic animation
            _animator.SetBool(_AnimParamCastMagicId, true);
        }

        public void OnCastMagicAnimationEnd(){
            // set param to false
            _animator.SetBool(_AnimParamCastMagicId, false);
        }

        private float _layerChangeVel;
        private IEnumerator EnableLeftHandLayer(){
            float currentLayerWeight = _animator.GetLayerWeight(_leftArmAnimLayerIndex);
            while(true){
                float targetWeight = Mathf.SmoothDamp(currentLayerWeight, 1f, ref _layerChangeVel, _EnableAnimLayerTime);
                _animator.SetLayerWeight(_leftArmAnimLayerIndex, targetWeight);
                currentLayerWeight = targetWeight;
                if(targetWeight >= 0.99) break;
                yield return null;
            }
        }
    }

}
