using Frame;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/UI_MainMenuMainWindow", 0)]
public class UI_MainMenuMainWindow : UIPanelBase
{
    private const string LocalSetPackName = "UI_MainMenuMainWindow";

    [SerializeField] private Text title;
    [SerializeField] private Text newGame_Button_Text;
    [SerializeField] private Text continue_Button_Text;
    [SerializeField] private Text rank_Button_Text;
    [SerializeField] private Text setting_Button_Text;
    [SerializeField] private Text quit_Button_Text;

    [SerializeField] private Button newGame_Button;
    [SerializeField] private Button continue_Button;
    [SerializeField] private Button rank_Button;
    [SerializeField] private Button setting_Button;
    [SerializeField] private Button quit_Button;

    public override void Init()
    {
        title = GetUI<Text>("Title");
        newGame_Button_Text = GetUI<Text>("NewGame_Button/Text");
        continue_Button_Text = GetUI<Text>("Continue_Button/Text");
        rank_Button_Text = GetUI<Text>("Rank_Button/Text");
        setting_Button_Text = GetUI<Text>("Setting_Button/Text");
        quit_Button_Text = GetUI<Text>("Quit_Button/Text");

        newGame_Button = GetUI<Button>("NewGame_Button");
        continue_Button = GetUI<Button>("Continue_Button");
        rank_Button = GetUI<Button>("Rank_Button");
        setting_Button = GetUI<Button>("Setting_Button");
        quit_Button = GetUI<Button>("Quit_Button");

        newGame_Button.onClick.AddListener(NewGame_ButtonClick);
        continue_Button.onClick.AddListener(Continue_ButtonClick);
        rank_Button.onClick.AddListener(Rank_ButtonClick);
        setting_Button.onClick.AddListener(Setting_ButtonClick);
        quit_Button.onClick.AddListener(Quit_ButtonClick);
    }

    protected override void OnUpdateLanguage()
    {
        title.FrameLocalSet(LocalSetPackName, "Title");
        newGame_Button_Text.FrameLocalSet(LocalSetPackName, "NewGame_Button_Text");
        continue_Button_Text.FrameLocalSet(LocalSetPackName, "Continue_Button_Text");
        rank_Button_Text.FrameLocalSet(LocalSetPackName, "Rank_Button_Text");
        setting_Button_Text.FrameLocalSet(LocalSetPackName, "Setting_Button_Text");
        quit_Button_Text.FrameLocalSet(LocalSetPackName, "Quit_Button_Text");
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
        Debug.Log("123123");
    }

    private void Setting_ButtonClick()
    {
        PlayButtonAudio();
        UIManager.Instance.Show<UI_SettingWindow>();
    }

    private void Quit_ButtonClick()
    {
        PlayButtonAudio();
        Application.Quit();
    }
}
