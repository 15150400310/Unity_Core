using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    SphereController sphere;
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&& sphere==null)
        {
            ResManager.Instance.LoadGameObjectAsync<SphereController>("Sphere", Call);
        }
        if (Input.GetKeyDown(KeyCode.B) && sphere != null)
        {
            PoolManager.Instance.PushGameObject(sphere.gameObject);
            sphere = null;
        }
    }

    void Call(SphereController sphereController)
    {
        sphere = sphereController;
    }
}
