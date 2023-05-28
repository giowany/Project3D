using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    public enum States
    {
        INITIATE,
        MENU,
        PLAYING,
        ENDGAME
    }

    public Dictionary<States, StateBase> ditionaryStates;

    private StateBase _currentState;

    private void Awake()
    {
        Instance = this;

        ditionaryStates = new Dictionary<States, StateBase>();
        ditionaryStates.Add(States.INITIATE, new StateInitiante());
        ditionaryStates.Add(States.MENU, new StateMenu());
        ditionaryStates.Add(States.PLAYING, new StatePlaying());
        ditionaryStates.Add(States.ENDGAME, new StateEndGame());

        SwitchState(States.INITIATE);
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();


    }

    private void SwitchState(States state)
    {
        if(_currentState != null) _currentState.OnStateExit();

        _currentState = ditionaryStates[state];

        if (_currentState != null) _currentState.OnStateEnter();
    }

    public void StartGame()
    {
        SwitchState(States.PLAYING);
    }

    public void EnterMenu()
    {
        SwitchState(States.MENU);
    }

    public void EndGame()
    {
        SwitchState(States.ENDGAME);
    }
}
