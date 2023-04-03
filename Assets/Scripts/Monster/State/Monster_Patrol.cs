using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Patrol : Monster_StateBase
{
    //巡逻的目标点
    private Vector3 target;
    public override void Enter()
    {
        //修改移动状态
        SetMoveState(true);
        //播放动画
        PlayAnimation("Run");
        //获取巡逻点
        target = monster.GetPatrolTarget();
        navMeshAgent.SetDestination(target);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        //检测和玩家的距离是否追击
        if (CheckFollowAndChangeState())
        {
            //如果切换去追击,就无视后面的逻辑
            return;
        }
        //到达巡逻点后，切换到待机状态
        if (Vector3.Distance(monster.transform.position,target)<1)
        {
            stateMachine.ChangeState<Monster_Idle>((int)MonsterStateType.Idle);
        }
        
    }
}
