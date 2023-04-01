using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class LVManager : LogicManagerBase<LVManager>
{
    private void Start()
    {
        Debug.Log("游戏开始");
    }
    protected override void CancelEventListener()
    {
    }

    protected override void RegisterEventListener()
    {
    }
}
