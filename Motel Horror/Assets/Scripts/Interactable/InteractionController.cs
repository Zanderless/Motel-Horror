using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    #region Variables

    [Header("Raycast")]
    public LayerMask interactableLayer;
    public float interactDistance;
    private InteractableBase interactable;

    #endregion

    public void Interact()
    {
        if (interactable == null)
            return;

        interactable.Interact();
    }

    private bool Raycast()
    {
        HUDManager hud = HUDManager.Instance;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,  out RaycastHit hit, interactDistance, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            interactable = hit.transform.GetComponent<InteractableBase>();
            if (interactable != null && !interactable.disabled)
            {
                hud.SetInteraction(true, interactable.hasInteractionText, interactable.interactionText);
                return true;
            }
        }

        hud.SetInteraction(false, false, "");
        interactable = null;
        return false;
    }

    private void FixedUpdate()
    {
        Raycast();
    }

}
