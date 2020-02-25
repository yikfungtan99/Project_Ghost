using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public Texture2D cursor;
    public Vector2 target;


    private void Start()
    {
        //Cursor.SetCursor(cursor, target, CursorMode.Auto);

    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
        
}
