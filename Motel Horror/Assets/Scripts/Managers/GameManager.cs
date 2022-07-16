using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variable

    public static GameManager Instance { get; private set; }

    public enum GameState { Playing, Paused, Transition};
    private GameState currentState;

    #endregion

    #region Gamestate

    public void SetGameState(GameState newState)
    {
        currentState = newState;
        UpdateGame();
    }

    public void UpdateGame()
    {
        switch (currentState)
        {
            case GameState.Playing:
                InputManager.Instance.SetInputStatus(InputManager.InputStatus.Enabled);
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                InputManager.Instance.SetInputStatus(InputManager.InputStatus.Disabled);
                Time.timeScale = 0;
                break;
        }
    }

    public GameState GetGameState()
    {
        return currentState;
    }

    #endregion

    public void ToggleGamePause()
    {
        if (currentState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
            InputManager.Instance.SetInputStatus(InputManager.InputStatus.Enabled);
            HUDManager.Instance.ChangeScreen(HUDManager.Screen.HUD);
        }
        else if (currentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
            InputManager.Instance.SetInputStatus(InputManager.InputStatus.Disabled);
            HUDManager.Instance.ChangeScreen(HUDManager.Screen.Pause);
        }
    }

    private void Start()
    {
        Instance = this;
    }

}
