using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Vector2 inventorySafeArea;

    private void OnEnable()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            float x = Random.Range(-inventorySafeArea.x, inventorySafeArea.x);
            float y = Random.Range(-inventorySafeArea.y, inventorySafeArea.y);

            if (!transform.GetChild(i).GetComponent<Item_Inventory>().onHold)
            {
                transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }

        }
    }


}
