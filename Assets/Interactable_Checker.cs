﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Checker : MonoBehaviour
{
    public Vector2 interactableOffset;
    public Vector2 interactableSize;
    public LayerMask interactableLayer;

    private void Update()
    {
        Collider2D[] interactable = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + interactableOffset.x, transform.position.y + interactableOffset.y), new Vector2(interactableSize.x, interactableSize.y), 0, interactableLayer);

        if (interactable != null)
        {
            for (int i = 0; i < interactable.Length; i++)
            {

                if (interactable[i].gameObject.GetComponent<Interactable>())
                {

                    interactable[i].gameObject.GetComponent<Interactable>().isSeen = true;

                }

            }

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x + interactableOffset.x, transform.position.y + interactableOffset.y), new Vector2(interactableSize.x, interactableSize.y));
    }

}
