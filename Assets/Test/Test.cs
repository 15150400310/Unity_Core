using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    void Start()
    {
        this.OnClick(Click);
        this.RemoveAllListener(FrameEventType.OnClick);
        PoolManager.Instance.OnClick(Click);
    }

    void Click(PointerEventData data,params object[] obj)
    {

    }
}
