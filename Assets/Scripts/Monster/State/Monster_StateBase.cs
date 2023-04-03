using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Frame;

/// <summary>
/// 怪物状态基类
/// </summary>
public abstract class Monster_StateBase : StateBase
{
    protected Monster_Controller monster;
    protected Animator animator;
    protected NavMeshAgent navMeshAgent;
    protected Player_Controller player { get => Player_Controller.Instance; }

    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        monster = owner as Monster_Controller;
        animator = monster.animator;
        navMeshAgent = monster.NavMeshAgent;
    }


    public override void UnInit()
    {
        monster = null;
        animator = null;
        navMeshAgent = null;
        base.UnInit();
    }
    /// <summary>
    /// 播放动画
    /// </summary>
    protected void PlayAnimation(string animationName)
    {
        animator.CrossFadeInFixedTime(animationName, 0.2f);
    }

    /// <summary>
    /// 设置移动状态
    /// </summary>
    /// <param name="canMove"></param>
    protected void SetMoveState(bool canMove)
    {
        navMeshAgent.enabled = canMove;
    }

    protected bool CheckFollowAndChangeState()
    {
        if (Vector3.Distance(monster.transform.position,player.transform.position)<5f)
        {
            stateMachine.ChangeState<Monster_Follow>((int)MonsterStateType.Follow);
            return true;
        }
        return false;
    }
}
