using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestMono
{
    Coroutine c;
    public TestMono()
    {
        this.AddUpdateListener(OnUpdate);
        c = this.StartCoroutine(DoAction());
    }

    void OnUpdate()
    {
        Debug.Log("OnUpdate");
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.RemoveUpdateListener(OnUpdate);
            this.StopCoroutine(c);
        }
    }

    IEnumerator DoAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("DoAction");
        }
    }
}

public class Test : MonoBehaviour
{
    public TestMono t;
    void Start()
    {
        //t = new TestMono();
        ResManager.LoadGameObjectAsync<SphereCollider>("Sphere", a=> { Debug.Log(a.transform.name); }, null);
    }

    
}
