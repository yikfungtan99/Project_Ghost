using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Player player;
    public Vector2 inventorySafeArea;
    public GameObject itemPrefab;

    private void Start() 
    { 
        player = transform.root.GetComponent<Player>();
    }

    public void RandomizePosition()
    {

        Debug.Log("randomize");

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
            }

        }
    }

    public void ObtainItem(string item)
    {

        float x = Random.Range(-inventorySafeArea.x, inventorySafeArea.x);
        float y = Random.Range(-inventorySafeArea.y, inventorySafeArea.y);

        GameObject obtained = itemPrefab;

        Instantiate(obtained, transform.GetChild(0));

        obtained.GetComponent<Item_Inventory>().itemName = item;

        obtained.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(inventorySafeArea.x, inventorySafeArea.y));
    }


}
