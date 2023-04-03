﻿using Frame;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/MainMenuMainWindow",0)]
public class UI_MainMenuMainWindow : UI_WindowBase
{
    [SerializeField] private Text Title;
    [SerializeField] private Button NewGame_Button;
    [SerializeField] private Button Continue_Button;
    [SerializeField] private Button Rank_Button;
    [SerializeField] private Button Setting_Button;
    [SerializeField] private Button Quit_Button;

    [SerializeField] private Text NewGame_Button_Text;
    [SerializeField] private Text Continue_Button_Text;
    [SerializeField] private Text Rank_Button_Text;
    [SerializeField] private Text Setting_Button_Text;
    [SerializeField] private Text Quit_Button_Text;

    private const string LocalSetPackName = "UI_MainMenuMainWindow";

    public override void Init()
    {
        NewGame_Button.onClick.AddListener(NewGame_ButtonClick);
        Continue_Button.onClick.AddListener(Continue_ButtonClick);
        Rank_Button.onClick.AddListener(Rank_ButtonClick);
        Setting_Button.onClick.AddListener(Setting_ButtonClick);
        Quit_Button.onClick.AddListener(Quit_ButtonClick);
    }

    protected override void OnUpdateLanguage()
    {
        Title.FrameLocalSet(LocalSetPackName, "Title");
        NewGame_Button_Text.FrameLocalSet(LocalSetPackName, "NewGame_Button");
        Continue_Button_Text.FrameLocalSet(LocalSetPackName, "Continue_Button");
        Rank_Button_Text.FrameLocalSet(LocalSetPackName, "Rank_Button");
        Setting_Button_Text.FrameLocalSet(LocalSetPackName, "Setting_Button");
        Quit_Button_Text.FrameLocalSet(LocalSetPackName, "Quit_Button");
    }

    /// <summary>
    /// 播放按钮音效
    /// </summary>
    private void PlayButtonAudio()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
    }

    private void NewGame_ButtonClick()
    {
        PlayButtonAudio();
        UIManager.Instance.Show<UI_NewGameWindow>();
    }

    private void Continue_ButtonClick()
    {
        PlayButtonAudio();
        UIManager.Instance.Show<UI_SaveWindow>();
    }

    private void Rank_ButtonClick()
    {
        PlayButtonAudio();
        UIManager.Instance.Show<UI_RankWindow>();
    }

    private void Setting_ButtonClick()
    {
        PlayButtonAudio();
        UIManager.Instance.Show<UI_SettingWindow>();
    }

    private void Quit_ButtonClick()
    {
        PlayButtonAudio();
    }
}