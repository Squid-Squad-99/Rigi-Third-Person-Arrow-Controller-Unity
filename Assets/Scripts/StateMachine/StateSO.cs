using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RigiArcher.StateMachineElement{

    [CreateAssetMenu(menuName = "State Machine/State")]
    public class StateSO: ScriptableObject
    {
        public List<ActionSO> ActionSOs;
        public State GetState(StateMachine stateMachine){
            return new State(this, stateMachine);
        }
    }

    public class State{
        private StateMachine _stateMachine;
        private StateSO _originSO;
        private List<Action> _actions;
        public StateMachine ThisStateMachine => _stateMachine;


        public State(StateSO stateSO, StateMachine stateMachine){
            _originSO = stateSO;
            _stateMachine = stateMachine;
        }

        public void Awake(){
            // init variable
            _actions = new List<Action>();

            // instantiate action from action SO
            foreach(ActionSO actionSO in _originSO.ActionSOs){
                // get action
                Action action = actionSO.GetAction(ThisStateMachine);
                // add to list
                _actions.Add(action);
            }

            // awake for each action
            foreach(Action action in _actions){
                action.Awake();
            }
        }
        
        public void Start(){
            // start for each action
            foreach(Action action in _actions){
                action.Start();
            }
        }
        
        public void OnStateEnter(){
            // let action handle on state enter
            foreach(Action action in _actions){
                action.OnStateEnter();
            }
        }

        public void Update(){
            // let action handle update
            foreach(Action action in _actions){
                action.Update();
            }
        }

        public void FixedUpdate(){
            // let action handle FixedUpdate
            foreach(Action action in _actions){
                action.FixedUpdate();
            }
        }

        public void LateUpdate(){
            // let action handle LateUpdate
            foreach(Action action in _actions){
                action.LateUpdate();
            }
        }

        public void OnStateExit(){
            // let action handle On state Exit
            foreach(Action action in _actions){
                action.OnStateExit();
            }           
        }
        
    }

}
