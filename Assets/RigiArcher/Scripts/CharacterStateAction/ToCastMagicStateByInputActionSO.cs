using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using RigiArcher.Magic;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/To Cast Magic State By Input ActionSO")]
    public class ToCastMagicStateByInputActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO CastMagicState;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new ToCastMagicStateByInputAction(this, stateMachine);
        }

    }

    public class ToCastMagicStateByInputAction : Action
    {
        // reference
        private UnityEvent _fireEvent;

        public ToCastMagicStateByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // reference
            _fireEvent = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>().InputFireEvent;
        }

        public override void OnStateEnter()
        {
            _fireEvent.AddListener(OnInputFire);
        }

        public override void OnStateExit()
        {
            _fireEvent.RemoveListener(OnInputFire);
        }

        private void OnInputFire()
        {
            // change to cast magic state
            ThisStateMachine.SwitchState(((ToCastMagicStateByInputActionSO)OriginSO).CastMagicState);
        }
    }
}
