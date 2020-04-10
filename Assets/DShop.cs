using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DShop : MonoBehaviour
{
    public int SLOT_NUMBER = 24;
    public DItem[] items;
    public DShopSlot[] slots;

    public Image detailImage;
    public TextMeshProUGUI detailPrice;
    public TextMeshProUGUI detailText;

    int currentSelect = -1;

    public void CreateShop()
    {
        slots = new DShopSlot[SLOT_NUMBER];
        items = new DItem[SLOT_NUMBER];

        for (int i = 0; i < items.Length; i++)
        {
            slots[i] = Instantiate(Resources.Load<GameObject>("ShopSlot") as GameObject, transform).GetComponent<DShopSlot>();
            slots[i].slotIndex = i;
            slots[i].shop = this;
        }
    }

    public void LoadItemList(DItem[] itemsList)
    {
        for (int i = 0; i < SLOT_NUMBER; i++)
        {
            if (i < itemsList.Length)
            {
                slots[i].itemImage.sprite = itemsList[i].sprite;
                slots[i].itemImage.enabled = true;
                items[i] = itemsList[i];
            }
            else
            {
                slots[i].itemImage.enabled = false;
                items[i] = null;
            }
        }
    }

    public void LoadItemDetail(int index)
    {
        if (items[index] == null) return;
        detailImage.sprite = items[index].sprite;
        detailPrice.text = items[index].priceGold.ToString();
        detailText.text = items[index].detail;
        currentSelect = index;
    }

    public void Buy()
    {
        if (currentSelect == -1) return;
        if (items[currentSelect] == null) return;
        if (DGameSystem.money < items[currentSelect].priceGold)
        {
            Debug.Log("Not enough money!");
        }
        else if (DGameSystem.inventory.CheckItemSlot(items[currentSelect]) == false)
        {
            Debug.Log("Have no more inventory slot!");
        }
        else
        {
            DGameSystem.SpendMoney(items[currentSelect].priceGold);
            DGameSystem.inventory.Add(items[currentSelect]);
        }
    }
}
