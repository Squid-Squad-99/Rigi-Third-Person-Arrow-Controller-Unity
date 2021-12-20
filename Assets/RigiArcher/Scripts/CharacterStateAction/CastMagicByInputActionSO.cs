using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using RigiArcher.Magic;

namespace RigiArcher.CharacterAction{

    [CreateAssetMenu(menuName = "State Machine/Actions/Cast Magic By Input ActionSO")]
    public class CastMagicByInputActionSO : ActionSO
    {
        public override Action GetAction(StateMachine stateMachine)
        {
            return new CastMagicByInputAction(this, stateMachine);
        }

    }

    public class CastMagicByInputAction : Action
    {
        // reference
        private UnityEvent _fireEvent;
        private MagicManager _magicManager;

        public CastMagicByInputAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine){}

        public override void Awake()
        {
            // reference
            _fireEvent = ThisStateMachine.GetComponent<ICharacterInputBroadcaster>().InputFireEvent;
            _magicManager = ThisStateMachine.GetComponent<MagicManager>();
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
            _magicManager.CastMagic();
        }
    }
}
