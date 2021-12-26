using System.Collections;
using System.Collections.Generic;
using RigiArcher.MeshSocket;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] GameObject _swordGameObject;

    [Header("Setting")]
    [SerializeField] float CoolDown = 0f;

    // data

    // reference
    private MeshSocketManager _meshSocketManager;
    private Animator _animator;
    private int _animParamEquipSword;
    private int _animParamUseSword;

    private void Start() {
        // reference
        _meshSocketManager = GetComponent<MeshSocketManager>();
        _animator = GetComponent<Animator>();

        // anim param
        _animParamEquipSword = Animator.StringToHash("EquipSword");
        _animParamUseSword = Animator.StringToHash("UseSword");

        // 
        _meshSocketManager.Attach(_swordGameObject.transform, MeshSocketManager.SocketIdEnum.Back);
    }

    public void EquipSword(){
        // animate equip
        _animator.SetBool(_animParamEquipSword, true);

    }

    public void OnAnimationEquipSword(){
        _meshSocketManager.Attach(_swordGameObject.transform, MeshSocketManager.SocketIdEnum.RightHand);
    }

    public void OnAnimationUnEquipSword(){
        _meshSocketManager.Attach(_swordGameObject.transform, MeshSocketManager.SocketIdEnum.Back);
    }

    public void OnAnimationDoneUseSword(){
        _animator.SetBool(_animParamUseSword, false);
    }

    public void UnEquipSword(){
        // animate Unequip
        _animator.SetBool(_animParamEquipSword, false);
    }

    public void UseSword(){
        // animate use sword
        _animator.SetBool(_animParamUseSword, true);
    }


}
