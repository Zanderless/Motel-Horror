using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Variables

    public static InventoryManager Instance { get; private set; }

    private List<Item> items = new List<Item>();

    #endregion

    public bool AddItem(Item item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
            HUDManager.Instance.UpdateInventory(items);
            return true;
        }

        return false;
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            HUDManager.Instance.UpdateInventory(items);
        }
    }

    public bool HasKey(int keyID)
    {
        foreach(Item i in items)
        {
            if (i.keyID.Equals(keyID))
                return true;
        }

        return false;
    }

    public List<Item> GetItems()
    {
        return items;
    }

    private void Awake()
    {
        Instance = this;
    }
}
