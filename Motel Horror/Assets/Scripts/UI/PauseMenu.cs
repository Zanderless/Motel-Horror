using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Resume()
    {
        GameManager.Instance.ToggleGamePause();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
