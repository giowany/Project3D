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
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i, string s)
    {
        if(i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
            PlayerPrefs.SetInt(s, i);
        }
    }

    public Vector3 GetPositionLastCheckPoint()
    {
        var position = checkPoints.Find(i => i.checkPointKey == lastCheckPointKey);
        return position.transform.position;
    }
}
