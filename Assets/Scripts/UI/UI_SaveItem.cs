using Frame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Pool]
public class UI_SaveItem : MonoBehaviour
{
    [SerializeField] private Text userName_Text;
    [SerializeField] private Text time_Text;
    [SerializeField] private Text score_Text;
    [SerializeField] private Text del_Button_Text;

    [SerializeField] private Image bg;
    
    [SerializeField] private Button del_Button;

    private static Color normalColor = new Color(0,0,0,0.6f);
    private static Color selectColor = new Color(0,0.6f,1,0.6f);

    private SaveItem saveItem;
    private UserData userData;

    private void Start()
    {
        

        del_Button.onClick.AddListener(Del_ButtonClick);
        this.OnMouseEnter(MouseEnter);
        this.OnMouseExit(MouseExit);
        this.OnClick(Click);
    }

    private void OnEnable()
    {
        userName_Text = this.GetUI<Text>("UserName_Text");
        time_Text = this.GetUI<Text>("Time_Text");
        score_Text = this.GetUI<Text>("Score/Score_Text");
        del_Button_Text = this.GetUI<Text>("Del_Button/Del_Button_Text");

        bg = this.GetUI<Image>("");

        del_Button = this.GetUI<Button>("Del_Button");

        del_Button_Text.FrameLocalSet("UI_SaveWindow", "SaveItem_DelButton_Text");
    }
    public void Init(SaveItem saveItem)
    {
        

        this.saveItem = saveItem;
        time_Text.text = saveItem.lastSaveTime.ToString("g");
        userData = SaveManager.LoadObject<UserData>(saveItem);
        userName_Text.text = userData.userName;
        score_Text.text = userData.score.ToString();
    }

    public void Destroy()
    {
        saveItem = null;
        userData = null;
        this.GameObjectPushPool();
    }

    public void Del_ButtonClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        SaveManager.DeleteSaveItem(saveItem);
        EventManager.EventTrigger("UpdateSaveItem");
        EventManager.EventTrigger("UpdateRankItem");
    }

    private void MouseEnter(PointerEventData pointerEventData,params object[] args)
    {
        bg.color = selectColor;
    }

    private void MouseExit(PointerEventData pointerEventData, params object[] args)
    {
        bg.color = normalColor;
    }

    private void Click(PointerEventData pointerEventData, params object[] args)
    {
        EventManager.EventTrigger<SaveItem,UserData>("EnterGame",saveItem,userData);
    }
}
