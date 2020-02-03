using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void OnEnable()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            float x = Random.Range(-300, 300);
            float y = Random.Range(-150, 150);

            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }


}
