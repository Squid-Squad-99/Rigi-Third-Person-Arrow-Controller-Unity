using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    /// <summary>
    /// handle Jump and when to switch to free fall state
    /// </summary>
    [CreateAssetMenu(menuName = "State Machine/Actions/Jump ActionSO")]
    public class JumpActionSO : ActionSO
    {
        [Header("Setting")]
        public float JumpHeight;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new JumpAction(this, stateMachine);
        }
    }

    public class JumpAction : Action
    {
        // reference
        private ApplyGravity _applyGravity;
        private Rigidbody _rigidbody;

        public JumpAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // set reference
            _applyGravity = ThisStateMachine.GetComponent<ApplyGravity>();
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
        }

        public override void OnStateEnter()
        {
            // caculate initial Speed
            float initialSpeed = Mathf.Sqrt(2 * 9.8f * _applyGravity.GravityScale * ((JumpActionSO)OriginSO).JumpHeight);
            // get target velocity
            Vector3 targetVelocity = new Vector3(_rigidbody.velocity.x, initialSpeed, _rigidbody.velocity.z);

            // change velocity
            _rigidbody.velocity = targetVelocity;

        }

    }

}