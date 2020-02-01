using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGhost : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EntangleFadeInOut entangleImage;

    private bool debugMsg = false;
    private bool startEntanglement = false;
    private bool entanglementInitialised = false;
    public bool mirrorGhostKillsPlayer = false;
    public bool isCoveredByCloth = false;

    public Transform image;
    private RectTransform imageRectTransform;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(player.trappedByMirrorGhost == true && isCoveredByCloth == false)
        {
            if (startEntanglement == false)
            {
                startEntanglement = true;
                InitialiseEntangle();
            }
        }
        
        if(entanglementInitialised == true && isCoveredByCloth == true)
        {
            Debug.Log(transform.name + " sequence deinitiated.");
            entanglementInitialised = false;
            entangleImage.startFadeOut = true;
            entangleImage.startFadeInToggle = false;
            player.trappedByMirrorGhost = false;
            player.pauseOnTriggerStay = false;
            startEntanglement = false;
            entanglementInitialised = false;
        }
        else if(isCoveredByCloth == true && entanglementInitialised == false && player.trappedByMirrorGhost == false)
        {
            if(debugMsg == true)
            {
                debugMsg = false;
                Debug.Log("Player has clothed the mirror without triggering the ghost.");
            }
        }
    }

    void InitialiseEntangle()
    {
        entanglementInitialised = true;

        entangleImage.ToggleImageEnable();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCoveredByCloth == false)
            {
                Debug.Log("Cloth has been placed onto the mirrorfront.");
                debugMsg = true;
                isCoveredByCloth = true;
            }
            else if(isCoveredByCloth == true)
            {
                Debug.Log("Cloth has been removed from the mirrorfront.");
                debugMsg = true;
                isCoveredByCloth = false;
            }
        }

    }
}
