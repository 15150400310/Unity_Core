using Frame;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/UI_SettingWindow", 1)]
public class UI_SettingWindow : UIPanelBase
{
    private const string LocalSetPackName = "UI_SettingWindow";

    [SerializeField] private Text globalVolume_Text;
    [SerializeField] private Text bgVolume_Text;
    [SerializeField] private Text effectVolume_Text;
    [SerializeField] private Text languageType_Text;
    [SerializeField] private Text quit_Button_Text;

    [SerializeField] private Button close_Button;
    [SerializeField] private Button setting_Button;
    [SerializeField] private Button quit_Button;
    
    [SerializeField] private Slider globalVolume_Slider;
    [SerializeField] private Slider bgVolume_Slider;
    [SerializeField] private Slider effectVolume_Slider;

    [SerializeField] private Dropdown languageType_Dropdown;

    public override void Init()
    {
        globalVolume_Text = GetUI<Text>("GlobalVolume");
        bgVolume_Text = GetUI<Text>("BGVolume");
        effectVolume_Text = GetUI<Text>("EffectVolume");
        languageType_Text = GetUI<Text>("LanguageType");
        quit_Button_Text = GetUI<Text>("Quit_Button/Text");

        close_Button = GetUI<Button>("Close_Button");
        setting_Button = GetUI<Button>("Setting_Button");
        quit_Button = GetUI<Button>("Quit_Button");

        globalVolume_Slider = GetUI<Slider>("GlobalVolume/Slider");
        bgVolume_Slider = GetUI<Slider>("BGVolume/Slider");
        effectVolume_Slider = GetUI<Slider>("EffectVolume/Slider");

        languageType_Dropdown = GetUI<Dropdown>("LanguageType/Dropdown");

        close_Button.onClick.AddListener(Close);
        setting_Button.onClick.AddListener(Setting_ButtonClick);
        quit_Button.onClick.AddListener(Quit_ButtonClick);
        
        globalVolume_Slider.onValueChanged.AddListener(GlobalVolume_Slider_ValueChanged);
        bgVolume_Slider.onValueChanged.AddListener(BGVolume_Slider_ValueChanged);
        effectVolume_Slider.onValueChanged.AddListener(EffectVolume_Slider_ValueChanged);
        languageType_Dropdown.onValueChanged.AddListener(LanguageType_Dropdown_ValueChanged);

        quit_Button.Hide();
        setting_Button.Hide();
    }

    /// <summary>
    /// 在游戏过程中的窗口初始化
    /// </summary>
    public void InitOnGame()
    {
        close_Button.Hide();
        quit_Button.Show();
    }

    public override void OnShow()
    {
        base.OnShow();
        globalVolume_Slider.value = GameManager.Instance.UserSetting.GlobalVolume;
        bgVolume_Slider.value = GameManager.Instance.UserSetting.BGVolume;
        effectVolume_Slider.value = GameManager.Instance.UserSetting.EffectVolume;
        languageType_Dropdown.value = (int)GameManager.Instance.UserSetting.LanguageType;
#if UNITY_ANDROID
        setting_Button.Show();
#endif
    }

    public override void OnClose()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        close_Button.Show();
        quit_Button.Hide();
        base.OnClose();
    }

    private void Setting_ButtonClick()
    {
        LVManager.Instance.ShowSettingWindow();
    }

    private void Quit_ButtonClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        Close();
        SceneManager.LoadScene("MainMenu");
    }

    protected override void OnUpdateLanguage()
    {
        globalVolume_Text.FrameLocalSet(LocalSetPackName, "GlobalVolume_Text");
        bgVolume_Text.FrameLocalSet(LocalSetPackName, "BGVolume_Text");
        effectVolume_Text.FrameLocalSet(LocalSetPackName, "EffectVolume_Text");
        languageType_Text.FrameLocalSet(LocalSetPackName, "LanguageType_Text");
        quit_Button_Text.FrameLocalSet(LocalSetPackName, "Quit_Button_Text");
    }

    private void GlobalVolume_Slider_ValueChanged(float value)
    {
        GameManager.Instance.UserSetting.GlobalVolume = value;
        AudioManager.Instance.GlobalVolume = value;
        SaveManager.SaveSetting(GameManager.Instance.UserSetting);
    }
    private void BGVolume_Slider_ValueChanged(float value)
    {
        GameManager.Instance.UserSetting.BGVolume = value;
        AudioManager.Instance.BGVolume = value;
        SaveManager.SaveSetting(GameManager.Instance.UserSetting);
    }

    private void EffectVolume_Slider_ValueChanged(float value)
    {
        GameManager.Instance.UserSetting.EffectVolume = value;
        AudioManager.Instance.EffectVolume = value;
        SaveManager.SaveSetting(GameManager.Instance.UserSetting);
    }

    private void LanguageType_Dropdown_ValueChanged(int value)
    {
        LanguageType language = (LanguageType)value;
        GameManager.Instance.UserSetting.LanguageType = language;
        LocalizationManager.Instance.CurrentLanguageType = language;
        SaveManager.SaveSetting(GameManager.Instance.UserSetting);
    }
}
