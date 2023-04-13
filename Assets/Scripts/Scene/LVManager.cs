using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class LVManager : LogicManagerBase<LVManager>
{
    private int score = 0;
    public int Score 
    { 
        get => score;
        set 
        {
            score = value;
            EventManager.EventTrigger<int>("UpdateScore", score);
        }
    }

    public Transform TempObjRoot;

    private bool isActiveSettingWindow = false;
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.CloseAll();
        //打开游戏主窗口
        UIManager.Instance.Show<UI_GameMainWindow>();
        Score = 0;
        //初始化玩家
        Player_Controller.Instance.Init(ConfigManager.Instance.GetConfig<Player_Config>("Player"));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowSettingWindow();
        }
    }

    protected override void RegisterEventListener()
    {
        EventManager.AddEventListener("MonsterDie", OnMonsterDie);
        EventManager.AddEventListener("GameOver", OnGameOver);
        EventManager.AddEventListener("Nirvana", Nirvana);
    }

    protected override void CancelEventListener()
    {
        EventManager.RemoveEventListener("MonsterDie", OnMonsterDie);
        EventManager.RemoveEventListener("GameOver", OnGameOver);
        EventManager.RemoveEventListener("Nirvana", Nirvana);
    }

    private void OnMonsterDie()
    {
        Score += 1;
    }

    private void OnGameOver()
    {
        //更新存档
        GameManager.Instance.UpdateScore(score);
        //打开结果页面
        UIManager.Instance.Show<UI_ResultWindow>().Init(score);
    }

    public void ShowSettingWindow()
    {
        isActiveSettingWindow = !isActiveSettingWindow;
        //暂停游戏，打开设置窗口
        if (isActiveSettingWindow)
        {
            GameManager.Instance.PauseGame();
            UIManager.Instance.Show<UI_SettingWindow>().InitOnGame();
        }
        //继续游戏，关闭设置窗口
        else
        {
            UIManager.Instance.Close<UI_SettingWindow>();
            GameManager.Instance.ContinueGame();

        }
    }

    private void Nirvana()
    {
        //满血复活
        UIManager.Instance.Close<UI_ResultWindow>();
        Player_Controller.Instance.Revive();
        GameManager.Instance.ContinueGame();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
