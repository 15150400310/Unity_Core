using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class Monster_Idle : Monster_StateBase
{
    private Coroutine goRatrolCoroutine;
    public override void Enter()
    {
        //修改移动状态
        SetMoveState(false);
        //播放动画
        PlayAnimation("Idle");
        //延迟一个随机的时间去巡逻
        goRatrolCoroutine = this.StartCoroutine(GoRatrol(Random.Range(1f, 5f)));
    }
    private IEnumerator GoRatrol(float time)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState<Monster_Patrol>((int)MonsterStateType.Patrol);

    }

    public override void Exit()
    {
        if (goRatrolCoroutine!=null)
        {
            this.StopCoroutine(goRatrolCoroutine);
            goRatrolCoroutine = null;
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        //检测和玩家的距离是否追击
        CheckFollowAndChangeState();
    }
}
