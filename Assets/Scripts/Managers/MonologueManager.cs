﻿using System.Collections;
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
    
    private int index;

    //! variables for coroutine (if needed)
    public bool isSentenceDrawn;

    //[HideInInspector]
    public bool showMonologue = false;
    public float showMonologueTimer;
    private float showMonologueTimerCounter;

    void Awake()
    {
        for(int i=0; i<sentenceList.Length; i++)
        {
            sentenceList[i].cooldownCounter = 0;
        }

        textBox.text = "";
        isSentenceDrawn = true;
    }

    void Start()
    {
        //! default index value
        index = -1;

        //! set current timer to set start time
        ResetMonologueTimer();

        //StartCoroutine(Type());
    }

    void Update()
    {
        CountMonologueTimer();

        CountTextCooldown();
    }

    //! NOT FOR POC : for coroutine to draw sentence letter by letter
    IEnumerator Type()
    {
        isSentenceDrawn = false;
        foreach (char letter in sentenceList[this.index].sentenceText.ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        //! setting cooldown for the current monologue text sentence that has finished to be displayed on screen
        sentenceList[this.index].cooldownCounter = sentenceList[this.index].cooldown;
        isSentenceDrawn = true;
    }

    void TypeSentence()
    {
        sentenceList[this.index].cooldownCounter = sentenceList[this.index].cooldown;
        textBox.text = sentenceList[this.index].sentenceText;
    }

    //! public function that can be called anywhere to display sentence in monologue text from given index
    public void DisplaySentence(int index)
    {
        if(sentenceList[index].cooldownCounter == 0)
        {
            //! for enabling the monologue gameobject
            showMonologue = true;

            if(showMonologueTimerCounter > 0)
            {
                ResetMonologueTimer();
            }
        }

        //! if these criteria are met, a new sentence will not be drawn
        if (index < 0 || index + 1 > sentenceList.Length || !isSentenceDrawn || sentenceList[index].cooldownCounter > 0)
        {
            if(index < 0)
            {
                Debug.Log("Sentence index is out of bounds (lower bound error).");
            }
            else if(index + 1 > sentenceList.Length)
            {
                Debug.Log("Sentence index is out of bounds (upper bound error).");
            }
            else if(!isSentenceDrawn)
            {
                Debug.LogWarning("An attempt to draw more than 1 sentence with coroutine was made.");
            }
            else if(sentenceList[index].cooldownCounter > 0)
            {
                Debug.Log("Sentence of index " + index + " cannot be displayed because it is on cooldown.");
            }
            return;
        }
        //! clear current text in textbox
        textBox.text = "";

        //! check for passed in index to display correct sentence in monologue text
        for (int i = 0; i < sentenceList.Length; i++)
        {
            if(index == i)
            {
                this.index = sentenceList[i].index;
            }
        }
        //StartCoroutine(Type());
        TypeSentence();
    }

    //! monologue timer & enabling textbox code
    void CountMonologueTimer()
    {
        textBox.gameObject.SetActive(showMonologue);

        if (showMonologue && showMonologueTimerCounter > 0)
        {
            showMonologueTimerCounter -= Time.deltaTime;
        }
        else
        {
            showMonologue = false;
            ResetMonologueTimer();
        }
    }

    //! individual monologue text cooldown
    void CountTextCooldown()
    {
        for (int i=0; i<sentenceList.Length; i++)
        {
            if(sentenceList[i].cooldownCounter > 0)
            {
                sentenceList[i].cooldownCounter -= Time.deltaTime;
            }
            if(sentenceList[i].cooldownCounter <= 0)
            {
                sentenceList[i].cooldownCounter = 0;
            }
        }
    }

    private void ResetMonologueTimer()
    {
        showMonologueTimerCounter = showMonologueTimer;
    }

    //! only for Editor script
    public void DebugFunction()
    {
        index = 2;
        if(sentenceList[index].cooldownCounter == 0)
        {
            TypeSentence();
        }
    }
}
