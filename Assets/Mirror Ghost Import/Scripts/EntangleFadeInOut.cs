using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntangleFadeInOut : MonoBehaviour
{
    [SerializeField] Image image;
    private Color trueBaseColor;

    public bool startFadeOut = false;
    public bool startFadeInToggle = false;
    public bool pauseUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        Color trueBaseColor = image.material.color;

        Color newColor = image.material.color;
        
        //color.a is modifying the colour's alpha value
        newColor.a = 0f;
        image.material.color = newColor;

        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseUpdate == false)
        {
            if (startFadeInToggle == false && image.enabled == true)
            {
                startFadeInToggle = true;
                StartCoroutine("StartFadeIn");
            }
        }

        if(startFadeOut == true)
        {
            startFadeOut = false;
            StartCoroutine("StartFadeOut");
        }
    }

    IEnumerator StartFadeIn()
    {
        Color newColor = image.material.color;
        newColor.a = 0.05f;
        image.material.color = newColor;

        for (float i = 0.05f; i<= 5f; i += 0.05f)
        {
            if(startFadeOut == false)
            {
                newColor = image.material.color;
                newColor.a = i;
                image.material.color = newColor;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator StartFadeOut()
    {
        Color newColor = image.material.color;

        for (float i = 2f; i >= 0.05f; i -= 0.05f)
        {
            if(startFadeInToggle == true)
            {
                newColor = image.material.color;
                newColor.a = i;
                image.material.color = newColor;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                break;
            }
        }
        if(startFadeInToggle == true)
        {
            newColor.a = 0.05f;
            image.material.color = newColor;
            image.enabled = false;
        }
    }

    public void ToggleImageEnable()
    {
        image.enabled = !image.enabled;
    }
}
