using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Face Move Direction ActionSO")]
    public class FaceMoveDirectionActionSO : ActionSO
    {
        [Header("Setting")]
        public float RotateSmoothTime;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new FaceMoveDirectionAction(this, stateMachine);
        }
    }

    public class FaceMoveDirectionAction : Action
    {
        // reference
        private Rigidbody _rigidbody;

        public FaceMoveDirectionAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // set up reference
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
        }

        float _rotationVelocity = 0f;
        public override void FixedUpdate(){

            // get horizontal velocity
            Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            // if no horizontal velocity, stay face direction
            if(horizontalVelocity.magnitude <= 1f) return;

            // get target rotation
            float targetRotation = Quaternion.LookRotation(horizontalVelocity).eulerAngles.y;

            // get new rotation
            float newRotation = Mathf.SmoothDampAngle(
                ThisStateMachine.transform.eulerAngles.y,
                targetRotation,
                ref _rotationVelocity,
                ((FaceMoveDirectionActionSO)OriginSO).RotateSmoothTime
            );


            // change rotation
            _rigidbody.MoveRotation(Quaternion.Euler(0f, newRotation, 0f));
        }
        
    }

}
