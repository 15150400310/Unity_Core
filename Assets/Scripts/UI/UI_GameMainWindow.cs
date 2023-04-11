using Frame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[UIElement(true, "UI/UI_GameMainWindow", 1)]
public class UI_GameMainWindow : UIPanelBase
{
    private const string LocalSetPackName = "UI_GameMainWindow";

    [SerializeField] private Text score_Text;
    [SerializeField] private Text bulletNum_Text;
    [SerializeField] private Text reload_Button_Text;

    [SerializeField] private Image hpBar_Fill_Image;

    [SerializeField] private Button reload_Button;
    [SerializeField] private Button setting_Button;
    
    [SerializeField] private FixedJoystick move_Joystick;
    [SerializeField] private FixedJoystick fire_Joystick;

    public override void Init()
    {
        score_Text = GetUI<Text>("Score_Image/Score_Text");
        bulletNum_Text = GetUI<Text>("BulletNum_Text");
        reload_Button_Text = GetUI<Text>("Reload_Button/Reload_Button_Text");
        hpBar_Fill_Image = GetUI<Image>("HPBar_Image/HPBar_Fill_Image");
        reload_Button = GetUI<Button>("Reload_Button");
        setting_Button = GetUI<Button>("Setting_Button");

        move_Joystick = GetGameObject("Move_Joystick").GetComponent<FixedJoystick>();
        fire_Joystick = GetGameObject("Fire_Joystick").GetComponent<FixedJoystick>();

        move_Joystick.OnUpdate(JoystickMove);
        fire_Joystick.OnUpdate(JoystickFire);
#if UNITY_ANDROID
        reload_Button.Show();
        setting_Button.Show();
        reload_Button.onClick.AddListener(Reload_ButtonClick);
        setting_Button.onClick.AddListener(Setting_ButtonClick);
#elif UNITY_STANDALONE
        reload_Button.Hide();
        setting_Button.Hide();
        move_Joystick.gameObject.SetActive(false);
        fire_Joystick.gameObject.SetActive(false);
#endif
    }

    private void JoystickMove()
    {
        EventManager.EventTrigger("JoystickMove", move_Joystick.Direction);
    }

    private void JoystickFire()
    {
        EventManager.EventTrigger("JoystickFire", fire_Joystick.Direction);
    }

    protected override void RegisterEventListener()
    {
        //base.RegisterEventListener();
        EventManager.AddEventListener<int>("UpdateHp", UpdateHp);
        EventManager.AddEventListener<int>("UpdateScore", UpdateScore);
        EventManager.AddEventListener<int,int>("UpdateBullet", UpdateBullet);
    }

    protected override void CancelEventListener()
    {
        //base.CancelEventListener();
        EventManager.RemoveEventListener<int>("UpdateHp", UpdateHp);
        EventManager.RemoveEventListener<int>("UpdateScore", UpdateScore);
        EventManager.RemoveEventListener<int, int>("UpdateBullet", UpdateBullet);
    }
    protected override void OnUpdateLanguage()
    {
        reload_Button_Text.FrameLocalSet(LocalSetPackName, "Reload_Button_Text");
    }
    private void UpdateHp(int hp)
    {
        hpBar_Fill_Image.fillAmount = hp / 100f;
    }

    private void UpdateScore(int num)
    {
        score_Text.text = num.ToString();
    }

    private void UpdateBullet(int curr,int max)
    {
        bulletNum_Text.text = curr + "/" + max;
    }

    /// <summary>
    /// 移动端换弹按钮
    /// </summary>
    private void Reload_ButtonClick()
    {
        Player_Controller.Instance.Reload();
    }

    private void Setting_ButtonClick()
    {
        LVManager.Instance.ShowSettingWindow();
    }

}
