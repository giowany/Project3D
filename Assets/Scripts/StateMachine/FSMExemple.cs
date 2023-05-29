using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMExemple : MonoBehaviour
{
    public enum ExempleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExempleEnum> machine;

    private void Start()
    {
        machine = new StateMachine<ExempleEnum>();
        machine.Init();
        machine.RegisterStates(ExempleEnum.STATE_ONE, new StateBase());
        machine.RegisterStates(ExempleEnum.STATE_TWO, new StateBase());
        machine.RegisterStates(ExempleEnum.STATE_THREE, new StateBase());
    }
}
