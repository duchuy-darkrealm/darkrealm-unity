using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DShopSeller : DInteractableObject,IPointerDownHandler
{
    public DItem[] items;

    public override void Interact()
    {
        OpenShop();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OpenShop();
    }

    public void OpenShop()
    {
        if (DGameSystem.canvasShop.activeSelf == false)
        {
            DGameSystem.player.GetComponent<DMovement>().Stand();
            DGameSystem.shop.LoadItemList(items);
            DGameSystem.canvasShop.SetActive(true);
            DGameSystem.canvasControl.SetActive(false);
        }
    }
}