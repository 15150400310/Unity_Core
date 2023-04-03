using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

[Pool]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rgb;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private new Collider collider;
    private int attack;

    public void Init(Vector3 dir,float movePower,int attack)
    {
        rgb.AddForce(dir.normalized * movePower);
        trailRenderer.emitting = true;
        collider.enabled = true;
        this.attack = attack;
        Invoke("DestroyOnInit", 20);
    }

    private void OnTriggerEnter(Collider other)
    {
        CancelInvoke("DestroyOnInit");
        StartCoroutine(DoDestroy());
        
        //TODO：攻击AI
        if (other.gameObject.tag == "Monster")
        {

        }

        
    }

    private void DestroyOnInit()
    {
        StartCoroutine(DoDestroy());
    }

    IEnumerator DoDestroy()
    {
        collider.enabled = false;
        rgb.velocity = Vector3.zero;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(2);
        //销毁自身
        Destroy();
    }
    private void Destroy()
    {
        this.GameObjectPushPool();
    }

}
