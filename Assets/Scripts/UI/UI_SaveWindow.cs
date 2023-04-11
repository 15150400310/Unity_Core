using Frame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/UI_SaveWindow", 1)]
public class UI_SaveWindow : UIPanelBase
{
    [SerializeField] private Button close_Button;
    [SerializeField] private Transform itemParent;

    private List<UI_SaveItem> UI_SaveItemList = new List<UI_SaveItem>();

    private bool wantUpdate = true;

    public override void Init()
    {
        close_Button = GetUI<Button>("Close_Button");
        itemParent = GetTransform("ViewPort/Content");

        close_Button.onClick.AddListener(Close);
    }

    public override void OnShow()
    {
        base.OnShow();
        if (wantUpdate)
        {
            UpdateAllSaveItem();
        }
    }

    public override void OnClose()
    {
        AudioManager.Instance.PlayOnShot("Audio/Button", UIManager.Instance);
        base.OnClose();
    }

    protected override void RegisterEventListener()
    {
        //base.RegisterEventListener();
        EventManager.AddEventListener("UpdateSaveItem", UpdateSaveItemFlag);
    }

    private void UpdateSaveItemFlag()
    {
        wantUpdate = true;
        //如果当前面板是激活状态,则立刻刷新
        if (gameObject.activeInHierarchy)
        {
            UpdateAllSaveItem();
        }
    }

    /// <summary>
    /// 更新全部存档项
    /// </summary>
    private void UpdateAllSaveItem()
    {
        //清空已有的
        for (int i = 0; i < UI_SaveItemList.Count; i++)
        {
            UI_SaveItemList[i].Destroy();
        }
        UI_SaveItemList.Clear();

        //放置新的
        List<SaveItem> saveItems = SaveManager.GetAllSaveItemByUpdateTime();
        for (int i = 0; i < saveItems.Count; i++)
        {
            UI_SaveItem item = ResManager.Load<UI_SaveItem>("UI/SaveItem", itemParent);
            item.Init(saveItems[i]);
            UI_SaveItemList.Add(item);
        }

        //更新布尔值
        wantUpdate = false;
    }
}
