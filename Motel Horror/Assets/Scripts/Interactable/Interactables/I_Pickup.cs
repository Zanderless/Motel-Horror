using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Pickup : InteractableBase
{
    public Item item;

    public override void Interact()
    {
        InventoryManager inv = InventoryManager.Instance;

        if (!inv.AddItem(item))
            return;

        Destroy(this.gameObject);
    }

    private void Start()
    {
        interactionText = $"Pickup {item.itemName}"; 
    }

}
