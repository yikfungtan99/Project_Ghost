using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    GameManager gm;
    Image fadeInOutImage;
    Image backgroundDisplay;

    public float fadeInTime;
    public float fadeOutTime;

    int sequenceCount;
    bool isFading;

    public CutsceneSequence[] sequence;

    private void Awake()
    {
        gm = GameManager.Instance;
		
		Time.timeScale = 1f;
        sequenceCount = 0;
        isFading = false;

        fadeInOutImage = transform.GetChild(1).GetComponent<Image>();
        backgroundDisplay = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        if (sequence[sequenceCount].stillImageBackground != null)
        {
            backgroundDisplay.sprite = sequence[sequenceCount].stillImageBackground;
        }
        else
        {
            Debug.LogWarning("Missing backgroud image. Rendering null cutscene image.");
            backgroundDisplay.sprite = null;
        }

        sequenceCount++;

        StartCoroutine(FadeOut(false));
    }

    public void NextSequence()
    {
        //! stop the spam
        if(isFading)
        {
            Debug.Log("Cutscene is still performing an animation!");
            return;
        }

        //! check for max sequence value
        if(sequenceCount == sequence.GetLength(0))
        {
            StartCoroutine(FadeOut(true));
        }
        else
        {
            StartCoroutine(FadeCutsceneAnimation());
        }
    }

    public void SceneTransition()
    {
        Debug.Log("Button click");
		UpdateAudio(1);
		
        StartCoroutine(FadeOut(true));
    }

    IEnumerator FadeCutsceneAnimation()
    {
        StartCoroutine(FadeIn());
        
        yield return new WaitForSeconds(fadeInTime);
        if (sequence[sequenceCount].stillImageBackground != null)
        {
            backgroundDisplay.sprite = sequence[sequenceCount].stillImageBackground;
        }
        else
        {
            Debug.LogWarning("Missing backgroud image. Rendering null cutscene image.");
            backgroundDisplay.sprite = null;
        }

        sequenceCount++;

        StartCoroutine(FadeOut(false));
    }

    IEnumerator FadeIn()
    {
        if(!isFading)
        {
            isFading = true;
            Debug.Log("Fading In...");

            fadeInOutImage.canvasRenderer.SetAlpha(0.0f);
            fadeInOutImage.CrossFadeAlpha(1f, fadeInTime, false);
            
            yield return new WaitForSeconds(fadeInTime);
            fadeInOutImage.canvasRenderer.SetAlpha(1.0f);

            isFading = false;
            Debug.Log("Finished Fading In");
        }
        else
        {
            Debug.Log("Cutscene is still performing an animation!");
        }
    }

    IEnumerator FadeOut(bool fadeOutScene)
    {
        if(!isFading)
        {
            isFading = true;
            Debug.Log("Fading Out...");

            fadeInOutImage.canvasRenderer.SetAlpha(1.0f);
            fadeInOutImage.CrossFadeAlpha(0.0f, fadeOutTime, false);

            yield return new WaitForSeconds(fadeOutTime);
			AudioManager.instance.ForceStopAudio("disquiet ambience");
            fadeInOutImage.canvasRenderer.SetAlpha(0.0f);

            if(fadeOutScene)
            {
                transform.GetComponent<Canvas>().enabled = false;
                isFading = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            isFading = false;
            Debug.Log("Finished Fading Out");
        }
        else
        {
            Debug.Log("Cutscene is still performing an animation!");
        }
    }
	
	void UpdateAudio(int index)
	{
		switch(index)
		{
			case 1: //! Click any button
				AudioManager.instance.ForcePlayAudio("button click");
				break;
		}
	}
}
