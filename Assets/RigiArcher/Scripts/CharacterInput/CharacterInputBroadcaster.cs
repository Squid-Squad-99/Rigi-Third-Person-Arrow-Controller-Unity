using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RigiArcher.CharacterInput
{

    public class CharacterInputBroadcaster : MonoBehaviour, ICharacterInputBroadcaster
    {

        public UnityEvent InputJumpEvent => _inputJumpEvent;
        public UnityEvent<Vector2> InputLookEvent => _inputLookEvent;
        public UnityEvent<Vector2> InputMoveEvent => _inputMoveEvent;

        [SerializeField] UnityEvent _inputJumpEvent;
        [SerializeField] UnityEvent<Vector2> _inputLookEvent;
        [SerializeField] UnityEvent<Vector2> _inputMoveEvent;

        private Vector2 _moveInputValue;
        private Vector2 _lookInputValue;

        private void Awake() {
            // init variable
            _moveInputValue = Vector2.zero;
            _lookInputValue = Vector2.zero;
        }

        void OnMove(InputValue inputValue) 
        {
            // asset that input value have been normalize
            Debug.Assert(inputValue.Get<Vector2>().magnitude <= 1.1f);

            _moveInputValue = inputValue.Get<Vector2>();
        }

        void OnLook(InputValue inputValue)
        {
            _lookInputValue = inputValue.Get<Vector2>();
        }

        void OnJump(InputValue inputValue)
        {
            InputJumpEvent.Invoke();
        }

        private void Update() {
            InputMoveEvent.Invoke(_moveInputValue);
            InputLookEvent.Invoke(_lookInputValue);
        }
    }
}

