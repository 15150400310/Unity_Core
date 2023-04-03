using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Follow : Monster_StateBase
{
    public override void Enter()
    {
        //修改移动状态
        SetMoveState(true);
        //播放动画
        PlayAnimation("Run");
    }
    public override void LateUpdate()
    {
        //如果距离玩家非常近,则攻击玩家
        if (Vector3.Distance(monster.transform.position, player.transform.position) < 1.5f)
        {
            stateMachine.ChangeState<Monster_Attack>((int)MonsterStateType.Attack);
            return;
        }
        //否则一直追击玩家
        navMeshAgent.SetDestination(player.transform.position);
    }
}
