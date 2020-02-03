using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform rectTransform;

    public string itemName;
    private ItemLibrary il;

    private Vector2 initPos;

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

        if(rectTransform.anchoredPosition.x < -300 || rectTransform.anchoredPosition.x > 300 || rectTransform.anchoredPosition.y < -150 || rectTransform.anchoredPosition.y > 150)
        {
            rectTransform.anchoredPosition = initPos;
        }

    }
}
