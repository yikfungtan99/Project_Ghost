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
    public bool[] notePickup;//keep track of the notes picked up on the ground
                                                                 //= new bool[10];  // was 8, changed to 10 to test will work or not; was a failure
    public bool[] noteReadup;//keep track of the notes you read in the game

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
        noteReadup = new bool[maxNoteCount];//game will automatically set the maxnotecount to exactly how many notes there are in the game
        notePickup = new bool[noteList.Count]; //noteList placed inside the array as there are 6 notes to be picked up, as noteList contains the notes that are pickupable, .Count to get int
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
                notePickup[i] = true;//indicte a note on the ground is picked up
                for(int j = 0; j < noteList[i].noteBundle.Count; j++)//make the note you picked up readable using th bool array
                {
                    noteReadup[noteList[i].noteBundle[j].GetComponent<JournalPageScript>().pageNum] = true;
                    //success in doing this cos got equal number of notereadup bools and notepickup bools
                }
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
            GameObject cur = FindPage(curIndex);//set current page to current page number
            GameObject next;
            if(FindPage(curIndex+1) != null) 
            {
                next = FindPage(curIndex + 1);//set next to current page number + 1, making it next page number just like how 2's next page is 3
                if (cur != null && next != null)
                {
                    cur.SetActive(false);
                    next.SetActive(true);
                    Debug.Log("NextPage called");
                }
            }
            else
            {
                for(int i = curIndex + 1; i < noteReadup.Length; i++)
                {
                    if(noteReadup[i])
                    {
                        next = FindPage(i);
                        if (cur != null && next != null)
                        {
                            cur.SetActive(false);
                            next.SetActive(true);
                            Debug.Log("NextPage called");
                            break;
                        }
                    }
                }
                
                /*for (int i = curIndex + 1; i < maxNoteCount; i++) //lesser than maxnotecount to prevent going out of control, 
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
                                *//*for (int k = maxNoteCount; k >= 0; k--)
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
                                }*//*
                                Debug.Log("NextPage called");
                            }
                            break;
                        }
                        else
                        {

                        }
                    }
                    *//**//*
                    if(notePickup[i] == true)
                    {
                        break;
                    }
                }*/
            }
            
            /*
                noteListIndividual[curIndex].SetActive(false);
                noteListIndividual[curIndex + 1].SetActive(true);
            */
        }
    }

    public void PrevPage(int curIndex)
    {
        Debug.Log("First call");
        Debug.Log(curIndex);
        if (curIndex >= 0)
        {
            Debug.Log("First point 1 call");
            GameObject cur = FindPage(curIndex);
            Debug.Log(cur);
            GameObject prev;
            Debug.Log("prev");
            if (FindPage(curIndex - 1) != null)
            {
                Debug.Log("Second call");
                prev = FindPage(curIndex - 1);
                if (cur != null && prev != null)
                {
                    Debug.Log("Third call");
                    cur.SetActive(false);
                    prev.SetActive(true);
                    Debug.Log("PrevPage called");
                }
            }
            else
            {
                for (int i = curIndex - 1; i >= 0; i--)
                {
                    Debug.Log("After else");
                    if (noteReadup[i])
                    {
                        Debug.Log("Fourth call");
                        prev = FindPage(i);
                        if (cur != null && prev != null)
                        {
                            Debug.Log("Fifth call");
                            cur.SetActive(false);
                            prev.SetActive(true);
                            Debug.Log("PrevPage called");
                            break;
                        }
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
        /*if(noteReadup[index])//if noteReadup[index] == true
        {
            
        }*/
        for (int i = 0; i < noteList.Count; i++)
        {
            for (int j = 0; j < noteList[i].noteBundle.Count; j++)
            {
                if (count == index)
                {
                    if (notePickup[i] == true && noteReadup[index])
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
        /**/

        Debug.LogError("No note found in index");
        return null;
    }
}
