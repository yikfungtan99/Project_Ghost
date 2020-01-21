using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{

    private GameObject crosshair;
    public Vector2 target;

    // Start is called before the first frame update
    void Awake()
    {
        crosshair = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
