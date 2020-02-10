using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Lighter : MonoBehaviour
{
    public bool lighterOn = true;

    public void toggleLighter()
    {
        if (lighterOn)
        {
            lighterOn = false;
            gameObject.SetActive(lighterOn);
        }
        else
        {
            lighterOn = true;
            gameObject.SetActive(lighterOn);
        }
    }

}
