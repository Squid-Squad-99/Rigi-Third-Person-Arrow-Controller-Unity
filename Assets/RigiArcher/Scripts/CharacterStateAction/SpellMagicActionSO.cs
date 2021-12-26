using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using RigiArcher.Magic;

namespace RigiArcher.CharacterAction
{

    [CreateAssetMenu(menuName = "State Machine/Actions/Spell Magic ActionSO")]
    public class SpellMagicActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO MoveState;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new SpellMagicAction(this, stateMachine); 
        }

    }

    internal class SpellMagicAction : Action
    {
        // reference
        private MagicManager _magicManager;
        private ICharacterInputBroadcaster _characterInputBroadcaster;

        public SpellMagicAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine) { }

        public override void Awake()
        {
            // reference
            _magicManager = ThisStateMachine.GetComponent<MagicManager>();
            _characterInputBroadcaster = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>();
        }

        public override void OnStateEnter()
        {
            _characterInputBroadcaster.InputFireEvent.AddListener(OnInputFire);
        }

        public override void OnStateExit()
        {
            _characterInputBroadcaster.InputFireEvent.RemoveListener(OnInputFire);
        }

        private void OnInputFire()
        {
            Debug.Log("cancel magic");
            _magicManager.CancelMagic();
            ThisStateMachine.SwitchState(((SpellMagicActionSO)OriginSO).MoveState);
        }
    }
}
