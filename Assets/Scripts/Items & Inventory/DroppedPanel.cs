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
                GameObject dropInstance = Instantiate(drop_indicator, transform.root.GetChild(0).position, Quaternion.identity);

                dropInstance.GetComponent<Item_Drop>().itemName = eventData.pointerDrag.GetComponent<Item_Inventory>().itemName;

                Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }

}
