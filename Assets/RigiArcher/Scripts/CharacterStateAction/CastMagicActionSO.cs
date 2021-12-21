using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using RigiArcher.Magic;

namespace RigiArcher.CharacterAction
{

    [CreateAssetMenu(menuName = "State Machine/Actions/Cast Magic ActionSO")]
    public class CastMagicActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO BaseState;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new CastMagicAction(this, stateMachine); 
        }

    }

    internal class CastMagicAction : Action
    {
        // reference
        private MagicManager _magicManager;

        public CastMagicAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine) { }

        public override void Awake()
        {
            // reference
            _magicManager = ThisStateMachine.GetComponent<MagicManager>();
        }

        public override void OnStateEnter()
        {
            // to base state when spell done
            _magicManager.CurrentEquipedMagic.SpellFinish.AddListener(()=> {
                ThisStateMachine.SwitchState(((CastMagicActionSO)OriginSO).BaseState);
            });

            // cast magic
            _magicManager.CastMagic();

        }
    }
}
