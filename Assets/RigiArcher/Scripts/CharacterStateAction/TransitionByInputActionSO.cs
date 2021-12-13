using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RigiArcher.StateMachineElement;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Transition By Input ActionSO")]
    public class TransitionByInputActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO ToStateSO;
        public CharacterInputEventEnum ListenInputEvent;

        public enum CharacterInputEventEnum{
            InputJumpEvent,
        }


        public override Action GetAction(StateMachine stateMachine)
        {
            return new TransitionByInputAction(this, stateMachine);
        }
    }

    public class TransitionByInputAction : Action
    {
        private UnityEvent _listeningInputEvent;

        public TransitionByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine) {}

        public override void Awake()
        {
            ICharacterInputBroadcaster characterInputBroadcaster = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>();
            switch (((TransitionByInputActionSO)OriginSO).ListenInputEvent)
            {
                case TransitionByInputActionSO.CharacterInputEventEnum.InputJumpEvent:
                _listeningInputEvent = characterInputBroadcaster.InputJumpEvent;
                    break;
                default:
                    break;
            }
            
        }

        public override void OnStateEnter()
        {
            _listeningInputEvent.AddListener(SwitchState);
        }

        public override void OnStateExit()
        {
            _listeningInputEvent.RemoveListener(SwitchState);
        }

        private void SwitchState(){
            ThisStateMachine.SwitchState(((TransitionByInputActionSO)OriginSO).ToStateSO);
        }


    }

}