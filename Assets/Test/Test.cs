using System;
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
            UIManager.Instance.Show<Test__Window>();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.Close<Test__Window>();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.Show<Test__Window2>();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.Instance.Close<Test__Window2>();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.CloseAll();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UIManager.Instance.Show<Test__Window>(4);
        }
    }
}
