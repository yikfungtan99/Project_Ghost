using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    public string itemName;
    private ItemLibrary il;

    private Vector2 initPos;
    public bool onHold = false;

    private void OnEnable()
    {

    }

    private void Awake()
    {
        canvas = transform.parent.parent.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        il = GameObject.Find("ItemLibrary").GetComponent<ItemLibrary>();
        GetComponent<Image>().sprite = il.GetSprite(itemName);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        initPos = rectTransform.anchoredPosition;
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
            if (rectTransform.anchoredPosition.x < -300 || rectTransform.anchoredPosition.x > 300 || rectTransform.anchoredPosition.y < -150 || rectTransform.anchoredPosition.y > 150)
            {
                rectTransform.anchoredPosition = initPos;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onHold && GameObject.Find("Player").GetComponent<Player>().inventoryOn)
        {
            transform.SetParent(transform.parent.parent.GetChild(0));

            rectTransform.anchoredPosition = new Vector2(Random.Range(-300, 300), Random.Range(-150, 150));

            onHold = false;
        }
    }
}
