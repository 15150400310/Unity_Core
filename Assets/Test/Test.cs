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
            EventManager.AddEventListener("Test", Func);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            EventManager.EventTrigger("Test");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            EventManager.RemoveEventListener("Test", Func);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.Clear();
        }
    }

    void Func()
    {
        Debug.Log("rrr");
    }
}
