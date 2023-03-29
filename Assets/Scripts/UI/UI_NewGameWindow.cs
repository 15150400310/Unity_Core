using Frame;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/NewGameWindow", 1)]
public class UI_NewGameWindow : UI_WindowBase
{
    [SerializeField] private InputField UserName_InputField;
    [SerializeField] private Text UserName_Text;
    [SerializeField] private Text UserName_Placeholder_Text;

    [SerializeField] private Button Back_Button;
    [SerializeField] private Button Play_Button;

    [SerializeField] private Text Back_Button_Text;
    [SerializeField] private Text Play_Button_Text;

    private const string LocalSetPackName = "UI_NewGameWindow";

    public override void Init()
    {
        Play_Button.onClick.AddListener(OnYesClick);
        Back_Button.onClick.AddListener(OnCloseClick);
    }

    protected override void OnUpdateLanguage()
    {
        UserName_Text.FrameLocalSet(LocalSetPackName, "UserName_Text");
        UserName_Placeholder_Text.FrameLocalSet(LocalSetPackName, "UserName_Placeholder_Text");
        Back_Button_Text.FrameLocalSet(LocalSetPackName, "Back_Button");
        Play_Button_Text.FrameLocalSet(LocalSetPackName, "Play_Button");
    }

    public override void Close()
    {
        UserName_InputField.text = "";
        base.Close();
    }

    public override void OnYesClick()
    {
        //如果没有输入用户名,直接点击进入游戏
        if (UserName_InputField.text.Length<1)
        {
            UIManager.Instance.AddTipsByLocalization("CheckName");
        }
        else
        {
            AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
            EventManager.EventTrigger<string>("CreateNewSaveAndEnterGame", UserName_InputField.text);
            base.OnYesClick();
        }
    }

    public override void OnCloseClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        base.OnCloseClick();
    }
}
