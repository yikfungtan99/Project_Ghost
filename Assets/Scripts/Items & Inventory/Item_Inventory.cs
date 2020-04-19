using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Player player;

    private RectTransform rectTransform;

    public string itemName;
    private ItemLibrary il;

    private Vector2 initPos;
    public bool onHold = false;

    private Inventory iv;

    private GameManager gm;

    private void Awake()
    {

        gm = GameManager.Instance;
        canvas = transform.parent.parent.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        iv = gm.inventory;
        
    }

    private void OnEnable()
    {
        if (!onHold)
        {
            if (rectTransform.anchoredPosition.x < -iv.inventorySafeArea.x || rectTransform.anchoredPosition.x > iv.inventorySafeArea.x || rectTransform.anchoredPosition.y < -iv.inventorySafeArea.y || rectTransform.anchoredPosition.y > iv.inventorySafeArea.y)
            {
                rectTransform.anchoredPosition = initPos;
            }
        }
    }

    private void Start()
    {
        il = gm.itemLibrary;
        GetComponent<Image>().sprite = il.GetSprite(itemName);
        player = gm.player;
    }

    private void Update()
    {

        if (player.inventoryOn)
        {

            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;


        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        canvasGroup.alpha = 0.6f;
        initPos = rectTransform.anchoredPosition;
        gm.mouseControl.changeCursor("grab");

        
    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.alpha = 1f;

        if (!onHold)
        {
            if (rectTransform.anchoredPosition.x < -iv.inventorySafeArea.x || rectTransform.anchoredPosition.x > iv.inventorySafeArea.x || rectTransform.anchoredPosition.y < -iv.inventorySafeArea.y || rectTransform.anchoredPosition.y > iv.inventorySafeArea.y)
            {
                rectTransform.anchoredPosition = initPos;
            }
        }
        else
        {

            rectTransform.anchoredPosition = new Vector2(0,0);

        }

        gm.mouseControl.changeCursor("item");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateMonologue(itemName);
        if (onHold && player.inventoryOn)
        {
            transform.SetParent(transform.parent.parent.GetChild(0));

            rectTransform.anchoredPosition = new Vector2(Random.Range(-iv.inventorySafeArea.x, iv.inventorySafeArea.x), Random.Range(-iv.inventorySafeArea.y, iv.inventorySafeArea.y));

            onHold = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        //mc.changeCursor("item");

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        //mc.exitCursor();
    }

    void UpdateMonologue(string itemName)
    {
        switch(itemName)
        {
            case "lighter":
                gm.monologueManager.DisplaySentence(38);
                break;
            case "red cloth":
                gm.monologueManager.DisplaySentence(39);
                break;
            case "talisman":
                gm.monologueManager.DisplaySentence(76);
                break;
            case "scissors":
                gm.monologueManager.DisplaySentence(77);
                break;
            case "spoon":
                gm.monologueManager.DisplaySentence(78);
                break;
            case "comb":
                gm.monologueManager.DisplaySentence(79);
                break;
            case "razor":
                gm.monologueManager.DisplaySentence(80);
                break;
            case "red thread":
                gm.monologueManager.DisplaySentence(81);
                break;
            case "empty photo frame":
                gm.monologueManager.DisplaySentence(82);
                break;
            case "lounge statue":
                gm.monologueManager.DisplaySentence(83);
                break;
            case "flour":
                gm.monologueManager.DisplaySentence(84);
                break;
            case "sugar":
                gm.monologueManager.DisplaySentence(85);
                break;
            case "baking soda":
                gm.monologueManager.DisplaySentence(86);
                break;
            case "pink dye":
                gm.monologueManager.DisplaySentence(87);
                break;
            case "kuih":
                gm.monologueManager.DisplaySentence(88);
                break;
            case "holy book":
                gm.monologueManager.DisplaySentence(92);
                break;
            case null:
                Debug.Log("Item is named improperly or is intentionally out of bounds.");
                break;
        }
    }
}
