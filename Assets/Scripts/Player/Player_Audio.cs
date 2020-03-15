using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    public void PlayFootstep1()
    {
        GameManager.Instance.audioManager.ForcePlayAudio("foot step 1");
    }

    public void PlayFootstep2()
    {
        GameManager.Instance.audioManager.ForcePlayAudio("foot step 2");
    }
}
