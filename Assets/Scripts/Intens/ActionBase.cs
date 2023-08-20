using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionBase : MonoBehaviour
{
    public SOInt soInt;
    public ItenType type;

    private void Start()
    {
        soInt = InvetoryManager.instance.GetItensForType(type).soInt;
    }

    protected virtual void RecoverLife()
    {
        if (soInt.value > 0)
        {
            InvetoryManager.instance.RemoveItensForType(type);
        }
    }
}
