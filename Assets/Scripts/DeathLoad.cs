using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathLoad : MonoBehaviour
{

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

}
