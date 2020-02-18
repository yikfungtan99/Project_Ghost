using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocTrigger : MonoBehaviour
{
    public void ActivateTrigger()
    {

        GameObject.Find("Enemy").GetComponent<MainGhost>().enabled = true;

    }
}
