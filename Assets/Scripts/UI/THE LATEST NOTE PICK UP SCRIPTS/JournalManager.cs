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

    [HideInInspector]
    public bool[] notePickup = new bool[8];

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
                int count = 1;
                for (int i = curIndex + 1; i < noteList.Count; i++)
                {
                    for (int j = 0; j < noteList[i].noteBundle.Count; j++)
                    {

                        if (curIndex + count == i )
                        {
                            if (notePickup[i] == true)
                            {
                                next = noteList[i].noteBundle[j].gameObject;
                                if (cur != null && next != null)
                                {
                                    cur.SetActive(false);
                                    next.SetActive(true);
                                    Debug.Log("NextPage called");
                                }
                                break;
                            }
                            else
                            {
                                
                            }
                        }
                        count++;
                    }
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
            GameObject prev = FindPage(curIndex - 1);
            if (cur != null && prev != null)
            {
                cur.SetActive(false);
                prev.SetActive(true);
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
