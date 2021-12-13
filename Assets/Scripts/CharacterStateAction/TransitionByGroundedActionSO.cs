using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Transition By Grounded ActionSO")]
    public class TransitionByGroundedActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO ToStateSO; 
        public bool TransitWhenGroundedIs;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new TransitionByGroundedAction(this, stateMachine);
        }
    }

    public class TransitionByGroundedAction : Action
    {
        // reference
        private ICheckIsGrounded _checkIsGrounded;

        public TransitionByGroundedAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // get reference
            _checkIsGrounded = ThisStateMachine.GetComponent<ICheckIsGrounded>();
        }

        public override void FixedUpdate()
        {
            // change state when grounded matched
            if(_checkIsGrounded.IsGrounded == ((TransitionByGroundedActionSO)OriginSO).TransitWhenGroundedIs){
                ThisStateMachine.SwitchState(((TransitionByGroundedActionSO)OriginSO).ToStateSO);
            }
        }

    }
}