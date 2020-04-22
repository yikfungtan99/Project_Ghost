using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnGhost : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;

        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.ghostMain.GetComponent<Transform>().position = gm.carrotMain.inactiveLocation.position;

    }
}
