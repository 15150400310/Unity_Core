using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlayOnShot("cannon_01",Camera.main.transform.position,1,true,CallBack,2);
        }
    }

    void CallBack()
    {
        Debug.Log("CallBack");
    }
}
