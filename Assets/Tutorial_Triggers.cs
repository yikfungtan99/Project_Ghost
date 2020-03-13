using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Triggers : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>())
        {

            GameManager.Instance.TutorialNavi.GetComponent<Animator>().SetTrigger("Next");
            Destroy(this.gameObject);

        }

    }

}
