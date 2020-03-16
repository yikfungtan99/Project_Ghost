using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocTrigger : MonoBehaviour
{
    public GameObject roomTrigger;

    public void ActivateTrigger()
    {
        GameManager.Instance.audioManager.PlayAudio("ghost trigger");
        roomTrigger.GetComponent<Trigger>().ChanceToTeleportGhost(true, true);
    }
}
