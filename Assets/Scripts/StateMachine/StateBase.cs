using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {

    }

    public virtual void OnStateStay()
    {

    }

    public virtual void OnStateExit()
    {

    }
}

public class StateInitiante : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        
    }

}

public class StateMenu : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        GameManager.Instance.PouseGame();
    }
}

public class StatePlaying : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        GameManager.Instance.ButtonStart();
    }
}

public class StateEndGame : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        GameManager.Instance.EndGame();
    }

    public override void OnStateExit()
    {
        GameManager.Instance.RestartGame();
    }
}
