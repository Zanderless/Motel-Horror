using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Variables

    public static HUDManager Instance { get; private set; }

    //Screens
    public enum Screen { HUD, Keypad, ItemView, Pause};
    private Screen currentScreen;

    [Header("Screens")]
    public GameObject HUD;
    public GameObject pauseScreen;
    public GameObject keypad;
    public GameObject itemView;

    [Header("Interaction")]
    public TextMeshProUGUI interactionTxt;
    public Sprite interactionReticle;
    public Vector2 interactionSize;

    [Header("Reticle")]
    public Image reticle;
    public Sprite defaultReticle;
    public Vector2 defaultSize;

    [Header("Inventory")]
    public Transform inventoryParent;
    public GameObject inventorySlotPrefab;
    public Sprite emptySprite;
    private List<GameObject> inventorySlots = new List<GameObject>();

    [Header("Text Popup")]
    public TextMeshProUGUI popupText;
    public float popupTime;

    [Header("Item Viewer")]
    public Image itemImage;

    [Header("Keypad")]
    public TextMeshProUGUI keypadTxt;

    #endregion

    #region Screens

    public void ChangeScreen(Screen newScreen)
    {
        switch (newScreen)
        {
            case Screen.HUD:
                HUD.SetActive(true);
                pauseScreen.SetActive(false);
                keypad.SetActive(false);
                itemView.SetActive(false);
                break;
            case Screen.Keypad:
                HUD.SetActive(true);
                pauseScreen.SetActive(false);
                keypad.SetActive(true);
                itemView.SetActive(false);
                break;
            case Screen.ItemView:
                HUD.SetActive(true);
                pauseScreen.SetActive(false);
                keypad.SetActive(false);
                itemView.SetActive(true);
                break;
            case Screen.Pause:
                HUD.SetActive(false);
                pauseScreen.SetActive(true);
                keypad.SetActive(false);
                itemView.SetActive(false);
                break;
        }

        currentScreen = newScreen;
    }

    public Screen GetScreen()
    {
        return currentScreen;
    }

    #endregion

    #region Interaction

    public void SetInteraction(bool interacting, bool hasText, string interactionText)
    {
        if (interacting)
            SetReticle(interactionReticle, interactionSize);
        else
            SetDefaultReticle();

        interactionTxt.gameObject.SetActive(hasText);
        interactionTxt.text = interactionText;
    }

    #endregion

    #region Reticle

    public void SetReticle(Sprite newReticle, Vector2 size)
    {
        reticle.sprite = newReticle;
        reticle.rectTransform.sizeDelta = size;
    }

    public void SetDefaultReticle()
    {
        reticle.sprite = defaultReticle;
        reticle.rectTransform.sizeDelta = defaultSize;
    }

    #endregion

    #region Inventory

    public void SetupInventory()
    {
        for(int i = 0; i < 18; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryParent);
            inventorySlots.Add(slot);
        }
    }

    public void UpdateInventory(List<Item> items)
    {
        for(int i = 0; i <inventorySlots.Count; i++)
        {
            Sprite sprite = emptySprite;

            if (i < items.Count)
                sprite = items[i].itemIcon;

            inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        }
    }

    #endregion

    #region Text Popup

    public void SetPopupText(string text)
    {
        CancelInvoke(nameof(HidePopup));
        popupText.gameObject.SetActive(true);
        popupText.text = text;
        Invoke(nameof(HidePopup), popupTime);
    }

    public void HidePopup()
    {
        popupText.gameObject.SetActive(false);
    }

    #endregion

    #region Item Viewer

    public void SetItemImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    #endregion

    #region Keypad

    public void UpdateKeypadText(string text)
    {
        keypadTxt.text = text;
    }

    #endregion

    #region Unity Methods

    private void Start()
    {
        Instance = this;
        ChangeScreen(Screen.HUD);
        SetupInventory();
    }

    #endregion

}
