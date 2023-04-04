using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame;

public class GameManager : SingletonMono<GameManager>
{
    public SaveItem SaveItem { get; private set; }
    public UserData UserData { get; private set; }
    public UserSetting UserSetting { get; private set; }

    private void Start()
    {
        UserSetting = SaveManager.LoadSetting<UserSetting>();
        //如果没有获取到用户设置
        if (UserSetting == null)
        {
            UserSetting = new UserSetting(1, 1, 1, LanguageType.Chinese);
            SaveManager.SaveSetting(UserSetting);
        }
        //根据用户设置,设置相关的内容
        AudioManager.Instance.GlobalVolume = UserSetting.GlobalVolume;
        AudioManager.Instance.BGVolume = UserSetting.BGVolume;
        AudioManager.Instance.EffectVolume = UserSetting.EffectVolume;
        LocalizationManager.Instance.CurrentLanguageType = UserSetting.LanguageType;
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame(SaveItem saveItem, UserData userData)
    {
        //全局保存当前的存档和用户数据
        this.SaveItem = saveItem;
        this.UserData = userData;
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// 再来一局
    /// </summary>
    public void RepeatGame()
    {
        UIManager.Instance.Show<UI_LoadingWindow>();
        SceneManager.LoadSceneAsync("Game");
    }

    /// <summary>
    /// 更新分数
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(int score)
    {
        //本次得分必须超过当前最大分，才有保存意义
        if (score>UserData.score)
        {
            //保存当前得分
            UserData.score = score;
            SaveManager.SaveObject(UserData, SaveItem);
            //排名有变化
            EventManager.EventTrigger("UpdateSaveItem");
            EventManager.EventTrigger("UpdateRankItem");
        }
    }
}
