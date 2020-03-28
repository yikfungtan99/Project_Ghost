using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonologueManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public Sentences[] sentenceList;
    [Space(10)]
    [Range(0.01f, 0.1f)]
    public float typingSpeed;

    private int oldIndex;
    public int index;

    //! variables for coroutine (if needed)
    public bool isSentenceDrawn;

    //[HideInInspector]
    public bool displayMonologue = false;

    void Awake()
    {
        textBox.text = "";
        isSentenceDrawn = true;
    }

    void Start()
    {
        //! default index value
        oldIndex = -2;
        index = -1;

        //! set current timer to set start time
        ResetMonologueTimer();
    }

    void Update()
    {
        CountMonologueDisplayTimer();
    }

    //! Coroutine to draw sentence letter by letter, unused as of now
    IEnumerator Type()
    {
        isSentenceDrawn = false;
        foreach (char letter in sentenceList[this.index].sentenceText.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        //! setting cooldown for the current monologue text sentence that has finished to be displayed on screen
        //sentenceList[this.index].cooldownCounter = sentenceList[this.index].cooldown;
        isSentenceDrawn = true;
    }

    void TypeDisplaySentence()
    {
        textBox.text = sentenceList[index].sentenceText;
    }

    public void DisplayPickUpSentence(string itemName, bool isItemInContainer)
    {
        //! for enabling the monologue gameobject
        if (!CheckIfStillOnDisplay(4))
        {
            sentenceList[4].displayMonologue = true;
        }
        else
        {
            sentenceList[oldIndex].displayMonologue = false;
            sentenceList[4].displayMonologue = true;
        }

        displayMonologue = sentenceList[4].displayMonologue;

        if (sentenceList[4].displayMonologueTimerCounter > 0)
        {
            ResetMonologueTimer();
        }

        //! differentiating whether item picked up comes from any form of container or not
        if (!isItemInContainer)
        {
            textBox.text = "Picked up one " + itemName + " and put it in the bag.";
            index = 4;
        }
        else
        {
            textBox.text = "Found one " + itemName + " inside and put it in the bag.";
            index = 4;
        }
    }

    //! public function that can be called anywhere to display sentence in monologue text from given index
    public void DisplaySentence(int index)
    {
        //! for enabling the monologue gameobject
        if (!CheckIfStillOnDisplay(index))
        {
            sentenceList[index].displayMonologue = true;
        }
        else
        {
            sentenceList[oldIndex].displayMonologue = false;
            sentenceList[index].displayMonologue = true;
        }

        displayMonologue = sentenceList[index].displayMonologue;

        if (sentenceList[index].displayMonologueTimerCounter > 0)
        {
            ResetMonologueTimer();
        }

        CheckIndexValidity();

        //! check for passed in index to display correct sentence in monologue text
        for (int i = 0; i < sentenceList.Length; i++)
        {
            if(index == i)
            {
                this.index = sentenceList[i].index;
            }
        }

        TypeDisplaySentence();
    }

    public bool CheckIfStillOnDisplay(int index)
    {
        bool isDisplaying = false;

        for (int i = 0; i < sentenceList.Length; i++)
        {
            if (index == i)
            {
                continue;
            }

            if (sentenceList[i].displayMonologue)
            {
                isDisplaying = true;
            }
        }

        return isDisplaying;
    }

    //! monologue timer & enabling textbox code
    void CountMonologueDisplayTimer()
    {
        if(textBox == null)
        {
            return;
        }

        textBox.gameObject.SetActive(displayMonologue);

        if(oldIndex != index)
        {
            oldIndex = index;
        }

        if(index == -1)
        {
            return;
        }

        if (sentenceList[index].displayMonologue && sentenceList[index].displayMonologueTimerCounter > 0)
        {
            sentenceList[index].displayMonologueTimerCounter -= Time.deltaTime;
        }

        if (sentenceList[index].displayMonologue && sentenceList[index].displayMonologueTimerCounter <= 0)
        {
            sentenceList[index].displayMonologue = false;
            displayMonologue = false;
            ResetMonologueTimer();
        }
    }

    private void ResetMonologueTimer()
    {
        //! the following is a new code used to count each sentence's invidivual displayMonologue timer state which is customisable
        for (int i = 0; i < sentenceList.Length; i++)
        {
            sentenceList[i].displayMonologueTimerCounter = sentenceList[i].displayMonologueTimer;
        }
    }

    private void CheckIndexValidity()
    {
        //! if these criteria are met, a new sentence will not be drawn
        if (index < -1 || index + 1 > sentenceList.Length || !isSentenceDrawn)
        {
            if (index < -1)
            {
                Debug.Log("Sentence index is out of bounds (lower bound error).");
            }
            else if (index + 1 > sentenceList.Length)
            {
                Debug.Log("Sentence index is out of bounds (upper bound error).");
            }
            else if (!isSentenceDrawn)
            {
                Debug.LogWarning("An attempt to draw more than 1 sentence with function was made.");
            }
            return;
        }

        //! clear current text in textbox
        textBox.text = "";
    }

    //! only for MonologueManagerEditor script
    public void DebugFunction()
    {
        
    }
}
