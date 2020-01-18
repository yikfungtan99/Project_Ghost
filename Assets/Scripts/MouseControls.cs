using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{

    private GameObject crosshair;
    public Vector2 target;
    //private Camera camera;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        crosshair = transform.GetChild(0).gameObject;
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        crosshair.transform.position = target;
    }
}
