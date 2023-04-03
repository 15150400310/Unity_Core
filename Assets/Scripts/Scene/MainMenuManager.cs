using Frame;
using System;
using UnityEngine;

/// <summary>
/// 菜单场景的总管理器
/// 负责调度整个场景的逻辑 流程
/// </summary>
public class MainMenuManager : LogicManagerBase<MainMenuManager>
{
    private void Start()
    {
        UIManager.Instance.CloseAll();
        //播放背景音乐
        AudioManager.Instance.PlayBGAudio("Audio/BG/Menu");
        //显示菜单主窗口
        UIManager.Instance.Show<UI_MainMenuMainWindow>();
    }

    protected override void RegisterEventListener()
    {
        EventManager.AddEventListener<string>("CreateNewSaveAndEnterGame", CreateNewSaveAndEnterGame);
        EventManager.AddEventListener<SaveItem,UserData>("EnterGame", EnterGame);
    }

    protected override void CancelEventListener()
    {
        EventManager.RemoveEventListener<string>("CreateNewSaveAndEnterGame", CreateNewSaveAndEnterGame);
        EventManager.RemoveEventListener<SaveItem, UserData>("EnterGame", EnterGame);
    }

    /// <summary>
    /// 创建一个存档并进入游戏
    /// </summary>
    /// <param name="userName"></param>
    private void CreateNewSaveAndEnterGame(string userName)
    {
        //建立存档
        SaveItem saveItem = SaveManager.CreateSaveItem();
        
        //创建首次存档时的用户数据
        UserData userData = new UserData(userName);
        SaveManager.SaveObject(userData, saveItem);

        EventManager.EventTrigger("UpdateSaveItem");
        EventManager.EventTrigger("UpdateRankItem");

        //进入游戏
        EnterGame(saveItem, userData);
    }

    private void EnterGame(SaveItem saveItem,UserData userData)
    {
        //显示加载面板
        UIManager.Instance.Show<UI_LoadingWindow>();
        GameManager.Instance.EnterGame(saveItem, userData);
        SceneManager.LoadSceneAsync("Game");
        
    }
}
