using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Motel/Items")]
public class Item : ScriptableObject
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
    public string itemText;

    [Header("Key")]
    public int keyID;
}
