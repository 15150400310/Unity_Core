using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : ManagerBase<UIManager>
{
    /// <summary>
    /// 元素资源库
    /// </summary>
    public Dictionary<Type, UIElement> UIElementDic { get { return GameRoot.Instance.GameSetting.UIElementDic; } }
}
