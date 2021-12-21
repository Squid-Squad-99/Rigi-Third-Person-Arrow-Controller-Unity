using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Move By Target Velocity ActionSO")]
    public class MoveByTargetVelocityActionSO : ActionSO
    {
        [Header("Setting")]
        public Vector3 TargetVelocity;
        public float SmoothTime;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new MoveByTargetVelocityAction(this, stateMachine);
        }
    }

    internal class MoveByTargetVelocityAction : Action
    {
        // reference
        private Rigidbody _rigidbody;
        private Animator _animator;
        private ApplyGravity _applyGravity;

        // data
        private Vector3 _currentSmoothVelocity;
        private int _animParamSpeedId;
        private bool _isPreStateApplyGravity;

        public override void Awake()
        {
            // set reference
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
            _animator = ThisStateMachine.GetComponent<Animator>();
            _applyGravity = ThisStateMachine.GetComponent<ApplyGravity>();

            // get anim param hash
            _animParamSpeedId = Animator.StringToHash("HorizontalSpeed");
        }

        public override void OnStateEnter()
        {
            // dont allow gravity (we controll all)
            _isPreStateApplyGravity = _applyGravity.enabled;
            _applyGravity.enabled = false;
        }

        public override void OnStateExit()
        {
            _applyGravity.enabled = _isPreStateApplyGravity;
        }

        public override void FixedUpdate()
        {
            // smooth damp to target speed
            Vector3 velocity = Vector3.SmoothDamp(
                _rigidbody.velocity,
                ((MoveByTargetVelocityActionSO)OriginSO).TargetVelocity,
                ref _currentSmoothVelocity,
                ((MoveByTargetVelocityActionSO)OriginSO).SmoothTime
            );

            // set rigibody velocity
            if(_rigidbody.velocity - velocity != Vector3.zero){
                _rigidbody.velocity = velocity;
                _animator.SetFloat(_animParamSpeedId, velocity.magnitude);
            }
        }

        public MoveByTargetVelocityAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}
    }
}
