using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EBAC.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {

        public Dictionary<T, StateBase> ditionaryStates;

        private StateBase _currentState;

        public StateBase CurrentState
        {
            get { return _currentState; }
        }

        public void Init()
        {
            ditionaryStates = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            ditionaryStates.Add(typeEnum, state);
        }

        public void SwitchState(T state, params object[] objs)
        {
            if (_currentState != null) _currentState.OnStateExit();

            _currentState = ditionaryStates[state];

            if (_currentState != null) _currentState.OnStateEnter(objs);
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();


        }
    }
}