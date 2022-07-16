using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{

    #region Variables

    public static Keypad Instance { get; private set; }

    private I_Keypad currentKeypad;

    private string code = "----";
    private int index = 0;
    private bool keypadDisabled;

    #endregion

    public void OpenKeypad()
    {
        HUDManager.Instance.ChangeScreen(HUDManager.Screen.Keypad);
        InputManager.Instance.SetInputStatus(InputManager.InputStatus.Disabled);
        code = currentKeypad.SavedCode;
        index = currentKeypad.SavedIndex;
        HUDManager.Instance.UpdateKeypadText(code);
    }

    public void CloseKeypad()
    {
        currentKeypad.SavedCode = code;
        currentKeypad.SavedIndex = index;
        HUDManager.Instance.UpdateKeypadText(code);
        HUDManager.Instance.ChangeScreen(HUDManager.Screen.HUD);
        InputManager.Instance.SetInputStatus(InputManager.InputStatus.Enabled);
        currentKeypad = null;
    }

    public void SetKeypad(I_Keypad pad)
    {
        currentKeypad = pad;
    }

    public void AddNumber(int number)
    {
        if (keypadDisabled)
            return;

        string newCode = "";

        for (int i = 0; i < 4; i++)
        {
            if (i == index)
                newCode += number;
            else
                newCode += code[i];
        }

        code = newCode;
        HUDManager.Instance.UpdateKeypadText(code);

        if (index < 3)
            index++;
        else
            CheckCode();
    }

    public void CheckCode()
    {
        keypadDisabled = true;

        if (int.Parse(code).Equals(currentKeypad.keyCode))
            StartCoroutine(CorrectCode());
        else
            StartCoroutine(WrongCode());
    }

    private IEnumerator CorrectCode()
    {
        HUDManager.Instance.UpdateKeypadText("Correct!");
        yield return new WaitForSeconds(0.5f);
        currentKeypad.disabled = true;
        currentKeypad.CallEvents();
        CloseKeypad();
    }

    private IEnumerator WrongCode()
    {
        int i = 0;

        while(i < 3)
        {
            HUDManager.Instance.UpdateKeypadText("Error!!");
            yield return new WaitForSeconds(0.25f);
            HUDManager.Instance.UpdateKeypadText("");
            yield return new WaitForSeconds(0.25f);
            i++;
        }

        code = "----";
        HUDManager.Instance.UpdateKeypadText(code);
        index = 0;
        keypadDisabled = false;
    }

    private void Start()
    {
        Instance = this;
    }

}
