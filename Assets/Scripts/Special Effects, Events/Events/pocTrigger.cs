using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocTrigger : MonoBehaviour
{
    public GameObject roomTrigger;

    public void ActivateTrigger()
    {
        roomTrigger.GetComponent<Trigger>().ChanceToTeleportGhost(true, true);
    }
}
