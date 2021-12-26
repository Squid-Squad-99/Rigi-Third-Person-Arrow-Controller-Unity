using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RigiArcher;
using RigiArcher.CharacterInput;
using RigiArcher.StateMachineElement;
using RigiArcher.Magic;

namespace RigiArcher.CharacterAction
{

    [CreateAssetMenu(menuName = "State Machine/Actions/Transition by Spell Finish ActionSO")]
    public class TransitionBySpellFinishActionSO : ActionSO
    {
        [Header("Setting")]
        public StateSO NextState;

        public override Action GetAction(StateMachine stateMachine)
        {
            return new TransitionBySpellFinishAction(this, stateMachine); 
        }

    }

    internal class TransitionBySpellFinishAction : Action
    {
        // reference
        private MagicManager _magicManager;

        public TransitionBySpellFinishAction(ActionSO actionSO, StateMachine stateMachine) : base(actionSO, stateMachine) { }

        public override void Awake()
        {
            // reference
            _magicManager = ThisStateMachine.GetComponent<MagicManager>();
        }

        public override void OnStateEnter()
        {
            Debug.Log("Spell State");
            // to next state when spell done
            _magicManager.CurrentEquipedMagic.SpellFinish.AddListener(()=> {
                ThisStateMachine.SwitchState(((TransitionBySpellFinishActionSO)OriginSO).NextState);
            });
        }
    }
}

