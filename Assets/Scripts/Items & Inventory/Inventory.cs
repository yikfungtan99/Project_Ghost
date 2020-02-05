using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Player player;
    public Vector2 inventorySafeArea;

    private void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    public void RandomizePosition()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {

            float x = Random.Range(-inventorySafeArea.x, inventorySafeArea.x);
            float y = Random.Range(-inventorySafeArea.y, inventorySafeArea.y);

            if (transform.GetChild(0).GetChild(i).GetComponent<Item_Inventory>())
            {
                if (!transform.GetChild(0).GetChild(i).GetComponent<Item_Inventory>().onHold)
                {
                    transform.GetChild(0).GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
                }

                Debug.Log(transform.GetChild(0).GetChild(i).GetComponent<RectTransform>().anchoredPosition);

            }

        }
    }


}
