using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Face Transform ActionSO")]
    public class FaceTransformActionSO : ActionSO
    {
        [Header("Setting")]
        public float RotateSmoothTime;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new FaceTransformAction(this, stateMachine);
        }
    }

    public class FaceTransformAction : Action
    {
        // refernce
        private Rigidbody _rigidbody;
        private Transform _vCamTraget;
        private Vector3 _lookDirection;

        public FaceTransformAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // get reference
            _rigidbody = ThisStateMachine.GetComponent<Rigidbody>();
            _vCamTraget = ThisStateMachine.VCamTarget;
        }

        public override void OnStateEnter()
        {
            _lookDirection = _vCamTraget.forward;
            _lookDirection.y = 0f;
        }

        float _rotationVelocity = 0f;
        public override void Update(){

            // // get look direction
            // Vector3 lookDirection = (((FaceTransformActionSO)OriginSO).LookAt.position - ThisStateMachine.transform.position).normalized;
            // lookDirection.y = 0f;

            // get target rotation
            float targetRotation = Quaternion.LookRotation(_lookDirection).eulerAngles.y;

            // get new rotation
            float newRotation = Mathf.SmoothDampAngle(
                ThisStateMachine.transform.eulerAngles.y,
                targetRotation,
                ref _rotationVelocity,
                ((FaceTransformActionSO)OriginSO).RotateSmoothTime
            );


            // change rotation
            _rigidbody.MoveRotation(Quaternion.Euler(0f, newRotation, 0f));
        }
        
    }

}
