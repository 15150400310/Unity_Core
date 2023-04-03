using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class LVManager : LogicManagerBase<LVManager>
{

    public Transform TempObjRoot;
    private void Start()
    {
        UIManager.Instance.CloseAll();
        //打开游戏主窗口
        UIManager.Instance.Show<UI_GameMainWindow>();
        //初始化玩家
        Player_Controller.Instance.Init(ConfigManager.Instance.GetConfig<Player_Config>("Player"));
    }

    private bool isActiveSettingWindow = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    protected override void CancelEventListener()
    {
    }

    protected override void RegisterEventListener()
    {
    }
}
