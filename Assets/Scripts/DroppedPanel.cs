using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppedPanel : MonoBehaviour, IDropHandler
{
    public GameObject drop_indicator;
    private GameObject self_instance;

    private ItemLibrary itemLibrary;

    private void Start()
    {
        itemLibrary = GameObject.Find("ItemLibrary").GetComponent<ItemLibrary>();

        if (!itemLibrary)
        {
            Debug.LogError("Item Library not found!");
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        
        if(eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<Item_Inventory>())
            {
                drop_indicator.GetComponent<Item_Drop>().itemType = itemLibrary.FindItem(eventData.pointerDrag.name);

                Instantiate(drop_indicator, transform.root.GetChild(0).position, Quaternion.identity);
                
                Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }

}
