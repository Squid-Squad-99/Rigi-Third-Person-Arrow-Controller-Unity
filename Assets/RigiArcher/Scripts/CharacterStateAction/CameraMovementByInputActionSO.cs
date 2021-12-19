using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

using RigiArcher.StateMachineElement;
using RigiArcher.CharacterInput;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Camera Movement By Input ActionSO")]
    public class CameraMovementByInputActionSO : ActionSO
    {
        [Header("Setting")]
        public float MouseSensitivity;
        [Range(10, 80)]
        public int TopClamp;
        [Range(10, 80)]
        public int BottomClamp;

        [Header("If Networking")]
        public bool IsNetworking;
        public bool EnableOnServer;
        public bool EnableOnClient;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new CameraMovementByInputAction(this, stateMachine);
        }
    }

    public class CameraMovementByInputAction : Action
    {

        // data
        private Vector2 _inputLookValue;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // reference
        private Transform _vCamTarget;
        private UnityEvent<Vector2> _inputLookEvent;

        public CameraMovementByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // set reference
            _vCamTarget = ThisStateMachine.VCamTarget;
            _inputLookEvent = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>().InputLookEvent;
        }

        public override void OnStateEnter()
        {
            _inputLookEvent.AddListener(OnInputLook);

            // init data
            _inputLookValue = Vector2.zero;
            Vector3 InitRotation = _vCamTarget.rotation.eulerAngles;
            _cinemachineTargetPitch = InitRotation.x;
            _cinemachineTargetYaw = InitRotation.y;
            if(_cinemachineTargetPitch > 270) _cinemachineTargetPitch -= 360;
        }

        public override void OnStateExit()
        {
            _inputLookEvent.RemoveListener(OnInputLook);
        }

        public override void LateUpdate()
        {
            if(((CameraMovementByInputActionSO)OriginSO).IsNetworking && NetworkManager.Singleton != null){
                if(NetworkManager.Singleton.IsServer && ((CameraMovementByInputActionSO)OriginSO).EnableOnServer == false){
                    return;
                }
                else if(NetworkManager.Singleton.IsClient && ((CameraMovementByInputActionSO)OriginSO).EnableOnClient == false){
                    return;
                }
            }
            RotateCamera(_inputLookValue);
        }

        private void OnInputLook(Vector2 inputValue)
        {
            _inputLookValue = inputValue;
        }

        private void RotateCamera(Vector2 inputValue)
        {
            _cinemachineTargetYaw += inputValue.x * Time.deltaTime * ((CameraMovementByInputActionSO)OriginSO).MouseSensitivity;
            _cinemachineTargetPitch += inputValue.y * Time.deltaTime * ((CameraMovementByInputActionSO)OriginSO).MouseSensitivity;

            // clamp angle
            _cinemachineTargetPitch = ClampAngle(
                _cinemachineTargetPitch,
                -((CameraMovementByInputActionSO)OriginSO).TopClamp,
                ((CameraMovementByInputActionSO)OriginSO).BottomClamp
            );

            _vCamTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }

}
