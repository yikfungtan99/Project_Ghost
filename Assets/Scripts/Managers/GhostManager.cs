﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public bool Triggered;
    public Transform[] Trigger;
    public Transform Player;
    public Transform currentMirror;
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameManager.Instance.playerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        
       /* if (Vector2.Distance(Player.position, Trigger[0].position) < 1f)
        {
            Triggered = true;




            // transform.Rotate(0, 180f, 0);
            // transform.eulerAngles = new Vector3(0, -180, 0);

        }*/
    }
}
