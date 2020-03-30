using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class NoteSets
{
    public List<GameObject> noteBundle = new List<GameObject>();
}

public class JournalManager : MonoBehaviour
{
    public Canvas canvas;  //////

    public static JournalManager Instance;

  //  [HideInInspector]             /////////////////// <--------------------THIS IS PANTANG
    public bool[] notePickup = new bool[10];  // was 8, changed to 10 to test will work or not; was a failure
    public List<GameObject> pauseMenu = new List<GameObject>();
    public GameObject noteWarning;
    public List<NoteSets> noteList = new List<NoteSets>();
    //public List<GameObject> noteListIndividual = new List<GameObject>();
    int maxNoteCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Debug.Log("notePickup.Length: " + notePickup.Length);
        maxNoteCount = 0;
        for (int i = 0; i < noteList.Count; i++)
        {
            for (int j = 0; j < noteList[i].noteBundle.Count; j++)
            {
                noteList[i].noteBundle[j].GetComponent<JournalPageScript>().pageNum = maxNoteCount;
                maxNoteCount++;
                Debug.Log(maxNoteCount);
            }
        }
        /*
        for(int i = 0; i < noteList.Count; i ++)
        {
            for (int j = 0; j < noteList.Count; j++)
            {
                noteListIndividual.Add(noteList[i].noteBundle[j]);
            }
        }
        */
    }
           

    public bool CheckNoteStatus(int noteNum)
    {
        for (int i = 0; i < notePickup.Length; i++)
        {
            return notePickup[i];
        }

        return false;
    }

    public void PickedUp(int noteNum)
    {
        for(int i = 0; i < notePickup.Length; i++)
        {
            if(i == noteNum)
            {
                notePickup[i] = true;
            }
        }
    }

    void TogglePause(bool t)
    {
        for(int i = 0; i < pauseMenu.Count; i ++)
        {
            pauseMenu[i].SetActive(t);
        }
    }

    GameObject GetCanvasChild(int index)
    {
        return canvas.transform.GetChild(index).gameObject;
    }

    public void OpenJournal()
    {
        //! close pause
        TogglePause(false);

        //! open note 1 || open whichever note that is already there
        for (int i = 0; i < notePickup.Length; i++)
        {
            if(notePickup[i] == true)
            {
                noteList[i].noteBundle[0].SetActive(true);
                break;
            }
            else if(i == notePickup.Length - 1)
            {
                noteWarning.SetActive(true);
            }
        }
    }

    public void NextPage(int curIndex)
    {
        if (curIndex != maxNoteCount)
        {
            GameObject cur = FindPage(curIndex);
            GameObject next;
            if(FindPage(curIndex+1) != null) 
            {
                next = FindPage(curIndex + 1);
                if (cur != null && next != null)
                {
                    cur.SetActive(false);
                    next.SetActive(true);
                    Debug.Log("NextPage called");
                }
            }
            else
            {
                for (int i = curIndex + 1; i < maxNoteCount; i++) //lesser than maxnotecount to prevent going out of control, 
                {
                    for (int j = 0; j < noteList[i].noteBundle.Count; j++)//???
                    {  
                      //  Debug.Log("curIdex: " + curIndex);
                      //  Debug.Log(i);
                        if (notePickup[i] == true)//got note?
                        {
                            next = noteList[i].noteBundle[j].gameObject;//activate next
                            if (cur != null && next != null)//both current and next got note picked up
                            {
                                cur.SetActive(false);//switch off current picture...
                                next.SetActive(true);//...and go to next picture
                                for (int k = maxNoteCount; k >= 0; k--)
                                {
                                    for (int l = noteList[k].noteBundle.Count - 1; l >= 0; l--)
                                    {
                                        if (notePickup[k] == true)
                                        {
                                            if(i == k && j == l)//no note picked up at next (no note with a greater note index thing is found)
                                            {
                                                noteList[k].noteBundle[l].transform.GetChild(0).gameObject.SetActive(false);//to disable the next button
                                                break;
                                            }
                                        }
                                    }
                                    if (notePickup[k] == true)
                                    {
                                        break;
                                    }
                                }
                                Debug.Log("NextPage called");
                            }
                            break;
                        }
                        else
                        {

                        }
                    }
                    /**/
                    if(notePickup[i] == true)
                    {
                        break;
                    }
                }
            }
            
            /*
                noteListIndividual[curIndex].SetActive(false);
                noteListIndividual[curIndex + 1].SetActive(true);
            */
        }
    }

    public void PrevPage(int curIndex)
    {
        if (curIndex != 0)
        {
            GameObject cur = FindPage(curIndex);
            GameObject prev;
            if (FindPage(curIndex - 1) != null)
            {
                prev = FindPage(curIndex - 1);
                if (cur != null && prev != null)
                {
                    cur.SetActive(false);
                    prev.SetActive(true);
                    Debug.Log("NextPage called");
                }
            }
            else
            {
                for (int i = curIndex - 1; i >= 0; i--)
                {
                    for (int j = 0; j < noteList[i].noteBundle.Count; j++)
                    {
                        Debug.Log("curIdex: " + curIndex);
                        Debug.Log(i);
                        if (notePickup[i] == true)
                        {
                            prev = noteList[i].noteBundle[j].gameObject;
                            if (cur != null && prev != null)
                            {
                                cur.SetActive(false);
                                prev.SetActive(true);
                                Debug.Log("NextPage called");
                            }
                            break;
                        }
                        else
                        {

                        }
                    }
                    if (notePickup[i] == true)
                    {
                        break;
                    }
                }
            }
            /*
                noteListIndividual[curIndex].SetActive(false);
                noteListIndividual[curIndex - 1].SetActive(true);
            */
        }
    }

    GameObject FindPage(int index)
    {
        int count = 0;
        
        for (int i = 0; i < noteList.Count; i++)
        {
            for (int j = 0; j < noteList[i].noteBundle.Count; j++)
            {
                
                if(count == index)
                {
                    if(notePickup[i] == true)
                    {
                        return noteList[i].noteBundle[j];
                    }
                    else
                    {
                        return null;
                    }
                }
                count++;
            }
        }

        Debug.LogError("No note found in index");
        return null;
    }
}
