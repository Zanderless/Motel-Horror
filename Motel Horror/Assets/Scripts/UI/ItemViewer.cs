using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewer : MonoBehaviour
{
    #region Variables

    public static ItemViewer Instance { get; private set; }

    private I_ItemViewer currentItem;

    #endregion

    public void OpenViewer()
    {
        HUDManager.Instance.SetItemImage(currentItem.itemImage);
        HUDManager.Instance.ChangeScreen(HUDManager.Screen.ItemView);
        InputManager.Instance.SetInputStatus(InputManager.InputStatus.Disabled);
    }

    public void CloseViewer()
    {
        HUDManager.Instance.ChangeScreen(HUDManager.Screen.HUD);
        InputManager.Instance.SetInputStatus(InputManager.InputStatus.Enabled);
        currentItem = null;
    }

    public void SetCurrentItem(I_ItemViewer item)
    {
        currentItem = item;
    }

    private void Start()
    {
        Instance = this;
    }

}
