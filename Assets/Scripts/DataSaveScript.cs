using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[System.Serializable]
public class GameData
{

    public int saveIndex;
    public string saveName;
    public string saveAge;
    public string saveOccupation;

}

public class DataSaveScript : MonoBehaviour
{

    public GameData curSaveData;

    public TMP_InputField inputName;
    public TMP_InputField inputAge;
    public TMP_InputField inputOccupation;

    private void Start()
    {

        curSaveData = new GameData();

    }

    public void Save()
    {

        Debug.Log("SAVING TO " + Application.persistentDataPath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");

        curSaveData.saveName = inputName.text;
        curSaveData.saveAge = inputAge.text;
        curSaveData.saveOccupation = inputOccupation.text;

        //set value into game data;
        bf.Serialize(file, curSaveData);
        file.Close();

    }

    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/gameInfo.dat"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameInfo.dat", FileMode.Open);

            curSaveData = (GameData)bf.Deserialize(file);
            file.Close();
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
