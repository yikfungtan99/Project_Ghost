using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private GameManager gm;
    public Vector2 Offset;
    private Animator anim;
    public Transform closet;

    // Start is called before the first frame update
    void Start()
    {

        gm = GameManager.Instance;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.tutorialSleep)
        {
            transform.position = new Vector2(gm.playerObject.transform.position.x + Offset.x, gm.playerObject.transform.position.y + Offset.y);
        }
        else
        {
            transform.position = new Vector2(closet.transform.position.x + Offset.x - 1.1f, closet.transform.position.y + Offset.y);

        }
        

    }
}
