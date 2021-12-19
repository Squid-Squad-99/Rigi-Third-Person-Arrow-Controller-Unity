using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{


    [CreateAssetMenu(menuName = "State Machine/Actions/Transition By Free Fall ActionSO")]
    public class TransitionByFreeFallActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO ToSTateSO;
        [Tooltip("Min speed to consider character is free fall")]
        public float FreeFallMinSpeed;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new TransitionByFreeFallAction(this, stateMachine);
        }
    }

    public class TransitionByFreeFallAction : Action
    {
        // reference
        private Rigidbody _rigidbody;
        private ICheckIsGrounded _checkIsGrounded;

        public TransitionByFreeFallAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
            _checkIsGrounded = ThisStateMachine.GetComponent<ICheckIsGrounded>();
        }

        public override void FixedUpdate()
        {
            if(_rigidbody.velocity.y <= -((TransitionByFreeFallActionSO)OriginSO).FreeFallMinSpeed){
                // is free falling
                ThisStateMachine.SwitchState(((TransitionByFreeFallActionSO)OriginSO).ToSTateSO);
            }
        }



    }
}