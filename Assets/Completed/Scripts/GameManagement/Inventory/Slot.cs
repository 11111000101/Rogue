using Completed;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        Transform previousParent = InventoryDragHandler.startParent;
        Transform newParent = transform;
        if (!item && Completed.InventoryManager.instance.isItemMovementPossible(previousParent, newParent))
        {
            InventoryDragHandler.itemBeingDragged.transform.SetParent(transform);
            GameManager.getInstance().getInventoryManager().HasChanged(previousParent, newParent); // update model
        }
    }
    #endregion
}