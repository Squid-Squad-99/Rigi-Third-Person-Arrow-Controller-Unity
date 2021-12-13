using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Set Animation Parameter ActionSO")]
    public class SetAnimParamActionSO : ActionSO
    {
        [Header("Setting")]
        public string AnimParamName;
        public ParamTypeEnum ParamType;
        public WhenEnum When;
        public bool BoolParamValue;
        public float FloatParamValue;
        public int IntParamValue;

        public enum ParamTypeEnum{
            Bool,
            Float,
            Int,
        }

        public enum WhenEnum{
            OnStateEnter,
            OnStateExit,
        }

        public override Action GetAction(StateMachine stateMachine)
        {
            return new SetAnimParamAction(this, stateMachine);
        }
    }

    public class SetAnimParamAction : Action
    {
        private int _animId;

        public SetAnimParamAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // get anim param id
            _animId = Animator.StringToHash(((SetAnimParamActionSO)OriginSO).AnimParamName);
        }

        public override void OnStateEnter()
        {
            if(IsNow(SetAnimParamActionSO.WhenEnum.OnStateEnter)) SetAnimParam();
        }

        public override void OnStateExit()
        {
            if(IsNow(SetAnimParamActionSO.WhenEnum.OnStateExit)) SetAnimParam();
        }

        private bool IsNow(SetAnimParamActionSO.WhenEnum now){
            if(((SetAnimParamActionSO)OriginSO).When == now){
                return true;
            }
            return false;
        }

        private void SetAnimParam(){
            switch (((SetAnimParamActionSO)OriginSO).ParamType)
            {
                case SetAnimParamActionSO.ParamTypeEnum.Bool:
                    ThisStateMachine.CharacterAnimator.SetBool(_animId, ((SetAnimParamActionSO)OriginSO).BoolParamValue);
                    break;
                case SetAnimParamActionSO.ParamTypeEnum.Float:
                    ThisStateMachine.CharacterAnimator.SetFloat(_animId, ((SetAnimParamActionSO)OriginSO).FloatParamValue);
                    break;
                case SetAnimParamActionSO.ParamTypeEnum.Int:
                    ThisStateMachine.CharacterAnimator.SetInteger(_animId, ((SetAnimParamActionSO)OriginSO).IntParamValue);
                    break;
                default:
                    break;
            }
            
        }

    }

}
