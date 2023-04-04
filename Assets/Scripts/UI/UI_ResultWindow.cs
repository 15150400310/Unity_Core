using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Frame;

[UIElement(true, "UI/ResultWindow", 1)]
public class UI_ResultWindow : UI_WindowBase
{
    [SerializeField] private Text Score_Text;
    [SerializeField] private Button Back_Button;
    [SerializeField] private Button Re_Button;
    [SerializeField] private Text Back_Button_Text;
    [SerializeField] private Text Re_Button_Text;

    private const string LocalSetPackName = "UI_ResultWindow";

    public override void Init()
    {
        Back_Button.onClick.AddListener(Back_ButtonClick);
        Re_Button.onClick.AddListener(Re_ButtonClick);
    }
    protected override void OnUpdateLanguage()
    {
        Back_Button_Text.FrameLocalSet(LocalSetPackName, "Back_Button_Text");
        Re_Button_Text.FrameLocalSet(LocalSetPackName, "Re_Button_Text");
    }
    public void Init(int score)
    {
        Score_Text.text = LocalizationManager.Instance.GetContent<L_Text>(LocalSetPackName, "Score").content + score.ToString();
        GameManager.Instance.PauseGame();
    }

    private void Back_ButtonClick()
    {
        //去主菜单
        GameManager.Instance.ContinueGame();
        SceneManager.LoadScene("MainMenu");
    }

    private void Re_ButtonClick()
    {
        //再次加载当前场景进行游戏
        GameManager.Instance.ContinueGame();
        GameManager.Instance.RepeatGame();
    }
}
