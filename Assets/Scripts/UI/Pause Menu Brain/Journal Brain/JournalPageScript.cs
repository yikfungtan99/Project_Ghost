using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalPageScript : MonoBehaviour
{
    public int pageNum;
    public Button nextButton;
    public Button prevButton;

    private void Start()
    {
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
        //pageNum++;
    }

    public void NextPage()
    {
        JournalManager.Instance.NextPage(pageNum);
    }

    public void PrevPage()
    {
        JournalManager.Instance.PrevPage(pageNum);
    }
}
