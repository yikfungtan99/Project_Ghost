using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour
{
    public Sprite glowSprite;
    public Sprite pressedSprite;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        SetSprite();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite()
    {
        if ((button != null) && (glowSprite != null) && (pressedSprite != null))
        {
            SpriteState spriteState = new SpriteState();
            spriteState = button.spriteState;

            button.spriteState = spriteState;
        }
    }

    public void QuitButton()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

}