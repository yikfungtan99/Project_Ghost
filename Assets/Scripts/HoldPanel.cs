using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldPanel : MonoBehaviour, IDropHandler
{

    private Inventory iv;

    private void Start()
    {
        iv = transform.parent.gameObject.GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Item_Inventory>())
            {
                if (transform.childCount != 0)
                {
                    Transform temp = transform.GetChild(0);

                    temp.SetParent(transform.parent.GetChild(0));

                    temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-iv.inventorySafeArea.x, iv.inventorySafeArea.x), Random.Range(-iv.inventorySafeArea.y, iv.inventorySafeArea.y));

                    temp.GetComponent<Item_Inventory>().onHold = false;

                }

                eventData.pointerDrag.GetComponent<Item_Inventory>().onHold = true;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                eventData.pointerDrag.transform.SetParent(transform);
            }
        }
    }
}
