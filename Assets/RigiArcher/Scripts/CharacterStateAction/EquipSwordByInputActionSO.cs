using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using UnityEngine.Events;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Equip Sword By Input ActionSO")]
    public class EquipSwordByInputActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO NextState;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new EquipSwordByInputAction(this, stateMachine);
        }
    }

    internal class EquipSwordByInputAction : Action
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
            // equip sword and switch to next state
            _swordManager.EquipSword();
            ThisStateMachine.SwitchState(((EquipSwordByInputActionSO)OriginSO).NextState);
        }

        public EquipSwordByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine)
        {
        }
    }
}

