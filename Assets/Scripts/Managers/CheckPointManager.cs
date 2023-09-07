using EBAC.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkPoints;

    public bool HasCheckPoint()
    {
        return SaveManager.instance.SaveSetup.lastCheckPoint > 0;
    }

    public void SaveCheckPoint(int i, string s)
    {
        if(i > SaveManager.instance.SaveSetup.lastCheckPoint)
        {
            SaveManager.instance.SaveLastCheckPoint(i);
        }
    }

    public Vector3 GetPositionLastCheckPoint()
    {
        var position = checkPoints.Find(i => i.checkPointKey == SaveManager.instance.SaveSetup.lastCheckPoint);
        return position.transform.position;
    }
}
