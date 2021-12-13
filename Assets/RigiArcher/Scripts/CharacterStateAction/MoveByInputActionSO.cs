using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Move By Input ActionSO")]
    public class MoveByInputActionSO : ActionSO
    {
        [Header("Setting")]
        [Tooltip("Max Horitontal speed of moving")]
        public float MaxMoveSpeed;
        public float SpeedChangeRate;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new MoveByInputAction(this, stateMachine);
        }
    }

    public class MoveByInputAction : Action
    {
        // data
        private Vector2 _cacheInputMoveValue;
        private int _animIdSpeed;

        // reference
        private Transform _vCamTarget; 
        private UnityEvent<Vector2> _inputMoveEvent; 
        private Rigidbody _rigidbody;
        private Animator _animator;

        public MoveByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // set up reference
            _vCamTarget = ThisStateMachine.VCamTarget;
            _inputMoveEvent = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>().InputMoveEvent;
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
            _animator = ThisStateMachine.CharacterAnimator;

            // init data
            _cacheInputMoveValue = Vector2.zero;

            // set anim param id
            _animIdSpeed = Animator.StringToHash("HorizontalSpeed");
        }

        public override void OnStateEnter()
        {
            // hook event
            _inputMoveEvent.AddListener(OnInputMove);
        }

        public override void OnStateExit()
        {
            // unhook event
            _inputMoveEvent.RemoveListener(OnInputMove);
        }

        public override void FixedUpdate()
        {
            // get target horizontal speed
            float targetHorizontalSpeed = (_cacheInputMoveValue != Vector2.zero) ? ((MoveByInputActionSO)OriginSO).MaxMoveSpeed : 0f;

            // get target horizontal velocity            
            float targetRotation = Mathf.Atan2(_cacheInputMoveValue.x, _cacheInputMoveValue.y) * Mathf.Rad2Deg + _vCamTarget.transform.eulerAngles.y;
			Vector3 targetHorizontalDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            Vector3 targetHorizontalVelocity = targetHorizontalDirection * targetHorizontalSpeed;

            // get current horizontal velocity
            Vector3 currentHorizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            // calculate new horizontal velocity
            Vector3 newHorizontalVelocity = Vector3.Lerp(
                currentHorizontalVelocity,
                targetHorizontalVelocity,
                ((MoveByInputActionSO)OriginSO).SpeedChangeRate * Time.fixedDeltaTime
            );

            // get new velocity
            Vector3 newVelocity = new Vector3(newHorizontalVelocity.x, _rigidbody.velocity.y, newHorizontalVelocity.z);

            // change velocity & animation param
            _rigidbody.velocity = newVelocity;
            _animator.SetFloat(_animIdSpeed, newHorizontalVelocity.magnitude);
            
        }

        private void OnInputMove(Vector2 inputValue)
        {
            _cacheInputMoveValue = inputValue;
        }

    }

}
