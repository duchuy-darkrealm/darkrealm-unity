using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DShopSlot : MonoBehaviour, IPointerDownHandler
{
    public int slotIndex;
    public DShop shop;
    public Image itemImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        shop.LoadItemDetail(slotIndex);
    }
}
