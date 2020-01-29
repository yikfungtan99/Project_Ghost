using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{

    public GameObject[] items;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject FindItem(string itemName)
    {

        //this will be different when comparing

    }

}
