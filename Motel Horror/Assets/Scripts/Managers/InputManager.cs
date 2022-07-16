using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InputManager : MonoBehaviour
{

    #region Variables

    public static InputManager Instance { get; private set; }

    public enum InputStatus { Disabled, Enabled};
    private InputStatus currentStatus;

    //Refrences
    private Player m_player;
    
    //Constants
    public const string moveX = "Move X";
    public const string moveY = "Move Y";
    public const string run = "Run";
    public const string crouch = "Crouch";
    public const string interact = "Interact";
    public const string pause = "Pause";

    #endregion

    #region Inputs

    public Vector2 GetMoveInput()
    {
        return m_player.GetAxis2D(moveX, moveY);
    }

    public bool GetRunInput()
    {
        return m_player.GetButton(run);
    }

    public bool GetCrouchInputDown()
    {
        return m_player.GetButtonDown(crouch);
    }

    public bool GetPauseInputDown()
    {
        return m_player.GetButtonDown(pause);
    }

    public bool GetInteractInputDown()
    {
        return m_player.GetButtonDown(interact);
    }

    #endregion

    #region Input Status

    public void SetInputStatus(InputStatus status)
    {
        currentStatus = status;
    }

    public InputStatus GetInputStatus()
    {
        return currentStatus;
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        m_player = ReInput.players.GetPlayer(0);

        currentStatus = InputStatus.Enabled;
    }

    #endregion

}
