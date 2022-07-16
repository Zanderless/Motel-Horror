using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_ItemViewer : InteractableBase
{

    #region Variables

    public Sprite itemImage;

    #endregion

    public override void Interact()
    {
        ItemViewer.Instance.SetCurrentItem(this);
        ItemViewer.Instance.OpenViewer();
    }
}
