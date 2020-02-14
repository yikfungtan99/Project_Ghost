﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_Inventory : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

    private void Awake()
    {
        canvas = transform.parent.parent.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        iv = transform.parent.parent.GetComponent<Inventory>();
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
        il = GameObject.Find("ItemLibrary").GetComponent<ItemLibrary>();
        GetComponent<Image>().sprite = il.GetSprite(itemName);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (onHold)
        {
            if (!player.inventoryOn)
            {

                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;

                rectTransform.anchoredPosition = new Vector2(0, 0);

            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }

        }
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
            if (rectTransform.anchoredPosition.x < -iv.inventorySafeArea.x || rectTransform.anchoredPosition.x > iv.inventorySafeArea.x || rectTransform.anchoredPosition.y < -iv.inventorySafeArea.y || rectTransform.anchoredPosition.y > iv.inventorySafeArea.y)
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

            rectTransform.anchoredPosition = new Vector2(Random.Range(-iv.inventorySafeArea.x, iv.inventorySafeArea.x), Random.Range(-iv.inventorySafeArea.y, iv.inventorySafeArea.y));

            onHold = false;
        }
    }
}
