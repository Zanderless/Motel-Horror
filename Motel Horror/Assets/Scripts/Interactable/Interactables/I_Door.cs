using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Door : InteractableBase
{
    [Header("Door")]
    public Transform door;

    public Vector3 openVector;
    private Vector3 closedVector;

    public float speed;
    private float transition;

    public enum OpenType { Transform, Rotation };
    public OpenType openType;

    private bool isOpen = false;

    [Header("Lock")]
    public bool isLocked;
    public int keyID;
    public string lockedMessage;

    public override void Interact()
    {
        if (disabled)
            return;

        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (isLocked)
        {
            if (!InventoryManager.Instance.HasKey(keyID))
            {
                HUDManager.Instance.SetPopupText(lockedMessage);
                return;
            }

            isLocked = false;
        }

        isOpen = !isOpen;
    }

    private void Update()
    {
        if ((isOpen && transition == 1) || (!isOpen && transition == 0))
            return;

        transition += (isOpen ? 1 : -1) * speed * Time.deltaTime;
        transition = Mathf.Clamp01(transition);

        if (door == null)
            door = transform;

        if (openType == OpenType.Transform)
            door.localPosition = Vector3.Lerp(closedVector, openVector, transition);
        else
            door.localRotation = Quaternion.Lerp(Quaternion.Euler(closedVector), Quaternion.Euler(openVector), transition);
    }

    private void Start()
    {
        if (openType == OpenType.Transform)
            closedVector = transform.localPosition;
        else
            closedVector = transform.localRotation.eulerAngles;
    }

}
