using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class Monster_Die : Monster_StateBase
{
    private Coroutine dieCoroutine;
    public override void Enter()
    {
        //修改移动状态
        SetMoveState(false);
        //播放动画
        PlayAnimation("Die");
        //让玩家得分 通知怪物管理器
        EventManager.EventTrigger("MonsterDie");
        dieCoroutine = this.StartCoroutine(Die());
    }


    public override void Exit()
    {
        if (dieCoroutine!=null)
        {
            this.StopCoroutine(dieCoroutine);
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        monster.Die();
    }
}
