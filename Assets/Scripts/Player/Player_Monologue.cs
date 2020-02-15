using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Monologue : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public string[] sentenceList;
    [Range(0.01f, 0.1f)]
    public float typingSpeed;
    private int index;

    void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        foreach(char letter in sentenceList[index].ToCharArray())
        {
            textBox.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
