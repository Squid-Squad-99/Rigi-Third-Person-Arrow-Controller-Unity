using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.StateMachineElement{

    public abstract class ActionSO : ScriptableObject
    {
        public abstract Action GetAction(StateMachine stateMachine);
    }

    public class Action{
        private StateMachine _stateMachine;
        private ActionSO _originSO;
        protected StateMachine ThisStateMachine => _stateMachine;
        protected ActionSO OriginSO => _originSO; 

        public Action(ActionSO actionSO, StateMachine stateMachine){
            _originSO = actionSO;
            _stateMachine = stateMachine;            
        }

        public virtual void Awake() {
        }

        public virtual void Start(){
            
        }
        
        public virtual void OnStateEnter(){

        }

        public virtual void Update(){

        }

        public virtual void FixedUpdate(){

        }

        public virtual void LateUpdate() {
            
        }

        public virtual void OnStateExit(){
            
        }
    }

}