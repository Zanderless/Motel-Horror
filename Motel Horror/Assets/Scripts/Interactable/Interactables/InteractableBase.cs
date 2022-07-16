using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{

    [Header("Interactable")]
    public bool hasInteractionText;
    public string interactionText;
    public bool disabled;

    public abstract void Interact();

    public override string ToString()
    {
        return "Press 'F' to " + interactionText;
    }

}
