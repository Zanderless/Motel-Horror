using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class I_Keypad : InteractableBase
{

    #region Variables

    [Header("Keypad")]
    public int keyCode;
    public string SavedCode { get; set; }
    public int SavedIndex { get; set; }
    public UnityEvent onCorrectCode = new UnityEvent();

    #endregion

    public override void Interact()
    {
        Keypad.Instance.SetKeypad(this);
        Keypad.Instance.OpenKeypad();
    }

    public void CallEvents()
    {
        onCorrectCode.Invoke();
    }

    private void Start()
    {
        SavedCode = "----";
        SavedIndex = 0;
    }

}
