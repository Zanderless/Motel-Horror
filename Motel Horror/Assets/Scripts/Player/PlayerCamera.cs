using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector2 sensitivity;
    public float lookMin = -60f, lookMax = 60;
    private float verticalLookRotation;

    //Fix mouse pos snapping
    private void CameraLook()
    {
        //Rotate on Y axis (Horizontal)
        transform.parent.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity.x);

        //Rotate on X axis (Vertical)
        verticalLookRotation += Input.GetAxis("Mouse Y") * sensitivity.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, lookMin, lookMax);
        transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void Update()
    {
        if (InputManager.Instance.GetInputStatus() == InputManager.InputStatus.Disabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        else if(InputManager.Instance.GetInputStatus() == InputManager.InputStatus.Enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        CameraLook();
    }
}
