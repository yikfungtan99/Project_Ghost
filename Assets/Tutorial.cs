using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private GameManager gm;
    public Vector2 Offset;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        gm = GameManager.Instance;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector2(gm.playerObject.transform.position.x + Offset.x, transform.position.y + Offset.y);

    }
}
