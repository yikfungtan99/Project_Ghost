using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public class JsonGameData
{

    public int saveIndex;
    public string saveName;
    public string saveAge;
    public string saveOccupation;

}

public class JsonSaveScript : MonoBehaviour
{

    public JsonGameData curSaveData;

    public TMP_InputField inputName;
    public TMP_InputField inputAge;
    public TMP_InputField inputOccupation;

    private void Start()
    {

        curSaveData = new JsonGameData();

    }

    public void Save()
    {
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
        #endif

        Debug.Log("SAVING TO " + Application.persistentDataPath);

        curSaveData.saveName = inputName.text;
        curSaveData.saveAge = inputAge.text;
        curSaveData.saveOccupation = inputOccupation.text;

        string json = JsonUtility.ToJson(curSaveData, true);

        File.WriteAllText(Application.persistentDataPath + "/jsonInfo", json);

    }

    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/jsonInfo"))
        {

            string json = File.ReadAllText(Application.persistentDataPath + "/jsonInfo");

            curSaveData = JsonUtility.FromJson<JsonGameData>(json);
            CheckLoad();

        }
        else
        {

            Debug.Log("No File found, Creating new save file at " + Application.persistentDataPath + "/gameInfo.dat");

        }

    }

    public void CheckLoad()
    {
        string output;

        output = curSaveData.saveName + " is " + curSaveData.saveAge + " years old and works as a " + curSaveData.saveOccupation;

        Debug.Log(output);

    }

}
