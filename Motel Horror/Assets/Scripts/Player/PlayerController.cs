using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    #region Variables

    public static PlayerController Instance { get; private set; }

    [Header("Movement")]
    public float forwardSpeed;
    public float strafeSpeed;
    private float moveModifier;

    [Header("Crouching")]
    public float crouchHeight;
    private float standHeight;
    public float crouchTransitionSpeed;
    private float crouchTransition;
    public float crouchModifier;
    private bool crouching;

    [Header("Running")]
    public float runModifier;
    public float runTransitionSpeed;
    private float runTransition;
    private bool running;

    [Header("Stamina")]
    public float maxStamina;
    private float _stamina;
    public float Stamina
    {
        get { return _stamina; }
        set
        {
            _stamina = value;
            _stamina = Mathf.Clamp(_stamina, 0, maxStamina);
        }
    }

    [Header("Gravity")]
    public float groundStickGravity = 20f;

    //Vectors
    private Vector2 inputDir;
    private Vector3 velocity;

    //Refrences
    private CharacterController m_controller;
    private InteractionController m_interact;
    private InputManager m_input;

    #endregion

    private void GetPlayerInput()
    {
        //Pause game
        if (m_input.GetPauseInputDown())
            GameManager.Instance.ToggleGamePause();

        if (m_input.GetInputStatus() == InputManager.InputStatus.Disabled)
            return;

        UpdateMoveModifier();

        //Get movement
        inputDir = m_input.GetMoveInput();
        inputDir.x *= strafeSpeed * moveModifier;
        inputDir.y *= forwardSpeed * moveModifier;

        velocity = new Vector3(inputDir.x, 0, inputDir.y);
        velocity = transform.TransformDirection(velocity);

        //Interaction
        if (m_input.GetInteractInputDown())
            m_interact.Interact();

        //Run
        running = m_input.GetRunInput() && !crouching;

        //Crouching
        Crouch();
    }

    private void UpdateMoveModifier()
    {
        moveModifier = crouching ? crouchModifier : Run();
    }

    private float Run()
    {
        runTransition += (running ? 1 : -1) * runTransitionSpeed * Time.deltaTime;
        runTransition = Mathf.Clamp01(runTransition);

        return Mathf.Lerp(1, runModifier, runTransition);
    }

    private void Crouch()
    {
        if (m_input.GetCrouchInputDown())
            crouching = !crouching;

        float prevHeight = m_controller.height;

        crouchTransition += (crouching ? 1 : -1) * crouchTransitionSpeed * Time.deltaTime;
        crouchTransition = Mathf.Clamp01(crouchTransition);

        m_controller.height = Mathf.Lerp(standHeight, crouchHeight, crouchTransition);

        if (!crouching && crouchTransition > 0)
        {

            float diff = m_controller.height - prevHeight;

            m_controller.enabled = false;
            transform.position += Vector3.up * diff;
            m_controller.enabled = true;
        }

        //Set camera height
        Transform cam = transform.GetChild(0);
        cam.localPosition = transform.up * (m_controller.height / 2);
    }

    private void MovePlayer()
    {
        if (m_input.GetInputStatus() == InputManager.InputStatus.Disabled)
            return;

        velocity.y = -groundStickGravity;
        m_controller.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        GetPlayerInput();
        MovePlayer();
    }

    private void Start()
    {
        Instance = this;

        //Set the player's stamina
        Stamina = maxStamina;

        //Get all refrences
        m_controller = GetComponent<CharacterController>();
        m_interact = GetComponent<InteractionController>();
        m_input = InputManager.Instance;

        //Set the standing height
        standHeight = m_controller.height;
    }

}
