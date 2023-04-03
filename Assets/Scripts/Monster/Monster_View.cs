using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class Monster_View : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponCollider;
    public Animator Animator 
    {
        get => animator;
        private set => animator = value;
    }

    private bool canHit;
    private int attackValue;

    public void Init(int attackValue)
    {
        this.attackValue = attackValue;
        weaponCollider.OnTriggerEnter(OnWeaponColliderTriggerEnter);
    }

    private void OnWeaponColliderTriggerEnter(Collider other,params object[] args)
    {
        if (!canHit)
        {
            return;
        }

        if (other.gameObject.tag == "Player")
        {
            canHit = false;
            //让玩家受伤
            AudioManager.Instance.PlayOnShot("Audio/Monster/拳头击中", transform);

        }
    }

    #region 动画事件
    private void Footstep()
    {
        AudioManager.Instance.PlayOnShot("Audio/Monster/走路", transform, 0.1f);
    }

    private void StartHit()
    {
        canHit = true;
    }

    private void StopHit()
    {
        canHit = false;
    }

    private void EndAttack()
    {
        EventManager.EventTrigger("EndAttack_" + transform.parent.GetInstanceID());
    }
    #endregion
}
