using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpots : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            CarrotMain mainGhost = collision.GetComponent<CarrotMain>();

            if (mainGhost)
            {

                Debug.Log("Switch");
                
                if(mainGhost.heading == 0)
                {

                    mainGhost.heading = 1;

                }
                else
                {

                    mainGhost.heading = 0;

                }

            }

        }
    }
}
