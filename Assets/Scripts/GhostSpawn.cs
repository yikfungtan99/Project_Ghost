using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    private Transform Target;
    public Transform[] Trigger;
    public bool Triggered=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        if (Vector2.Distance(Target.position, Trigger[0].position) < 1f)
        {
            Triggered = true;

        }

      
    }

    /*void SpawnGhost()
    {
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>().position.x = Target;
    }*/


}

