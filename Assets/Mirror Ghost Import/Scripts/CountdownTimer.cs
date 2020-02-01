using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTimeMirrorGhost = 0f;
    private float currentTimeFourGhost = 0f;

    [SerializeField] Player player;
    [SerializeField] MirrorGhost mirror;
    [SerializeField] float startingTimeMirrorGhost;
    [SerializeField] float startingTimeFourGhost;

    [SerializeField] Text countdownText;

    void Start()
    {
        currentTimeMirrorGhost = startingTimeMirrorGhost;
        currentTimeFourGhost = startingTimeFourGhost;
        countdownText.enabled = false;
    }

    void Update()
    {
        if(player.trappedByMirrorGhost == true)
        {
            if(countdownText.enabled == false)
            {
                countdownText.enabled = true;
            }
            currentTimeMirrorGhost -= 1f * Time.deltaTime;
            countdownText.text = currentTimeMirrorGhost.ToString("0");

            if(mirror.isCoveredByCloth == true)
            {
                currentTimeMirrorGhost = startingTimeMirrorGhost;
                countdownText.enabled = false;
                player.trappedByMirrorGhost = false;
            }

            if(currentTimeMirrorGhost <= 0)
            {
                player.trappedByMirrorGhost = false;
                currentTimeMirrorGhost = startingTimeMirrorGhost;
                countdownText.enabled = false;
                player.isPlayerDead = true;
            }
        }
    }
}
