using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Interactable : MonoBehaviour
{
    private Interactable it;

    private void Awake()
    {
        it = GetComponent<Interactable>();
    }

    public void Update()
    {
        if (it.inRange)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        it.inRange = false;
    }

    public void Interact()
    {

        Debug.Log("Woooo Painting!");

    }
}
