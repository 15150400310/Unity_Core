using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Frame;

/// <summary>
/// 怪物的状态类型
/// </summary>
public enum MonsterStateType
{
    Idle,
    Patrol,
    Follow,
    Attack,
    GetHit,
    Die
}

[Pool]
public class Monster_Controller : MonoBehaviour,IStateMachineOwner
{
    #region 自身组件
    [SerializeField] private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent { get => navMeshAgent; private set => navMeshAgent = value; }
    private StateMachine stateMachine;
    #endregion
    #region View组件
    private Monster_View view;
    public Animator animator { get; private set; }
    
    #endregion
    #region 数值
    private int hp;
    #endregion

    public void Init(Monster_Config config)
    {
        view = PoolManager.Instance.GetGameObject<Monster_View>(config.ModelPrefab,transform);
        view.transform.localPosition = Vector3.zero;
        view.Init(config.Attack);
        animator = view.Animator;
        hp = config.HP;

        //初始化状态机
        stateMachine = ResManager.Load<StateMachine>();
        stateMachine.Init(this);
        stateMachine.ChangeState<Monster_Idle>((int)MonsterStateType.Idle);
    }

    /// <summary>
    /// 获取巡逻点
    /// </summary>
    public Vector3 GetPatrolTarget()
    {
        return MonsterManager.Instance.GetPatrolTarget();
    }
}
