using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Frame;

[Pool]
public class UI_SaveItem : MonoBehaviour
{
    [SerializeField] private Image BG;
    [SerializeField] private Text UserName_Text;
    [SerializeField] private Text Time_Text;
    [SerializeField] private Text Score_Text;
    [SerializeField] private Text Del_Button_Text;
    [SerializeField] private Button Del_Button;

    private static Color normalColor = new Color(0,0,0,0.6f);
    private static Color selectColor = new Color(0,0.6f,1,0.6f);

    private SaveItem saveItem;

    private void Start()
    {
        Del_Button.onClick.AddListener(Del_ButtonClick);
        this.OnMouseEnter(MouseEnter);
        this.OnMouseExit(MouseExit);
        this.OnClick(Click);
    }
    public void Init(SaveItem saveItem)
    {
        this.saveItem = saveItem;

        Time_Text.text = saveItem.lastSaveTime.ToString("g");
        //TODO:用户数据
        Del_Button_Text.FrameLocalSet("UI_SaveWindow", "SaveItem_DelButtonText");
    }

    public void Destroy()
    {
        saveItem = null;
        this.GameObjectPushPool();
    }

    public void Del_ButtonClick()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        SaveManager.DeleteSaveItem(saveItem);
        EventManager.EventTrigger("UpdateSaveItem");
        //TODO:更新排行榜
    }

    private void MouseEnter(PointerEventData pointerEventData,params object[] args)
    {
        BG.color = selectColor;
    }

    private void MouseExit(PointerEventData pointerEventData, params object[] args)
    {
        BG.color = normalColor;
    }

    private void Click(PointerEventData pointerEventData, params object[] args)
    {
        EventManager.EventTrigger<SaveItem>("EnterGame",saveItem);
    }
}
