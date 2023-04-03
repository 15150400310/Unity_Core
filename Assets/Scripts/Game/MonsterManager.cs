using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

/// <summary>
/// 怪物管理器
/// </summary>
public class MonsterManager : LogicManagerBase<MonsterManager>
{
    [SerializeField] private Transform createMonsterPoint;
    [SerializeField] private Transform[] targets;
    private int monsterCount = 0;
    private LV_Config config;
    
    private void Start()
    {
        config = ConfigManager.Instance.GetConfig<LV_Config>("LV");
        InvokeRepeating("CreateMonster", config.CreateMonsterInterval, 1);
    }

    /// <summary>
    /// 每X秒生成一只怪物
    /// </summary>
    private void CreateMonster()
    {
        //当前未达到怪物上线
        if (monsterCount<config.MaxMonsterCountOnScene)
        {
            float randomNum = Random.value;
            monsterCount += 1;
            for (int i = 0; i < config.CreateMonsterConfigs.Length; i++)
            {
                //当前随机数大于配置中的概率
                if (randomNum>config.CreateMonsterConfigs[i].Probability)
                {
                    Monster_Controller monster_Controller = ResManager.Load<Monster_Controller>("Monster", LVManager.Instance.TempObjRoot);
                    monster_Controller.transform.position = createMonsterPoint.position;
                    monster_Controller.Init(config.CreateMonsterConfigs[i].Monster_Config);
                    return;
                }
            }
            
        }
    }

    /// <summary>
    /// 获取巡逻点
    /// </summary>
    public Vector3 GetPatrolTarget()
    {
        return targets[Random.Range(0, targets.Length)].position;
    }

    protected override void CancelEventListener()
    {

    }

    protected override void RegisterEventListener()
    {

    }
}
