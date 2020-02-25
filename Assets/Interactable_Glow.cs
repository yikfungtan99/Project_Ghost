using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Glow : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log("Set");
        sprite.sprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = transform.parent.localScale;
        
    }

}
