using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using UnityEngine.Events;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Use Sword By Input ActionSO")]
    public class UseSwordByInputSO : ActionSO
    {

        public override Action GetAction(StateMachine stateMachine)
        {
            return new UseSwordByInput(this, stateMachine);
        }
    }

    internal class UseSwordByInput : Action
    {
        // reference
        private UnityEvent _attackEvent; 
        private SwordManager _swordManager;

        public override void Awake() {
            // reference
            _attackEvent = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>().InputAttackEvent;
            _swordManager = ThisStateMachine.GetComponent<SwordManager>();
        }

        public override void OnStateEnter()
        {
            _attackEvent.AddListener(OnInputAttack);
        }

        public override void OnStateExit()
        {
            _attackEvent.RemoveListener(OnInputAttack);
        }

        private void OnInputAttack()
        {
            _swordManager.UseSword();
        }

        public UseSwordByInput(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine)
        {
        }
    }
}

