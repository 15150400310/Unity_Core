using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Frame;

[UIElement(true, "UI/UI_ResultWindow", 1)]
public class UI_ResultWindow : UIPanelBase
{
    private const string LocalSetPackName = "UI_ResultWindow";

    [SerializeField] private Text score_Text;
    [SerializeField] private Text back_Button_Text;
    [SerializeField] private Text re_Button_Text;
    [SerializeField] private Text ads_Button_Text;

    [SerializeField] private Button back_Button;
    [SerializeField] private Button re_Button;
    [SerializeField] private Button ads_Button;

    public override void Init()
    {
        score_Text = GetUI<Text>("Score_Text");
        back_Button_Text = GetUI<Text>("Back_Button/Text");
        re_Button_Text = GetUI<Text>("Re_Button/Text");
        ads_Button_Text = GetUI<Text>("Ads_Button/Text");

        back_Button = GetUI<Button>("Back_Button");
        re_Button = GetUI<Button>("Re_Button");
        ads_Button = GetUI<Button>("Ads_Button");

        back_Button.onClick.AddListener(Back_ButtonClick);
        re_Button.onClick.AddListener(Re_ButtonClick);
        ads_Button.onClick.AddListener(Ads_ButtonClick);
    }
    protected override void OnUpdateLanguage()
    {
        back_Button_Text.FrameLocalSet(LocalSetPackName, "Back_Button_Text");
        re_Button_Text.FrameLocalSet(LocalSetPackName, "Re_Button_Text");
    }
    public void Init(int score)
    {
        score_Text.text = LocalizationManager.Instance.GetContent<L_Text>(LocalSetPackName, "Score").content + score.ToString();
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

    private void Ads_ButtonClick()
    {
        AdsManager.Instance.ShowRewardedAd(()=> {
            EventManager.EventTrigger("Nirvana");
        });
        
    }
}
