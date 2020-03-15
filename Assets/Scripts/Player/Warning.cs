using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    GameManager gm;
    public float xOffset;
    public float yOffset;
    private void Start()
    {

        gm = GameManager.Instance;

    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (gm.playerObject)
        {

            transform.position = new Vector3(gm.playerObject.transform.position.x + xOffset, gm.playerObject.transform.position.y + yOffset);

        }
       
    }
}
