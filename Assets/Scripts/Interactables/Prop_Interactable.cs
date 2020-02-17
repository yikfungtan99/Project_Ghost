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

    public void Interact()
    {
        GameObject.Find("Player").GetComponent<MonologueManager>().showMonologue = true;

    }
}
