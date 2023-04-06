using Frame;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/UI_NewGameWindow", 1)]
public class UI_NewGameWindow : UIPanelBase
{
    private const string LocalSetPackName = "UI_NewGameWindow";

    [SerializeField] private Text userName_Text;
    [SerializeField] private Text userName_Placeholder_Text;
    [SerializeField] private Text play_Button_Text;
    [SerializeField] private Text back_Button_Text;

    [SerializeField] private Button play_Button;
    [SerializeField] private Button back_Button;
    
    [SerializeField] private InputField userName_InputField;
    
    public override void Init()
    {
        userName_Text = GetUI<Text>("UserName/Text");
        userName_Placeholder_Text = GetUI<Text>("UserName/UserName_Input/Placeholder");
        play_Button_Text = GetUI<Text>("Play_Button/Text");
        back_Button_Text = GetUI<Text>("Back_Button/Text");
        
        play_Button = GetUI<Button>("Play_Button");
        back_Button = GetUI<Button>("Back_Button");

        userName_InputField = GetUI<InputField>("UserName/UserName_Input");
        
        play_Button.onClick.AddListener(OnYesClick);
        back_Button.onClick.AddListener(OnCloseClick);
    }

    protected override void OnUpdateLanguage()
    {
        userName_Text.FrameLocalSet(LocalSetPackName, "UserName_Text");
        userName_Placeholder_Text.FrameLocalSet(LocalSetPackName, "UserName_Placeholder_Text");
        back_Button_Text.FrameLocalSet(LocalSetPackName, "Back_Button_Text");
        play_Button_Text.FrameLocalSet(LocalSetPackName, "Play_Button_Text");
    }

    public override void OnClose()
    {
        userName_InputField.text = "";
        base.OnClose();
    }

    public override void OnYesClick()
    {
        //如果没有输入用户名,直接点击进入游戏
        if (userName_InputField.text.Length<1)
        {
            UIManager.Instance.AddTipsByLocalization("CheckName");
        }
        else
        {
            AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
            EventManager.EventTrigger<string>("CreateNewSaveAndEnterGame", userName_InputField.text);
            base.OnYesClick();
        }
    }

    public override void OnCloseClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        base.OnCloseClick();
    }
}
