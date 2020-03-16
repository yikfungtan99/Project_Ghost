using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathLoad : MonoBehaviour
{

    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);

        gm.playerObject.transform.position = gm.playerCheckpointPosition;
        gm.playerObject.SetActive(true);


    }

}
